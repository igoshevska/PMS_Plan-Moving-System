(function () {
    angular.module('homeModule',
        [
            'ngTable',
            //Providers
            'ui.bootstrap',
            'ui.router',         
        ]).config(
            [
                '$stateProvider', '$urlRouterProvider',
                function ($stateProvider, $urlRouterProvider) {
                    $urlRouterProvider.otherwise("/Home");

                    $stateProvider
                        .state('Home',
                            {
                                url: "/Home",
                                templateUrl: "/app/home/templates/home.html",
                                controller: 'homeController'
                                
                            })
                }
            ]);
})();
