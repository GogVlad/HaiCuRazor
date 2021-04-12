"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/messagehub").build();

connection.on("AddMember", function (user, id) {
    alert(`user=${user} with id = ${id}`);
});

connection.start().then(function () {
    console.log("Connection established");
}).catch(function (err) {
    return console.error(err.toString());
});