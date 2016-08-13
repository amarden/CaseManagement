angular.module('caseManagement')
    .controller("ViewClientVisitCtrl", function (Client, ClientVisit, DropDown, Need, $window, $mdToast,
                                                 $scope, $state, $stateParams, $mdSidenav, $q, $sanitize) {
        var vm = this;
        $scope.mc.showAdmin = false;

        vm.dropDown = DropDown(["visitResult", "responder", "contactType", "referralType", "agency"]);

        vm.openNotes = function () {
            //var host = $window.location.host;
            //$window.open(host+"/app/dist/print.html#/client/visit/" + vm.ClientVisit.clientVisitId + "/note");
            $state.go('clientVisitNotes', { 'id': vm.ClientVisit.clientVisitId });
        };

        ClientVisit.resource.get({ id: $stateParams.id }, function (data) {
            vm.ClientVisit = data;
            Client.resource.get({ id: vm.ClientVisit.clientId }, function (client) {
                vm.Client = client;
                //console.log(client);
                var name = vm.Client.firstName + " " + vm.Client.lastName + " - " + new Date(vm.ClientVisit.dateVisit).toDateString();
                $scope.mc.title = name;
                var needs = vm.Client.ClientNeeds.map(function (d) { return d.needId; });
                vm.notes = $sanitize(vm.ClientVisit.notes.replace(/\n/g, '<br/>'));
                Need.query(function(data) {
                    vm.dropDown.needOptions = data.filter(function (d) {
                        return needs.indexOf(d.needId) > -1;
                    });
                });
            });
        });

        vm.addReferral = function () {
            vm.ClientVisit.ClientVisitReferrals.push({});
            $scope.clientVisitForm.$dirty = true;
        };

        vm.removeReferral = function (i) {
            vm.ClientVisit.ClientVisitReferrals.splice(i, 1);
            $scope.clientVisitForm.$dirty = true;
        };

        vm.needsFollowUp = function () {
            if (vm.ClientVisit) {
                return vm.ClientVisit.followUp;
            }
        };

        vm.toggleRight = function () {
            $mdSidenav("right")
                .toggle();
        };

        vm.save = function () {
            var c = Client.resource.update({ id: vm.Client.clientId }, vm.Client);
            var v = ClientVisit.resource.update({ id: vm.ClientVisit.clientVisitId }, vm.ClientVisit);
            var message;
            $q.all([c.$promise, v.$promise])
                .then(function () {
                    message = "Changes were succesfully changed";
                    $scope.clientVisitForm.$dirty = false;
                }).catch(function (response) {
                    if (response.status === 400) {
                        message = "Please fill in agency for each referral";
                    } else {
                        message = "There was an error saving your changes";
                    }
                }).finally(function () {
                    $mdToast.show(
                        $mdToast.simple()
                        .textContent(message)
                        .position('top right')
                        .hideDelay(1500)
                    );
                });
        };

        vm.navToClient = function () {
            $state.go("client", { id: vm.Client.clientId });
        };

        $scope.$on("$stateChangeStart", function (event) {
            if ($scope.clientVisitForm.$dirty) {
                if (!confirm("You are trying to leave a page with unsaved changes, you will discard these changes if you hit okay, to back and save these changes hit cancel")) {
                    event.preventDefault();
                } else {
                    $scope.clientVisitForm.$dirty = false;
                }
            }
        });
    });