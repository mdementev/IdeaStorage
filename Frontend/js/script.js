// script.js

var ideaStorage = angular.module('ideaStorage', ['ui.router']);

ideaStorage.config(function($stateProvider, $httpProvider) {
	$stateProvider
    	.state('dashboard', {
      		url: '/dashboard',
      		templateUrl: 'pages/dashboard.html',
			controller: 'dashboardController'
    	})
    	.state('index', {
      		url: '',
      		templateUrl: 'pages/signin.html',
			controller: 'loginController'
    	})
		.state('newnote', {
			url: '/dashboard/newnote',
			templateUrl: 'pages/newnote.html',
			controller: 'newnoteController'
		})
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

					localStorage.setItem("Email", $('#inputEmail').val());
					localStorage.setItem("Password", $('#inputPassword').val());
					getUser();
					$state.go('dashboard');
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

deleteNote = function(sender, id){

	$(sender).parent().parent().parent().remove();

	var dt = {
		"Credentials": {
			"Email": localStorage.getItem("Email"),
			"Password": localStorage.getItem("Password")
		},
		"NodeId": id
	};
	$.ajax({
		type: "POST",
		url: "http://o142:88/IdeaStorage/DeleteNode",
		data: dt,
		//dataType: "json",
		success: function(data, textStatus, jqxhr) {
			if (jqxhr.status == 200) {
				localStorage.setItem('userData', JSON.stringify(data));
			}
		},
		error: function(jqxhr, textStatus, errorThrown){
			if (jqxhr.status == 401) {
				$('#alert').show()
			}
		}

	});
};

ideaStorage.controller('dashboardController', function($scope, $http, $window, $state) {
	var retrievedObject = localStorage.getItem('userData');
	var obj = JSON.parse(retrievedObject);
	$scope.fname = obj.FirstName;
	$scope.sname = obj.SecondName;
	$scope.nodes = obj.Nodes;

	$scope.searchTerm = function () {
		var dt = {
			"Credentials": {
				"Email": localStorage.getItem("Email"),
				"Password": localStorage.getItem("Password")
			},
			"SearchTerm": $("#searchInput").val()
		};
		$.ajax({
			type: "POST",
			url: "http://o142:88/IdeaStorage/FindNodeByTag",
			data: dt,
			//dataType: "json",
			success: function (data, textStatus, jqxhr) {
				if (jqxhr.status == 200) {
					localStorage.setItem('userData', JSON.stringify(data));
					$state.go('dashboard');
				}
			},
			error: function (jqxhr, textStatus, errorThrown) {
				if (jqxhr.status == 401) {
					$('#alert').show()
				}
			}

		});
	};

	var $nodeContainer = $('#nodeContainer');
	$scope.nodes.forEach(function (node) {

		var pDefault = document.createElement("div");
		$(pDefault).addClass("panel panel-default");

		var pHeading = document.createElement("div");
		$(pHeading).addClass("panel-heading ");
		$(pDefault).append(pHeading);

		var pTitle = document.createElement("h3");
		$(pTitle).addClass("panel-title");
		$(pTitle).text(node.Title);
		$(pTitle).append('<a id="deleteNote" type="submit" onclick="deleteNote(this, ' + node.NodeId + ')"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></a>');
		$(pTitle).append('<a id="editNote" ng-click="editNote()"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></a>');
		$(pHeading).append(pTitle);

		var pBody = document.createElement("div");
		$(pBody).addClass("panel-body");
		$(pBody).html(node.Text);
		$(pBody).append("<br>");

		node.Tags.forEach(function (tag) {
			var pTag = document.createElement("span");
			$(pTag).addClass("tag label label-primary");
			$(pTag).text(tag.Name);
			$(pBody).append(pTag);
		});

		$(pDefault).append(pBody);

		$nodeContainer.append(pDefault);


	});
});

ideaStorage.controller('newnoteController', function($scope, $http, $window, $state) {
	var retrievedObject = localStorage.getItem('userData');
	var obj = JSON.parse(retrievedObject);
	$scope.fname = obj.FirstName;
	$scope.sname = obj.SecondName;

	$scope.createNote = function(){
		var tags = new Array();

		$("#tags").val().split(',').forEach(function(name){
			tags.push({"Name": name});
		});

		var memberfilter = new Array();
		memberfilter[0] = "Name";
		var dt = {
			"Credentials": {
				"Email": localStorage.getItem("Email"),
				"Password": localStorage.getItem("Password")
			},
			"Node":{
				"Title": $("#title").val(),
				"Text": $("#description").val(),
				"Tags": tags
			}
			};
		$.ajax({
			type: "POST",
			url: "http://o142:88/IdeaStorage/CreateNode",
			data: dt,
			//dataType: "json",
			success: function(data, textStatus, jqxhr) {
				if (jqxhr.status == 200) {
					localStorage.setItem('userData', JSON.stringify(data));
					$state.go('dashboard');
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
	var user = {"Email": localStorage.getItem("Email"), "Password": localStorage.getItem("Password")};
	$.ajax({
		type: "POST",
		url: "http://o142:88/IdeaStorage/GetLoggedInUser",
		data: user,
		//dataType: "json",
		success: function(data, textStatus, jqxhr) {
			if (jqxhr.status == 200) {
				localStorage.setItem('userData', JSON.stringify(data));
			}
		},
		error: function(jqxhr, textStatus, errorThrown){
			if (jqxhr.status == 401) {
				$('#alert').show()
			}
		}

	});
}

function search(sender){
	var term = $(sender).val();
	$('.label-primary').not(':contains("' + term + '")').parent().parent().hide();
	$('.label-primary:contains("' + term + '")').parent().parent().show();
}

function logout(sender){
}