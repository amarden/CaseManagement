angular.module('caseManagement')
    .controller('ClientVisitDirCtrl', function ($scope, $mdDialog, $state) {
        var vm = this;

        vm.ClientVisits = $scope.client.ClientVisits.sort(function(a, b) {
            return new Date(b.dateVisit) - new Date(a.dateVisit);
        });

        vm.closeDialog = function () {
            $mdDialog.hide();
        };

        vm.addVisit = function () {
            $state.go("createClientVisit", { id: $scope.client.clientId });
        };

        vm.navToVisit = function (id) {
            $state.go('clientVisit', { 'id': id });
        };
    });