angular.module('caseManagement')
    .directive('clientSideNav', function () {
        return {
            restrict: 'E',
            templateUrl: 'client-sidenav/client-sidenav.html',
            controller: 'ClientSideNavCtrl',
            controllerAs: 'sn',
            scope: {
                client:'='
            }
        };
    });