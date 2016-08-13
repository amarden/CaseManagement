angular.module('caseManagement')
    .factory('ClientDocument', function ($resource) {
        var obj = {};

        obj.resource = $resource('/api/ClientDocuments/:id', { id: '@_id' }, {
            get: {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken") },
                isArray: true
            },
            remove: {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken") },
                method: 'DELETE'
            },
            update: {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken") },
                method: 'PUT'
            }
        });

        return obj;
    });
 