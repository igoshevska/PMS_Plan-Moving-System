(function () {

    angular.module('commerceModule').controller('workOrderEngineerDetailsController',
        [
            '$scope', '$rootScope', 'commerceService', '$uibModal', 'pmsModels', 'NgTableParams', '$stateParams',
            function ($scope, $rootScope, commerceService, $uibModal, pmsModels, NgTableParams, $stateParams) {

                $scope.engWorkOrderId = 0;

                if ($stateParams.id) {
                    $scope.engWorkOrderId = $stateParams.id;
                }

                $scope.engWorkOrderModel = new pmsModels.WorkOrderEngineer();

                $scope.getEngineerWorkOrderById = function (engWorkOrderId) {
                    commerceService.getEngineerWorkOrderById(engWorkOrderId)
                        .then(function (result) {
                            $scope.engWorkOrderModel = result;
                        });
                }


                var object = {
                    productId: 1,
                    parentId: null

                };
                $scope.getAllElementsOfProductByProductID = function (object) {
                    commerceService.getAllElementsOfProductByProductID(object)
                        .then(function (result) {
                            $scope.engWorkOrderModel = result;
                        });
                }
                $scope.getAllElementsOfProductByProductID(object);




                $scope.editWorkOrderEngineer = function (engWorkOrder) {
                    var modalInstance = $uibModal.open({
                        animation: false,
                        scope: $scope,
                        backdrop: 'static',
                        templateUrl: "/app/commerce/templates/workOrderEngineerModalUpdate.html",
                        controller: 'workOrderEngineerModalUpdateController',
                        windowClass: 'modal custom-modal-width',
                        size: 'lg',
                        resolve: {
                            engWorkOrder: function () {
                                return engWorkOrder;
                            }
                        }
                    });
                    modalInstance.result.then(function (userData) {
                        if (userData == "Success") {
                        }
                        else {
                            $scope.errorMessageModal(userData);
                        }
                    });
                }

                
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

                $scope.GetTree = function () {
                    $scope.treeViewTable = new NgTableParams({
                        page: 1,
                        count: 200
                    }, {
                        //counts: [10, 20, 30, 50],
                        getData: function (params) {

                            return commerceService.getTreeView()
                                .then(function (result) {

                                    var data = result;
                                    params.total(result.totalItems);
                                    $scope.totalRows = result.totalItems;
                                    $scope.tree = data;

                                    return data;
                                });
                        }
                    });

                };

                $scope.reloadTable = function () {
                    $scope.treeViewTable.reload();
                };

                $scope.GetTree();

                $scope.cancel = function () {
                    $uibModalInstance.dismiss("Close");
                }

                $scope.getEngineerWorkOrderById($scope.engWorkOrderId);


            }]);
}());