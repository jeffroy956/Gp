/// <reference path="_references.js" />
(function (gp, ko) {
    "use strict";
    function FamilyRepository(serverRequest) {
        var apiController = "/api/Families";

        function getAll() {
            var repoPromise = new Promise(function (resolve, reject) {
                serverRequest
                    .sendRequest("GET", apiController)
                    .then(function (data) {
                        resolve(mapFamilies(data));
                    });

            });

            return repoPromise;
        }

        function mapFamilies(serverData) {
            var rtnFamilies = [];
            serverData.forEach(function (family) {
                rtnFamilies.push({
                    familyId: family.familyId,
                    name: ko.observable(family.name),
                    companions: ko.observable(family.companions),
                    enemies: ko.observable(family.enemies)
                });
            });

            return rtnFamilies;
        }

        function post() {

        }

        function put() {

        }

        function remove(){

        }

        var publicApi = {
            getAll: getAll
        };

        return publicApi;
    }



    gp.FamilyRepository = FamilyRepository;

})(window.gp = window.gp || {}, ko);