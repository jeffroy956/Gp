/// <reference path="_references.js" />
(function (gp, ko) {
    "use strict";
    function FamilyMapper() {

        function mapFamilies(serverData) {
            var rtnFamilies = [];
            serverData.forEach(function (family) {
                rtnFamilies.push({
                    familyId: family.familyId,
                    name: ko.observable(family.name),
                    companions: ko.observableArray(family.companions),
                    enemies: ko.observableArray(family.enemies)
                });
            });

            return rtnFamilies;
        }

        var publicApi = {
            mapFamilies: mapFamilies
        };

        return publicApi;
    }



    gp.familyMapper = new FamilyMapper();

})(window.gp = window.gp || {}, ko);