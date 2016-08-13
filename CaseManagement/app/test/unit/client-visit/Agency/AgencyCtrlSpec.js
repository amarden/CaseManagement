describe('Agency Controller', function () {
    var scope, agencyCtrl;

    var mockAgencies = [
        { "agencyId": 1, "name": "Parenting Journey" },
        { "agencyId": 2, "name": "Watertown Food Pantries" },
        { "agencyId": 3, "name": "HWR Fund" },
        { "agencyId": 4, "name": "Children's Charter" },
        { "agencyId": 5, "name": "$2 Food Bags" }
    ];

    beforeEach(function () {
        module('caseManagement');

        inject(function ($controller, $rootScope) {
            scope = $rootScope.$new();
            scope.agencies = mockAgencies;
            scope.ref = {};
            agencyCtrl = $controller('AgencyCtrl', { $scope: scope });
        });
    });

    it('should find agency by id and return the right name', function () {
        var agencyName = agencyCtrl.__findAgencyById(1);
        expect(agencyName).toBe("Parenting Journey");
    });

    it('should find agency by id and return blank when the id is not in the collection', function () {
        var agencyName = agencyCtrl.__findAgencyById(89789701);
        expect(agencyName).toBe("");
    });

    it('should find agency by name and return the right id', function () {
        var agencyId = agencyCtrl.__findAgencyByName("Parenting Journey");
        expect(agencyId).toBe(1);
    });

    it('should find agency by id and return blank when the id is not in the collection', function () {
        var agencyId = agencyCtrl.__findAgencyByName("Ladi dadadada");
        expect(agencyId).toBe(null);
    });

    it('should get matches based on text type', function () {
        expect(agencyCtrl.filtered.length).toBe(0);
        agencyCtrl.getMatches("a");
        expect(agencyCtrl.filtered.length).toBe(4);
        agencyCtrl.getMatches("w");
        expect(agencyCtrl.filtered.length).toBe(2);
        agencyCtrl.getMatches("wwwww");
        expect(agencyCtrl.filtered.length).toBe(0);
    });

    it('should set ref object\'s id and name to the parameters passed', function () {
        expect(scope.ref).toEqual({ agencyName: "" });
        agencyCtrl.setAgency("Some Agency", 125);
        expect(scope.ref).toEqual({ agencyId: 125, agencyName: "Some Agency" });
    });

    it('should NOT assign ref the id and text when searchText is changed', function () {
        expect(scope.ref).toEqual({ agencyName: "" });
        expect(agencyCtrl.searchText).toBe("");
        agencyCtrl.searchText = "HWR Fund";
        scope.$digest();
        expect(scope.ref).toEqual({ agencyName: "" });
    });

    it('should assign ref text when searchText is changed and cannot find id by name', function () {
        expect(scope.ref).toEqual({ agencyName: "" });
        expect(agencyCtrl.searchText).toBe("");
        agencyCtrl.searchText = "HWR Funds";
        scope.$digest();
        expect(scope.ref).toEqual({ agencyName: "HWR Funds", agencyId: null });
    });

    it('should assign search text to right agency based on agency id that it is set with', function () {
        inject(function ($controller, $rootScope) {
            scope = $rootScope.$new();
            scope.agencies = mockAgencies;
            scope.ref = {};
            scope.ref.agencyId = 4;
            agencyCtrl = $controller('AgencyCtrl', { $scope: scope });
        });
        expect(agencyCtrl.searchText).toBe("Children's Charter");

    });
});