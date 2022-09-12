(function () {
    angular.module('dashboardModule',
        [
            'ngTable',
            //Providers
            'ui.bootstrap',
            'ui.router',
            'ui.select',
            'nvd3',
        ]).config(
            [
                '$stateProvider', '$urlRouterProvider',
                function ($stateProvider, $urlRouterProvider) {
                    $urlRouterProvider.otherwise("/Dashboard");

                    $stateProvider
                        .state('dashboard',
                            {
                                url: "/Dashboard",
                                templateUrl: "/app/dashboard/templates/dashboard.html",
                                controller: 'dashboardController'
                            })
                }
            ]);
})();
