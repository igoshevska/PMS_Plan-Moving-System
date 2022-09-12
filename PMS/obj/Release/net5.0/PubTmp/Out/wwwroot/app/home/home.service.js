"use strict";

angular.module("homeModule").factory("homeService",
    [
         "$rootScope","repository", function ($rootScope,repository) {
            return {
                testApiCall: function (data) {
                    var url = "/HomeApi/TestMethod";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getAllTestPMS: function () {
                    var url = "/HomeApi/GetAllTestPMS";
                    var result = repository.promisePost(url, null, "POST");
                    return result;
                }
                

            };


        }
    ]);