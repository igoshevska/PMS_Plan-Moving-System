(function () {

    angular.module('homeModule').controller('priceProposalDetailsController',
        [
            '$scope', '$rootScope', 'homeService', 'pmsModels', '$stateParams', '$state',
            function ($scope, $rootScope, homeService, pmsModels, $stateParams, $state) {

                if ($stateParams.id) {
                    $scope.proposalId = $stateParams.id;
                }

                $scope.user = sessionStorage.getItem('Fullname');
                $scope.role = sessionStorage.getItem('Role');

                $scope.getProposalByProposalId = function () {
                    homeService.getProposalByProposalId($scope.proposalId)
                        .then(function (result) {
                            $scope.proposalDetails = result.response;
                        });
                }

                $scope.getProposalByProposalId();

                $scope.back = function () {
                    $state.go('Home', {
                    });
                }
                

            }]);
}());

