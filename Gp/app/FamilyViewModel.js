/// <reference path="_references.js" />
(function (gp, ko, _) {
    "use strict";

    function FamilyViewModel(familyRepository) {

        //todo: remove this references, do lexical scope with public api
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

        //this.addCompanionToFamily = function (companion) {
        //    _self.currentFamily().companions.push(companion);
        //    _self.availableRelations.remove(companion);
        //}

        this.addEnemyToFamily = function (enemy) {
            _self.currentFamily().enemies.push(enemy);
            _self.availableRelations.remove(enemy);
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