(function () {
    
    angular.module('appModule',
        [
            //Providers
            'ui.bootstrap',
            'ngSanitize',
            'ngStorage',
            
            'homeModule',
            'accountModule',
           
        ]).config(
            [
                '$locationProvider',
                function ($locationProvider) {
                    $locationProvider.html5Mode({
                        enabled: true,
                        requireBase: false
                    });

                }
            ]);


})();
