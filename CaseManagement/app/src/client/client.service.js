angular.module('caseManagement')
    .factory('Client', function ($resource) {
        var obj = {};

        obj.__transformDate = function (data) {
            data = JSON.parse(data);
            data.dob = new moment(data.dob)._d;
            data.intakeDate = new moment(data.intakeDate)._d;
            return data;
        };

        obj.resource = $resource('/api/Clients/:id', { id: '@_id' }, {
            get: {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken") },
                transformResponse: function (data) {
                    return obj.__transformDate(data);
                }
            },
            query: {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken") },
                isArray: true
            },
            update: {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken") },
                method: 'PUT'
            },
            save: {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken") },
                method: "POST"
            },
            getFollowUps: {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken") },
                url: "/api/Clients/FollowUp",
                method: "GET",
                isArray:true
            }
        });

        return obj;
    });
 