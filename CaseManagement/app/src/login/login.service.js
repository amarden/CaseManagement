angular.module("caseManagement")
    .service("Login", function ($http, $state, $q) {
        this.login = function (credentials) {
            var defer = $q.defer();
            $http({
                method: 'POST',
                url: "/Token",
                data: $.param(credentials),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (result) {
                sessionStorage.setItem('accessToken', result.access_token);
                $state.go("directory");
            }).error(function (data) {
                defer.resolve(data.error_description);
            });
            return defer.promise;
        };

        this.register = function (userInfo) {
            var defer = $q.defer();
            $http({
                method: 'POST',
                url: "/api/Account/Register",
                data: $.param(userInfo),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function () {
                defer.resolve("success");
            }).error(function (data) {
                defer.resolve(data.error_description);
            });
            return defer.promise;
        };

        this.isAuthenticated = function () {
            var token = sessionStorage.getItem("accessToken");
            if (token) {
                return true;
            } else { 
                return false;
            }
        };
    });