(function () {

    angular.module('accountModule').controller('registerUserModalController',
        [
            '$scope', '$rootScope', 'accountService', '$uibModal', 'NgTableParams', '$state', 'pmsModels', '$state', '$uibModalInstance',
            function ($scope, $rootScope, accountService, $uibModal, NgTableParams, $state, pmsModels, $state, $uibModalInstance) {

                $scope.registerModel = new pmsModels.RegisterViewModel();
                $scope.showPassword = false;

                $scope.registeringUser = function () {
                    accountService.registeringUser($scope.registerModel)
                        .then(function (result) {
                            if (result == "Success") {
                                $uibModalInstance.close(result);
                            }
                            else {
                                $scope.message = result;
                            }
                        });
                }

                $scope.showPassword = function () {
                    var x = document.getElementById("showPassword");
                    if (x.type === "password") {
                        x.type = "text";
                    } else {
                        x.type = "password";
                    }
                }

                $scope.showConfirmPassword = function () {
                    var x = document.getElementById("showConfirmPassword");
                    if (x.type === "password") {
                        x.type = "text";
                    } else {
                        x.type = "password";s
                    }
                }

                $scope.cancel = function () {
                    $uibModalInstance.close();
                }

                sessionStorage.setItem('apiUrl', apiUrl);
                sessionStorage.setItem('uiUrl', uiUrl);

                $scope.openPriceProposal = function (uiUrl) {
                    location.replace(uiUrl + '/Home')
                };

                $scope.loginWithFacebook = function () {
                    var modalInstance = $uibModal.open({
                        animation: false,
                        scope: $scope,
                        backdrop: 'static',
                        templateUrl: "/app/account/templates/facebookLogin.html",
                        controller: 'facebookLoginController',
                        windowClass: 'modal custom-modal-width',
                        size: 'md',
                    });
                    modalInstance.result.then(function (result) {
                        if (result == "Success") {
                        }
                    });
                }

                $scope.checkLoginState = function () {
                    FB.getLoginStatus(function (response) {
                        $("#authstatus").html("<code>" + JSON.stringify(response, null, 2) + "</code>");
                        var accessToken = JSON.stringify(response.authResponse.accessToken);
                       $scope.loginWithFacebook(accessToken);
                    })
                };


                $scope.onFBLogin = function () {
                    FB.login(function (response) {
                        if (response.authResponse) {
                            var accessToken = JSON.stringify(response.authResponse.accessToken);
                            $scope.loginWithFacebook(accessToken);
                        }
                    });
                }







                $scope.loginWithFacebook = function (accessToken) {
                    accountService.loginWithFacebook(accessToken)
                        .then(function (result) {
                            if (result != "Error") {
                                sessionStorage.setItem('accessToken', result);
                                $scope.openPriceProposal(uiUrl);
                            }
                            else {
                                $scope.message = "Something went wrong";
                            }
                        });
                }

                
                sessionStorage.setItem('apiUrl', apiUrl);
                sessionStorage.setItem('uiUrl', uiUrl);

                $scope.openPriceProposal = function (uiUrl) {
                    location.replace(uiUrl + '/Home')
                };



            }]);
}());
