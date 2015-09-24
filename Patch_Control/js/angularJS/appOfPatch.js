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
