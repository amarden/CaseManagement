describe('Create Client Controller', function () {
    var clientCtrl, scope, httpBackend, mdSidenav, toast, $mdToast;

    beforeEach(function () {
        module('caseManagement');

        mdSidenav = {};
        mdSidenav.toggle = jasmine.createSpy();
        module(function ($provide) {
            $provide.factory('$mdSidenav', function () {
                return function () {
                    return mdSidenav;
                };
            });
        });

        inject(function ($controller, $rootScope, $injector) {
            scope = $rootScope.$new();
            scope.mc = {};
            scope.clientForm = {};
            clientCtrl = $controller('CreateClientCtrl', { $scope: scope });
            $injector.get('DropDown');
            httpBackend = $injector.get('$httpBackend');
            $mdToast = $injector.get('$mdToast');
            toast = $mdToast.simple();
        });

        spyOn(scope, '$on').and.callThrough();

        spyOn($mdToast, "show").and.callFake(function () {
            return toast;
        });

        testHelper.mockHttp(httpBackend);
    });

    it('toggles side nav on toggleRight', function () {
        clientCtrl.toggleRight();
        expect(mdSidenav.toggle).toHaveBeenCalled();
    });

    it('fills the drop down menus', function () {
        httpBackend.flush();
        expect(clientCtrl.dropDown.race.length).toBeGreaterThan(1);
        expect(clientCtrl.dropDown.ethnicity.length).toBeGreaterThan(1);
        expect(clientCtrl.dropDown.language.length).toBeGreaterThan(1);
        expect(clientCtrl.dropDown.housingStatus.length).toBeGreaterThan(1);
        expect(clientCtrl.dropDown.translation.length).toBeGreaterThan(1);
        expect(clientCtrl.dropDown.learnAbout.length).toBeGreaterThan(1);
        expect(clientCtrl.dropDown.need.length).toBeGreaterThan(1);
        expect(clientCtrl.dropDown.gender.length).toBeGreaterThan(1);
        expect(clientCtrl.dropDown.nope).toBeUndefined();
    });

    it("returns false for clientExist", function () {
        expect(clientCtrl.clientExists()).toBe(false);
    });

    it("starts with empty Collections", function () {
        expect(clientCtrl.client.ClientCommunities.length).toBe(0);
        expect(clientCtrl.client.ClientFamilies.length).toBe(0);
        expect(clientCtrl.client.ClientNeeds.length).toBe(0);
        expect(clientCtrl.client.ClientProviders.length).toBe(0);
        expect(clientCtrl.client.ClientVisits.length).toBe(0);
    });

    it("has a title of Create Client", function () {
        expect(scope.mc.title).toBe("Create Client");
    });

    it("should assign current date to intakeDate", function () {
        var today = new Date().toDateString();
        expect(clientCtrl.client.intakeDate.toDateString()).toBe(today);
    });

    it("should set form to not dirty when successful save and call toast", function () {
        httpBackend.when("POST", "/api/Clients")
          .respond(200);
        scope.clientForm.$dirty = true;
        clientCtrl.save();
        httpBackend.flush();
        expect(scope.clientForm.$dirty).toBe(false);
        expect($mdToast.show).toHaveBeenCalled();
    });

    it("should NOT set form to not dirty when successful save and call toast", function () {
        httpBackend.when("POST", "/api/Clients")
          .respond(500);
        scope.clientForm.$dirty = true;
        clientCtrl.save();
        httpBackend.flush();
        expect(scope.clientForm.$dirty).toBe(true);
        expect($mdToast.show).toHaveBeenCalled();
    });

    it("should test stateChangeStart", function () {
        console.log("test nav away confirmation");
    });

    it("should disable save button after click and re-enable after return from server", function () {
        expect(clientCtrl.disabledSave).toBe(false);
        httpBackend.when("POST", "/api/Clients")
          .respond(200);
        clientCtrl.save();
        expect(clientCtrl.disabledSave).toBe(true);
        httpBackend.flush();
        expect(scope.clientForm.$dirty).toBe(false);
    });
});
