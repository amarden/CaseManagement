describe('Directory Controller', function () {
    var dirCtrl, scope, state, httpBackEnd, Client;
    var mockClients = [
        {
            firstName: "Aaron",
            lastName: "Marden",
            gender: "m"
        },
        {
            firstName: "Jessica",
            lastName: "Marden",
            gender: "f"
        },
        {
            firstName: "Jenna",
            lastName: "Nisasn",
            gender: "f"
        }
    ];

    var mockFollowUps = [

    ];

    beforeEach(function () {
        module('caseManagement');

        module(function ($provide) {
            $provide.service('Client', function () {
                this.resource = {};
                this.resource.query = function () {
                    return mockClients;
                };
                this.resource.getFollowUps = function () {
                    return mockFollowUps;
                };
            });
        });

        inject(function ($controller, $rootScope, $injector) {
            scope = $rootScope.$new();
            scope.mc = {};
            dirCtrl = $controller('DirectoryCtrl', { $scope: scope });
            Client = $injector.get('Client');
            state = $injector.get('$state');
            httpBackEnd = $injector.get('$httpBackend');
        });

        spyOn(state, 'go').and.callFake(function (state, params) {
            return { 'state': state, 'params': params };
        });

        spyOn(Client.resource, 'query').and.callThrough();
    });

    it("title should be directory", function () {
        expect(scope.mc.title).toBe("Directory");
    });

    it('should start with no one in filtered variable', function () {
        expect(dirCtrl.filtered.length).toBe(0);
    });

    it('should start with whatever returns from client.query()', function () {
        expect(Client.resource.query().length).toBe(3);
    });

    it('should return one person with the name aaron', function () {
        dirCtrl.getMatches("Aaron");
        expect(dirCtrl.filtered.length).toBe(1);
    });

    it('should return one person with the an A', function () {
        dirCtrl.getMatches("A");
        expect(dirCtrl.filtered.length).toBe(3);
    });

    it('should navigate to right client page when called', function () {
        httpBackEnd.when('GET', 'client/client.html')
            .respond(200);
        dirCtrl.nav(3);
        scope.$digest();
        expect(state.go).toHaveBeenCalled();
        expect(state.go).toHaveBeenCalledWith("client", { id: 3 });
    });

    it('should return no one with a Z', function () {
        dirCtrl.getMatches("Z");
        expect(dirCtrl.filtered.length).toBe(0);
    });
});