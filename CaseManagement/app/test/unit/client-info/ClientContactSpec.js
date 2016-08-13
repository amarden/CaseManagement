describe('Client Contact Controller', function () {
    var contactCtrl, scope, mockClientContacts, httpBackend, state, $mdDialog;


    beforeEach(function () {
        mockClientContacts = [
            {
                clientVisitId: 2
            },
            {
                clientVisitId: 3
            }
        ];
        module('caseManagement');

        inject(function ($controller, $rootScope, $injector) {
            scope = $rootScope.$new();
            scope.client = {};
            scope.client.ClientVisits = mockClientContacts;
            contactCtrl = $controller('ClientVisitDirCtrl', { $scope: scope });
            $mdDialog = $injector.get('$mdDialog');
            httpBackend = $injector.get('$httpBackend');
            state = $injector.get('$state');
        });

        spyOn(state, 'go').and.callFake(function (state, params) {
            return { 'state': state, 'params': params };
        });

        spyOn($mdDialog, 'hide').and.callThrough();
    });

    it("should close dialog when closed is called", function () {
        contactCtrl.closeDialog();
        expect($mdDialog.hide).toHaveBeenCalled();
    });

    it('equal whatever is in scope.client.ClientFamilies', function () {
        expect(contactCtrl.ClientVisits).toBe(mockClientContacts);
    });

    it('should navigate to the correct state given parameter', function () {
        httpBackend.when('GET', 'client-visit/client-visit.html')
            .respond(200);
        contactCtrl.navToVisit(3);
        scope.$digest();
        expect(state.go).toHaveBeenCalled();
        expect(state.go).toHaveBeenCalledWith("clientVisit", { id: 3 });
    });
});