var app = angular.module('myApp', ['ngRoute']);

app.config(['$routeProvider',
       function ($routeProvider) {
           $routeProvider.
               when('/', {
                   templateUrl: 'views/home.html',
                   controller: 'homeController'
               }).
               when('/permission_staff', {
                   templateUrl: 'views/permission_staff.html',
                   controller: 'addstaffController'
               }).
               when('/staff', {
                   templateUrl: 'views/staff.html',
                   controller: 'addstaffController'
               }).
               when('/patchs', {
                   templateUrl: 'views/patchs.html',
                   // controller: 'ViewStudentsController'
               }).
               when('/upload', {
                   templateUrl: 'views/upload.html',
                   // controller: 'ViewStudentsController'
               }).
               when('/showUploads', {
                   templateUrl: 'views/showUploads.html',
                   // controller: 'ViewStudentsController'
               }).
               when('/add_staff', {
                   templateUrl: 'views/add_staff.html',
                   controller: 'addstaffController'
               }).
               when('/add_permission', {
                   templateUrl: 'views/add_permission.html',
                   controller: 'addstaffController'
               }).
               when('/staffProfile', {
                   templateUrl: 'views/staffProfile.html',
                   // controller: 'ViewStudentsController'
               }).
               when('/edit_staff_role', {
                   templateUrl: 'views/edit_staff_role.html',
                   // controller: 'ViewStudentsController'
               }).
               when('/edit_staff_profile', {
                   templateUrl: 'views/edit_staff_profile.html',
                   // controller: 'ViewStudentsController'
               }).
               when('/change_password', {
                   templateUrl: 'views/change_password.html',
                   // controller: 'ViewStudentsController'
               }).
               when('/editProfile', {
                   templateUrl: 'views/editProfile.html',
                   // controller: 'ViewStudentsController'
               }).
              otherwise({
                  redirectTo: '/index.html'
              });
       }]);

app.controller("homeController", function ($scope,$http) {

    $http.get("api/staff/staffall").success(function(data) {

        $scope.staff = data;

    });

});

app.controller("addstaffController", function ($scope, $http) {

    $http.get("api/staff/staffall").success(function (data) {
    
        $scope.staff = data;

    });

    $http.get("api/staff/staffrole").success(function (data) {

        $scope.staffrole = data;

    });

});

app.controller('patchInfoController', function ($scope, $http) {
    $http.get('api/patchs/PatchInformations')
    .success(function (response) {
        $scope.patchs = response;
    })
});
