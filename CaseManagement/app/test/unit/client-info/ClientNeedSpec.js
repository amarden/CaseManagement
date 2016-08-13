describe('Client Need Controller', function () {
    var needCtrl, scope, mockNeeds, $mdDialog, compile, httpBackend;

    beforeEach(function () {
        mockNeeds = [
            {
                clientNeedId: 2,
                clientId: 2131,
                needId: 4,
                met: false
            },
            {
                clientNeedId: 1,
                clientId: 2131,
                needId: 7,
                met: true
            }
            ,
            {
                clientNeedId: 4,
                clientId: 2131,
                needId: 8,
                met: true
            }
        ];
        module('caseManagement');
        module('templates');

        inject(function ($controller, $rootScope, $injector, $compile) {
            scope = $rootScope.$new();
            scope.client = {};
            scope.client.ClientNeeds = mockNeeds;
            needCtrl = $controller('ClientNeedCtrl', { $scope: scope });
            $mdDialog = $injector.get('$mdDialog');
            httpBackend = $injector.get('$httpBackend');
            compile = $compile;
        });

        spyOn($mdDialog, 'hide').and.callThrough();
    });

    it("should close dialog when closed is called", function () {
        needCtrl.closeDialog();
        expect($mdDialog.hide).toHaveBeenCalled();
    });

    it('equal whatever is in scope.client.ClientNeeds', function () {
        expect(needCtrl.ClientNeeds).toBe(mockNeeds);
    });

    it('should add to ClientNeeds when add function is called', function () {
        expect(needCtrl.ClientNeeds.length).toBe(3);
        needCtrl.addNeed();
        expect(needCtrl.ClientNeeds.length).toBe(4);
    });


    it('should delete to ClientNeeds when add function is called', function () {
        expect(needCtrl.ClientNeeds.length).toBe(3);
        needCtrl.removeNeed(1);
        expect(needCtrl.ClientNeeds.length).toBe(2);
        expect(needCtrl.ClientNeeds[1].needId).toBe(8);
    });


    it('should deactivate need', function () {
        expect(needCtrl.ClientNeeds.filter(function(d) { return d.met === false; }).length).toBe(1);
        needCtrl.deactivateNeed(needCtrl.ClientNeeds[0]);
        expect(needCtrl.ClientNeeds[0].met).toBeTruthy();
        expect(needCtrl.ClientNeeds.filter(function (d) { return d.met === false; }).length).toBe(0);
    });

    it('should activate need', function () {
        expect(needCtrl.ClientNeeds.filter(function (d) { return d.met === true; }).length).toBe(2);
        needCtrl.activateNeed(needCtrl.ClientNeeds[1]);
        expect(needCtrl.ClientNeeds[1].met).toBeFalsy();
        expect(needCtrl.ClientNeeds.filter(function (d) { return d.met === true; }).length).toBe(1);
    });

    it('should populate list with needs properly', function () {
        httpBackend.when('GET', '/api/needs')
            .respond([]);
        var formElement = "<ul><li ng-repeat='item in client.ClientNeeds'></li></ul>";
        var element = compile(formElement)(scope);
        scope.$digest();
        httpBackend.flush();
        expect(element.find('li').length).toEqual(3);
    });
});