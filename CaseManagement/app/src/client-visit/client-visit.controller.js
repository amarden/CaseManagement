angular.module('caseManagement')
    .controller("ClientVisitCtrl", function (Client, ClientVisit, DropDown,
                                             $scope, $state, $stateParams, $mdSidenav, $mdToast, $q) {
        var vm = this;
        $scope.mc.showAdmin = false;
        vm.disabledSave = false; // Used to disable button after save button is click to prevent double clicking.

        vm.ClientVisit = {};
        vm.ClientVisit.ClientVisitReferrals = [];

        vm.dropDown = DropDown(["visitResult", "responder", "contactType", "referralType", "agency"]);

        Client.resource.get({ id: $stateParams.id }, function (client) {
            vm.ClientVisit.clientId = client.clientId;
            vm.Client = client;
            //console.log(vm.Client);
            var name = vm.Client.firstName + " " + vm.Client.lastName + " - Contact";
            $scope.mc.title = name;
        });

        vm.addReferral = function () {
            vm.ClientVisit.ClientVisitReferrals.push({});
        };

        vm.removeReferral = function (i) {
            vm.ClientVisit.ClientVisitReferrals.splice(i, 1);
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
            vm.disabledSave = true;
            var c = Client.resource.update({id: vm.Client.clientId }, vm.Client);
            var v = ClientVisit.resource.save(vm.ClientVisit);
            var message;
            $q.all([c.$promise, v.$promise])
                .then(function (data) {
                    message = "Changes were succesfully changed";
                    $scope.clientVisitForm.$dirty = false;
                    $state.go('clientVisit', { id: data[1].clientVisitId });
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
                    vm.disabledSave = false;
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