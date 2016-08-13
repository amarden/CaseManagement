describe('angularjs homepage todo list', function () {
    it('Create a Client', function () {
        browser.get('http://localhost:57978/app/dist/');

        element.all(by.tagName('button')).last().click();
        browser.getLocationAbsUrl();
        browser.driver.sleep(20);
        element(by.model('cc.client.firstName')).sendKeys("Aaron");
        element(by.model('cc.client.lastName')).sendKeys("Marden");
        element(by.model('cc.client.address')).sendKeys("29 Jefferson St Apt 1, Cambridge MA 02141");
        element.all(by.css("md-select")).each(function (eachElement, index) {
            eachElement.click(); // select the
            browser.waitForAngular(); // wait for the renderings to take effect
            element(by.css("md-option")).click(); // select the first md-option
            browser.waitForAngular(); // wait for the renderings to take effect
        });
    });
});