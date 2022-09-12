(function () {

    angular.module('employeesModule').controller('employeesController',
        [
            '$scope', '$rootScope', 'employeesService', 'NgTableParams',
            function ($scope, $rootScope, employeesService, NgTableParams) {

                $scope.searchText = "";

                $scope.GetEmployeesFiltered = function () {
                    $scope.employeesDataTable = new NgTableParams({
                        page: 1,
                        count: 10
                    }, {
                        counts: [10, 20, 30, 50],
                        getData: function (params) {
                            var dataForSend = {
                                page: params.page(),
                                rows: params.count(),
                                searchtext: $scope.searchText
                            };
                            return employeesService.getAllEmployeesEfficiencyFiltered(dataForSend)
                                .then(function (result) {

                                    var data = result.items;
                                    params.total(result.totalItems);
                                    $scope.totalRows = result.totalItems;
                                    $scope.allEmployeesEfficiency = data;

                                    return data;
                                });
                        }
                    });

                };

                //$scope.reloadTable = function () {
                //    $scope.userDataTable.reload();
                //};

                //$scope.DeleteSearchText = function () {
                //    $scope.searchText = '';
                //    $scope.GetEmployyesFiltered();
                //}
                $scope.GetEmployeesFiltered();
            }]);
}());