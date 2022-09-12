(function () {

    angular.module('configurationModule').controller('machineModalController',
        [
            '$scope', '$rootScope', 'configurationService', '$uibModalInstance', '$uibModal', 'pmsModels', 'machine',  
            function ($scope, $rootScope, configurationService, $uibModalInstance, $uibModal, pmsModels, machine) {

                $scope.machineModalTitle = 'Додавање на нова машина';
                $scope.machineModalSubmitButton = 'Додади';
                $scope.model = new pmsModels.Machine();

                if (machine !== null) {
                    $scope.machineModalTitle = 'Промена на машина';
                    $scope.machineModalSubmitButton = 'Промени';
                    $scope.model = angular.copy(machine);
                }

                $scope.getAllLocations = function () {
                    configurationService.getAllLocations()
                        .then(function (result) {
                            $scope.locations = result;
                        });
                };
                
                $scope.createMachine = function () {
                    if (machine != null) {                 
                        if ($scope.model !== null) {
                            configurationService.updateMachine($scope.model)
                                .then(function (result) {
                                    $uibModalInstance.close(result);
                                });
                        };
                    } else {                            
                        if ($scope.model !== null) {
                            configurationService.createMachine($scope.model)
                                .then(function (result) {
                                    $uibModalInstance.close(result);
                                });
                        };
                    }
                }

                $scope.cancel = function () {
                    $uibModalInstance.dismiss("Close");
                }

                $scope.getAllLocations();

            }]);
}());