/// <reference path="_references.js" />
(function (gp, ko) {
    "use strict";
    function PlanRepository(serverRequest) {
        var apiController = "/api/Plans";

        function getCalendarPlans(calendarId) {
            return serverRequest.sendRequest("GET", apiController + "/Calendar/" + calendarId);
        }

        var publicApi = {
            getCalendarPlans: getCalendarPlans
        };

        return publicApi;
    }



    gp.PlanRepository = PlanRepository;

})(window.gp = window.gp || {}, ko);