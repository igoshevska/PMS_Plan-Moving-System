(function () {

    angular.module('commerceModule').controller('workOrderEngineerModalController',
        [
            '$scope', '$rootScope', 'commerceService', '$uibModalInstance', '$uibModal', 'pmsModels', 'subWorkOrder', 'subPhases','$state',
            function ($scope, $rootScope, commerceService, $uibModalInstance, $uibModal, pmsModels, subWorkOrder,  subPhases, $state) {

                $scope.workOrderEngineerModalTitle = 'Креирање на инженерски налог';
                $scope.workOrderEngineerModalSubmitButton = 'Потврди';
                subWorkOrder.haveEngineerWorkOrder = false;
                $scope.model = new pmsModels.WorkOrderEngineer();
                $scope.model.commerceSubWorkOrder = subWorkOrder;
                $scope.model.quantity = subWorkOrder.quantity;
                $scope.workerType = new pmsModels.WorkerType();

                $scope.createEngineerWorkOrder = function (workOrder) {
                    $state.go('workOrderEngineerDetails', {
                        id: 10
                    });
                }

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

                items.forEach(subPhasesFinction);
                function subPhasesFinction(item, index, arr) {
                    arr[index].subPhase = subPhases[index];
                }

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

                $scope.checkAllPhaseOne = function() {
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

                $scope.createNewEngineerWorkOrder = function () {
                    if ($scope.model !== null) {
                        commerceService.createNewEngineerWorkOrder($scope.model)
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