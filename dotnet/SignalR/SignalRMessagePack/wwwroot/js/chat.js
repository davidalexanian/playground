"use strict";

document.getElementById("sendToServerMessagePackButton").addEventListener("click", function (event) {
    messagePackHub
        .invoke("SendToServerButtonClick", payloadToSend)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
});

document.getElementById("sendToServerJsonButton").addEventListener("click", function (event) {
    jsonHub
        .invoke("SendToServerButtonClick", payloadToSend)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
});

document.getElementById("sendToServerArgumentsMessagePackButton").addEventListener("click", function (event) {
    let args = Object.values(payloadToSend);
    messagePackHub
        .invoke("SendToServerArgumentsButtonClick", ...args)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
});

document.getElementById("sendToServerArgumentsJsonButton").addEventListener("click", function (event) {
    let args = Object.values(payloadToSend);
    jsonHub
        .invoke("SendToServerArgumentsButtonClick", ...args)
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
});

document.getElementById("sendFromServerMessagePackButton").addEventListener("click", function (event) {
    messagePackHub
        .invoke("SendFromServerButtonClick")
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
});
document.getElementById("sendFromServerJsonButton").addEventListener("click", function (event) {
    jsonHub
        .invoke("SendFromServerButtonClick")
        .catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
});

// hub setup
let messagePackHub = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Debug)
    .withUrl("/messagePackHub", { transport: signalR.HttpTransportType.WebSockets })
    .withHubProtocol(new signalR.protocols.msgpack.MessagePackHubProtocol())
    .withAutomaticReconnect()
    .build();

messagePackHub.on("ReceiveMessageFromServer", function (payload) {
    console.log("ReceiveMessageFromServer", payload);

    let pre = document.createElement("pre");
    pre.textContent = JSON.stringify(payload, undefined, 2);
    document.getElementById("messagesList").prepend(pre);
});

let jsonHub = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Debug)
    .withUrl("/jsonHub", { transport: signalR.HttpTransportType.WebSockets })
    .withAutomaticReconnect()
    .build();

jsonHub.on("ReceiveMessageFromServer", function (payload) {
    console.log("ReceiveMessageFromServer", payload);

    let pre = document.createElement("pre");
    pre.textContent = JSON.stringify(payload, undefined, 2);
    document.getElementById("messagesList").prepend(pre);
});

try {
    messagePackHub.start();
    jsonHub.start();
} catch (err) {
    console.log(err);
}

const payloadToSend =
{
    PropId: "29de8602-b7cc-4514-b086-443c1fe4b880",
    PropInt: 10,
    PropString: "abcdef",
    PropDecimal: 123.456,
    PropDecimalString: 88,
    PropDecimalNullable: null,
    PropDouble: 123.456,
    PropDateTime: "2024-03-03T06:05:05.7307678+01:00",
    PropDateTimeNullable: "2024-03-03T05:05:05.7307678Z",
    PropDateTimeOffset: "2024-03-01T06:05:05.7307678+01:00",
    PropDateTimeOffsetNullable: "2024-03-01T05:05:05.7307678+00:00",
    PropEnum: 2,
    PropItems: [
        {
            ItemId: 1,
            ItemName: "Name1"
        },
        {
            ItemId: 2,
            ItemName: "Name2"
        }
    ],
    PropDict: {
        aa: "Aaa",
        bb: 11,
        cc: {
            "Cc1": 1,
            "Cc2": "Cc2"
        },
        ee: null,
        ff: [11, "aa", null]
    }
};
