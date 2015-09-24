var app = angular.module('myApp', ['ngRoute', 'ngSanitize', 'summernote', 'ngAnimate', 'ui.bootstrap', 'ngDropzone'])

app.config(['$routeProvider', '$locationProvider',
       function ($routeProvider, $locationProvider) {
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
                   controller: 'uploadController'
               }).
               when('/showUploads', {
                   templateUrl: 'views/showUploads.html',
                   controller: 'MyPatchController'
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
               when('/change_password/:id', {
                   templateUrl: 'views/change_password.html',
                   controller: 'staffController'
               }).
               when('/editProfile/:id', {
                   templateUrl: 'views/editProfile.html',
                   controller: 'staffController'
               }).
               when('/editPatch/:patchID', {
                   templateUrl: 'views/editPatch.html',
                   controller: 'MyPatchController'
               }).
               when('/login', {
                   templateUrl: 'login.html',
               }).
              otherwise({
                  redirectTo: '/login'
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

app.directive('passwordConfirm', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        scope: {
            matchTarget: '=',
        },
        require: 'ngModel',
        link: function link(scope, elem, attrs, ctrl) {
            var validator = function (value) {
                ctrl.$setValidity('match', value === scope.matchTarget);
                return value;
            }

            ctrl.$parsers.unshift(validator);
            ctrl.$formatters.push(validator);

            // This is to force validator when the original password gets changed
            scope.$watch('matchTarget', function (newval, oldval) {
                validator(ctrl.$viewValue);
            });
        }
    };
}]);

// controller staffController
app.controller("staffController", function ($scope, $http, $routeParams) {

    $scope.password = null;
    $scope.passwordConfirmation = null;

    $http.get("api/staff/staffall").success(function (data) {

        $scope.staff = data;
    });

    $scope.EDstaff = function (id) {
        window.location = "#/edit_staff_profile/" + id;
    };

    $scope.profile = function (id) {
        window.location = "#/staffProfile/" + id;
    };

    $scope.EDstaffprofile = function (id) {
        window.location = "#/editProfile/" + id;
    };

    $scope.EDpassword = function (id) {
        window.location = "#/change_password/" + id;
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
        //swal("Good job!", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem erat, tincidunt vitae ipsum et, pellentesque maximus enim. Mauris eleifend ex semper, lobortis purus sed, pharetra felis", "success")
        window.alert("Add staff successful!");
        window.location = "#/staff"
        window.location.reload(true);
    }

    $scope.editstaff = function () {

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

    $scope.editstaffprofile = function () {

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
        window.location = "#/staffProfile/" + $scope.staffonly.StaffID;
        window.location.reload(true);
    }

    $scope.editpasswordstaff = function (id, password) {
        console.log(id);
        console.log(password);
        var staff = {
            "StaffID": id,
            "StaffPassword": password
        };

        console.log(staff);
        $http.post("api/staff/editpasswordstaff", staff).success(function (data, header, status, config) {

            $scope.staff = data;
        });
        window.location = "#/staffProfile/" + id;
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

            var staff = new Array();
            staff = $scope.staffroleonly.PermissionItemID.split(',').map(Number);
            $scope.permissionrole = staff;
            console.log($scope.permissionrole);
            
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
        console.log($scope.permissions2)
        var staffaccess = {
            "StaffRoleName": $scope.StaffRoleName,
            "PermissionItemID": $scope.permissions2,
            
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

     //toggle selection for a given staffrole by id
    $scope.toggleEditSelection = function toggleEditSelection(PermisstionID) {

        var idx = $scope.permissionrole.indexOf(PermisstionID);

        // is currently selected
        if (idx > -1) {
            $scope.permissionrole.splice(idx, 1);
        }

            // is newly selected
        else {
            $scope.permissionrole.push(PermisstionID);
        }
    };

    $scope.editstaffrole = function () {

        var staffaccess = {
            "StaffRoleID": $scope.staffroleonly.StaffRoleID,
            "StaffRoleName": $scope.staffroleonly.StaffRoleName,
            "PermissionItemID": $scope.permissionrole
        };

        console.log(staffaccess);
        $http.post("api/staff/staffaccessedit", staffaccess).success(function (data, header, status, config) {

            $scope.staffaccess = data;
            console.log(data);
        });
        window.alert("Update staffrole successful!");
        window.location = "#/permission_staff"
        window.location.reload(true);

    }
});

app.controller("PermissionGroupController", function ($scope, $http, $routeParams) {

    $http.get("api/staff/permissiongroup").success(function (data) {

        $scope.permissiongroup = data;
        console.log($scope.permissiongroup);
        
    });

   
});

app.controller("LoginController", function ($scope, $location, $http, $routeParams) {

    $scope.submit = function (username, password) {

    $http.get("api/staff/staffall").success(function (data) {

        $scope.staff = data;
        //console.log($scope.staff)

        var json = $scope.staff;
        console.log(json);

        for (var i = 0; i < json.length; i++) {
            Username = json[i].StaffCode;
            Password = json[i].StaffPassword;

            console.log(Username)
            console.log(Password)
            var user = username;
            var pass = password;

            if (user == Username && pass == Password) {

                parent.location = 'index.html';
            }        
        }   
    });
    };
});

//--------------------------------------------- PATCH ------------------------------------------------//

app.controller("patchInfoController", function ($scope, $http, $filter) {

    $http.get('api/patchs/PatchInformations')
    .success(function (response) {
        $scope.patchs = response;
    });
    $http.get('api/patchs/softwareversion')
    .success(function (response) {
        $scope.softwareVersion = response;
    });
    $http.get('api/patchs/softwareType')
    .success(function (response) {
        $scope.softwareType = response;
    });
});

//-------------------------------- Upload Patch Informations ----------------------------------
app.controller('uploadController', function ($scope, $modal, $log, $http, $rootScope, $filter) {

    $http.get('api/patchs/softwareversion')
    .success(function (response) {
        $scope.softwareVersion = response;
    });
    $http.get('api/patchs/softwareType')
    .success(function (response) {
        $scope.softwareType = response;
    });

    $scope.addPatchInfos = function () {
        //console.log(ck.getData());
        var date = $filter('date')(new Date(), 'yyyy-MM-d HH:mm:ss');
        var stringHTML = $('#summernote').code();
        var newPatchInfos = {
            "patchsName": $scope.patchsName,
            "patchsDescription": stringHTML,
            "patchsInsertDate": date,
            "patchsInsertBy": 'Admin',
            "softwareVersionID": $scope.softwareVersionID,
            "patchsVersionNumber": $scope.patchsVersionNumber,
            "softwareTypeID": $scope.softwareTypeID
        }

        console.log(newPatchInfos)
        $http.post('api/patchs/PatchInformations', newPatchInfos)
            .success(function (items) {
                $scope.patchInfos = items
                alert('Upload Successfull!!');
                $rootScope.open();
            })

        //window.location = '#/showUploads';
    };
});

//---------------------------------- Dropzone to Upload Files ------------------------------------------

app.controller('FileUploadCtrl', ['$scope', '$http', function (scope, $http, $rootScope, $modalInstance) {
    //============== DRAG & DROP =============
    // source for drag&drop: http://www.webappers.com/2011/09/28/drag-drop-file-upload-with-html5-javascript/
    var dropbox = document.getElementById("dropbox")
    scope.dropText = 'Drop files here...'

    // init event handlers
    function dragEnterLeave(evt) {
        evt.stopPropagation()
        evt.preventDefault()
        scope.$apply(function () {
            scope.dropText = 'Drop files here...'
            scope.dropClass = ''
        })
    }
    dropbox.addEventListener("dragenter", dragEnterLeave, false)
    dropbox.addEventListener("dragleave", dragEnterLeave, false)
    dropbox.addEventListener("dragover", function (evt) {
        evt.stopPropagation()
        evt.preventDefault()
        var clazz = 'not-available'
        var ok = evt.dataTransfer && evt.dataTransfer.types && evt.dataTransfer.types.indexOf('Files') >= 0
        scope.$apply(function () {
            scope.dropText = ok ? 'Drop files here...' : 'Only files are allowed!'
            scope.dropClass = ok ? 'over' : 'not-available'
        })
    }, false)
    dropbox.addEventListener("drop", function (evt) {
        console.log('drop evt:', JSON.parse(JSON.stringify(evt.dataTransfer)))
        evt.stopPropagation()
        evt.preventDefault()
        scope.$apply(function () {
            scope.dropText = 'Drop files here...'
            scope.dropClass = ''
        })
        var files = evt.dataTransfer.files
        if (files.length > 0) {
            scope.$apply(function () {
                scope.files = []
                for (var i = 0; i < files.length; i++) {
                    scope.files.push(files[i])
                }
            })
        }
    }, false)
    //============== DRAG & DROP =============

    scope.setFiles = function (element) {
        scope.$apply(function (scope) {
            console.log('files:', element.files);
            // Turn the FileList object into an Array
            scope.files = []
            for (var i = 0; i < element.files.length; i++) {
                scope.files.push(element.files[i])
            }
            scope.progressVisible = false
        });
    };

    scope.uploadFile = function () {
        var fd = new FormData()
        for (var i in scope.files) {
            fd.append("uploadedFile", scope.files[i])
        }
        $http.post('api/patchs/UploadFiles', fd)
            .success(function () {
                scope.ok();
                window.location = '#/showUploads';
            })
    }

    function uploadProgress(evt) {
        scope.$apply(function () {
            if (evt.lengthComputable) {
                scope.progress = Math.round(evt.loaded * 100 / evt.total)
            } else {
                scope.progress = 'unable to compute'
            }
        })
    }

    function uploadComplete(evt) {
        /* This event is raised when the server send back a response */
        alert(evt.target.responseText)
    }

    function uploadFailed(evt) {
        alert("There was an error attempting to upload the file.")
    }

    function uploadCanceled(evt) {
        scope.$apply(function () {
            scope.progressVisible = false
        })
        alert("The upload has been canceled by the user or the browser dropped the connection.")
    }
}]);


//--------------------------------------- Alert Upload --------------------------------------------------

app.controller('ModalDemoCtrl', function ($scope, $modal, $log, $rootScope) {

    $scope.items = ['item1', 'item2', 'item3'];

    $scope.animationsEnabled = true;

    $rootScope.open = function (size) {

        var modalInstance = $modal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            size: size,
            resolve: {
                items: function () {
                    return $scope.items;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            $scope.selected = selectedItem;
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.toggleAnimation = function () {
        $scope.animationsEnabled = !$scope.animationsEnabled;
    };

});

// Please note that $modalInstance represents a modal window (instance) dependency.
// It is not the same as the $modal service used above.

app.controller('ModalInstanceCtrl', function ($scope, $modalInstance, items) {

    $scope.items = items;
    $scope.selected = {
        item: $scope.items[0]
    };

    $scope.ok = function () {
        $modalInstance.close($scope.selected.item);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});

app.controller('MyPatchController', ['$http', '$scope', '$routeParams', '$filter',
    function ($http, $scope, $routeParams, $filter) {
        $http.get('api/patchs/softwareversion')
        .success(function (response) {
            $scope.softwareVersion = response;
        });
        $http.get('api/patchs/softwareType')
        .success(function (response) {
            $scope.softwareType = response;
        });
        $http.get('api/patchs/MyPatch')
        .success(function (response) {
            $scope.myPatch = response;
        });
        $scope.editPatchDisplay = function () {
            $http.get('api/patchs/MyPatchDetails/' + $routeParams.patchID)
                .success(function (response) {
                    $scope.editSuccess = response;
                    console.log($scope.editSuccess)
                });
        }
        $scope.editPatch = function (patchID) {
            window.location = '#/editPatch/' + patchID;
        }

        $scope.updatePatchInformations = function () {
            var date = $filter('date')(new Date(), 'yyyy-MM-d HH:mm:ss');
            var stringHTML = $('#summernote').code();
            var update = {
                "patchsID": $scope.editSuccess.patchsID,
                "patchsName": $scope.editSuccess.patchsName,
                "patchsDescription": stringHTML,
                "patchsUpdateDate": date,
                "patchsUpdateBy": 'Mo',
                "softwareVersionID": $scope.editSuccess.softwareVersionID,
                "patchsVersionNumber": $scope.editSuccess.patchsVersionNumber,
                "softwareTypeID": $scope.editSuccess.softwareTypeID
            }
            console.log(update);
            $http.post('api/patchs/UpdateMyPatch', update)
                .success(function () {
                    window.location = '#/showUploads';
                })
        }

        $scope.deletedMyPatch = function (patchID) {
            //$http.post('api/patchs/DeletedMyPatch/' + patchID, patchID)
            //        .success(function () {
            //            window.location.reload(true);
            //        })
            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this patch!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, delete it!",
                cancelButtonText: "No, cancel plx!",
                closeOnConfirm: false,
                closeOnCancel: false,
            }, function (isConfirm) {
                if (isConfirm) {
                    swal("Deleted!",
                        "Your patch has been deleted. And, auto update in 3 minutes.",
                        "success");
                    $http.post('api/patchs/DeletedMyPatch/' + patchID, patchID)
                        .success(function () {
                            setTimeout(function () {
                                window.location.reload(true);
                            }, 3000);
                        })
                } else {
                    swal("Cancelled", "Your patch is safe :)", "error");
                }
            });
        }
    }]);


//app.controller('uploadController', ['$scope','$http', 'fileUpload', function ($scope, $http, fileUpload) {
//    $http.get('api/patchs/PatchInformations')
//        .success(function (response) {
//            $scope.patchs = response;
//            $scope.patchDes = response.patchsDescription;
//        });
//    $http.get('api/patchs/softwareversion')
//    .success(function (response) {
//        $scope.softwareVersion = response;
//    });
//    $http.get('api/patchs/softwareType')
//    .success(function (response) {
//        $scope.softwareType = response;
//    });

//    $scope.uploadFile = function () {
//        var file = $scope.myFile;
//        console.log('file is ');
//        console.dir(file);
//        var uploadUrl = "api/patchs/PatchInformations";
//        fileUpload.uploadFileToUrl(file, uploadUrl);
//    };

//}])
//.directive('fileModel', ['$parse', function ($parse) {
//    return {
//        restrict: 'A',
//        link: function (scope, element, attrs) {
//            var model = $parse(attrs.fileModel);
//            var modelSetter = model.assign;

//            element.bind('change', function () {
//                scope.$apply(function () {
//                    modelSetter(scope, element[0].files[0]);
//                });
//            });
//        }
//    };
//}])
//.service('fileUpload', ['$http', function ($http) {
//    this.uploadFileToUrl = function (file, uploadUrl) {
//        var fd = new FormData();
//        fd.append('file', file);
//        $http.post(uploadUrl, fd, {
//            transformRequest: angular.identity,
//            headers: { 'Content-Type': undefined }
//        })
//        .success(function () {
//        })
//        .error(function () {
//        });
//    }
//}])
//.directive('progressBar', [
//        function () {
//            return {
//                link: function ($scope, el, attrs) {
//                    $scope.$watch(attrs.progressBar, function (newValue) {
//                        el.css('width', newValue + '%');
//                    });
//                }
//            };
//        }
//]);
