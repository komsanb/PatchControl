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
                   controller: 'PermissionController'
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
               when('/staffProfile/:id', {
                   templateUrl: 'views/staffProfile.html',
                   controller: 'staffController'
               }).
               when('/edit_staff_role/:id', {
                   templateUrl: 'views/edit_staff_role.html',
                   controller: 'PermissionController'
               }).
               when('/edit_staff_profile/:id', {
                   templateUrl: 'views/edit_staff_profile.html',
                   controller: 'staffController'
               }).
               when('/change_password', {
                   templateUrl: 'views/change_password.html',
                   controller: 'staffController'
               }).
               when('/editProfile', {
                   templateUrl: 'views/editProfile.html',
                   controller: 'staffController'
               }).
              otherwise({
                  redirectTo: '/index.html'
              });
       }]);

//app.factory("editstaff", function ($http) {

//    var urlBase = "api/staff/staffall";
//    var dataFactory = [];

//    dataFactory.getStaff = function (id) {
//        return $http.get(urlBase + '/' + id)
//    };
//    return dataFactory;
//});

app.controller("staffController", function ($scope, $http, $routeParams) {

    $http.get("api/staff/staffall").success(function (data) {
    
        $scope.staff = data;
    });
    
    $scope.EDstaff = function (id) {
        window.location = "#/edit_staff_profile/" + id;    
    };

    $scope.profile = function (id) {
        window.location = "#/staffProfile/" + id;
    };

    $scope.getstaff = function () {
    $http.get("api/staff/staffall/" + $routeParams.id).success(function (data) {

        $scope.staffonly = data;
    });
    };

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
            window.alert("Add staff successful!");
            window.location = "#/staff"
            window.location.reload(true);
    }

    $scope.editstaff = function () {
        console.log();

        var staff = {
            "StaffID": $scope.staffonly.StaffID,
            "StaffCode": $scope.staffonly.StaffCode,
            "StaffRoleID": $scope.staffonly.StaffRoleID,
            "GenderID": $scope.staffonly.GenderID,
            "StaffFirstname": $scope.staffonly.StaffFirstname,
            "StaffLastname": $scope.staffonly.StaffLastname,
            "Address1": $scope.staffonly.Address1,
            "Address2": $scope.staffonly.Address2,
            "City": $scope.staffonly.City,
            "ProvinceID": $scope.staffonly.ProvinceID,
            "Zipcode": $scope.staffonly.Zipcode,
            "Telephone": $scope.staffonly.Telephone,
            "Mobile": $scope.staffonly.Mobile,
            "Email": $scope.staffonly.Email
        };

        console.log(staff);
        $http.post("api/staff/staffedit", staff).success(function (data, header, status, config) {

            $scope.staff = data;
            console.log(data);
        });
        window.location = "#/staff"
        window.location.reload(true);
    }

    $scope.deletestaff = function (id) {
        console.log(id);

        var staff = {
            "StaffID": id,
            "Deleted": 1
        };

        console.log(staff);
        $http.post("api/staff/staffdelete", staff).success(function (data, header, status, config) {

            $scope.staff = data;
            console.log(data);
        });
        window.location.reload(true);
    }
});


app.controller("PermissionController", function ($scope, $http, $routeParams) {

    $http.get("api/staff/staffrole").success(function (data) {

        $scope.staffrole = data;

    });

    $scope.EDpromission = function (id) {
        window.location = "#/edit_staff_role/" + id;
    };

    $scope.getstaffrole = function () {
        $http.get("api/staff/staffroleaccess/" + $routeParams.id).success(function (data) {
            $scope.staffroleonly = data[0];

            console.log($scope.staffroleonly);
        });
    };

    $scope.permissions = [
    { id: 2, text: 'Add StaffRole' },
    { id: 3, text: 'Edit StaffRole' },
    { id: 4, text: 'Delete StaffRole' }
    ];

    $scope.staffs = [
    { id: 6, text: 'Add Staff' },
    { id: 7, text: 'Edit Staff' },
    { id: 8, text: 'Delete Staff' },
    { id: 9, text: 'View Profile' }
    ];

    $scope.files = [
    { id: 11, text: 'Download File' },
    { id: 13, text: 'Upload File' },
    { id: 15, text: 'Edit File' },
    { id: 16, text: 'Delete File' }
    ];

    $scope.permissions2 = [
    
    ];

    // toggle selection for a given staffrole by id
    $scope.toggleSelection = function toggleSelection(PermisstionID) {
   
        var idx = $scope.permissions2.indexOf(PermisstionID);

        // is currently selected
        if (idx > -1) {
            $scope.permissions2.splice(idx, 1);
        }

            // is newly selected
        else {
            $scope.permissions2.push(PermisstionID);
        }
    };
 
    $scope.addstaffrole = function () {
   
        var staffaccess = {
            "StaffRoleName": $scope.StaffRoleName,
            "PermissionItemID": $scope.permissions2
        };

        //var parameters = { 'staffaccess': { 'StaffRoleName': $scope.StaffRoleName }, 'permissionItemdata': $scope.permissions2 };
        //var parameters = {'StaffRoleName': $scope.StaffRoleName , 'permissionItemdata': $scope.permissions2 };
        console.log(staffaccess);
        $http.post("api/staff/staffaccess", staffaccess).success(function (data, header, status, config) {

            $scope.staffaccess = data;
            console.log(data);
        });
        window.alert("Add staffrole successful!");
        window.location = "#/permission_staff"
        window.location.reload(true);
       
    }

    $scope.editstaffrole = function () {

        var staffaccess = {
            "StaffRoleID": $scope.staffroleonly.StaffRoleID,
            "StaffRoleName": $scope.staffroleonly.StaffRoleName,
            "PermissionItemID": $scope.staffroleonly.PermissionItemID
        };

        console.log(staffaccess);
        $http.post("api/staff/staffaccessedit", staffaccess).success(function (data, header, status, config) {

            $scope.staffaccess = data;
            console.log(data);
        });
        window.alert("Add staffrole successful!");
        window.location = "#/permission_staff"
        window.location.reload(true);

    }

    $scope.deletestaffrole = function (id) {

        var staffaccess = {
            "StaffRoleID": id,
            "Deleted": 1
        };

        console.log(staffaccess);
        $http.post("api/staff/staffroledelete", staffaccess).success(function (data, header, status, config) {

            $scope.staffaccess = data;
            console.log(data);
        });
        window.alert("Add staffrole successful!");
        window.location = "#/permission_staff"
        window.location.reload(true);

    }
 
});


