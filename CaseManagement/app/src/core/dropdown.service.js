angular.module('caseManagement')
    .factory('DropDown', function (Language, Ethnicity, Responder, Translation,
                                   Race, ContactType, HousingStatus, Agency, Need, VisitResult,
                                   ReferralType, Gender, LearnAbout, FileType) {
        var query = {
            language: function () { return Language.query(); },
            learnAbout: function() { return LearnAbout.query(); },
            ethnicity: function () { return Ethnicity.query(); },
            gender: function () { return Gender.query(); },
            contactType: function () { return ContactType.query(); },
            housingStatus: function () { return HousingStatus.query(); },
            agency: function () { return Agency.query(); },
            race: function () { return Race.query(); },
            responder: function () { return Responder.query(); },
            translation: function () { return Translation.query(); },
            need: function () { return Need.query(); },
            visitResult: function () { return VisitResult.query(); },
            referralType: function () { return ReferralType.query(); },
            fileType: function () { return FileType.query(); },
        };

        return function (queries) {
            var data = {};
            queries.forEach(function (d) {
                data[d] = query[d]();
            });
            return data;
        };
    });