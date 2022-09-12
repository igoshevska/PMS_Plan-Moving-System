(function () {

    angular.module('configurationModule').controller('userModalController',
        [
            '$scope', '$rootScope', 'configurationService', '$uibModalInstance', '$uibModal', 'pmsModels', 'user',
            function ($scope, $rootScope, configurationService, $uibModalInstance, $uibModal, pmsModels, user) {

                $scope.userModalTitle = 'Додавање на нов корисник';
                $scope.userModalSubmitButton = 'Додади';
                $scope.model = new pmsModels.User();
                $scope.model.isActive = true;
                
                if (user !== null) {
                    $scope.userModalTitle = 'Промена на корисник';
                    $scope.userModalSubmitButton = 'Промени';
                    $scope.model = angular.copy(user);
                }
                
                $scope.createUser = function () {
                    if (user != null) {                 
                        if ($scope.model !== null) {
                            configurationService.updateUser($scope.model)
                                .then(function (result) {
                                    $uibModalInstance.close(result);
                                });
                        };
                    } else {                            
                        if ($scope.model !== null) {
                            configurationService.createUser($scope.model)
                                .then(function (result) {
                                    $uibModalInstance.close(result);
                                });
                        };
                    }
                }

                $scope.deleteSelectedWorkerType = function () {
                    $scope.model.workerType = null;
                }

                $scope.cancel = function () {
                    $uibModalInstance.dismiss("Close");
                }


            }]);
}());