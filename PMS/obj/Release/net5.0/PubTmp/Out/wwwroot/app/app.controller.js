(function () {

    angular.module("appModule").controller('appController',
        [
            '$scope', '$rootScope', 'appService', '$window','$location',
            function ($scope, $rootScope, appService, $window, $location) {
          
                $scope.isActive = function (route) {
                    return route === $location.path();
                }
               
                $scope.goToPage = function (link) {              
                    $window.location.href = link;                               
                };
           
            }]);
}());

