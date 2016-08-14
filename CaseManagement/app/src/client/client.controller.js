angular.module('caseManagement')
    .controller('CreateClientCtrl', function ($scope, $state, Client, DropDown, $mdSidenav, $mdToast) {
        var vm = this;
        vm.disabledSave = false; // Used to disable button after save button is click to prevent double clicking.
        $scope.mc.title = "Create Client";
        $scope.mc.showAdmin = false;

        //Client Model Set up
        vm.client = {};
        vm.client.ClientCommunities = [];
        vm.client.ClientFamilies = [];
        vm.client.ClientNeeds = [];
        vm.client.ClientVisits = [];

        vm.clientExists = function () {
            return false; 
        };

        vm.client.intakeDate = new Date();

        vm.dropDown = DropDown(["race", "ethnicity", "language", "housingStatus", "translation", "gender", "learnAbout", "need"]);

        vm.toggleRight = function () {
            $mdSidenav("right")
                .toggle();
        };

        vm.save = function () {
            vm.disabledSave = true;
            var responseMessage;
            Client.resource.save(vm.client, function (data) {
                $scope.clientForm.$dirty = false;
                responseMessage = 'Changes were succesfully changed';
                $state.go('client', { id: data.clientId });
            }, function () {
                responseMessage = 'There was an error saving your changes';
            }).$promise.finally(function () {
                $mdToast.show(
                 $mdToast.simple()
                  .textContent(responseMessage)
                  .position('top right')
                  .hideDelay(1500)

                );
                vm.disabledSave = false;
            });
        };

        $scope.$on("$stateChangeStart", function (event) {
            if ($scope.clientForm.$dirty) {
                var go = confirm("You are trying to leave a page with unsaved changes, you will discard these changes if you hit okay, to back and save these changes hit cancel");
                if (!go) {
                    event.preventDefault();
                } else {
                    $scope.clientForm.$dirty = false;
                }
            }
        });
    });