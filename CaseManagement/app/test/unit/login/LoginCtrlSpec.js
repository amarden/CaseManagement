describe('Login Controller', function () {
    var loginCtrl, Login, scope, q;

    var message = "Yipeee";

    beforeEach(function () {
        module('caseManagement');
        module(function ($provide) {
            $provide.service('Login', function () {
                this.login = function () {
                };
            });
        });

        inject(function ($controller, $rootScope, $injector) {
            scope = $rootScope.$new();
            loginCtrl = $controller('LoginCtrl');
            Login = $injector.get('Login');
            q = $injector.get('$q');
        });

        spyOn(Login, "login").and.callFake(function () {
            var defer = q.defer();
            defer.resolve(message);
            return defer.promise;
        });
    });

    it("should default to grant_type equals password", function () {
        expect(loginCtrl.credentials.grant_type).toBe("password");
    });

    it("should return a message onto 'errorMessage' when logging in", function () {
        loginCtrl.submit();
        scope.$apply();
        expect(loginCtrl.errorMessage).toBe(message);
    });
});