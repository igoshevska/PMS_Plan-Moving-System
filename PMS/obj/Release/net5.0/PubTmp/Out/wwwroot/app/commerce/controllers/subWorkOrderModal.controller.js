(function () {

    angular.module('commerceModule').controller('subWorkOrderModalController',
        [
            '$scope', '$rootScope', 'commerceService', '$uibModalInstance', '$uibModal', 'pmsModels', 'subWorkOrder', 'workOrderId', '$filter',
            function ($scope, $rootScope, commerceService, $uibModalInstance, $uibModal, pmsModels, subWorkOrder, workOrderId, $filter) {

                $scope.dateFormat = 'dd.MM.yyyy';
                $scope.altInputFormats = ['d!/M!/yyyy'];

                $scope.today = new Date();

                $scope.popup1 = {
                    opened: false
                };

                $scope.open1 = function () {
                    $scope.popup1.opened = true;
                };

                $scope.subWorkOrderModalTitle = 'Додавање на нов под-налог';
                $scope.subWorkOrderModalSubmitButton = 'Додади';
                $scope.model = new pmsModels.SubWorkOrder();
                $scope.model.commerceWorkOrderId = workOrderId;

                //to be checked, here for testing
                $scope.model.status = new pmsModels.Status();
                $scope.model.status.id = 2;
                $scope.model.status.name = '';
                

                if (subWorkOrder !== null) {
                    $scope.subWorkOrderModalTitle = 'Промена на под-налог';
                    $scope.subWorkOrderModalSubmitButton = 'Промени';
                    $scope.model = angular.copy(subWorkOrder);
                    $scope.model.date = new Date($scope.model.date);
                }

                $scope.createSubWorkOrder = function () {
                    $scope.model.date = $scope.model.date.toJSON();
                    if (subWorkOrder != null) {                 
                        if ($scope.model !== null) {
                            commerceService.updateCommerceSubWorkOrder($scope.model)           
                                .then(function (result) {
                                    $uibModalInstance.close(result);
                                });
                        };
                    } else {                                   
                        if ($scope.model !== null) {
                            commerceService.createCommerceSubWorkOrder($scope.model)           
                                .then(function (result) {
                                    $uibModalInstance.close(result);
                                });
                        };
                    }
                }


                $scope.cancel = function () {
                    $uibModalInstance.dismiss("Close");
                }

            }]);
}());