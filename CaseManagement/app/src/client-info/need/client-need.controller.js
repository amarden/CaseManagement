angular.module("caseManagement")
    .controller("ClientNeedCtrl", function ($scope, $mdDialog, Need) {
        var vm = this;

        vm.ClientNeeds = $scope.client.ClientNeeds;

        vm.needOptions = Need.query();

        vm.closeDialog = function () {
            $mdDialog.hide();
        };

        vm.deactivateNeed = function (need) {
            need.met = true;
        };

        vm.removeNeed = function (id) {
            var index = vm.ClientNeeds.findIndex(function (d) { return d.clientNeedId === id; });
            vm.ClientNeeds.splice(index, 1);
        };

        vm.activateNeed = function (need) {
            need.met = false;
        };

        vm.addNeed = function () {
            vm.ClientNeeds.push({ dateCreated: new Date(), met: false});
        };
    });