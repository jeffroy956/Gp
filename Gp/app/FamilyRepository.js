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
                        resolve(gp.familyMapper.mapFamilies(data));
                    });

            });

            return repoPromise;
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