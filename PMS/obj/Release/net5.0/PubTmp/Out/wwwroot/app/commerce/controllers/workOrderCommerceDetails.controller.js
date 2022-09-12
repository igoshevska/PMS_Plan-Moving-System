(function () {

    angular.module('commerceModule').controller('workOrderCommerceDetailsController',
        [
            '$scope', '$rootScope', 'commerceService', '$uibModal', 'pmsModels', 'NgTableParams', '$stateParams', '$state',
            function ($scope, $rootScope, commerceService, $uibModal, pmsModels, NgTableParams, $stateParams, $state) {

                $scope.workOrderId = 0;
                
                if ($stateParams.id) {
                    $scope.workOrderId = $stateParams.id;
                }
                
                $scope.workOrderModel = new pmsModels.WorkOrder();

                $scope.getCommerceWorkOrderById = function (workOrderId) {
                    commerceService.getCommerceWorkOrderById(workOrderId)
                        .then(function (result) {
                            $scope.workOrderModel = result;
                        });
                }

                $scope.getSubWorkOrdersFiltered = function () {
                    $scope.subWorkOrdersDataTable = new NgTableParams({
                        page: 1,
                        count: 10
                    }, {
                        counts: [10, 20, 30, 50],
                        total: 0,
                        getData: function (params) {
                            var dataForSend = {
                                page: params.page(),
                                rows: params.count(),
                                searchtext: '',
                                mainCommerceWorkOrderID: $scope.workOrderId
                            };
                            return commerceService.getAllCommerceSubWorkOrdersFiltered(dataForSend)
                                .then(function (result) {
                                    var data = result.items;
                                    params.total(result.totalItems);
                                    $scope.totalRows = result.totalItems;
                                    $scope.allSubWorkOrders = data;
                                    return data;
                                });
                        }
                    });
                };

                $scope.subPhases = null;
                $scope.getAllSubPhases = function () {
                    commerceService.getAllSubPhases()
                        .then(function (result) {
                            $scope.subPhases = result;
                        });
                };

                $scope.getAllSubPhases();


                
                $scope.createEditSubWorkOrder = function (subWorkOrder, workOrderId ) {
                    var modalInstance = $uibModal.open({
                        animation: false,
                        scope: $scope,
                        backdrop: 'static',
                        templateUrl: "/app/commerce/templates/subWorkOrderModal.html",
                        controller: 'subWorkOrderModalController',
                        windowClass: 'modal custom-modal-width',
                        size: 'md',
                        resolve: {
                            subWorkOrder: function () {
                                return subWorkOrder;
                            },
                            workOrderId: function () {
                                return workOrderId;
                            }
                        }
                    });
                    modalInstance.result.then(function (subWorkOrderData) {
                        //if (subWorkOrderData.message == "Success") {
                        if (subWorkOrderData == "Success") {
                            $scope.reloadTable();
                        }
                        else {
                            $scope.errorMessageModal(subWorkOrderData);
                        }
                    });
                }

                $scope.createWorkOrderEngineer = function (subWorkOrder) {
                    var modalInstance = $uibModal.open({
                        animation: false,
                        scope: $scope,
                        backdrop: 'static',
                        templateUrl: "/app/commerce/templates/workOrderEngineerModal.html",
                        controller: 'workOrderEngineerModalController',
                        windowClass: 'modal custom-modal-width',
                        size: 'lg',
                        resolve: {
                            subWorkOrder: function () {
                                return subWorkOrder;
                            },
                            subPhases: function () {
                                return $scope.subPhases;
                            },
   
                        }
                    });
                    modalInstance.result.then(function (enginnerWorkOrderData) {
                        if (enginnerWorkOrderData.message == "Success") {
                            engWorkOrderId = enginnerWorkOrderData.id;
                            $scope.openWorkOrderEngineerDetails(engWorkOrderId);
                            $scope.reloadTable();
                        }
                        else {
                            $scope.errorMessageModal(enginnerWorkOrderData);
                        }
                    });
                }

                //proveri ja ovaa funkcija shto treba da prima/prakja
                $scope.openWorkOrderEngineerDetails = function (engWorkOrderId) {
                    $state.go('workOrderEngineerDetails', {
                        id: engWorkOrderId
                    });
                }

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

                $scope.reloadTable = function () {
                    $scope.subWorkOrdersDataTable.reload();
                };

                $scope.cancel = function () {
                    $uibModalInstance.dismiss("Close");
                }

                $scope.getCommerceWorkOrderById($scope.workOrderId);
                $scope.getSubWorkOrdersFiltered();
                

            }]);
}());