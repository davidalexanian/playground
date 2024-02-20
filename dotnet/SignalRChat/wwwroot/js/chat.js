"use strict";

// button clicks
document.getElementById("sendToOthersButton").addEventListener("click", function (event) {
    let user = document.getElementById("userInput").value;
    let message = document.getElementById("messageInput").value;

    connectionChatHub
        .invoke("SendToOtherUsers", user, message)  // or send (doesnt waits for the server metod to complete)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
});

document.getElementById("sendToUserButton").addEventListener("click", function (event) {
    connectionChatHub.invoke("SendMessageToUser", {
        fromUser: document.getElementById("userInput").value,
        toUser: document.getElementById("toUserInput").value,
        message: document.getElementById("messageInput").value,
        dict: {
            "apple": "apple", "orange": 15, "banana": { a: 1, b: 2 }
        }
    }).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("sendBigPayload").addEventListener("click", function (event) {
    connectionChatHub
        .invoke("sendBigPayloadToCaller", document.getElementById("userInput").value)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
});

// connect to chat hub
let connectionChatHub = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Debug)
    .withUrl("/chatHub", { transport: signalR.HttpTransportType.WebSockets })
    .withHubProtocol(new signalR.protocols.msgpack.MessagePackHubProtocol())
    .build();

connectionChatHub.on("ReceiveMessage", function (user, message) {
    let li = document.createElement("li");
    li.textContent = `${user} says ${message}`;
    document.getElementById("messagesList").appendChild(li);
});

connectionChatHub.on("receiveMessageSentToUser", function (response) {
    let li = document.createElement("li");
    li.textContent = `messsage receiver: ${JSON.stringify(response)}`;
    document.getElementById("messagesList").appendChild(li);
});

connectionChatHub.on("receiveBigMessage", function (response) {
    console.log(response);

    //assuming 1B per char
    document.getElementById("bigPayloadSpan").textContent =
        response.a1.length +
        response.a2.length +
        response.a3.length +
        response.a4.length +
        response.a5.length + ' bytes';
});

connectionChatHub.onclose(async () => {
    await startChatHub();  // manual reconnect, best practice to call start after on
});
async function startChatHub() {
    try {
        await connectionChatHub.start();
        console.log("SignalR ChatHub Connected");
    } catch (err) {
        console.log(err);
    }
};
startChatHub();

// connect clockHub
let connectionClockHub = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Debug)
    .withUrl("/clockHub")
    .withAutomaticReconnect()
    .build();

connectionClockHub.on("ShowTime", function (clockPayload) {
    document.getElementById("timeSpan").textContent = JSON.stringify(clockPayload);
});

async function startClockHub() {
    try {
        await connectionClockHub.start();
        console.log("SignalR ClockHub Connected");
    } catch (err) {
        console.log(err);
    }
};
startClockHub();