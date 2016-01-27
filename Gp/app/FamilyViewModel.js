/// <reference path="_references.js" />
(function (gp, ko, _) {
    "use strict";

    function FamilyViewModel(familyRepository) {
        var families = ko.observableArray([]);
        var currentFamily = ko.observable();
        var availableRelations = ko.observableArray();

        familyRepository.getAll()
        .then(function (serverData) {
            families(serverData);
        });

        function selectFamily(family) {
            currentFamily(family);
            populateAvailableRelations(family);
        }

        function addCompanion(family) {
            currentFamily().addCompanion(family);
            availableRelations.remove(family);
        }

        function addEnemy(family) {
            currentFamily().addEnemy(family);
            availableRelations.remove(family);
        }

        function removeCompanion(family) {
            currentFamily().removeCompanion(family);
            availableRelations.push(family);
        }

        function removeEnemy(family) {
            currentFamily().removeEnemy(family);
            availableRelations.push(family);
        }

        //function handleRelationChanged(sender, changeFamilyArgs) {
        //    if (changeFamilyArgs.action === "added") {
        //        availableRelations.remove(changeFamilyArgs.family);
        //    }
        //    else if (changeFamilyArgs.action === "removed") {
        //        availableRelations.push(changeFamilyArgs.family);
        //    }
        //}

        function populateAvailableRelations(currentFamily) {
            var unselectedRelations = [];

            families().forEach(function (potentialRelation) {
                if (currentFamily.familyId !== potentialRelation.familyId &&
                    !hasRelation(currentFamily.companions(), potentialRelation) &&
                    !hasRelation(currentFamily.enemies(), potentialRelation)) {
                    unselectedRelations.push(potentialRelation);
                }
            });
            availableRelations(unselectedRelations);
        }

        function hasRelation(searchList, potentialRelation) {
            return _.some(searchList, function (relation) {
                return relation.familyId === potentialRelation.familyId;
            });
        }

        var publicApi = {
            families: families,
            currentFamily: currentFamily,
            availableRelations: availableRelations,
            selectFamily: selectFamily,
            addCompanion: addCompanion,
            removeCompanion, removeCompanion,
            addEnemy: addEnemy,
            removeEnemy: removeEnemy
            
        }

        return publicApi;
    }

    gp.FamilyViewModel = FamilyViewModel;

})(window.gp = window.gp || {}, ko, _);