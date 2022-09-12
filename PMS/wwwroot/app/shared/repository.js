"use strict";

angular.module("appModule").factory("repository",
    ["$rootScope", "$http", "$q", "$location","$window", function ($rootScope, $http, $q, $location, $window) {

        return {
            promisePost: function (url, data, method) {

                var baseUrl = sessionStorage.getItem('apiUrl');

                var d = $q.defer();

                if (method == undefined)
                    method = "POST";

                if (data == undefined)
                    data = "";

                $http({
                    method: method,
                    url: baseUrl + url,
                    async: true,
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') },
                    mode: "queue",
                    data: JSON.stringify(data),
                    dataType: "json"
                }).then(function (response) {
                    d.resolve(response.data);

                },
                    function (response) {
                        d.reject(response);
                        if (response.status == 404) {
                            var url = $location.absUrl();
                            location.replace(url + '/UnauthorizedUser');
                        }
                        else if (response.status == 403) {
                            alert("You do not have permission for this action!");
                        }

                    });


                return d.promise;
            }
        };
    }]);