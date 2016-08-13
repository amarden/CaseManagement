describe('Client Provider Controller', function () {
    var clientProvCtrl, scope, mockProviders, $mdDialog, httpBackend, mockNeeds;

    beforeEach(function () {
        mockProviders = [
            {
                something: 2
            },
            {
                something: 1
            }
        ];

        mockNeeds = [
            {needId: 1},
            {needId: 2}
        ];
        module('caseManagement');

        inject(function ($controller, $rootScope, $injector) {
            scope = $rootScope.$new();
            scope.client = {};
            scope.client.ClientNeeds = [];
            scope.client.ClientProviders = mockProviders;
            scope.client.ClientNeeds = mockNeeds;
            clientProvCtrl = $controller('ClientProviderCtrl', { $scope: scope });
            $mdDialog = $injector.get('$mdDialog');
            httpBackend = $injector.get("$httpBackend");
        });

        spyOn($mdDialog, 'hide').and.callThrough();

        testHelper.mockHttp(httpBackend);
    });

    it("should close dialog when closed is called", function () {
        clientProvCtrl.closeDialog();
        expect($mdDialog.hide).toHaveBeenCalled();
    });

    it('should add to ClientProviderts when add function is called', function () {
        expect(clientProvCtrl.ClientProviders.length).toBe(2);
        clientProvCtrl.addProvider();
        expect(clientProvCtrl.ClientProviders.length).toBe(3);
        expect(clientProvCtrl.ClientProviders[2].dateCreated).toBeTruthy();
    });

    it('should remove ClientProviderts when remove function is called', function () {
        expect(clientProvCtrl.ClientProviders.length).toBe(2);
        clientProvCtrl.removeProvider(0);
        expect(clientProvCtrl.ClientProviders.length).toBe(1);
        expect(clientProvCtrl.ClientProviders[0].something).toBe(1);
    });

    it("should query the dropdowns and filter out need optins based on client needs", function () {
        httpBackend.flush();
        expect(clientProvCtrl.agencies.length).toBeGreaterThan(1);
        expect(clientProvCtrl.needOptions.length).toBe(2);
    });
});