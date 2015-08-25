var app = angular.module('myApp', ['ngRoute']);

app.config(['$routeProvider',
       function ($routeProvider) {
           $routeProvider.
              //when('/test', {
              //    templateUrl: 'views/test.html',
              //  //  controller: 'AddStudentController'
              //}).
              when('/permission_staff', {
                  templateUrl: 'views/permission_staff.html',
                 // controller: 'ViewStudentsController'
              }).
               when('/staff', {
                   templateUrl: 'views/staff.html',
                   // controller: 'ViewStudentsController'
               }).
               when('/add_permission', {
                   templateUrl: 'views/add_permission.html',
                   // controller: 'ViewStudentsController'
               }).
               when('/add_staff', {
                   templateUrl: 'views/add_staff.html',
                   // controller: 'ViewStudentsController'
               }).
              otherwise({
                  redirectTo: '/index.html'
              });
       }]);