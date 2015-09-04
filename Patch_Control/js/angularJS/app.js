var app = angular.module('myApp', ['ngRoute']);

app.config(['$routeProvider',
       function ($routeProvider) {
           $routeProvider.
               when('/', {
                   templateUrl: 'views/home.html',
                   //controller: 'homeController'
               }).
               when('/permission_staff', {
                   templateUrl: 'views/permission_staff.html',
                   controller: 'staffController'
               }).
               when('/staff', {
                   templateUrl: 'views/staff.html',
                   controller: 'staffController'
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
                   controller: 'staffController'
               }).
               when('/add_permission', {
                   templateUrl: 'views/add_permission.html',
                   controller: 'PermissionController'
               }).
               when('/staffProfile', {
                   templateUrl: 'views/staffProfile.html',
                   // controller: 'ViewStudentsController'
               }).
               when('/edit_staff_role/:id', {
                   templateUrl: 'views/edit_staff_role.html',
                   controller: 'staffController'
               }).
               when('/edit_staff_profile/:id', {
                   templateUrl: 'views/edit_staff_profile.html',
                   controller: 'staffController'
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

app.controller("staffController", function ($scope, $http) {

    $http.get("api/staff/staffall").success(function (data) {
    
        $scope.staff = data;

    });

    $scope.go = function (data) {
        window.location = "#/edit_staff_profile/" + data;

    }

    $http.get("api/staff/staffrole").success(function (data) {

        $scope.staffrole = data;

    });

    $http.get("api/staff/province").success(function (data) {

        $scope.province = data;

    });

    $http.get("api/staff/gender").success(function (data) {

        $scope.gender = data;

    });

    $scope.addstaff = function () {
        if ($scope.Address1 == null, $scope.Address2 == null, $scope.City == null, $scope.Zipcode == null, $scope.Telephone == null, $scope.Mobile == null, $scope.Email == null) {
            $scope.Address1 = "";
            $scope.Address2 = "";
            $scope.City = "";
            $scope.Zipcode = "";
            $scope.Telephone = "";
            $scope.Mobile = "";
            $scope.Email = "";

        }      
            var staff = {
                "StaffCode" : $scope.StaffCode,
                "StaffPassword" : $scope.StaffPassword,
                "StaffRoleID" : $scope.StaffRoleID,
                "GenderID" : $scope.GenderID,
                "StaffFirstname" : $scope.StaffFirstname,
                "StaffLastname" : $scope.StaffLastname,
                "Address1" : $scope.Address1,
                "Address2" : $scope.Address2,
                "City" : $scope.City,
                "ProvinceID" : $scope.ProvinceID,
                "Zipcode" : $scope.Zipcode,
                "Telephone" : $scope.Telephone,
                "Mobile" : $scope.Mobile,
                "Email" : $scope.Email
            };
        
            console.log(staff);
            $http.post("api/staff/staffall", staff).success(function (data, header, status, config) {

                $scope.staff = data;

            });               
        window.location = "#/staff"
        window.location.reload(true);
    }

    $scope.editstaff = function () {
        if ($scope.Address1 == null, $scope.Address2 == null, $scope.City == null, $scope.Zipcode == null, $scope.Telephone == null, $scope.Mobile == null, $scope.Email == null) {
            $scope.Address1 = "";
            $scope.Address2 = "";
            $scope.City = "";
            $scope.Zipcode = "";
            $scope.Telephone = "";
            $scope.Mobile = "";
            $scope.Email = "";

        }
        var staff = {
            "StaffID" : 2,
            "StaffCode": $scope.StaffCode,
            "StaffPassword": $scope.StaffPassword,
            "StaffRoleID": $scope.StaffRoleID,
            "GenderID": $scope.GenderID,
            "StaffFirstname": $scope.StaffFirstname,
            "StaffLastname": $scope.StaffLastname,
            "Address1": $scope.Address1,
            "Address2": $scope.Address2,
            "City": $scope.City,
            "ProvinceID": $scope.ProvinceID,
            "Zipcode": $scope.Zipcode,
            "Telephone": $scope.Telephone,
            "Mobile": $scope.Mobile,
            "Email": $scope.Email
        };

        console.log(staff);
        $http.Post("api/staff/staffall", staff).success(function (data, header, status, config) {

            $scope.staff = data;

        });
        window.location = "#/staff"
        window.location.reload(true);
    }
});

app.controller("PermissionController", function ($scope, $http) {
    $scope.addstaffrole = function () {
        
        var staff = {
            "StaffRoleName": $scope.StaffRoleName,
            "PermissionItemsID": $scope.PermissionItemsID
        };

        console.log(staff);
        $http.post("api/staff/staffall", staff).success(function (data, header, status, config) {

            $scope.staff = data;

        });
        window.location = "#/permission_staff"
        window.location.reload(true);
    }

});
