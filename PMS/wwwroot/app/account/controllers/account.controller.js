(function () {

    angular.module('accountModule').controller('accountController',
        [
            '$scope', '$rootScope', 'accountService', '$uibModal', 'NgTableParams', '$state', 'pmsModels', '$state',
            function ($scope, $rootScope, accountService, $uibModal, NgTableParams, $state, pmsModels, $state) {

                $scope.model = new pmsModels.LoginUserViewModel();

                $scope.login = function () {
                    accountService.login($scope.model)
                        .then(function (result) {
                            if (result != "Error") {
                                sessionStorage.setItem('accessToken', result);
                                $scope.openPriceProposal(uiUrl);
                            }
                            else {
                                $scope.message = "Invalid username or password";
                            }
                            
                        });
                }

                sessionStorage.setItem('apiUrl', apiUrl);
                sessionStorage.setItem('uiUrl', uiUrl);

                $scope.openPriceProposal = function (uiUrl) {
                    location.replace(uiUrl+'/Home')
                };
     
            }]);
}());

