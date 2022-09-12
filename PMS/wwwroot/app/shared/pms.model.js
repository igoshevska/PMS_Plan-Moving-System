'use strict'

angular.module('appModule').factory("pmsModels",
    [

        function () {
            var model = this;

            //Calculate Price Proposal
            model.CalculatePriceProposalViewModel = function () {
                this.id = 0;
                this.distance = 0;
                this.livingArea = 0;
                this.atticArea = 0;
                this.hasPiano = false;
                this.addressFrom = '';
                this.addressTo = ''
            };

            // Login User
            model.LoginUserViewModel = function () {
                this.userName = '';
                this.password = '';
            };

            //SerachViewModel
            model.SearchViewModel = function () {
                this.page = 0;
                this.rows = 0;
                this.searchText = '';
            }


            return model;

        }

    ]);