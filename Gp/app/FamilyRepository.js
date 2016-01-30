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
                rtnFamilies.push(new gp.Family(family));
            });

            return rtnFamilies;
        }


        function save(families) {
            serverRequest.sendRequest("POST", apiController, toJSON(families));
        }

        function toJSON(families) {
            return ko.toJSON(families.map(function (family) {
                var copy = ko.toJS(family);
                delete copy.isDirty;
                delete copy.selected;
                return copy;
            }));
        }

        function remove(){

        }

        var publicApi = {
            getAll: getAll,
            save: save
        };

        return publicApi;
    }



    gp.FamilyRepository = FamilyRepository;

})(window.gp = window.gp || {}, ko);