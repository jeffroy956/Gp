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
        this.availableRelations = ko.observableArray();

        this.selectFamily = function (family) {
            _self.currentFamily(family);
            populateAvailableRelations(family);
        }

        this.addCompanionToCurrentFamily = function (companion) {
            this.currentFamily().companions.push(companion);
            this.availableRelations.remove(companion);
        }

        this.addEnemyToCurrentFamily = function (enemy) {
            this.currentFamily().enemies.push(enemy);
            this.availableRelations.remove(enemy);
        }

        function populateAvailableRelations(currentFamily) {
            var unselectedRelations = [];

            _self.families().forEach(function (potentialRelation) {
                if (currentFamily.familyId !== potentialRelation.familyId &&
                    !hasRelation(currentFamily.companions(), potentialRelation) &&
                    !hasRelation(currentFamily.enemies(), potentialRelation)) {
                    unselectedRelations.push(potentialRelation);
                }
            });
            _self.availableRelations(unselectedRelations);
        }

        function hasRelation(searchList, potentialRelation) {
            return _.some(searchList, function (relation) {
                return relation.familyId === potentialRelation.familyId;
            });
        }
    }

    gp.FamilyViewModel = FamilyViewModel;

})(window.gp = window.gp || {}, ko, _);