(function () {
    angular.module('employeesModule',
        [
            'ngTable',
            //Providers
            'ui.bootstrap',
            'ui.router',
            'ui.select',
            'ng-ip-address',
        ]).config(
            [
                '$stateProvider', '$urlRouterProvider',
                function ($stateProvider, $urlRouterProvider) {
                    $urlRouterProvider.otherwise("/Employees");

                    $stateProvider
                        .state('employees',
                            {
                                url: "/Employees",      //!!!default state for configuration, url name must match with url name in /Shared/_Layout.cshtml
                                templateUrl: "/app/employees/templates/employees.html",
                                controller: 'employeesController'

                            })

                }
            ]).run(['$state', function ($state) {                   //routing page initial load not working solution -> .run() 
                $state.go('employees');
            }]);
})();