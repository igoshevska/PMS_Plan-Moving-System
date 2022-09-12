(function () {

    angular.module('configurationModule').controller('configurationController',
        [
            '$scope', '$rootScope', 'configurationService', '$uibModal', 'NgTableParams',
            function ($scope, $rootScope, configurationService, $uibModal, NgTableParams) {

                $scope.title = "Конфигурација";
                $scope.activeTab = 0;

            }]);
}());