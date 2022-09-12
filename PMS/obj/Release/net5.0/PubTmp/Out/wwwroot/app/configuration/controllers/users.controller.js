(function () {

    angular.module('configurationModule').controller('userController',
        [
            '$scope', '$rootScope', 'configurationService', '$uibModal', 'NgTableParams',
            function ($scope, $rootScope, configurationService, $uibModal, NgTableParams) {

                $scope.userId = 0;
                $scope.searchText = "";

                $scope.getUserById = function () {
                    configurationService.getUserById($scope.userId)
                        .then(function (result) {
                            $scope.user = result;
                        });
                };

                $scope.getAllRoles = function () {
                    configurationService.getAllRoles()
                        .then(function (result) {
                            $scope.roles = result;
                        });
                };

                $scope.getAllWorkerTypes = function () {
                    configurationService.getAllWorkerTypes()
                        .then(function (result) {
                            $scope.workerTypes = result;
                        });
                };


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

                $scope.createEditUser = function (user) {
                    var modalInstance = $uibModal.open({
                        animation: false,
                        scope: $scope,
                        backdrop: 'static',
                        templateUrl: "/app/configuration/templates/userModal.html",
                        controller: 'userModalController',
                        windowClass: 'modal custom-modal-width',
                        size: 'md',
                        resolve: {
                            user: function () {
                                return user;
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

                $scope.GetUserFiltered = function () {
                    $scope.userDataTable = new NgTableParams({
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
                            return configurationService.getAllUsersFiltered(dataForSend)
                                .then(function (result) {

                                    var data = result.items;
                                    params.total(result.totalItems);
                                    $scope.totalRows = result.totalItems;
                                    $scope.allUsers = data;

                                    return data;
                                });
                        }
                    });

                };

                $scope.reloadTable = function () {
                    $scope.userDataTable.reload();
                };

                $scope.DeleteSearchText = function () {
                    $scope.searchText = '';
                    $scope.GetUserFiltered();
                }

                $scope.getAllRoles();
                $scope.getAllWorkerTypes();
                $scope.GetUserFiltered();

            }]);
}());