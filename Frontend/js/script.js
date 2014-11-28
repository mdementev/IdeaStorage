// script.js

var ideaStorage = angular.module('ideaStorage', ['ui.router']);

ideaStorage.config(function($stateProvider, $httpProvider) {
	$stateProvider
    	.state('dashboard', {
      		url: '/dashboard',
      		templateUrl: 'pages/dashboard.html'
    	})
    	.state('index', {
      		url: '',
      		templateUrl: 'pages/signin.html',
			controller: 'loginController'
    	});

});

ideaStorage.controller('loginController', function($scope, $http, $window, $state) {

	$scope.urlBase = 'http://o142:88/IdeaStorage';
	$scope.submit = function () {
		var user = {"Email": $('#inputEmail').val(), "Password": $('#inputPassword').val()};
		$.ajax({
			type: "POST",
			url: "http://o142:88/IdeaStorage/login",
			data: user,
			//dataType: "json",
			success: function(data, textStatus, jqxhr) {
				if (jqxhr.status == 200) {
					$state.go('dashboard');
					getUser();
				}
			},
			error: function(jqxhr, textStatus, errorThrown){
				if (jqxhr.status == 401) {
					$('#alert').show()
				}
			}

		});
	}
});

function getUser(){
	$.ajax({
		type: "POST",
		url: "http://o142:88/IdeaStorage/GetLoggedInUser",
		success: function(data, textStatus, jqxhr) {
			if (jqxhr.status == 200) {
				alert("Success!");
			}
		},
		error:$('#alert').show()
	});
}