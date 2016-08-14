angular.module('caseManagement')
    .controller("AgencyCtrl", function ($scope) {
        var vm = this;
        vm.filtered = [];

        vm.__findAgencyById = function (id) {
            var agency = $scope.agencies.filter(function (d) {
                return d.agencyId === id;
            })[0];

            return agency ? agency.name : "";
        };

        vm.__findAgencyByName = function (name) {
            var agency = $scope.agencies.filter(function (d) {
                return d.name === name;
            })[0];

            return agency ? agency.agencyId : null;
        };

        vm.getMatches = function (text) {
            var agencies = $scope.agencies.filter(function (d) {
                return d.name.toLowerCase().indexOf(text.toLowerCase()) > -1;
            });
            vm.filtered = agencies;
        };

        vm.setAgency = function (name, id) {
            $scope.ref.agencyId = id;
            $scope.ref.agencyName = name;
        };

        $scope.$watch("vm.searchText", function () {
            var id = vm.__findAgencyByName(vm.searchText);
            if (!id) {
                $scope.ref.agencyName = vm.searchText;
                $scope.ref.agencyId = null;
            }
        });

        vm.searchText = vm.__findAgencyById($scope.ref.agencyId);
        $scope.ref.agencyName = vm.searchText;
    });