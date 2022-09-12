(function () {
    angular.module('commerceModule',
        [
            'ngTable',
            //Providers
            'ui.bootstrap',
            'ui.router',
            'ui.select',
        ]).config(
            [
                '$stateProvider', '$urlRouterProvider', '$urlMatcherFactoryProvider', 
                function ($stateProvider, $urlRouterProvider, $urlMatcherFactoryProvider) {
                    $urlRouterProvider.otherwise("/Commerce");
                    $urlMatcherFactoryProvider.caseInsensitive(true);

                    $stateProvider
                        .state('workOrderCommerce',             
                            {
                                url: "/Commerce",   //!!!default state for commerce, url name must match with url name in /Shared/_Layout.cshtml
                                templateUrl: "/app/commerce/templates/workOrderCommerce.html",
                                controller: 'workOrderCommerceController',
                            })

                        .state('workOrderCommerceDetails',
                            {
                                url: "/workOrderCommerceDetails/:id",
                                templateUrl: "/app/commerce/templates/workOrderCommerceDetails.html",
                                controller: 'workOrderCommerceDetailsController',
                            })
                        .state('subWorkOrder',
                            {
                                url: "/subWorkOrder",
                                templateUrl: "/app/commerce/templates/subWorkOrderModal.html",
                                controller: 'subWorkOrderModalController',

                            })
                        .state('workOrderEngineer',
                            {
                                url: "/workOrderEngineer",
                                templateUrl: "/app/commerce/templates/workOrderEngineer.html",
                                controller: 'workOrderEngineerController',

                            })
                        .state('workOrderEngineerDetails',
                            {
                                url: "/workOrderEngineerDetails/:id",
                                templateUrl: "/app/commerce/templates/workOrderEngineerDetails.html",
                                controller: 'workOrderEngineerDetailsController',

                            })
                }
            ]).run(['$state', function ($state) {                   //routing page initial load not working solution -> .run() 
                $state.go('workOrderCommerce');
            }]);
})();
