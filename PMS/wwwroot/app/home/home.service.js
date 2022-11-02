"use strict";

angular.module("homeModule").factory("homeService",
    [
         "$rootScope","repository", function ($rootScope,repository) {
            return {
                createPriceProposal: function (data) {
                    var url = "/ProposalOrder/CreatePriceProposal";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getAllProposalsAndOrdersByUserNameFiltered: function (data) {
                    var url = "/ProposalOrder/GetAllProposalsAndOrdersByUserNameFiltered";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                createOrder: function (data) {
                    var url = "/ProposalOrder/CreateOrder";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getProposalByProposalId: function (data) {
                    var url = "/ProposalOrder/GetProposalByProposalId";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getCurrenUser: function (data) {
                    var url = "/ProposalOrder/GetCurrenUser";
                    var result = repository.promisePost(url, data, "POST");
                    return result;
                },
                getCurrentCulture: function () {
                    var url = "/ProposalOrder/GetCurrentCulture";
                    var result = repository.promisePost(url, null, "GET");
                    return result;
                },
                getAllCultures: function () {
                    var url = "/ProposalOrder/GetAllCultures";
                    var result = repository.promisePost(url, null, "GET");
                    return result;
                },
            };
        }
    ]);