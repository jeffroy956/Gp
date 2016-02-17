/// <reference path="_references.js" />
(function (gp, ko) {
    "use strict";
    function PlanRepository(serverRequest) {
        var apiController = "/api/Plans";

        function getCalendarPlans(calendarId) {
            var repoPromise = new Promise(function (resolve, reject) {
                serverRequest
                .sendRequest("GET", apiController + "/Calendar/" + calendarId)
                .then(function (data) {
                //    resolve(mapFamilies(data));
                });
            });
            return repoPromise;
        }

        var publicApi = {
            getCalendarPlans: getCalendarPlans
        };

        return publicApi;
    }



    gp.PlanRepository = PlanRepository;

})(window.gp = window.gp || {}, ko);