(function () {

    angular.module('accountModule').controller('accountController',
        [
            '$scope', '$rootScope', 'accountService', '$uibModal', 'NgTableParams', '$state', 'pmsModels', '$state',
            function ($scope, $rootScope, accountService, $uibModal, NgTableParams, $state, pmsModels, $state) {

                $scope.model = new pmsModels.LoginUserViewModel();

                $scope.login = function () {
                    accountService.login($scope.model)
                        .then(function (result) {
                            if (result.response != "Error") {
                                sessionStorage.setItem('accessToken', result.response);
                                $scope.openPriceProposal(uiUrl);
                            }
                            else {
                                $scope.message = "Invalid username or password";
                            }
                        });
                }

                $scope.registeringUser = function () {
                    var modalInstance = $uibModal.open({
                        animation: false,
                        scope: $scope,
                        backdrop: 'static',
                        templateUrl: "/app/account/templates/registerUserModal.html",
                        controller: 'registerUserModalController',
                        windowClass: 'modal custom-modal-width',
                        size: 'md',
                    });
                    modalInstance.result.then(function (result) {
                        if (result == "Success") {
                            $scope.message = "The registration was successful, now you can login";
                        }
                    });
                }

                sessionStorage.setItem('apiUrl', apiUrl);
                sessionStorage.setItem('uiUrl', uiUrl);

                $scope.openPriceProposal = function (uiUrl) {
                    location.replace(uiUrl+'/Home')
                };

                $scope.openLoginPage = function (uiUrl) {
                    location.replace(uiUrl + '/Account')
                };
     
            }]);
}());

