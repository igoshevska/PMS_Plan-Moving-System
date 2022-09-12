(function () {
    
    angular.module('appModule',
        [
            //Providers
            'ui.bootstrap',
            'ui.router',
            'ui.select',
            'ngAnimate',
            'ngRoute',
            'ngSanitize',
            'ngStorage',
            'ui.bootstrap.datetimepicker',
            'angular-click-outside',
            'angularFileUpload',
            


            //Modules
            'homeModule',
            'commerceModule',
            'configurationModule',
            'dashboardModule',
            'employeesModule',
            'monitoringModule',

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
