"use strict";

angular.module("appModule").factory("repository",
    ["$rootScope", "$http", "$q", "$location","$window", function ($rootScope, $http, $q, $location, $window) {

        return {
            promisePost: function (url, data, method) {

                var baseUrl = new $window.URL($location.absUrl()).origin;

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
                    //headers: { 'Authorization': 'Bearer ' + accessToken },
                    mode: "queue",
                    data: JSON.stringify(data),
                    dataType: "json"
                }).then(function (response) {
                    d.resolve(response.data);

                },
                    function (response) {
                        d.reject(response);

                    });

                return d.promise;
            },
            syncAjax: function (url, data, success, method) {
                var baseUrl = new $window.URL($location.absUrl()).origin;


                if (method == undefined)
                    method = "POST";

                $.ajax({
                    mode: "queue",
                    url: baseUrl + url,
                    async: false,
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    //headers: { 'Authorization': 'Bearer ' + accessToken },
                    type: method,
                    data: JSON.stringify(data),
                    dataType: "json",
                    success: function (d, s, x) {
                        if (success) {
                            if (d.hasOwnProperty("d")) {
                                var response = JSON.parse(d.d);
                            } else {
                                response = d;
                            }
                            success(response);
                        }

                       
                    },
                    error: function (r, p, x) {
                        if (error) {
                            error(r, p, x);
                        }
                        

                        
                    }
                });
            },
            downloadFile: function (url, postData, success, error) {
                var deferred;
                $.fileDownload(url, {
                    prepareCallback: function () {
                        deferred = $q.defer();
                    },
                    failCallback: function (e) {
                        error(e);
                    },
                    successCallback: function (e) {
                        success(e);
                    },
                    httpMethod: "POST",
                    data: postData
                });
            }
        };
    }]);