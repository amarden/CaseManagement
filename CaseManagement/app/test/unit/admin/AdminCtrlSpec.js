describe('Administration Controller', function () {
    var scope, httpBackend, adminCtrl, injector, q;

    var mockNeed = { "needId": 9, "needName": "Fuel" };
    var mockLanguage = { "languageId": 1, "languageName": "English" };
    var mockList = [{ "languageId": 1, "languageName": "English" }, { "languageId": 2, "languageName": "Spanish" }, { "languageId": 99, "languageName": "Spanish" }];
    var mockListNinetyEight = [{ "languageId": 1, "languageName": "English" }, { "languageId": 98, "languageName": "Spanish" }, { "languageId": 99, "languageName": "Spanish" }];
    var mockFake = { "fakeId": 1, "name": "Name" };
    var fakeResource = { "save": function () { } };

    beforeEach(function () {
        module('caseManagement');

        module(function ($provide) {
            $provide.factory("FakeResource", function () {
                this.save = function () {

                };
            });
        });

        inject(function ($controller, $rootScope, $injector) {
            scope = $rootScope.$new();
            adminCtrl = $controller('AdminCtrl', { $scope: scope });
            httpBackend = $injector.get('$httpBackend');
            q = $injector.get("$q");
            injector = $injector;
        });

        spyOn(adminCtrl, "__getResource").and.callFake(function () {
            return fakeResource;
        });

        fakeResource.save = function (item) {
            var defer = q.defer();
            mockList.push(item);
            defer.resolve();
            return defer;
        };

        testHelper.mockHttp(httpBackend);
    });

    it('get proper key name based on object', function () {
        expect(adminCtrl.__getNameKey(mockNeed)).toBe('needName');
        expect(adminCtrl.__getNameKey(mockLanguage)).toBe('languageName');
        expect(adminCtrl.__getNameKey(mockFake)).toBe('name');
    });

    it('get a list from valid drop down name and make sure it has name key', function () {
        expect(adminCtrl.dropDownList.length).toBe(0);
        adminCtrl.getList("gender");
        httpBackend.flush();
        expect(adminCtrl.dropDownList.length).toBe(2);
        expect(adminCtrl.dropDownList[0].hasOwnProperty("name")).toBeTruthy();
        expect(adminCtrl.dropDownList[0].name).toBe("Male");
    });

    it('gets next Id', function () {
        var nextNumber = adminCtrl.__getNextId(mockList, "languageId");
        expect(nextNumber).toBe(3);
    });

    it('gets next Id', function () {
        var nextNumber = adminCtrl.__getNextId(mockList, "languageId");
        expect(nextNumber).toBe(3);
    });

    it('gets next Id and handles 98', function () {
        var nextNumber = adminCtrl.__getNextId(mockListNinetyEight, "languageId");
        expect(nextNumber).toBe(100);
    });

    it('adds an object to list on save', function () {
        adminCtrl.getList("language");
        httpBackend.flush();
        adminCtrl.dropDownList = mockList;
        expect(adminCtrl.dropDownList.length).toBe(3);
        adminCtrl.name = "Test Item";
        adminCtrl.save();
        expect(adminCtrl.dropDownList.length).toBe(4);
        expect(adminCtrl.dropDownList[3].languageName).toBe("Test Item");
        expect(adminCtrl.dropDownList[3].languageId).toBe(3);
    });

});