angular.module('caseManagement')
    .factory('ClientVisit', function ($resource) {
        var obj = {};

        obj.__transformDate = function (data) {
            data = JSON.parse(data);
            data.dateVisit = new moment(data.dateVisit)._d;
            data.dateFollowUp = data.dateFollowUp ? new moment(data.dateFollowUp)._d : null;
            return data;
        };

        obj.resource = $resource('/api/ClientVisits/:id', { id: '@_id' }, {
            save: {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken") },
                method: "POST",
                transformRequest: function (data) {
                    data.viewReferrals = data.ClientVisitReferrals;
                    return JSON.stringify(data);
                }
            },
            update: {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken") },
                method: 'PUT',
                transformRequest: function (data) {
                    data.viewReferrals = data.ClientVisitReferrals;
                    return JSON.stringify(data);
                }
            },
            get: {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken") },
                transformResponse: function (data) {
                    return obj.__transformDate(data);
                }
            }
        });

        return obj;
    });
