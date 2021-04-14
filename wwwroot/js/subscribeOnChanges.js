"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/messagehub").build();

connection.on("AddMember", function (user, id) {
    // Remember string interpolation
    $("#list").append(`<li class="member" member-id="${id}">
	   <span class="name">${user}</span><span class="delete fa fa-remove"></span><i class="startEdit fa fa-pencil" data-toggle="modal" data-target="#editClassmate"></i>
     </li>`);
});

connection.on("RemoveMember", function (id) {
    var targetMemberTag = $(`li.member[member-id=${id}]`)
    targetMemberTag.remove();
});

connection.start().then(function () {
    console.log("Connection established");
}).catch(function (err) {
    return console.error(err.toString());
});