angular.module("traineeApp")
    .controller("createCtrl", ($scope, createUrl, apiSvc) => {
        $scope.traineeCreate = {};
        $scope.insert = f => {
            apiSvc.post(createUrl, {
                TraineeName: $scope.traineeCreate.TraineeName,
                AdmitDate: $scope.traineeCreate.AdmitDate,
                Email: $scope.traineeCreate.Email,
                Picture: $scope.traineeCreate.Picture.base64,
                ImageType: $scope.traineeCreate.Picture.filetype
            })
                .then(r => {
                    $scope.model.trainees.push(r.data);
                    $scope.msg = 'Successfully save to data.';
                    f.$setPristine();
                    f.$setUntouched();
                    $scope.traineeCreate = {};
                }, err => {
                    console.log(err);
                    $scope.msgErr = 'Failed to save data';
                })
        }
    });