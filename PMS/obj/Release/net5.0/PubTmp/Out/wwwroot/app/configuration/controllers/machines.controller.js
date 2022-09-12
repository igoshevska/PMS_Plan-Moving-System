(function () {

    angular.module('configurationModule').controller('machineController',
        [
            '$scope', '$rootScope', 'configurationService', '$uibModal', 'NgTableParams', 
            function ($scope, $rootScope, configurationService, $uibModal, NgTableParams) {

                $scope.machineId = 0;
                $scope.searchText = "";

                $scope.errorMessageModal = function (message) {
                    var modalInstance = $uibModal.open({
                        animation: false,
                        scope: $scope,
                        backdrop: 'static',
                        templateUrl: "/app/shared/errorModal.html",
                        controller: 'errorModalController',
                        windowClass: 'modal custom-modal-width',
                        size: 'md',
                        resolve: {
                            message: function () {
                                return message;
                            }
                        }
                    });
                };

                $scope.createEditMachine = function (machine) {
                    var modalInstance = $uibModal.open({
                        animation: false,
                        scope: $scope,
                        backdrop: 'static',
                        templateUrl: "/app/configuration/templates/machineModal.html",
                        controller: 'machineModalController',
                        windowClass: 'modal custom-modal-width',
                        size: 'md',
                        resolve: {
                            machine: function () {
                                    return machine;
                            }
                        }
                    });

                    modalInstance.result.then(function (userData) {
                        if (userData == "Success") {
                            $scope.reloadTable();
                        }
                        else {
                            $scope.errorMessageModal(userData);
                        }
                    });
                };

                $scope.GetMachineFiltered = function () {
                    $scope.machineDataTable = new NgTableParams({
                        page: 1,
                        count: 10
                    }, {
                        counts: [10, 20, 30, 50],             
                        getData: function (params) {
                            var dataForSend = {
                                page: params.page(),
                                rows: params.count(),
                                searchtext: $scope.searchText
                            };
                            return configurationService.getAllMachinesFiltered(dataForSend)
                                .then(function (result) {

                                    var data = result.items;
                                    params.total(result.totalItems);
                                    $scope.totalRows = result.totalItems;
                                    $scope.allMachines = data;

                                    return data;
                                });
                        }
                    });
                };

                $scope.reloadTable = function () {
                    $scope.machineDataTable.reload();
                };

                $scope.DeleteSearchText = function () {
                    $scope.searchText = '';
                    $scope.GetMachineFiltered();
                }


                $scope.GetMachineFiltered();
                
            }]);
}());