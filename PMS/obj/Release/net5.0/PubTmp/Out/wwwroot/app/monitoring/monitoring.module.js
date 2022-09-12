(function () {
    angular.module('monitoringModule',
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
                    $urlRouterProvider.otherwise("/Monitoring");

                    $stateProvider
                        .state('monitoring',
                            {
                                url: "/Monitoring",      //!!!default state for configuration, url name must match with url name in /Shared/_Layout.cshtml
                                templateUrl: "/app/monitoring/templates/monitoring.html",
                                controller: 'monitoringController'

                            })

                }
            ]).run(['$state', function ($state) {                   //routing page initial load not working solution -> .run() 
                $state.go('monitoring');
            }]);
})();