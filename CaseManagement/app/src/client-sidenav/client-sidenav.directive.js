angular.module('caseManagement')
    .directive('clientSideNav', function () {
        return {
            restrict: 'E',
            templateUrl: 'dist/client-sidenav/client-sidenav.html',
            controller: 'ClientSideNavCtrl',
            controllerAs: 'sn',
            scope: {
                client:'='
            }
        };
    });