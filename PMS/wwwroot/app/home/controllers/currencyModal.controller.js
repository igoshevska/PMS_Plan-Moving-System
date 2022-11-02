(function () {

    angular.module('homeModule').controller('currencyModalController',
        [
            '$scope', '$rootScope', 'homeService', 'pmsModels', '$stateParams', '$uibModalInstance', '$state',
            function ($scope, $rootScope, homeService, pmsModels, $stateParams, $uibModalInstance, $state) {

                $scope.getAllCultures = function () {
                    homeService.getAllCultures()
                        .then(function (result) {
                            $scope.allCultures = result.response;
                        });
                }

                $scope.getAllCultures();

                $scope.changeCurrency = function (currency) {
                    sessionStorage.setItem('currentCulture', currency);
                    $uibModalInstance.close(currency);
                }

                $scope.cancel = function () {
                    $uibModalInstance.close();
                }
        
            }]);
}());
