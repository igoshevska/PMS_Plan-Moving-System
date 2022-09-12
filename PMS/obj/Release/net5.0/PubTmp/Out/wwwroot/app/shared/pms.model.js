'use strict'

angular.module('appModule').factory("pmsModels",
    [
        
        function () {
            var model = this;
            //User models
            model.User = function () {
                this.id = 0;
                this.userName = '';
                this.name = '';
                this.surname = '';
                this.password = '';
                this.workerType = null;
                this.email = '';
                this.userIsActive = false;
                this.role = null;
            };
            //Machine models
            model.Machine = function () {
                this.id = 0;
                this.name = '';
                this.location = null;
                this.machineIpAddress = '';
                this.tabletIpAddress = '';
                this.isActive = false;
            };
            //Work-order models
            model.WorkOrder = function () {
                this.id = 0;
                this.name = '';
                this.dateFrom = null;
                this.dateTo = null;
            };

            model.WorkOrderState = function () {
                this.id = 0;
                this.name = '';
                this.objectType = null;
            };

            model.Priorities = function () {
                this.id = 0;
                this.name = '';
            };

            //Sub-work orders models
            model.SubWorkOrder = function () {
                this.id = 0;
                this.name = '';
                this.quantity = 0;
                this.note = '';
                this.status = null;
                this.commerceWorkOrderId = null;
                this.date = null;
                this.haveEngineerWorkOrder = false;
            };

            model.Status = function () {
                this.id = 0;
                this.name = '';
                this.objectType = null;
            };

            //Shifts
            model.Shift = function () {
                this.id = 0;
                this.name = '';
                this.startShiftTime = null;
                this.endShiftTime = null;
                this.shiftBreakTimeDuration = 0;
            };

            //Work-order engineer/// da se dodadat zavrshnite operacii u modelot i da se linkuvaat 
            model.WorkOrderEngineer = function () {
                this.id = 0;
                this.quantity = 0;
                this.size = 0;
                this.product = null;
                this.workerType = null;
                this.commerceSubWorkOrder = null;
                this.serialNumber = '';
                this.subPhasesStates = [
                    {
                        id : 0,
                        isActive : false,
                        engineerWorkOrderID : 0,
                        subPhase : null
                    },
                    {
                        id: 0,
                        isActive: false,
                        engineerWorkOrderID: 0,
                        subPhase: null
                    },
                    {
                        id: 0,
                        isActive: false,
                        engineerWorkOrderID: 0,
                        subPhase: null
                    },
                    {
                        id: 0,
                        isActive: false,
                        engineerWorkOrderID: 0,
                        subPhase: null
                    },
                    {
                        id: 0,
                        isActive: false,
                        engineerWorkOrderID: 0,
                        subPhase: null
                    },
                    {
                        id: 0,
                        isActive: false,
                        engineerWorkOrderID: 0,
                        subPhase: null
                    },
                    {
                        id: 0,
                        isActive: false,
                        engineerWorkOrderID: 0,
                        subPhase: null
                    },
                    {
                        id: 0,
                        isActive: false,
                        engineerWorkOrderID: 0,
                        subPhase: null
                    }
                ];
            };

            model.SubPhasesStates = function () {
                this.id = 0;
                this.isActive = false;
                this.engineerWorkOrderID = 0;
                this.subPhase = null;
            };

            //for testing
            model.ProductType = function () {
                this.id = 0;
                this.name = '';
            };
            model.ProductName = function () {
                this.id = 0;
                this.name = '';
            };
            model.WorkerType = function () {
                this.id = 0;
                this.name = '';
                this.description = '';
            };


            return model;

        }

    ]);