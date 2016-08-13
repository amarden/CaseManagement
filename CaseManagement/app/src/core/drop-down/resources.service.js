angular.module('caseManagement')
    .factory('Language', function ($resource) {
        return $resource('/api/languages/:id', { id: '@_id' });
    })
    .factory('Gender', function ($resource) {
        return $resource('/api/genders/:id', { id: '@_id' });
    })
    .factory('Ethnicity', function ($resource) {
        return $resource('/api/ethnicities/:id', { id: '@_id' });
    })
    .factory('Need', function ($resource) {
        return $resource('/api/needs/:id', { id: '@_id' });
    })
    .factory('Responder', function ($resource) {
        return $resource('/api/responders/:id', { id: '@_id' });
    })
    .factory('ContactType', function ($resource) {
        return $resource('/api/contacttypes/:id', { id: '@_id' });
    })
    .factory('HousingStatus', function ($resource) {
        return $resource('/api/housingstatus/:id', { id: '@_id' });
    })
    .factory('Agency', function ($resource) {
        return $resource('/api/agencies/:id', { id: '@_id' });
    })
    .factory('Race', function ($resource) {
        return $resource('/api/races/:id', { id: '@_id' });
    })
    .factory('Translation', function ($resource) {
        return $resource('/api/translations/:id', { id: '@_id' });
    })
    .factory('Need', function ($resource) {
        return $resource('/api/needs/:id', { id: '@_id' });
    })
    .factory('VisitResult', function ($resource) {
        return $resource('/api/VisitResults/:id', { id: '@_id' });
    })
     .factory('LearnAbout', function ($resource) {
         return $resource('/api/LearnAbouts/:id', { id: '@_id' });
     })
    .factory('ReferralType', function ($resource) {
        return $resource('/api/ReferralTypes/:id', { id: '@_id' });
    })
     .factory('FileType', function ($resource) {
         return $resource('/api/FileTypes/:id', { id: '@_id' });
     });
