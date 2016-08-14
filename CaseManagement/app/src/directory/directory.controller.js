angular.module('caseManagement')
    .controller('DirectoryCtrl', function (Client, $scope, $state) {
        $scope.mc.title = "Directory";
        $scope.mc.showAdmin = true;
        var vm = this;
        var followUps= [];

        vm.filtered = [];
        var clients = Client.resource.query();
        Client.resource.getFollowUps(function (data) {
            followUps = data;
            vm.status = "Upcoming";
            vm.subsetFollowUps();
        });

        vm.subsetFollowUps = function () {
            if (vm.status === "Overdue") {
                vm.followUps = followUps.filter(function (d) { return +new Date() > +new Date(d.dateFollowUp); });
            } else if (vm.status === "Upcoming") {
                vm.followUps = followUps.filter(function (d) { return +new Date() < +new Date(d.dateFollowUp); });
            }
        };

        vm.nav = function (id) {
            $state.go('client', { 'id': id });
        };

        vm.navVisit = function (id) {
            $state.go('clientVisit', { 'id': id });
        };

        vm.getMatches = function (text) {
            var filtered = clients.filter(function (d) {
                return d.firstName.toLowerCase().indexOf(text.toLowerCase()) > -1;
            });
            vm.filtered = filtered.sort();
        };

        vm.createClient = function () {
            $state.go('createClient');
        };
    });