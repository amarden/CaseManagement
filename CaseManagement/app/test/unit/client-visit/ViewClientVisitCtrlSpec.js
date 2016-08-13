describe('View Client Visit Controller', function () {
    var clientCtrl, scope, httpBackend, mdSidenav, state, Client, ClientVisit, updateClient, updateClientVisit, q;

    var mockReferrals = [
      {
          id: 1,
      },
      {
          id: 3,
      },
      {
          id: 4,
      }
    ];
      beforeEach(function () {
        var mockClientVisit = { "ClientVisitReferrals": [], "ContactType": { "contactTypeId": 1, "contactTypeName": "Phone" }, "Responder": { "responderId": 1, "responderName": "Danielle" }, "VisitResult": { "visitResultId": 1, "visitResultName": "Referral Over Phone" }, "clientId": 113, "clientVisitId": 3296, "dateVisit": "2014-10-08T00:00:00.000Z", "contactTypeId": 1, "responderId": 1, "timeSpentMintues": 30, "housingChange": false, "mentalHealthChange": false, "dateFollowUp": null, "visitResultId": 1, "notes": "phone scheduled appointment. ", "followUp": false, "dateCreated": "2016-01-17T15:44:20.863", "dateEdited": "2016-01-17T15:44:20.863" };
        var mockClient = { "ClientCommunities": [], "ClientFamilies": [], "ClientNeeds": [{ "Need": { "needId": 9, "needName": "Fuel" }, "clientNeedId": 1845, "clientId": 113, "needId": 9, "note": null, "active": true }, { "Need": { "needId": 10, "needName": "Utilities" }, "clientNeedId": 1846, "clientId": 113, "needId": 10, "note": null, "active": true }, { "Need": { "needId": 8, "needName": "Housing" }, "clientNeedId": 1847, "clientId": 113, "needId": 8, "note": null, "active": true }], "ClientProviders": [], "ClientVisits": [{ "ClientVisitReferrals": [{ "ReferralType": { "referralTypeId": 9, "referralTypeName": "Fuel" }, "clientVisitReferralId": 687, "clientVisitId": 2777, "referralTypeId": 9, "referralAgencyId": 39, "contactName": null }], "ContactType": { "contactTypeId": 2, "contactTypeName": "Face to Face" }, "Responder": { "responderId": 1, "responderName": "Danielle" }, "VisitResult": { "visitResultId": 1, "visitResultName": "Referral Over Phone" }, "clientId": 113, "clientVisitId": 2777, "dateVisit": "2014-10-27T00:00:00", "contactTypeId": 2, "responderId": 1, "timeSpentMintues": 60, "housingChange": false, "mentalHealthChange": false, "dateFollowUp": null, "visitResultId": 1, "notes": "Intake. ", "followUp": false, "dateCreated": "2016-01-17T15:44:20.887", "dateEdited": "2016-01-17T15:44:20.887" }, { "ClientVisitReferrals": [], "ContactType": { "contactTypeId": 1, "contactTypeName": "Phone" }, "Responder": { "responderId": 1, "responderName": "Danielle" }, "VisitResult": { "visitResultId": 1, "visitResultName": "Referral Over Phone" }, "clientId": 113, "clientVisitId": 3296, "dateVisit": "2014-10-08T00:00:00", "contactTypeId": 1, "responderId": 1, "timeSpentMintues": 30, "housingChange": false, "mentalHealthChange": false, "dateFollowUp": null, "visitResultId": 1, "notes": "phone scheduled appointment. ", "followUp": false, "dateCreated": "2016-01-17T15:44:20.863", "dateEdited": "2016-01-17T15:44:20.863" }, { "ClientVisitReferrals": [], "ContactType": { "contactTypeId": 1, "contactTypeName": "Phone" }, "Responder": { "responderId": 1, "responderName": "Danielle" }, "VisitResult": { "visitResultId": 1, "visitResultName": "Referral Over Phone" }, "clientId": 113, "clientVisitId": 3297, "dateVisit": "2014-10-13T00:00:00", "contactTypeId": 1, "responderId": 1, "timeSpentMintues": 15, "housingChange": false, "mentalHealthChange": false, "dateFollowUp": null, "visitResultId": 1, "notes": "client cancelled.  Rescheduled", "followUp": false, "dateCreated": "2016-01-17T15:44:20.87", "dateEdited": "2016-01-17T15:44:20.87" }, { "ClientVisitReferrals": [], "ContactType": { "contactTypeId": 1, "contactTypeName": "Phone" }, "Responder": { "responderId": 1, "responderName": "Danielle" }, "VisitResult": { "visitResultId": 1, "visitResultName": "Referral Over Phone" }, "clientId": 113, "clientVisitId": 3298, "dateVisit": "2014-10-21T00:00:00", "contactTypeId": 1, "responderId": 1, "timeSpentMintues": 15, "housingChange": false, "mentalHealthChange": false, "dateFollowUp": null, "visitResultId": 1, "notes": "client cancelled.  Rescheduled", "followUp": false, "dateCreated": "2016-01-17T15:44:20.88", "dateEdited": "2016-01-17T15:44:20.88" }], "Ethnicity": { "ethnicityId": 99, "ethnicityName": "Missing" }, "Gender": { "genderId": 2, "genderName": "Female" }, "HousingStatu": { "housingStatusId": 1, "housingStatusName": "Rent" }, "Language": { "languageId": 1, "languageName": "English" }, "LearnAbout": { "learnAboutId": 99, "learnAboutName": "Missing" }, "Race": { "raceId": 99, "raceName": "Missing" }, "Translation": { "translationId": 1, "translationValue": "No" }, "clientId": 113, "firstName": "Christopher", "nickName": null, "lastName": "Rodriguez", "dob": "1965-08-27T00:00:00.000Z", "address": null, "intakeDate": "2016-01-17T00:00:00", "genderId": 2, "languageId": 1, "english": true, "veteran": false, "ethnicityId": 99, "email": null, "phone": null, "dateCreated": "2016-01-17T15:43:29.877", "dateEdited": "2016-01-17T15:43:29.877", "learnAboutId": 99, "raceId": 99, "housingStatusId": 1, "houseLossRisk": false, "translationId": 1, "canContact": false };

        module('caseManagement');

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
                this.resource.get = function (id, callback) {
                    callback(mockClientVisit);
                };
                this.resource.update = function () {
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
            scope.clientVisitForm = {};
            scope.clientVisitForm.$dirty = false;
            clientCtrl = $controller('ViewClientVisitCtrl', { $scope: scope });
            Client = $injector.get('Client');
            ClientVisit = $injector.get('ClientVisit');
            httpBackend = $injector.get("$httpBackend");
            state = $injector.get("$state");
            q = $injector.get("$q");
        });

        spyOn(Client.resource, 'update').and.callFake(function (i, item) {
            updateClient = item;
            var deferred = q.defer();
            deferred.resolve('Remote call result');
            return deferred.promise;
        });

        spyOn(ClientVisit.resource, 'update').and.callFake(function (i, item) {
            updateClientVisit = item;
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

    it('should populate with correct Client and Client Visit', function () {
        expect(clientCtrl.Client.firstName).toBe("Christopher"); 
        expect(clientCtrl.ClientVisit.Responder.responderName).toBe("Danielle");
    });

    it('should populate title with first name, last name, and date', function () {
        var name = clientCtrl.Client.firstName + " " + clientCtrl.Client.lastName + " - " +
            new Date(clientCtrl.ClientVisit.dateVisit).toDateString();
        expect(scope.mc.title).toBe(name);
    });

    it('should return correct follow up status', function () {
        expect(clientCtrl.needsFollowUp()).toBe(false);
    });

    it('should navigate to right client', function () {
        httpBackend.when('GET', 'client/client.html')
            .respond(200);
        clientCtrl.navToClient();
        expect(state.go).toHaveBeenCalled();
        expect(state.go).toHaveBeenCalledWith("client", { id: 113 });
    });

    it('should add one ClientReferral every time it is called', function () {
        expect(clientCtrl.ClientVisit.ClientVisitReferrals.length).toBe(0);
        clientCtrl.addReferral();
        expect(clientCtrl.ClientVisit.ClientVisitReferrals.length).toBe(1);
        clientCtrl.addReferral();
        expect(clientCtrl.ClientVisit.ClientVisitReferrals.length).toBe(2);
    });

    it('should remove one ClientReferral every time it is called', function () {
        expect(clientCtrl.ClientVisit.ClientVisitReferrals.length).toBe(0);
        clientCtrl.addReferral();
        clientCtrl.addReferral();
        expect(clientCtrl.ClientVisit.ClientVisitReferrals.length).toBe(2);
        clientCtrl.removeReferral(1);
        expect(clientCtrl.ClientVisit.ClientVisitReferrals.length).toBe(1);
    });

    it('should remove the right one ', function () {
        clientCtrl.ClientVisit.ClientVisitReferrals = mockReferrals;
        var exist = clientCtrl.ClientVisit.ClientVisitReferrals
            .filter(function (d) { return d.id === 1; });
        expect(exist.length).toBe(1);
        clientCtrl.removeReferral(0);
        expect(clientCtrl.ClientVisit.ClientVisitReferrals.length).toBe(2);
        var empty = clientCtrl.ClientVisit.ClientVisitReferrals
                    .filter(function (d) { return d.id === 1; });
        expect(empty.length).toBe(0);
    });

    it('should save changes properly', function () {
        clientCtrl.Client.clientId = 200;
        clientCtrl.ClientVisit.clientVisitId = 9;
        clientCtrl.save();
        expect(updateClient.clientId).toBe(200);
        expect(updateClientVisit.clientVisitId).toBe(9);
    });
   
    it("should test stateChangeStart", function () {
        console.log("test nav away confirmation");
    });
});