(function () {

    angular.module('commerceModule').controller('workOrderPriorityController',
        [
            '$scope', '$rootScope', 'commerceService', '$uibModalInstance', '$uibModal', 'pmsModels', 'workOrder',
            function ($scope, $rootScope, commerceService, $uibModalInstance, $uibModal, pmsModels, workOrder) {

                $scope.model = new pmsModels.WorkOrder();
                $scope.model = angular.copy(workOrder);


                $scope.getAllPriorities = function () {
                    commerceService.getAllPriorities()
                        .then(function (result) {
                            $scope.priorities = result;
                        });
                };

                $scope.updateWorkOrderPriority = function (workOrderId, priorityId) {
                    var dataForSend = { workOrderId, priorityId };
                    commerceService.updateWorkOrderPriority(dataForSend)
                        .then(function (result) {
                            $uibModalInstance.close(result);
                        });
                }

                $scope.cancel = function () {
                    $uibModalInstance.dismiss("Close");
                }

                $scope.getAllPriorities();

            }]);
}());