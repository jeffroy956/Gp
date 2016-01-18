/// <reference path="_references.js" />
(function (gp, ko, _) {
    "use strict";

    function FamilyViewModel(familyRepository) {
        var _self = this;

        this.families = ko.observableArray([]);

        familyRepository.getAll()
        .then(function (serverData) {
            _self.families(serverData);
        });

        this.currentFamily = ko.observable();
        this.availableCompanions = ko.observableArray();
        this.availableEnemies = ko.observableArray();

        this.selectFamily = function (family) {
            _self.currentFamily(family);
            populateAvailableRelations(family, "companions", "enemies");
            populateAvailableRelations(family, "enemies", "companions");
        }

        function populateAvailableRelations(currentFamily, targetRelationType, otherRelationType) {
            var unselectedRelations = [];
            var targetRelationList = currentFamily[targetRelationType]();
            var otherRelationList = currentFamily[otherRelationType]();

            _self.families().forEach(function (potentialRelation) {
                if (currentFamily.familyId !== potentialRelation.familyId &&
                    !hasRelation(targetRelationList, potentialRelation) &&
                    !hasRelation(otherRelationList, potentialRelation)) {
                    unselectedRelations.push(potentialRelation);
                }
            });
            _self['available' + targetRelationType.substring(0, 1).toUpperCase() + targetRelationType.substring(1)](unselectedRelations);
        }

        function hasRelation(searchList, potentialRelation) {
            return _.some(searchList, function (relation) {
                return relation.familyId === potentialRelation.familyId;
            });
        }
    }

    gp.FamilyViewModel = FamilyViewModel;

})(window.gp = window.gp || {}, ko, _);