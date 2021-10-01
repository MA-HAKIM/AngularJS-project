angular.module("traineeApp")
    .controller("editCtrl", ($scope, apiSvc, editUrl, $routeParams) => {
        var id = $routeParams.id;
        $scope.traineeEdit = {};
        var existing = $scope.model.trainees.find(a => a.TraineeId == id);
        angular.copy(existing, $scope.traineeEdit);
        $scope.update = f => {
            var obj = {
                TraineeId: $scope.traineeEdit.TraineeId,
                TraineeName: $scope.traineeEdit.TraineeName,
                AdmitDate: $scope.traineeEdit.AdmitDate,
                Email: $scope.traineeEdit.Email,
                Picture: ""
            }
            if ($scope.traineeEdit.Picture.filesize && $scope.traineeEdit.Picture.filesize >= 0) {
                obj.Picture = $scope.traineeEdit.Picture.base64;
                obj.ImageType = $scope.traineeEdit.Picture.filetype;
            }
            apiSvc.put(editUrl + "/" + $scope.traineeEdit.TraineeId, obj)
                .then(r => {
                    var i = $scope.model.trainees.findIndex(a => a.TraineeId == id);
                    $scope.model.trainees[i].TraineeName = r.data.TraineeName;
                    $scope.model.trainees[i].AdmitDate = r.data.AdmitDate;
                    $scope.model.trainees[i].Email = r.data.Email;
                    $scope.model.trainees[i].Picture = r.data.Picture;
                    $scope.msg = 'Successfully modified this data.'
                    f.$setPristine();
                    f.$setUntouched();
                }, err => {
                    $scope.msgErr = 'Failed to modified data.'
                })

        }
    });