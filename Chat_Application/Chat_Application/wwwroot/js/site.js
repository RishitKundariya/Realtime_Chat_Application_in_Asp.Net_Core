// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict";
var ReceivedUserId;
$(document).ready(function () {
    loadUserList();
});

function loadUserList() {
    var request = $.ajax('/Home/GetAllUserListByUserId', {
        type: 'GET',  // http method
        data: {}, //{ myData: 'This is my data.' },  data to submit
        success: function (data) {
            $('#userList').html(data);
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('p').append('Error' + errorMessage);
        }
    });
}


function loadChatScreen(id) {
    ReceivedUserId = id ;
    var req = $.ajax('/Home/GetChat', {
        type: 'GET',  // http method
        data: { ReceivedUserId: ReceivedUserId }, //{myData: 'This is my data.' },  data to submit
        success: function (data) {
            $('#userChatScreen').html(data);
        },
        error: function (errorMessage) {
            alert(errorMessage);
        }
    });

}




var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();


connection.on("ReceiveMessage", function () {
    //alert("ReceivedUser id" + ReceivedUserId);
    loadUserList();
    loadChatScreen(ReceivedUserId);

});

connection.start();

function SendMessage() {
    $.ajax('/Home/SaveChat', {
        type: 'GET',  // http method
        data: { ReceiveUserId: ReceivedUserId, message: $("#txtMessage").val()}, //{myData: 'This is my data.' },  data to submit
        success: function (data) {
            //loadUserList();
            //loadChatScreen(ReceivedUserId);
        },
        error: function (errorMessage) {
            alert(errorMessage);
        }
    });
}

