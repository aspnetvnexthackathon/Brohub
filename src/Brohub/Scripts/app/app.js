/// <reference path="../angular/angular.js" />
/// <reference path="../angular/angular-route.js" />
/// <reference path="../jquery-2.1.1.js" />
var Brohub = (function (angular, $) {
    var brohub = angular.module('Brohub', ['ngRoute']);

    brohub.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

        // Configure routes to have #! before the path.
        //$locationProvider.hasPrefix('!');

        // Configure the routes
        $routeProvider
        .when('/Home',
        {
            templateUrl: 'Views/Home/Index.html',
            controller: 'HomeController',
            controllerAs: 'homeCtrl'
        })
        .otherwise({
            redirectTo: '/Home'
        });
    }]);

    brohub.service('RepositoryAnalyzer', ['$http', function ($http) {
        this.analyzeRepository = function (cloneUrl) {
            return $http({ 
                method: 'POST', 
                url: '/api/repository',
                data: cloneUrl
            });
        };

        this.getRepositoryBadges = function(repositoryId)
        {
            return $http({
                    method: 'GET',
                    url: '/api/badges/' + repositoryId
                });
        };

        this.getRankingForBadge = function (repositoryId, category) {
            return $http({
                method: 'GET',
                url: '/api/badges/' + repositoryId + '/category/' + category
            });
        };

        this.getBadgesForUser = function (repositoryId, userId) {
            return $http({
                method: 'GET',
                url: 'api/badges/' + repositoryId + '/user/' + userId
            });
        };
    }]);

    brohub.controller('HomeController', ['RepositoryAnalyzer', function (repositoryAnalyzer) {
        var self = this;
        repositoryAnalyzer.analyzeRepository('"https://git01.codeplex.com/aspnetwebstack.git"')
        .success(function (data) {
            self.bro = data;
        });
    }]);

})(angular, jQuery);