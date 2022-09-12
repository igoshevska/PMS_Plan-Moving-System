(function () {

    angular.module('configurationModule').controller('shiftController',
        [
            '$scope', '$rootScope', 'configurationService', 'pmsModels',
            function ($scope, $rootScope, configurationService, pmsModels) {

                $scope.firstShift = new pmsModels.Shift();
                $scope.secondShift = new pmsModels.Shift();
                $scope.thirdShift = new pmsModels.Shift();

                $scope.firstShiftChangeSuccess = false;
                $scope.secondShiftChangeSuccess = false;
                $scope.thirdShiftChangeSuccess = false;

                $scope.getAllShifts = function () {
                    configurationService.getAllShifts()
                        .then(function (result) {
                            $scope.firstShift = result[0];
                            $scope.secondShift = result[1];
                            $scope.thirdShift = result[2];
                        });
                };

                $scope.updateFirstShift = function () {
                    if ($scope.firstShift != null) {
                        configurationService.updateShift($scope.firstShift)
                            .then(function (result) {
                                if (result === 'Success') {
                                    $scope.firstShiftChangeSuccess = true;
                                    setTimeout(function () {
                                        $scope.firstShiftChangeSuccess = false;
                                    }, 1000);
                                }
                                //else show error message
                            });
                    }
                }

                $scope.updateSecondShift = function () {
                    if ($scope.secondShift != null) {
                        configurationService.updateShift($scope.secondShift)
                            .then(function (result) {
                                if (result === 'Success') {
                                    $scope.secondShiftChangeSuccess = true;
                                    setTimeout(function () {
                                        $scope.secondShiftChangeSuccess = false;
                                    }, 1000);
                                }
                                //else show error message
                            });
                    }
                }

                $scope.updateThirdShift = function () {
                    if ($scope.thirdShift != null) {
                        configurationService.updateShift($scope.thirdShift)
                            .then(function (result) {
                                if (result === 'Success') {
                                    $scope.thirdShiftChangeSuccess = true;
                                    setTimeout(function () {
                                        $scope.thirdShiftChangeSuccess = false;
                                    }, 1000);
                                }
                                //else show error message
                            });
                    }
                }
                




                $scope.getAllShifts();

            }]);
}());