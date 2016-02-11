/// <reference path="_references.js" />
(function (gp, ko) {
    "use strict";

    function VarietyViewModel(variety, allFamilies) {

        var selectedFamily;
        
        if (variety.family) {
            selectedFamily =
                ko.utils.arrayFirst(allFamilies, function (family) {
                    return family.familyId === variety.family.familyId;
                });
        }

        var family = ko.observable(selectedFamily);

        function selectFamily(newFamily) {
            variety.family = newFamily;
            family(newFamily);
        }

        var publicApi = {
            family: family,
            availableFamilies: allFamilies,
            selectFamily: selectFamily
        }

        return publicApi;
    }

    gp.VarietyViewModel = VarietyViewModel;

})(window.gp = window.gp || {}, ko);