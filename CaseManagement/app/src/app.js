'use strict';

angular.module('caseManagement', [
    'ui.router',
    'ngAnimate',
    'ngMessages',
    'ngAria',
    'ngMaterial',
    'ngResource',
    'ngFileUpload',
    'ngSanitize'
])
.config(function ($mdThemingProvider) {
    $mdThemingProvider.theme('default')
      .primaryPalette('blue')
      .accentPalette('green');
})
.config(function ($stateProvider) {
    $stateProvider
        .state('login', 
        {
            url: "/login",
            templateUrl: "dist/login/login.html",
            controller: "LoginCtrl",
            controllerAs: "lc"
        })
        .state('home', 
        {
            url: "/directory",
            templateUrl: "dist/directory/directory.html",
            controller: 'DirectoryCtrl',
            controllerAs: 'dc'
        })
         .state('administration',
        {
            url: "/admin",
            templateUrl: "dist/admin/admin.html",
            controller: 'AdminCtrl',
            controllerAs: 'ac'
        })
        .state('directory',
        {
            url: "/directory",
            templateUrl: "dist/directory/directory.html",
            controller: 'DirectoryCtrl',
            controllerAs: 'dc'
        })
        .state('createClient',
        {
            url: "/client",
            templateUrl: "dist/client/client.html",
            controller: 'CreateClientCtrl',
            controllerAs: 'cc'
        })
        .state('client',
        {
            url: "/client/:id",
            templateUrl: "dist/client/client.html",
            controller: 'ViewClientCtrl',
            controllerAs: 'cc'
        })
        .state('createClientVisit',
        {
            url: "/client/visit/new/:id",
            templateUrl: "dist/client-visit/client-visit.html",
            controller: 'ClientVisitCtrl',
            controllerAs: 'cv'
        })
        .state('clientVisit',
        {
            url: "/client/visit/:id",
            templateUrl: "dist/client-visit/client-visit.html",
            controller: 'ViewClientVisitCtrl',
            controllerAs: 'cv'
        })
        .state('clientVisitNotes',
        {
            url: "/client/visit/:id/note",
            templateUrl: "dist/client-visit/note.html",
            controller: 'ViewClientVisitCtrl',
            controllerAs: 'cv'
        });
})
.run(function ($rootScope, $state, Login) {
    $rootScope.$on('$stateChangeStart', function (e, toState) {

        var isLogin = toState.name === "login";
        if (isLogin) {
            return; // no need to redirect 
        }

        // now, redirect only not authenticated
        var isAuth = Login.isAuthenticated();

        if (isAuth === false) {
            e.preventDefault(); // stop current execution
            $state.go('login'); // go to login
        }
    });
});