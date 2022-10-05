"use strict";

angular.module("accountModule").factory("accountService",
    [
         "$rootScope","repository", function ($rootScope,repository) {
            return {
                login: function (data) {
                    var url = "/Account/Login";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getUrls: function () {
                    var url = "/Account/GetUrls";
                    var result = repository.promisePost(url, null, "GET");
                    return result;
                },
                registeringUser: function (data) {
                    var url = "/Account/RegisteringUser";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                loginWithFacebook: function (data) {
                    var url = "/Account/LoginWithFacebook";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },

                

            };


        }
    ]);