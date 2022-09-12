(function () {

    angular.module('commerceModule').controller('workOrderEngineerModalUpdateController',
        [
            '$scope', '$rootScope', 'commerceService', '$uibModalInstance', '$uibModal', 'pmsModels', 'engWorkOrder',  '$state',
            function ($scope, $rootScope, commerceService, $uibModalInstance, $uibModal, pmsModels, engWorkOrder,  $state) {

                $scope.workOrderEngineerModalTitle = 'Измена на инженерски налог';
                $scope.workOrderEngineerModalSubmitButton = 'Потврди'; 
                $scope.model = engWorkOrder;

                $scope.onChangeProductType = function (productTypeId) {
                    commerceService.getAllProductsByProductTypeId(productTypeId)
                        .then(function (result) {
                            $scope.products = result;
                            $scope.model.products = null;
                        });
                };

                $scope.getAllWorkerTypes = function () {
                    commerceService.getAllWorkerTypes()
                        .then(function (result) {
                            $scope.workerTypes = result;
                        });
                };

                $scope.getAllWorkerTypes();

                $scope.getAllProductTypes = function () {
                    commerceService.getAllProductTypes()
                        .then(function (result) {
                            $scope.productTypes = result;

                        });
                };
                $scope.getAllProductTypes();

                var items = $scope.model.subPhasesStates;
                $scope.checkAll = function () {
                    if ($scope.selectAll) {
                        items.forEach(allCheckboxesTrueFalse);
                        function allCheckboxesTrueFalse(item, index, arr) {
                            arr[index].isActive = true;
                        }
                    } else {
                        items.forEach(allCheckboxesTrueFalse);
                        function allCheckboxesTrueFalse(item, index, arr) {
                            arr[index].isActive = false;
                        }
                    }
                };

                $scope.checkAllPhaseOne = function () {
                    if ($scope.model.subPhasesStates[4].isActive) {
                        for (var i = 0; i < 5; i++) {
                            items[i].isActive = true;
                        }
                    } else {
                        for (var i = 0; i < 5; i++) {
                            items[i].isActive = false;
                        }
                    }
                };

                $scope.checkAllPhaseThree = function () {
                    if ($scope.model.subPhasesStates[7].isActive) {
                        for (var i = 6; i < 8; i++) {
                            items[i].isActive = true;
                        }
                    } else {
                        for (var i = 6; i < 8; i++) {
                            items[i].isActive = false;
                        }
                    }
                };

                $scope.updateNewEngineerWorkOrder = function () {
                        if ($scope.model !== null) {
                            commerceService.updateEngineerWorkOrder($scope.model)
                                .then(function (result) {
                                    $uibModalInstance.close(result)
                                });
                        };
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss("Close");
                }
            }]);
}());