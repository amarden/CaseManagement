angular.module("caseManagement")
    .controller("ClientDocumentCtrl", function ($scope, $mdDialog, $window, Upload, FileType, ClientDocument) {
        var vm = this;

        vm.ClientDocuments = $scope.client.ClientDocuments;
        vm.fileTypeOptions= FileType.query();

        var getFiles = function () {
            ClientDocument.resource.get({ id: $scope.client.clientId }, function (data) {
                vm.ClientDocuments = $scope.client.ClientDocuments = data;
                console.log($scope.client.ClientDocuments);
            });
        };

        vm.closeDialog = function () {
            $mdDialog.hide();
        };

        vm.downloadFile = function (doc) {
            $window.open("/Upload/Download?docId=" + doc.clientDocumentId);
        };

        vm.uploadFile = function (doc) {
            Upload.upload({
                url: '/Upload/Create',
                data: { uploadFile: doc.file, fileType: doc.fileTypeId, clientId: $scope.client.clientId }
            }).then(function () {
                doc.success = true;
                getFiles();
            }, function (resp) {
                doc.fail = true;
                console.log('Error status: ' + resp.status);
            }, function (evt) {
                var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
            });
        };

        vm.removeDocument = function (index) {
            if(vm.ClientDocuments[index].clientDocumentId) {
                ClientDocument.resource.remove({ id: vm.ClientDocuments[index].clientDocumentId }, function () {
                    getFiles();
                });
            }
            vm.ClientDocuments.splice(index, 1);
        };

        vm.addDocument = function () {
            vm.ClientDocuments.push({});
        };
    });