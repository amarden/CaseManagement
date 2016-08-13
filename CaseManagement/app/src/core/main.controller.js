angular.module('caseManagement')
    .controller("MainCtrl", function ($state, Login) {
        var vm = this;

        vm.goHome = function () {
            $state.go('directory');
        };
        
        vm.navToAdmin = function () {
            $state.go('administration');
        };

        if (!Login.isAuthenticated()) {
            $state.go("login");
        } else {
            $state.go("directory");
        }
    });