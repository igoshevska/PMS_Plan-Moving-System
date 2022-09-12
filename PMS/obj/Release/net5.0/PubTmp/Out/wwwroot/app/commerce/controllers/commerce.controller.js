(function () {

    angular.module('commerceModule').controller('commerceController',
        [
            '$scope', '$rootScope', 'configurationService', '$uibModal', 'NgTableParams',
            function ($scope, $rootScope, configurationService, $uibModal, NgTableParams) {

                $scope.title = "Комерција";

            }]);
}());