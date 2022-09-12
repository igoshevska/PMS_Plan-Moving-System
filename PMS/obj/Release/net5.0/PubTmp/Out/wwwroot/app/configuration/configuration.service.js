"use strict";

angular.module("configurationModule").factory("configurationService",
    [
        "$rootScope", "repository", function ($rootScope, repository) {
            return {
                //Users
                testApiCall: function (data) {
                    var url = "/ConfigurationApi/TestMethod";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getAllUsersFiltered: function (data) {
                    var url = "/ConfigurationApi/GetAllUsersFiltered";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getUserById: function (data) {
                    var url = "/ConfigurationApi/GetUserById";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getAllRoles: function () {
                    var url = "/ConfigurationApi/GetAllRoles";
                    var result = repository.promisePost(url, null, "GET");
                    return result;
                },
                getAllWorkerTypes: function () {
                    var url = "/ConfigurationApi/GetAllWorkerTypes";
                    var result = repository.promisePost(url, null, "GET");
                    return result;
                },
                createUser: function (data) {
                    var url = "/ConfigurationApi/CreateUser";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                updateUser: function (data) {
                    var url = "/ConfigurationApi/UpdateUser";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                //Machines
                getAllMachinesFiltered: function (data) {
                    var url = "/ConfigurationApi/GetAllMachinesFiltered";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getMachineById: function (data) {
                    var url = "/ConfigurationApi/GetMachineById";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                createMachine: function (data) {
                    var url = "/ConfigurationApi/CreateMachine";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                updateMachine: function (data) {
                    var url = "/ConfigurationApi/UpdateMachine";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getAllLocations: function () {
                    var url = "/ConfigurationApi/GetAllLocations";
                    var result = repository.promisePost(url, null, "GET");
                    return result;
                },
                //Shifts
                getAllShifts: function () {
                    var url = "/ConfigurationApi/GetAllShifts";
                    var result = repository.promisePost(url, null, "GET");
                    return result;
                }, 
                updateShift: function (data) {
                    var url = "/ConfigurationApi/UpdateShift";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                
                getAllEmployeesEfficiencyFiltered: function (data) {
                    var url = "/ConfigurationApi/GetAllEmployeesEfficiencyFiltered";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
 


            };
        }
    ]);