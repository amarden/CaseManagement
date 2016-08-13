describe('Client Visit Service', function () {
    var clientVisit, httpBackend, scope, q, agency;

    var mockReferrals = [
        {
            referralAgencyId: 2,
            referralAgencyName: ""
        },
        {
            referralAgencyId: 4,
            referralAgencyName: "Stuff"
        },
        {
            referralAgencyId: 0,
            referralAgencyName: "Mental Health Legal Services"
        },
        {
            referralAgencyId: null,
            referralAgencyName: "Mental Aaron"
        }
    ];

    var mockReferrals2 = [
     {
         referralAgencyId: 0,
         referralAgencyName: ""
     },
     {
         referralAgencyId: 4,
         referralAgencyName: "Stuff"
     },
     {
         referralAgencyId: 0,
         referralAgencyName: "Mental Health Legal Services"
     },
     {
         referralAgencyId: null,
         referralAgencyName: "Mental Aaron"
     }
    ];

    var mockClientVisit1 = {
        housingChange: true,
        mentalChange: false,
        dateVisit: "01/01/2015",
        dateFollowUp: "02/07/1987",
        ClientVisitReferrals: mockReferrals
    };

    var mockClientVisit2 = {
        housingChange: true,
        mentalChange: false,
        dateVisit: "05/01/2015",
        dateFollowUp: null,
        ClientVisitReferrals: mockReferrals2
    };

    var mockClientVisits = [
        {
            housingChange: true,
            mentalChange: false
        },
        {
            housingChange: true,
            mentalChange: false
        },
    ];
    
    beforeEach(function () {
        module('caseManagement');

        module(function ($provide) {
            $provide.service('Agency', function () {
                this.query = function () {
                    return testHelper.dropDowns.agency;
                };
                this.save = function (referral, callback) {
                    referral.agencyId = 1 + referral.name.length;
                    testHelper.dropDowns.agency.push(referral);
                    callback();
                };
            });
        });

        inject(function ($injector, $rootScope, $q) {
            clientVisit = $injector.get('ClientVisit');
            q = $q;
            httpBackend = $injector.get('$httpBackend');
            scope = $rootScope.$new();
            agency = $injector.get('Agency');
        });

        spyOn(clientVisit.resource, 'query').and.returnValue(mockClientVisits);
    });

    it('should return all client visits on query', function () {
        var clients = clientVisit.resource.query();
        expect(clients.length).toBe(2);
    });

    it('should return correct date', function () {
        spyOn(clientVisit.resource, 'get')
            .and.returnValue(clientVisit.__transformDate(JSON.stringify(mockClientVisit1)));

        var visit = clientVisit.resource.get({ id: 1 });
        expect(visit.dateVisit).toEqual(new Date("01/01/2015"));
        expect(visit.dateFollowUp).toEqual(new Date("02/07/1987"));
    });

    it('should return correct date if dateFollowUp is null', function () {
        spyOn(clientVisit.resource, 'get')
            .and.returnValue(clientVisit.__transformDate(JSON.stringify(mockClientVisit2)));

        var visit = clientVisit.resource.get({ id: 1 });
        expect(visit.dateVisit).toEqual(new Date("05/01/2015"));
        expect(visit.dateFollowUp).toEqual(null);
    });

});