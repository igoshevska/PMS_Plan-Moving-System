

(function () {

    angular.module('homeModule').controller('homeController',
        [
            '$scope', '$http', '$rootScope', 'homeService', '$uibModal', 'NgTableParams', '$state', 'pmsModels', '$window',
            function ($scope, $http, $rootScope, homeService, $uibModal, NgTableParams, $state, pmsModels, $window) {

                $scope.searchText = "";
                $scope.currentLanguage = "";
                $scope.serachModel = new pmsModels.SearchViewModel();

                $scope.openProposalDetails = function (proposalId) {
                    $state.go('priceProposalDetails', {
                        id: proposalId
                    });
                }

                $scope.calculatePriceProposal = function () {
                    var modalInstance = $uibModal.open({
                        animation: false,
                        scope: $scope,
                        backdrop: 'static',
                        templateUrl: "/app/home/templates/priceProposalModal.html",
                        controller: 'priceProposalModalController',
                        windowClass: 'modal custom-modal-width',
                        size: 'md',
                    });
                    modalInstance.result.then(function (result) {
                        if (result == "Success") {
                            $scope.reloadTable();
                        }
                        else {
                                "Error"
                        }
                    });
                }

                $scope.chooseCurrency = function () {
                    var modalInstance = $uibModal.open({
                        animation: false,
                        scope: $scope,
                        backdrop: 'static',
                        templateUrl: "/app/home/templates/currencyModal.html",
                        controller: 'currencyModalController',
                        windowClass: 'modal custom-modal-width',
                        size: 'md',
                    });
                    modalInstance.result.then(function (currency) {
                        if (currency != null) {
                            $scope.reloadTable();
                            $scope.currentLanguage = currency;
                        }
                        else {
                            "Error"
                        }
                    });
                }

                $scope.getCurrentCulture = function () {
                    homeService.getCurrentCulture()
                        .then(function (result) {
                            if (result.response != "Error") {
                                var currentCulture = sessionStorage.getItem('currentCulture', result.response);
                                if (currentCulture == null) {
                                    sessionStorage.setItem('currentCulture', result.response);
                                    $scope.currentLanguage = result.response;
                                }
                                else {
                                    $scope.currentLanguage = sessionStorage.getItem('currentCulture', result.response);
                                }
                                
                            } 
                        });
                }

                $scope.getCurrentCulture();

                $scope.getAllProposalsAndOrdersByUserNameFiltered = function () {
                    $scope.userDataTable = new NgTableParams({
                        page: 1,
                        count: 10
                    }, {
                        counts: [10, 20, 30, 50],
                        getData: function (params) {
                            $scope.serachModel.page = params.page(),
                                $scope.serachModel.rows = params.count(),
                                $scope.serachModel.searchText = $scope.searchText;
                            
                            return homeService.getAllProposalsAndOrdersByUserNameFiltered($scope.serachModel)
                                .then(function (result) {
                                    var data = result.response.items;
                                    params.total(result.response.totalItems);
                                    $scope.totalRows = result.response.totalItems;
                                    $scope.allProposals = data;

                                    return data;
                                });
                        }
                    });
                };

                $scope.getAllProposalsAndOrdersByUserNameFiltered();

                $scope.reloadTable = function () {
                    $scope.userDataTable.reload();
                };

                $scope.getCurrenUser = function () {
                    homeService.getCurrenUser()
                        .then(function (result) {
                            $scope.user = result.response;
                            sessionStorage.setItem('Fullname', $scope.user.name + ' ' + $scope.user.surname);
                            sessionStorage.setItem('Role', $scope.user.role.name);
                           
                        });
                }

                $scope.getCurrenUser();

                $scope.DeleteSerachText = function () {
                    $scope.searchText = '';
                    $scope.getAllProposalsAndOrdersByUserNameFiltered();
                }

                var accessToken = sessionStorage.getItem('accessToken');

                $scope.connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44398/notificationHub", {
                    accessTokenFactory: () => accessToken,
                    skipNegotiation: true,
                    transport: signalR.HttpTransportType.WebSockets
                }).build();

                $scope.connection.on("ReciveMessage", function (message) {
                    $window.alert(message);
                    $scope.showAlert(message);
                });

                $scope.connection.start().catch(function (err) {
                });

                //Send the message  
                $scope.send = function (proposalNumber) { 
                    $scope.connection.invoke("SendNotification", proposalNumber).catch(err => console.error(err.toString()));
                };

                $scope.createOrder = function (proposalId, proposalNumber) {
                    homeService.createOrder(proposalId)
                        .then(function (result) {
                            $scope.res = result.response;
                            if ($scope.res == "Success") {
                                $scope.send(proposalNumber);
                                $scope.reloadTable();
                            }
                        });
                }

            }]);
}());


