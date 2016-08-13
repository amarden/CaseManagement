angular.module("caseManagement")
    .controller("ClientFamilyCtrl", function ($scope, $mdDialog) {
        var vm = this;

        vm.ClientFamilies = $scope.client.ClientFamilies;
        vm.closeDialog = function () {
            $mdDialog.hide();
        };

        vm.removeFamily = function (index) {
            vm.ClientFamilies.splice(index, 1);
        };

        vm.addFamily = function () {
            vm.ClientFamilies.push({dateCreated: new Date()});
        };
    });