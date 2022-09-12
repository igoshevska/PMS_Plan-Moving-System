"use strict";

angular.module("monitoringModule").factory("mnonitoringService",
    [
        "$rootScope", "repository", function ($rootScope, repository) {
            return {
                getAllMachinesMonitoring: function () {
                    var url = "/MonitoringApi/GetAllMachinesMonitoring";
                    var result = repository.promisePost(url, null, "GET");
                    return result;
                },
               

            };
        }
    ]);