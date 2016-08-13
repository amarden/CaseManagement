describe('Client Service', function () {
    var client, resource, httpBackend;
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

    var mockClient = {
        firstName: "Aaron",
        lastName: "Marden",
        gender: "male",
        dob: "01/12/1987"
    };

    beforeEach(function () {
        module('caseManagement');

        inject(function ($injector) {
            client = $injector.get('Client');
            resource = $injector.get('$resource');
            httpBackend = $injector.get('$httpBackend');
        });

        spyOn(client.resource, 'query').and.returnValue(mockClients);
        spyOn(client.resource, 'get').and.returnValue(mockClient);
    });

    it('should return all clients on query', function () {
        var clients = client.resource.query();
        expect(clients.length).toBe(3);
    });

    it('should return single client on get', function () {
        var oneClient = client.resource.get({ id: 1 });
        expect(oneClient.firstName).toBe('Aaron');
    });

    it('should take client object with string dob and return with date dob', function () {
        var myClient = client.__transformDate(JSON.stringify(mockClient));
        expect(myClient.dob).toEqual(new Date('01/12/1987')); 
    });
});