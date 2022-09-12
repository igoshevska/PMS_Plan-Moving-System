(function () {

    angular.module('monitoringModule').controller('monitoringController',
        [
            '$scope', '$rootScope', 'mnonitoringService', 'NgTableParams',
            function ($scope, $rootScope, mnonitoringService, NgTableParams) {

                $scope.test = "Monitoring module works :D";

                $scope.getAllMachinesMonitoring = function () {
                    mnonitoringService.getAllMachinesMonitoring()
                        .then(function (result) {
                            $scope.machines = result;
                        });
                };
                $scope.getAllMachinesMonitoring();
 
            }]);
}());