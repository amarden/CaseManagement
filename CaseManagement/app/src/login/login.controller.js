angular.module("caseManagement")
    .controller("LoginCtrl", function (Login, $mdDialog) {
        var vm = this;
        vm.credentials = {};
        vm.credentials.grant_type = "password";
        vm.loading = false;

        vm.submit = function () {
            vm.loading = true;
            Login.login(vm.credentials).then(function (message) {
                vm.errorMessage = message;
            });
        };

        vm.openRegistration = function (ev) {
            $mdDialog.show({
                controller: "RegisterCtrl",
                controllerAs: 'rc',
                templateUrl: 'login/register.dialog.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                usefullscreen: true
            });
        };
    });