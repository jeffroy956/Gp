/// <reference path="_references.js" />
(function (gp, ko, _) {
    "use strict";

    function FamilyViewModel(familyRepository) {
        var _self = this;

        this.families = ko.observableArray([]);

        familyRepository.getAll()
        .then(function(serverData)
        {
            _self.families(serverData);
        });

        this.currentFamily = ko.observable();
        this.availableCompanions = ko.observableArray();

        this.selectFamily = function(family) {
            _self.currentFamily(family);
            populateAvailableRelations(family, "Companions");
        }

        function populateAvailableRelations(currentFamily, relationType) {
            var unselectedRelations = [];
            var relationList = currentFamily[relationType.toLowerCase()]();

            _self.families().forEach(function (potentialRelation) {
                if (currentFamily.familyId !== potentialRelation.familyId) {
                    if (!_.some(relationList,
                        function (relation) {
                        return relation.familyId === potentialRelation.familyId;
                    })) {
                        unselectedRelations.push(potentialRelation);
                    }
                }
            });

            _self['available' + relationType](unselectedRelations);
        }
    }

    gp.FamilyViewModel = FamilyViewModel;

})(window.gp = window.gp || {}, ko, _);