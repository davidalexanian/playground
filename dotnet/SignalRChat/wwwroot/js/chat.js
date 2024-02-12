"use strict";

// disable the send button until connection is established.
document.getElementById("sendToAllButton").disabled = true;
document.getElementById("sendToUserButton").disabled = true;

// button clicks
document.getElementById("sendToAllButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;

    connection
        .invoke("SendMessageToAll", user, message)  // or send (doesnt waits for the server metod to complete)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
});
document.getElementById("sendToUserButton").addEventListener("click", function (event) {
    connection.invoke("SendMessageToUser", {
        fromUser: document.getElementById("userInput").value,
        toUser: document.getElementById("toUserInput").value,
        message: document.getElementById("messageInput").value
    }).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

// connect
let connection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("/chatHub")
    .withAutomaticReconnect()
    .build();

// best practice to call start after on
connection.on("ReceiveMessage", function (user, message) {
    let li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} says ${message}`;
});

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
        document.getElementById("sendToAllButton").disabled = false;
        document.getElementById("sendToUserButton").disabled = false;
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();  // manual reconnect
});
start();