(function () {
    "use strict";

    // Creating a module == [], inside the brackets you specify what modules are included.
    var myApp = angular.module('myApp', ['ngRoute', 'ngMaterial']);

    myApp.config(function ($routeProvider, $locationProvider) {

        $locationProvider.hashPrefix('');

        $routeProvider.when('/', {
            controller: 'aboutController',
            controllerAs: 'vm',
            templateUrl: '/views/about2.html'
        });

        $routeProvider.when('/about', {
            controller: 'aboutController',
            controllerAs: 'vm',
            templateUrl: '/views/about.html'
        });

        $routeProvider.otherwise({ redirectTo: '/' });
    });
        /*
        .value('localeSupported', ['en-US', 'fi-FI',])
        .value('localeFallbacks', { 'en': 'en-US', 'fi': 'fi-FI' })
        .value('localeConf', getLocaleConf());
        */
})();