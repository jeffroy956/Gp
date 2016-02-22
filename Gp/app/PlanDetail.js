/// <reference path="_references.js" />
/// <reference path="_references.js" />
(function (gp, ko) {
    "use strict";

    function PlanDetail(planDto) {

        var eventDescription = ko.observable(planDto.eventDescription);
        var planDate = ko.observable(planDto.planDate);
        var actualDate = ko.observable(planDto.actualDate);

        var publicApi = {
            eventDescription: eventDescription,
            planDate: planDate,
            actualDate: actualDate
        }

        return publicApi;
    }

    gp.PlanDetail = PlanDetail;

})(window.gp = window.gp || {}, ko);