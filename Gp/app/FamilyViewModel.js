﻿/// <reference path="_references.js" />
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
            var current = currentFamily();
            if (current) {
                current.selected(false);
            }

            family.selected(true);
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
            insertAvailableRelation(family);
        }

        function removeEnemy(family) {
            currentFamily().removeEnemy(family);
            insertAvailableRelation(family);
        }

        function insertAvailableRelation(family) {
            var relations = availableRelations();
            if (relations.length === 0) {
                availableRelations.push(family);
            }
            else {
                var index;
                for (index = 0; index < relations.length; index++) {
                    if (ko.unwrap(relations[index].name) > ko.unwrap(family.name)) {
                        break;
                    }
                }
                availableRelations.splice(index, 0, family);
            }
        }

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

        function isDirty() {
            var modifiedFamily = ko.utils.arrayFirst(families(), function (family) {
                return family.isDirty() === true;
            });

            if (modifiedFamily) {
                return true;
            }

            return false;
        }

        function saveChanges() {
            var modifiedFamilies =
                ko.utils.arrayFilter(families(), function (family) {
                    return ko.unwrap(family.isDirty) === true;
                });

            familyRepository.save(modifiedFamilies);
        }

        var publicApi = {
            families: families,
            currentFamily: currentFamily,
            availableRelations: availableRelations,
            selectFamily: selectFamily,
            addCompanion: addCompanion,
            removeCompanion, removeCompanion,
            addEnemy: addEnemy,
            removeEnemy: removeEnemy,
            isDirty: ko.computed(isDirty),
            saveChanges: saveChanges
            
        }

        return publicApi;
    }

    gp.FamilyViewModel = FamilyViewModel;

})(window.gp = window.gp || {}, ko, _);