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
        message: document.getElementById("messageInput").value
    }).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("sendBigPayload").addEventListener("click", function (event) {
    connectionChatHub
        .invoke("SendBigPayloadToCaller", document.getElementById("userInput").value)
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
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} says ${message}`;
});

connectionChatHub.on("ReceiveBigMessage", function (response) {
    // assuming 1B per char
    document.getElementById("bigPayloadSpan").textContent =
        response.A1.length +
        response.A2.length +
        response.A3.length +
        response.A4.length +
        response.A5.length + ' bytes';
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
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("/clockHub")
    .withAutomaticReconnect()
    .build();

connectionClockHub.on("ShowTime", function (currentTime) {
    document.getElementById("timeSpan").textContent = currentTime;
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