(function () {

    angular.module('appModule').controller('errorModalController',
        [
            '$scope', '$rootScope', '$uibModalInstance', '$uibModal', 'message',
            function ($scope, $rootScope, $uibModalInstance, $uibModal, message) {

                $scope.message = message;

                $scope.cancel = function () {
                    $uibModalInstance.dismiss("Close");
                }

            }]);

}());