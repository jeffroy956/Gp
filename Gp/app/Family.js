/// <reference path="_references.js" />
(function (gp, ko) {
    "use strict";
    function Family(dto) {
        var name = ko.observable(dto.name);
        var familyId = dto.familyId;
        var companions = ko.observableArray(dto.companions);
        var enemies = ko.observableArray(dto.enemies);
        var isDirty = ko.observable(false);
        var dirtyCheckFields = [name, companions, enemies];

        dirtyCheckFields.forEach(function (observable) {
            observable.subscribe(function () {
                isDirty(true);
            });
        });

        function addCompanion(companion) {
            companions.push(companion);
        }

        function removeCompanion(companion) {
            companions.remove(companion);
        }

        function addEnemy(enemy) {
            enemies.push(enemy);
        }

        function removeEnemy(enemy) {
            enemies.remove(enemy);
        }

        var publicApi = {
            name: name,
            familyId: familyId,
            companions: companions,
            enemies: enemies,
            addCompanion: addCompanion,
            removeCompanion: removeCompanion,
            addEnemy: addEnemy,
            removeEnemy: removeEnemy,
            isDirty: isDirty
        }

        return publicApi;
    }

    gp.Family = Family;

})(window.gp = window.gp || {}, ko);