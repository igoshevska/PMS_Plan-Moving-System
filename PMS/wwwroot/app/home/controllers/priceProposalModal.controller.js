(function () {

    angular.module('homeModule').controller('priceProposalModalController',
        [
            '$scope', '$rootScope', 'homeService', '$uibModal', '$uibModalInstance', 'pmsModels',
            function ($scope, $rootScope, homeService, $uibModal, $uibModalInstance, pmsModels) {

                $scope.model = new pmsModels.CalculatePriceProposalViewModel();
                $scope.model.distance = 10;
                $scope.createPriceProposal = function () {
                    homeService.createPriceProposal($scope.model)
                        .then(function (result) {
                            $uibModalInstance.close(result);

                        });
                };

                $scope.cancel = function () {
                    $uibModalInstance.close();
                }

                $scope.reloadTable = function () {
                    $scope.userDataTable.reload();
                };
            }]);
}());

