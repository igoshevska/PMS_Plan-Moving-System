(function () {
    angular.module('configurationModule',
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
                    $urlRouterProvider.otherwise("/Configuration");

                    $stateProvider
                        .state('users',
                            {
                                url: "/Configuration",      //!!!default state for configuration, url name must match with url name in /Shared/_Layout.cshtml
                                templateUrl: "/app/configuration/templates/users.html",
                                controller: 'userController'

                            })
                        .state('machines',
                            {
                                url: "/machines",
                                templateUrl: "/app/configuration/templates/machines.html",
                                controller: 'machineController'

                            })
                        .state('shifts',
                            {
                                url: "/shifts",
                                templateUrl: "/app/configuration/templates/shifts.html",
                                controller: 'shiftController'

                            })
                }
            ]).run(['$state', function ($state) {                   //routing page initial load not working solution -> .run() 
                $state.go('users');
            }]);
})();
