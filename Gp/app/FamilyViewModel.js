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

        function addEnemyToFamily(enemy) {
            currentFamily().enemies.push(enemy);
            availableRelations.remove(enemy);
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

        var publicApi = {
            families: families,
            currentFamily: currentFamily,
            availableRelations: availableRelations,
            selectFamily: selectFamily,
            addEnemyToFamily: addEnemyToFamily
        }

        return publicApi;
    }

    gp.FamilyViewModel = FamilyViewModel;

})(window.gp = window.gp || {}, ko, _);