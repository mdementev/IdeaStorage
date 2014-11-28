// script.js

var ideaStorage = angular.module('ideaStorage', ['ui.router']);

ideaStorage.config(function($stateProvider, $urlRouterProvider) {
	$stateProvider
    	.state('dashboard', {
      		url: '/dashboard',
      		templateUrl: '/pages/dashboard.html'
    	})
    	.state('index', {
      		url: '',
      		templateUrl: '/pages/signin.html'
    	});
});