(function () {

    angular.module('homeModule').controller('homeController',
        [
            '$scope', '$rootScope', 'homeService',
            function ($scope, $rootScope, homeService) {

                $scope.title = "home controller works!";

                
                $scope.getAllTestPMS() = function () {
                    homeService.getAllTestPMS()
                        .then(function (result) {
                            $scope.workOrderModel = result;
                        });
                }

                $scope.getAllTestPMS();

                
            }]);
}());

