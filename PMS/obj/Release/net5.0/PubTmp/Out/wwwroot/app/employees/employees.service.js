"use strict";

angular.module("employeesModule").factory("employeesService",
    [
        "$rootScope", "repository", function ($rootScope, repository) {
            return {
                getAllEmployeesEfficiencyFiltered: function (data) {
                    var url = "/EmployeesApi/GetAllEmployeesEfficiencyFiltered";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                
            };
        }
    ]);