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

        var onRelationChanged = new gp.Event(this);

        function addCompanion(companion) {
            companions.push(companion);
            onRelationChanged.notify({ action: "added", family: companion });
        }

        function removeCompanion(companion) {
            companions.remove(companion);
            onRelationChanged.notify({ action: "removed", family: companion });
        }

        function addEnemy(enemy) {
            enemies.push(enemy);
            onRelationChanged.notify({ action: "added", family: enemy });
        }

        function removeEnemy(enemy) {
            enemies.remove(enemy);
            onRelationChanged.notify({ action: "removed", family: enemy });
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
            onRelationChanged: onRelationChanged,
            isDirty: isDirty
        }

        return publicApi;
    }

    gp.Family = Family;

})(window.gp = window.gp || {}, ko);