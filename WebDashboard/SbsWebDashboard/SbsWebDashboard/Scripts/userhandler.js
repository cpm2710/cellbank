﻿var users;
var GetAllUsersURl = "http://localhost:9600/UserService.svc/";
function getAllUsers(callback) {
    $.getJSON(GetAllUsersURl,
        function (data) {
            if (data != undefined) {
                callback(data);
            }
        });
}