angular.module('caseManagement')
    .directive('agency', function () {
        return {
            restrict: 'E',
            templateUrl: 'client-visit/agency/agency.partial.html',
            controller: 'AgencyCtrl',
            controllerAs: 'ac',
            scope: {
                ref: '=',
                agencies: '='
            }
        };
    });