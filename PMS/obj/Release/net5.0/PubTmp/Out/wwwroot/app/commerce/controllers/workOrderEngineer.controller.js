(function () {

    angular.module('commerceModule').controller('workOrderEngineerController',
        [
            '$scope', '$rootScope', 'commerceService', '$uibModal', 'NgTableParams', '$state',
            function ($scope, $rootScope, commerceService, $uibModal, NgTableParams, $state) {

                $scope.searchText = "";

                //go to details 
                $scope.openWorkOrderEngineerDetails = function (workOrder) {
                    $state.go('workOrderEngineerDetails', {
                        id: workOrder.id
                    });
                }

                $scope.GetWorkOrderFiltered = function () {
                    $scope.workOrderDataTable = new NgTableParams({
                        page: 1,
                        count: 10
                    }, {
                        counts: [10, 20, 30, 50],
                        total: 0,
                        getData: function (params) {
                            var dataForSend = {
                                page: params.page(),
                                rows: params.count(),
                                searchtext: $scope.searchText
                            };
                            return commerceService.getAllEngineerWorkOrdersFiltered(dataForSend)
                                .then(function (result) {

                                    var data = result.items;
                                    params.total(result.totalItems);
                                    $scope.totalRows = result.totalItems;
                                    $scope.allWorkOrders = data;

                                    return data;
                                });
                        }
                    });
                };

                $scope.reloadTable = function () {
                    $scope.workOrderDataTable.reload();
                };

                $scope.DeleteSearchText = function () {
                    $scope.searchText = '';
                    $scope.GetWorkOrderFiltered();
                }

                $scope.GetWorkOrderFiltered();

            }]);
}());