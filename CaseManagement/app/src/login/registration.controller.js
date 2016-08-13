angular.module('caseManagement')
    .controller("RegisterCtrl", function (Login, $mdDialog) {
        var vm = this;

        vm.register = function () {
            Login.register(vm.userInfo).then(function (message) {
                console.log(message);
                if (message === 'success') {
                    vm.closeDialog();
                }
                vm.errorMessage = message;
            });
        };

        vm.closeDialog = function () {
            $mdDialog.hide();
        };
    });