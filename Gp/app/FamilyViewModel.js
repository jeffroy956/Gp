/// <reference path="_references.js" />
(function (gp, ko) {
    "use strict";

    function FamilyViewModel(familyRepository) {
        var _self = this;

        this.families = ko.observableArray([]);

        familyRepository.getAll()
        .then(function(serverData)
        {
            _self.families(serverData);
        });
    }

    gp.FamilyViewModel = FamilyViewModel;

})(window.gp = window.gp || {}, ko);