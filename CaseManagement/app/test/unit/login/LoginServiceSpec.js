describe('Login Service', function () {
    var Login, state, q, scope, fakeStore, httpBackend;


    beforeEach(function () {
        module('caseManagement');

        inject(function ($controller, $rootScope, $injector) {
            scope = $rootScope.$new();
            Login = $injector.get('Login');
            q = $injector.get('$q');
            state = $injector.get('$state');
            httpBackend = $injector.get('$httpBackend');
        });

        fakeStore = {};

        spyOn(window.sessionStorage, "setItem").and.callFake(function (key, value) {
            fakeStore[key] = value;
        });

        spyOn(window.sessionStorage, "getItem").and.callFake(function (key) {
            return fakeStore[key];
        });

        spyOn(state, 'go').and.callFake(function (state) {
            return { 'state': state};
        });


    });

    it("should return true if there is something in accessToken sessionStorage", function () {
        fakeStore.accessToken = "something";
        expect(Login.isAuthenticated()).toBeTruthy();
    });

    it("should return false if there is nothing in accessToken sessionStorage", function () {
        expect(Login.isAuthenticated()).toBeFalsy();
    });

    it("should assign accessToken with 'credentials' on successful http post call", function () {
        httpBackend.when('POST', '/Token')
            .respond(200, JSON.stringify({ access_token: "Yay" }));

        Login.login({});
        httpBackend.flush();
        expect(fakeStore.accessToken).toBe("Yay");
    });

    it("should navigate to directory on successful http post call", function () {
        httpBackend.when('POST', '/Token')
            .respond(200, JSON.stringify({ access_token: "Yay" }));

        Login.login({});
        httpBackend.flush();
        expect(state.go).toHaveBeenCalled();
        expect(state.go).toHaveBeenCalledWith("directory");
    });

    it("should return error_description on data object in failed http post call", function () {
        httpBackend.when('POST', '/Token')
           .respond(500, JSON.stringify({ error: "invalid_grant", error_description: "Yay" }));
        var test = Login.login({});
        httpBackend.flush();
        expect(test.$$state.value).toBe("Yay");
   });
});