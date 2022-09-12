(function () {
    angular.module('accountModule',
        [
            'ngTable',
            //Providers
            'ui.bootstrap',
            'ui.router',         
        ]).config(
            [
                '$stateProvider', '$urlRouterProvider', '$urlMatcherFactoryProvider',
                function ($stateProvider, $urlRouterProvider, $urlMatcherFactoryProvider) {
                    $urlRouterProvider.otherwise("/Account");

                    $stateProvider
                        .state('Account',
                            {
                                url: "/Account",
                                templateUrl: "/app/account/templates/account.html",
                                controller: 'accountController'
                                
                            })
                        //.state('Home',
                        //    {
                        //        url: "/Home",
                        //        templateUrl: "/app/home/templates/home.html",
                        //        controller: 'homeController'

                        //    })
                }
            ]);
})();
