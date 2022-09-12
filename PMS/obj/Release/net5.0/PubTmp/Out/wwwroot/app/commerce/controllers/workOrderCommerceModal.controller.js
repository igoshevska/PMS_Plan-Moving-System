(function () {

    angular.module('commerceModule').controller('workOrderCommerceModalController',
        [
            '$scope', '$rootScope', 'commerceService', '$uibModalInstance', '$uibModal', 'pmsModels', 'workOrder', '$filter', '$state',
            function ($scope, $rootScope, commerceService, $uibModalInstance, $uibModal, pmsModels, workOrder, $filter, $state) {

                $scope.model = new pmsModels.WorkOrder();
                $scope.dateFormat = 'dd.MM.yyyy';
                $scope.altInputFormats = ['d!/M!/yyyy'];

                $scope.today = new Date();
                $scope.future = new Date(2121, 1, 1);
                
                $scope.popup1 = {
                    opened: false
                };

                $scope.open1 = function () {
                    $scope.popup1.opened = true;
                };

                $scope.popup2 = {
                    opened: false
                };

                $scope.open2 = function () {
                    $scope.popup2.opened = true;
                };

                $scope.workOrderModalTitle = 'Додавање на нов налог';
                $scope.workOrderModalSubmitButton = 'Додади';
                $scope.model = new pmsModels.WorkOrder();

                if (workOrder !== null) {
                    $scope.workOrderModalTitle = 'Промена на налог';
                    $scope.workOrderModalSubmitButton = 'Промени';
                    $scope.model = angular.copy(workOrder);
                    $scope.model.dateFrom = new Date($scope.model.dateFrom);
                    $scope.model.dateTo = new Date($scope.model.dateTo);
                }

                $scope.createCommerceWorkOrder = function () {
                    $scope.model.dateFrom = $scope.model.dateFrom.toJSON();
                    $scope.model.dateTo = $scope.model.dateTo.toJSON();
                    if (workOrder != null) {                 
                        if ($scope.model !== null) {
                            commerceService.updateCommerceWorkOrder($scope.model)
                                .then(function (result) {
                                    var res = {
                                        result: result,
                                        status: 'update'
                                    };
                                    $uibModalInstance.close(res);
                                });
                        };
                    } else {
                        if ($scope.model !== null) {
                            commerceService.createCommerceWorkOrder($scope.model)
                                .then(function (result) {
                                    var res = {
                                        result: result,
                                        status: 'create'
                                    };
                                    $uibModalInstance.close(res);
                                });
                        };
                    }
                }

                $scope.getAllPriorities = function () {
                    commerceService.getAllPriorities()
                        .then(function (result) {
                            $scope.priorities = result;
                        });
                };

                $scope.cancel = function () {
                    $uibModalInstance.dismiss("Close");
                }

                $scope.getAllPriorities();

            }]);
}());