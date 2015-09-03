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
                   controller: 'staffController'
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

app.controller("staffController", function ($scope, $http) {

    $http.get("api/staff/staffall").success(function (data) {
    
        $scope.staff = data;

    });

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
            $http.post("api/staff/staffall", staff).success(function (data, header, status, config) {

                $scope.staff = data;

            });               
        window.location = "#/staff"
        window.location.reload(true);
    }
    
    //$scope.cancel = function () {
    //    $scope.editing = false;
    //    $scope.data = $scope.copy;
    //}
    //$http.get("api/staff/province", { params: { StaffsID: $scope.StaffsID } }).success(function (data) {
    //    alert("Deleted Successfully!!");
    //    cleardetails();
    //    selectStudentDetails('', '');
    //})
    //  .error(function () {
    //      $scope.error = "An Error has occured while loading posts!";
    //  });       
});

app.controller('patchInfoController', function ($scope, $http) {
    $http.get('api/patchs/PatchInformations')
    .success(function (response) {
        $scope.patchs = response;
    })
});

