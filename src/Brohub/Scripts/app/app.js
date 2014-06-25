/// <reference path="../angular/angular.js" />
/// <reference path="../angular/angular-route.js" />
/// <reference path="../jquery-2.1.1.js" />
var Brohub = (function (angular, $) {
    var brohub = angular.module('Brohub', ['ngRoute']);

    brohub.config(['$routeProvider', function ($routeProvider) {
        $routeProvider
        .when('/',
        {
            templateUrl: 'Views/Home/Index.html',
            controller: 'HomeController',
            controllerAs: 'homeCtrl'
        })
        .otherwise({
            redirectTo: '/'
        })
    }]);

    brohub.controller('HomeController', function () {
        this.bro = 'Bro from Home';
    });

})(angular, jQuery);