describe('Main Controller', function () {
    var mainCtrl, state, httpBackEnd, scope;

    beforeEach(function () {
        module('caseManagement');

        inject(function ($controller, $rootScope, $injector) {
            mainCtrl = $controller('MainCtrl');
            state = $injector.get('$state');
            httpBackEnd = $injector.get('$httpBackend');
            scope = $rootScope.$new();
        });

        spyOn(state, 'go').and.callFake(function (state, params) {
            return { 'state': state, 'params': params };
        });
    });

    it('should navigate to directory when going home', function () {
        httpBackEnd.when('GET', 'dist/login/login.html')
            .respond(200);

        httpBackEnd.when('GET', 'dist/directory/directory.html')
            .respond(200);

        mainCtrl.goHome();
        scope.$digest();
        expect(state.go).toHaveBeenCalled();
        expect(state.go).toHaveBeenCalledWith("directory");
    });
});