angular.module("caseManagement")
    .controller("ClientReferralCtrl", function ($scope, $mdDialog, $state) {
        var vm = this;
        vm.ClientReferrals = $scope.client.ClientVisitReferrals;
        //var needs = $scope.client.ClientNeeds.map(function(d) { return d.needId; });

        vm.closeDialog = function () {
            $mdDialog.hide();
        };

        vm.navToVisit = function (id) {
            $state.go('clientVisit', { 'id': id });
        };
    });