var app = angular.module('myApp', ['ngRoute']);

app.config(['$routeProvider',
       function ($routeProvider) {
           $routeProvider.
              when('/test', {
                  templateUrl: 'views/test.html',
                //  controller: 'AddStudentController'
              }).
              when('/viewStudents', {
                  templateUrl: 'viewStudents.htm',
                  controller: 'ViewStudentsController'
              }).
              otherwise({
                  redirectTo: '/index.html'
              });
       }]);