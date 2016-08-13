describe('Create Client Visit Controller', function () {
    var clientCtrl, scope, httpBackend, mdSidenav, state, Client, ClientVisit, q, mockClient, mockClientVisit;

    beforeEach(function () {
        module('caseManagement');

        mockClient = { "ClientCommunities": [], "ClientFamilies": [], "ClientNeeds": [{ "Need": { "needId": 9, "needName": "Fuel" }, "clientNeedId": 1845, "clientId": 113, "needId": 9, "note": null, "active": true }, { "Need": { "needId": 10, "needName": "Utilities" }, "clientNeedId": 1846, "clientId": 113, "needId": 10, "note": null, "active": true }, { "Need": { "needId": 8, "needName": "Housing" }, "clientNeedId": 1847, "clientId": 113, "needId": 8, "note": null, "active": true }], "ClientProviders": [], "ClientVisits": [{ "ClientVisitReferrals": [{ "ReferralType": { "referralTypeId": 9, "referralTypeName": "Fuel" }, "clientVisitReferralId": 687, "clientVisitId": 2777, "referralTypeId": 9, "referralAgencyId": 39, "contactName": null }], "ContactType": { "contactTypeId": 2, "contactTypeName": "Face to Face" }, "Responder": { "responderId": 1, "responderName": "Danielle" }, "VisitResult": { "visitResultId": 1, "visitResultName": "Referral Over Phone" }, "clientId": 113, "clientVisitId": 2777, "dateVisit": "2014-10-27T00:00:00", "contactTypeId": 2, "responderId": 1, "timeSpentMintues": 60, "housingChange": false, "mentalHealthChange": false, "dateFollowUp": null, "visitResultId": 1, "notes": "Intake. ", "followUp": false, "dateCreated": "2016-01-17T15:44:20.887", "dateEdited": "2016-01-17T15:44:20.887" }, { "ClientVisitReferrals": [], "ContactType": { "contactTypeId": 1, "contactTypeName": "Phone" }, "Responder": { "responderId": 1, "responderName": "Danielle" }, "VisitResult": { "visitResultId": 1, "visitResultName": "Referral Over Phone" }, "clientId": 113, "clientVisitId": 3296, "dateVisit": "2014-10-08T00:00:00", "contactTypeId": 1, "responderId": 1, "timeSpentMintues": 30, "housingChange": false, "mentalHealthChange": false, "dateFollowUp": null, "visitResultId": 1, "notes": "phone scheduled appointment. ", "followUp": false, "dateCreated": "2016-01-17T15:44:20.863", "dateEdited": "2016-01-17T15:44:20.863" }, { "ClientVisitReferrals": [], "ContactType": { "contactTypeId": 1, "contactTypeName": "Phone" }, "Responder": { "responderId": 1, "responderName": "Danielle" }, "VisitResult": { "visitResultId": 1, "visitResultName": "Referral Over Phone" }, "clientId": 113, "clientVisitId": 3297, "dateVisit": "2014-10-13T00:00:00", "contactTypeId": 1, "responderId": 1, "timeSpentMintues": 15, "housingChange": false, "mentalHealthChange": false, "dateFollowUp": null, "visitResultId": 1, "notes": "client cancelled.  Rescheduled", "followUp": false, "dateCreated": "2016-01-17T15:44:20.87", "dateEdited": "2016-01-17T15:44:20.87" }, { "ClientVisitReferrals": [], "ContactType": { "contactTypeId": 1, "contactTypeName": "Phone" }, "Responder": { "responderId": 1, "responderName": "Danielle" }, "VisitResult": { "visitResultId": 1, "visitResultName": "Referral Over Phone" }, "clientId": 113, "clientVisitId": 3298, "dateVisit": "2014-10-21T00:00:00", "contactTypeId": 1, "responderId": 1, "timeSpentMintues": 15, "housingChange": false, "mentalHealthChange": false, "dateFollowUp": null, "visitResultId": 1, "notes": "client cancelled.  Rescheduled", "followUp": false, "dateCreated": "2016-01-17T15:44:20.88", "dateEdited": "2016-01-17T15:44:20.88" }], "Ethnicity": { "ethnicityId": 99, "ethnicityName": "Missing" }, "Gender": { "genderId": 2, "genderName": "Female" }, "HousingStatu": { "housingStatusId": 1, "housingStatusName": "Rent" }, "Language": { "languageId": 1, "languageName": "English" }, "LearnAbout": { "learnAboutId": 99, "learnAboutName": "Missing" }, "Race": { "raceId": 99, "raceName": "Missing" }, "Translation": { "translationId": 1, "translationValue": "No" }, "clientId": 113, "firstName": "Christopher", "nickName": null, "lastName": "Rodriguez", "dob": "1965-08-27T00:00:00.000Z", "address": null, "intakeDate": "2016-01-17T00:00:00", "genderId": 2, "languageId": 1, "english": true, "veteran": false, "ethnicityId": 99, "email": null, "phone": null, "dateCreated": "2016-01-17T15:43:29.877", "dateEdited": "2016-01-17T15:43:29.877", "learnAboutId": 99, "raceId": 99, "housingStatusId": 1, "houseLossRisk": false, "translationId": 1, "canContact": false };
        mockClientVisit = {};

        mdSidenav = {};
        mdSidenav.toggle = jasmine.createSpy();
        module(function ($provide) {
            $provide.factory('$mdSidenav', function () {
                return function () {
                    return mdSidenav;
                };
            });

            $provide.service('ClientVisit', function () {
                this.resource = {};

                this.resource.save = function () {
                };
            });

            $provide.service('Client', function () {
                this.resource = {};

                this.resource.get = function (id, callback) {
                    callback(mockClient);
                };

                this.resource.update = function () {

                };
            });

            $provide.factory('$stateParams', function () {
                return { id: 108 };
            });
        });

        inject(function ($controller, $rootScope, $injector) {
            scope = $rootScope.$new();
            scope.mc = {};
            scope.clientForm = {};
            clientCtrl = $controller('ClientVisitCtrl', { $scope: scope });
            $injector.get('DropDown');
            httpBackend = $injector.get('$httpBackend');
            state = $injector.get("$state");
            Client = $injector.get('Client');
            ClientVisit = $injector.get('ClientVisit');
            q = $injector.get("$q");
        });

        spyOn(Client.resource, 'update').and.callFake(function (i, item) {
            mockClient = item;
            var deferred = q.defer();
            deferred.resolve('Remote call result');
            return deferred.promise;
        });

        spyOn(ClientVisit.resource, 'save').and.callFake(function (item) {
            mockClientVisit = item;
            var deferred = q.defer();
            deferred.resolve('Remote call result');
            return deferred.promise;
        });

        spyOn(state, 'go').and.callFake(function (state, params) {
            return { 'state': state, 'params': params };
        });

        testHelper.mockHttp(httpBackend);
    });

    it('toggles side nav on toggleRight', function () {
        clientCtrl.toggleRight();
        expect(mdSidenav.toggle).toHaveBeenCalled();
    });

    it('fills the drop down menus', function () {
        httpBackend.flush();
        expect(clientCtrl.dropDown.visitResult.length).toBeGreaterThan(1);
        expect(clientCtrl.dropDown.responder.length).toBeGreaterThan(1);
        expect(clientCtrl.dropDown.contactType.length).toBeGreaterThan(1);
        expect(clientCtrl.dropDown.referralType.length).toBeGreaterThan(1);
        expect(clientCtrl.dropDown.agency.length).toBeGreaterThan(1);
        expect(clientCtrl.dropDown.nope).toBeUndefined();
    });

    it("starts with empty Collections", function () {
        expect(clientCtrl.ClientVisit.ClientVisitReferrals.length).toBe(0);
    });

    it("should populate client with the mockClient", function () {
        expect(clientCtrl.Client.firstName).toBe("Christopher"); 
    });

    it("should add and remove Client Visit Referrals", function () {
        expect(clientCtrl.ClientVisit.ClientVisitReferrals.length).toBe(0);
        clientCtrl.addReferral();
        clientCtrl.addReferral();
        expect(clientCtrl.ClientVisit.ClientVisitReferrals.length).toBe(2);
        clientCtrl.removeReferral();
        expect(clientCtrl.ClientVisit.ClientVisitReferrals.length).toBe(1);
    });
    
    it("should respond with the Client Visit followup property", function () {
        clientCtrl.ClientVisit.followUp = true;
        expect(clientCtrl.needsFollowUp()).toBe(true);
        clientCtrl.ClientVisit.followUp = false;
        expect(clientCtrl.needsFollowUp()).toBe(false);
    });

    it('should save changes properly', function () {
        clientCtrl.Client.clientId = 200;
        clientCtrl.ClientVisit.clientVisitId = 9;
        clientCtrl.save();
        expect(mockClient.clientId).toBe(200);
        expect(mockClientVisit.clientVisitId).toBe(9);
    });

    it('should navigate to right client', function () {
        httpBackend.when('GET', 'client/client.html')
            .respond(200);
        clientCtrl.navToClient();
        expect(state.go).toHaveBeenCalled();
        expect(state.go).toHaveBeenCalledWith("client", { id: 113 });
    });

    it("should test stateChangeStart", function () {
        console.log("test nav away confirmation");
    });
});

