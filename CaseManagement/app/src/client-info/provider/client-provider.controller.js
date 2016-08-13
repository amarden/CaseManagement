angular.module("caseManagement")
    .controller("ClientProviderCtrl", function ($scope, $mdDialog, Need, Agency) {
        var vm = this;
        vm.ClientProviders = $scope.client.ClientProviders;
        var needs = $scope.client.ClientNeeds.map(function(d) { return d.needId; });

        Agency.query(function (data) {
            vm.agencies = data;
        });

        Need.query(function (data) {
            vm.needOptions = data.filter(function(d) { 
                return needs.indexOf(d.needId) > -1;
            });
        });

        vm.closeDialog = function () {
            $mdDialog.hide();
        };

        vm.removeProvider = function (index) {
            vm.ClientProviders.splice(index, 1);
        };

        vm.addProvider = function () {
            vm.ClientProviders.push({ dateCreated: new Date()});
        };

    });