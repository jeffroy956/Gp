/// <reference path="_references.js" />
(function (gp, ko) {
    "use strict";

    function PlanViewModel(planRepository, calendarPlans) {

        var defaultCalendar = calendarPlans[0];

        planRepository.getCalendarPlans(defaultCalendar.calendarId);

        var calendarPlans = ko.observableArray([]);

        var publicApi = {
            calendarPlans: calendarPlans
        };

        return publicApi;
    }

    gp.PlanViewModel = PlanViewModel;

})(window.gp = window.gp || {}, ko);