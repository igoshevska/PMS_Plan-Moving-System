"use strict";

angular.module("commerceModule").factory("commerceService",
    [
        "$rootScope", "repository", function ($rootScope, repository) {
            return {
                //Work orders commerce
                getAllCommerceWorkOrdersFiltered: function (data) {
                    var url = "/CommerceApi/GetAllCommerceWorkOrdersFiltered";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                createCommerceWorkOrder: function (data) {
                    var url = "/CommerceApi/CreateCommerceWorkOrder";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                updateCommerceWorkOrder: function (data) {
                    var url = "/CommerceApi/UpdateCommerceWorkOrder";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getCommerceWorkOrderById: function (data) {
                    var url = "/CommerceApi/GetCommerceWorkOrderById";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                }, 
                updateWorkOrderPriority: function (data) {
                    var url = "/CommerceApi/UpdateWorkOrderPriority";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },

                //Sub-work orders commerce
                getCommerceSubWorkOrderById: function (data) {
                    var url = "/CommerceApi/GetCommerceSubWorkOrderById";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getAllPriorities: function () {
                    var url = "/CommerceApi/GetAllPriorities";
                    var result = repository.promisePost(url, null, "GET");
                    return result;
                },
                getAllCommerceSubWorkOrdersFiltered: function (data) {
                    var url = "/CommerceApi/GetAllCommerceSubWorkOrdersFiltered";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                createCommerceSubWorkOrder: function (data) {
                    var url = "/CommerceApi/CreateCommerceSubWorkOrder";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                updateCommerceSubWorkOrder: function (data) {
                    var url = "/CommerceApi/UpdateCommerceSubWorkOrder";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },

                getAllTestPMS: function () {
                    var url = "/HomeApi/GetAllTestPMS";
                    var result = repository.promisePost(url, null, "POST");
                    return result;
                },

                
                //Work order Engineer
                getAllProductTypes: function () {
                    var url = "/CommerceApi/GetAllProductTypes";
                    var result = repository.promisePost(url, null, "GET");
                    return result;
                },
                getAllProductsByProductTypeId: function (data) {
                    var url = "/CommerceApi/GetAllProductsByProductTypeId";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getAllEngineerWorkOrdersFiltered: function (data) {
                    var url = "/CommerceApi/GetAllEngineerWorkOrdersFiltered";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                createNewEngineerWorkOrder: function (data) {
                    var url = "/CommerceApi/CreateNewEngineerWorkOrder";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                updateEngineerWorkOrder: function (data) {
                    var url = "/CommerceApi/UpdateEngineerWorkOrder";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getAllWorkerTypes: function () {
                    var url = "/ConfigurationApi/GetAllWorkerTypes";
                    var result = repository.promisePost(url, null, "GET");
                    return result;
                },
                getAllSubPhases: function () {
                    var url = "/CommerceApi/GetAllSubPhases";
                    var result = repository.promisePost(url, null, "GET");
                    return result;
                },
                getEngineerWorkOrderById: function (data) {
                    var url = "/CommerceApi/GetEngineerWorkOrderById";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getAllElementsOfProductByProductID: function (data) {
                    var url = "/CommerceApi/GetAllElementsOfProductByProductID";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getTreeView: function () {
                    var url = "/CommerceApi/GetTreeView";
                    var result = repository.promisePost(url, null, "POST");
                    return result;
                },

                
                

            };
        }
    ]);