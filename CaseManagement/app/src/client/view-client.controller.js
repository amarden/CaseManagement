angular.module('caseManagement')
    .controller('ViewClientCtrl', function ($scope, Client, DropDown,
                                            $state, $mdSidenav, $stateParams, $mdToast, Need) {
        var vm = this;
        $scope.mc.showAdmin = false;

        vm.clientExists = function () {
            return true;
        };

        Client.resource.get({ id: $stateParams.id }, function (client) {
            //console.log(client); 
            vm.client = client;
            $scope.mc.title = vm.client.firstName + " " + vm.client.lastName;
            var needs = vm.client.ClientNeeds.map(function (d) { return d.needId; });
            Need.query(function (data) {
                vm.dropDown.needOptions = data.filter(function (d) {
                    return needs.indexOf(d.needId) > -1;
                });
            });
        });

        vm.dropDown = DropDown(["race", "ethnicity", "language", "housingStatus", "translation", "gender", "learnAbout", "need"]);

        vm.toggleRight = function () {
            $mdSidenav("right")
                .toggle();
        };

        vm.addVisit = function () {
            $state.go('createClientVisit', { id: vm.client.clientId });
        };

        vm.save = function () {
            var responseMessage;
            Client.resource.update({ id: vm.client.clientId }, vm.client, function () {
                $scope.clientForm.$dirty = false;
                responseMessage = 'Changes were succesfully changed';
            }, function () {
                responseMessage = 'There was an error saving your changes';
            }).$promise.finally(function () {
                $mdToast.show(
                  $mdToast.simple()
                    .textContent(responseMessage)
                    .position('top right')
                    .hideDelay(1500)
                );
            });
        };

        $scope.$on("$stateChangeStart", function (event) {
            if ($scope.clientForm.$dirty) {
                if (!confirm("You are trying to leave a page with unsaved changes, you will discard these changes if you hit okay, to back and save these changes hit cancel")) {
                    event.preventDefault();
                } else {
                    $scope.clientForm.$dirty = false;
                }
            }
        });
    });