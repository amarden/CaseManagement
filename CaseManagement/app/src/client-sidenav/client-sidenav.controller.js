angular.module("caseManagement")
    .controller("ClientSideNavCtrl", function ($scope, $mdDialog, $mdSidenav) {
        var vm = this;
        vm.client = $scope.client;
        vm.toggleRight = function () {
            $mdSidenav("right")
                .toggle();
        };

        vm.openDocuments = function (ev) {
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                controller: "ClientDocumentCtrl",
                controllerAs: 'cd',
                templateUrl: 'dist/client-info/document/client-document.dialog.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                usefullscreen: true,
            });
        };


        vm.openNeeds = function (ev) {
            $mdDialog.show({
                scope: $scope,        
                preserveScope: true,
                controller: "ClientNeedCtrl",
                controllerAs: 'cn',
                templateUrl: 'dist/client-info/need/client-need.dialog.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                usefullscreen: true,
            });
        };

        vm.openReferrals = function (ev) {
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                controller: "ClientReferralCtrl",
                controllerAs: 'cp',
                templateUrl: 'dist/client-info/referral/client-referral.dialog.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                usefullscreen: true,
            });
        };

        vm.openFamily = function (ev) {
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                controller: "ClientFamilyCtrl",
                controllerAs: 'cf',
                templateUrl: 'dist/client-info/family/client-family.dialog.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                usefullscreen: true,
            });
        };

        vm.openContacts = function (ev) {
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                controller: "ClientVisitDirCtrl",
                controllerAs: 'cv',
                templateUrl: 'dist/client-info/visit/client-visit.dialog.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                usefullscreen: true,
            });
        };
    });