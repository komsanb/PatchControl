//--------------------------------------------- PATCH ------------------------------------------------//

app.controller("patchInfoController", function ($scope, $http, $filter, $routeParams) {

    $http.get('api/patchs/PatchInformations')
    .success(function (response) {
        $scope.patchs = response;
        console.log($scope.patchs);
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
        var date = $filter('date')(new Date(), 'yyyy-MM-d HH:mm:ss');//get current date
        var stringSetHTML = $('#summernote').code(); //get code from summernote
        $scope.staffFirstName = localStorage.getItem('StaffFirstName');
        $scope.staffId= localStorage.getItem('StaffID');
        var newPatchInfos = {
            "staffID": $scope.staffId,
            "patchsName": $scope.patchsName,
            "patchsDescription": stringSetHTML,
            "patchsInsertDate": date,
            "patchsUpdateDate": date,
            "patchsInsertBy": $scope.staffFirstName,
            "softwareVersionID": $scope.softwareVersionID,
            "patchsVersionNumber": $scope.patchsVersionNumber,
            "softwareTypeID": $scope.softwareTypeID
        }

        console.log(newPatchInfos)
        swal({
            title: "Success!",
            text: "Patch informations has been safe",
            type: "success",
            closeOnConfirm: true,
        }, function () {
            $http.post('api/patchs/PatchInformations', newPatchInfos)
                 .success(function (items) {
                     $scope.patchInfos = items
                     $scope.patchsName = "";
                     $scope.softwareVersionID = "";
                     $scope.patchsVersionNumber = "";
                     $scope.softwareTypeID = "";
                 });
            $rootScope.open('lg');
        });
    };
});

//---------------------------------- Dropzone to Upload Files ------------------------------------------

app.controller('MyCtrl', ['$scope', 'FileUploader', function($scope, FileUploader) {
    var uploader = $scope.uploader = new FileUploader({
        url: ''
    });

    // FILTERS

    uploader.filters.push({
        name: 'customFilter',
        fn: function(item /*{File|FileLikeObject}*/, options) {
            return this.queue.length < 10;
        }
    });

    // CALLBACKS

    uploader.onWhenAddingFileFailed = function(item /*{File|FileLikeObject}*/, filter, options) {
        console.info('onWhenAddingFileFailed', item, filter, options);
    };
    uploader.onAfterAddingFile = function(fileItem) {
        console.info('onAfterAddingFile', fileItem);
    };
    uploader.onAfterAddingAll = function(addedFileItems) {
        console.info('onAfterAddingAll', addedFileItems);
    };
    uploader.onBeforeUploadItem = function(item) {
        console.info('onBeforeUploadItem', item);
    };
    uploader.onProgressItem = function(fileItem, progress) {
        console.info('onProgressItem', fileItem, progress);
    };
    uploader.onProgressAll = function(progress) {
        console.info('onProgressAll', progress);
    };
    uploader.onSuccessItem = function(fileItem, response, status, headers) {
        console.info('onSuccessItem', fileItem, response, status, headers);
    };
    uploader.onErrorItem = function(fileItem, response, status, headers) {
        console.info('onErrorItem', fileItem, response, status, headers);
    };
    uploader.onCancelItem = function(fileItem, response, status, headers) {
        console.info('onCancelItem', fileItem, response, status, headers);
    };
    uploader.onCompleteItem = function(fileItem, response, status, headers) {
        console.info('onCompleteItem', fileItem, response, status, headers);
    };
    uploader.onCompleteAll = function() {
        console.info('onCompleteAll');
    };

    console.info('uploader', uploader);
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
        var staffID = localStorage.getItem('StaffID');
        $http.get('api/patchs/MyPatch/' + staffID)
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
            $scope.staffFirstName = localStorage.getItem('StaffFirstName');
            var stringHTML = $('#summernote').code();
            var update = {
                "patchsID": $scope.editSuccess.patchsID,
                "patchsName": $scope.editSuccess.patchsName,
                "patchsDescription": stringHTML,
                "patchsUpdateDate": date,
                "patchsUpdateBy": $scope.staffFirstName,
                "softwareVersionID": $scope.editSuccess.softwareVersionID,
                "patchsVersionNumber": $scope.editSuccess.patchsVersionNumber,
                "softwareTypeID": $scope.editSuccess.softwareTypeID
            }
            console.log(update);
            

            $http.post('api/patchs/UpdateMyPatch', update)
                    .success(function () {
                        swal({
                            title: "Updated!",
                            text: "Informations has been updated",
                            type: "success",
                            showConfirmButton: true
                        }, function (isConfirm) {
                            if (isConfirm)
                                window.location = '#/showUploads';
                        });                        
                    })                
            }

        $scope.deletedMyPatch = function (patchID) {
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
                            }, 2500);
                        })
                } else {
                    swal("Cancelled", "Your patch is safe :)", "error");
                }
            });
        }
    }]);
