(function () {

    angular.module('homeModule').controller('homeController',
        [
            '$scope', '$rootScope', 'homeService', '$uibModal', 'NgTableParams', '$state', 'pmsModels',
            function ($scope, $rootScope, homeService, $uibModal, NgTableParams, $state, pmsModels) {


                $scope.searchText = "";
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
                                    var data = result.items;
                                    params.total(result.totalItems);
                                    $scope.totalRows = result.totalItems;
                                    $scope.allProposals = data;

                                    return data;
                                });
                        }
                    });

                };

                $scope.getAllProposalsAndOrdersByUserNameFiltered();


                $scope.createOrder = function (proposalId) {
                    homeService.createOrder(proposalId)
                        .then(function (result) {
                            $scope.res = result;
                            if ($scope.res == "Success") {
                                $scope.reloadTable();
                            }
                        });
                }

                $scope.reloadTable = function () {
                    $scope.userDataTable.reload();
                };


                $scope.getCurrenUser = function () {
                    homeService.getCurrenUser()
                        .then(function (result) {
                            $scope.user = result;
                            sessionStorage.setItem('Fullname', $scope.user.name + ' ' + $scope.user.surname);
                            sessionStorage.setItem('Role', $scope.user.role.name);
                        });
                }

                $scope.getCurrenUser();

                $scope.DeleteSerachText = function () {
                    $scope.searchText = '';
                    $scope.getAllProposalsAndOrdersByUserNameFiltered();
                }

            }]);
}());

