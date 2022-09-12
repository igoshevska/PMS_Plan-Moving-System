(function () {
    angular.module('homeModule',
        [
            'ngTable',
            //Providers
            'ui.bootstrap',
            'ui.router',         
        ]).config(
            [
                '$stateProvider', '$urlRouterProvider', '$urlMatcherFactoryProvider',
                function ($stateProvider, $urlRouterProvider, $urlMatcherFactoryProvider) {
                    $urlRouterProvider.otherwise("/Home");

                    $stateProvider
                        .state('Home',
                            {
                                url: "/Home",
                                templateUrl: "/app/home/templates/home.html",
                                controller: 'homeController'
                                
                            })
                        .state('priceProposalDetails',
                            {
                                url: "/ProposalDetails/:id",
                                templateUrl: "/app/home/templates/priceProposalDetails.html",
                                controller: 'priceProposalDetailsController',
                            })
                }
            ]);
})();
