(function () {

    angular.module('commerceModule').controller('workOrderCommerceController',
        [
            '$scope', '$rootScope', 'commerceService', '$uibModal', 'NgTableParams', '$state', 
            function ($scope, $rootScope, commerceService, $uibModal, NgTableParams, $state) {

                $scope.searchText = "";


                $scope.getAllTestPMS = function () {
                    commerceService.getAllTestPMS()
                        .then(function (result) {
                            $scope.workOrderModel = result;
                        });
                }

                $scope.getAllTestPMS();

                $scope.errorMessageModal = function (message) {
                    var modalInstance = $uibModal.open({
                        animation: false,
                        scope: $scope,
                        backdrop: 'static',
                        templateUrl: "/app/shared/errorModal.html",
                        controller: 'errorModalController',
                        windowClass: 'modal custom-modal-width',
                        size: 'md',
                        resolve: {
                            message: function () {
                                return message;
                            }
                        }
                    });
                };
                
                $scope.createEditWorkOrder = function (workOrder) {
                    var modalInstance = $uibModal.open({
                        animation: false,
                        scope: $scope,
                        backdrop: 'static',
                        templateUrl: "/app/commerce/templates/workOrderCommerceModal.html",
                        controller: 'workOrderCommerceModalController',
                        windowClass: 'modal custom-modal-width',
                        size: 'md',
                        resolve: {
                            workOrder: function () {
                                return workOrder;
                            }
                        }
                    });
                    modalInstance.result.then(function (res) {
                        if (res.result ==  "Success" && res.status == 'update') {
                            $scope.reloadTable();
                        }
                        else if (res.result.message == "Success" && res.status == 'create') {
                            workOrderId = res.result.id;
                            $scope.openWorkOrderDetails(workOrderId);
                        }
                        else {
                            $scope.errorMessageModal(res.result);        
                        }
                    });
                };

                //go to details 
                $scope.openWorkOrderDetails = function (workOrderId) {
                    $state.go('workOrderCommerceDetails', {
                        id: workOrderId
                    });
                }


                $scope.changeWorkOrderPriority = function (workOrder) {
                    var modalInstance = $uibModal.open({
                        animation: false,
                        scope: $scope,
                        backdrop: 'static',
                        templateUrl: "/app/commerce/templates/workOrderPriority.html",
                        controller: 'workOrderPriorityController',
                        windowClass: 'modal custom-modal-width',
                        size: 'md',
                        resolve: {
                            workOrder: function () {
                                return workOrder;
                            }
                        }
                    });
                    modalInstance.result.then(function (userData) {
                        if (userData == "Success") {
                            $scope.reloadTable();
                        }
                        else {
                            $scope.errorMessageModal(userData);
                        }
                    });
                };

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
                            return commerceService.getAllCommerceWorkOrdersFiltered(dataForSend)
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