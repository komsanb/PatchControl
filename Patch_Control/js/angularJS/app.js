var app = angular.module('myApp', ['ngRoute']);

app.config(['$routeProvider',
       function ($routeProvider) {
           $routeProvider.
              when('/upload', {
                  templateUrl: 'views/upload.html',
                //  controller: 'AddStudentController'
              }).
              when('/showUploads', {
                  templateUrl: 'views/showUploads.html',
                //  controller: 'ViewStudentsController'
              }).
               when('/patchs', {
                   templateUrl: 'views/patchs.html'
               }).
               when('/detailsPatch', {
                   templateUrl: 'views/detailsPatch.html'
               }).
              otherwise({
                  redirectTo: '/index.html'
              });
       }]);