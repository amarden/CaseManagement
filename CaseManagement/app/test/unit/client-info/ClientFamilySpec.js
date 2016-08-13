describe('Client Family Controller', function () {
    var famCtrl, scope, mockFamilyMembers, $mdDialog;

    beforeEach(function () {
        mockFamilyMembers = [
            {
                name: "Aaron",
                livesWith: true,
                age: null
            },
            {
                name: "Jessica",
                age: 17,
                livesWith: false
            }
            ,
            {
                name: "Jenna",
                age: 27,
                livesWith: true
            }
        ];
        module('caseManagement');

        inject(function ($controller, $rootScope, $injector) {
            scope = $rootScope.$new();
            scope.client = {};
            scope.client.ClientFamilies = mockFamilyMembers;
            famCtrl = $controller('ClientFamilyCtrl', { $scope: scope });
            $mdDialog = $injector.get('$mdDialog');
        });

        spyOn($mdDialog, 'hide').and.callThrough();
    });

    it("should close dialog when closed is called", function () {
        famCtrl.closeDialog();
        expect($mdDialog.hide).toHaveBeenCalled();
    });

    it('equal whatever is in scope.client.ClientFamilies', function () {
        expect(famCtrl.ClientFamilies).toBe(mockFamilyMembers);
    });

    it('should add to ClientFamilies when add function is called', function () {
        expect(famCtrl.ClientFamilies.length).toBe(3);
        famCtrl.addFamily();
        expect(famCtrl.ClientFamilies.length).toBe(4);
    });

    it('should remove ClientFamily when removed', function () {
        expect(famCtrl.ClientFamilies.length).toBe(3);
        famCtrl.removeFamily(1);
        expect(famCtrl.ClientFamilies.length).toBe(2);
    });

    it('should remove the right one', function () {
        var exist = famCtrl.ClientFamilies.filter(function (d) { return d.age === 17; });
        expect(exist.length).toBe(1);
        famCtrl.removeFamily(1);
        exist = famCtrl.ClientFamilies.filter(function (d) { return d.age === 17; });
        expect(exist.length).toBe(0);
    });
});