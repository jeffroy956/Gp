/// <reference path="_references.js" />
/// <reference path="_references.js" />
(function (gp, ko) {
    "use strict";

    function ObjectMapper() {

        function mapPlans(planListDto) {
            return ko.observableArray(planListDto);
        }

        var publicApi = {
            mapPlans: mapPlans
        }

        return publicApi;
    }

    gp.ObjectMapper = ObjectMapper;

})(window.gp = window.gp || {}, ko);