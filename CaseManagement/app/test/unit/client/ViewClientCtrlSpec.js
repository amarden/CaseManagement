describe('View Client Controller', function () {
    var clientCtrl, scope, httpBackend, mdSidenav, $mdToast, toast;

    var mockClient = {"ClientCommunities":[],"ClientFamilies":[{"clientFamilyId":346,"clientId":108,"name":"Son","age":"10","liveWith":true,"dateCreated":"2016-01-17T15:43:29.877"}],"ClientNeeds":[{"Need":{"needId":14,"needName":"Other"},"clientNeedId":1837,"clientId":108,"needId":14,"note":null,"active":true}],"ClientProviders":[],"ClientVisits":[{"ClientVisitReferrals":[{"ReferralType":{"referralTypeId":6,"referralTypeName":"Mental Health"},"clientVisitReferralId":680,"clientVisitId":2771,"referralTypeId":6,"referralAgencyId":85,"contactName":"Marcy West"}],"ContactType":{"contactTypeId":1,"contactTypeName":"Phone"},"Responder":{"responderId":1,"responderName":"Danielle"},"VisitResult":{"visitResultId":4,"visitResultName":"Case Management"},"clientId":108,"clientVisitId":2771,"dateVisit":"2015-01-12T00:00:00","contactTypeId":1,"responderId":1,"timeSpentMintues":60,"housingChange":false,"mentalHealthChange":false,"dateFollowUp":null,"visitResultId":4,"notes":"Needed help with therapy referrals","followUp":false,"dateCreated":"2016-01-17T15:44:20.717","dateEdited":"2016-01-17T15:44:20.717"},{"ClientVisitReferrals":[],"ContactType":{"contactTypeId":1,"contactTypeName":"Phone"},"Responder":{"responderId":1,"responderName":"Danielle"},"VisitResult":{"visitResultId":2,"visitResultName":"Referral to Collateral"},"clientId":108,"clientVisitId":3286,"dateVisit":"2014-09-19T00:00:00","contactTypeId":1,"responderId":1,"timeSpentMintues":30,"housingChange":false,"mentalHealthChange":false,"dateFollowUp":null,"visitResultId":2,"notes":"Needed help with transportation for son after school.  Discussed possible resources, high school student helping transport","followUp":false,"dateCreated":"2016-01-17T15:44:20.693","dateEdited":"2016-01-17T15:44:20.693"},{"ClientVisitReferrals":[],"ContactType":{"contactTypeId":1,"contactTypeName":"Phone"},"Responder":{"responderId":1,"responderName":"Danielle"},"VisitResult":{"visitResultId":4,"visitResultName":"Case Management"},"clientId":108,"clientVisitId":3287,"dateVisit":"2014-09-22T00:00:00","contactTypeId":1,"responderId":1,"timeSpentMintues":30,"housingChange":false,"mentalHealthChange":false,"dateFollowUp":null,"visitResultId":4,"notes":"Follow up.","followUp":false,"dateCreated":"2016-01-17T15:44:20.703","dateEdited":"2016-01-17T15:44:20.703"}],"Ethnicity":{"ethnicityId":1,"ethnicityName":"American"},"Gender":{"genderId":2,"genderName":"Female"},"HousingStatu":{"housingStatusId":1,"housingStatusName":"Rent"},"Language":{"languageId":1,"languageName":"English"},"LearnAbout":{"learnAboutId":99,"learnAboutName":"Missing"},"Race":{"raceId":1,"raceName":"White"},"Translation":{"translationId":1,"translationValue":"No"},"clientId":108,"firstName":"Kimberly","nickName":null,"lastName":"Davidson","dob":"1970-01-01T00:00:00.000Z","address":null,"intakeDate":"2016-01-17T00:00:00","genderId":2,"languageId":1,"english":true,"veteran":false,"ethnicityId":1,"email":null,"phone":null,"dateCreated":"2016-01-17T15:43:29.877","dateEdited":"2016-01-17T15:43:29.877","learnAboutId":99,"raceId":1,"housingStatusId":1,"houseLossRisk":false,"translationId":1,"canContact":false};

    beforeEach(function () {
        module('caseManagement');

        mdSidenav = {};
        mdSidenav.toggle = jasmine.createSpy();
        module(function ($provide) {
            $provide.factory('$mdSidenav', function () {
                return function () {
                    return mdSidenav;
                };
            });

            $provide.factory('$stateParams', function () {
                return { id: 108 };
            });
        });

        inject(function ($controller, $rootScope, $injector) {
            scope = $rootScope.$new();
            scope.mc = {};
            clientCtrl = $controller('ViewClientCtrl', { $scope: scope });
            $injector.get('DropDown');
            httpBackend = $injector.get('$httpBackend');
            $mdToast = $injector.get('$mdToast');
            scope.clientForm = {};
            toast = $mdToast.simple();
        });

        spyOn($mdToast, "show").and.callFake();

        spyOn(toast, "textContent").and.callFake(function (string) {
            return string;
        });

        httpBackend.when('GET', '/api/Clients/108')
            .respond(200, JSON.stringify(mockClient));

        httpBackend.when('GET', 'directory/directory.html')
            .respond(200, JSON.stringify(mockClient));

        testHelper.mockHttp(httpBackend);
    });

    it('toggles side nav on toggleRight', function () {
        clientCtrl.toggleRight();
        expect(mdSidenav.toggle).toHaveBeenCalled();
    }); 

    it('should be the same client as what is returned by get', function () {
        expect(clientCtrl.client).toBeFalsy({});
        httpBackend.flush();
        expect(clientCtrl.client.ClientFamilies).toEqual(mockClient.ClientFamilies);
        expect(clientCtrl.client.clientId).toBe(mockClient.clientId);
    });

    it('should have a title that is the first name and last name of the client', function () {
        httpBackend.flush();
        expect(scope.mc.title).toBe(mockClient.firstName + " " + mockClient.lastName);
    });

    it('should create toast on update', function () {
        httpBackend.flush();
        httpBackend.when('PUT', '/api/Clients/108')
          .respond(200);
        clientCtrl.save();
        httpBackend.flush();
        expect($mdToast.show).toHaveBeenCalled();
    });

    it('should set form to not dirty if update successful and call toast', function () {
        httpBackend.flush();
        httpBackend.when('PUT', '/api/Clients/108')
          .respond(200);
        clientCtrl.save();
        httpBackend.flush();
        expect(scope.clientForm.$dirty).toBeFalsy();
        expect($mdToast.show).toHaveBeenCalled();
    });

    it('should set form to not dirty if update successful and call toast', function () {
        httpBackend.flush();
        httpBackend.when('PUT', '/api/Clients/108')
            .respond(500);
        scope.clientForm.$dirty = true;
        clientCtrl.save();
        httpBackend.flush();
        expect(scope.clientForm.$dirty).toBe(true);
        expect($mdToast.show).toHaveBeenCalled();
    });

    it("should test stateChangeStart", function () {
        console.log("test nav away confirmation");
    });
});
