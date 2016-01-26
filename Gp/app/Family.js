/// <reference path="_references.js" />
(function (gp, ko) {
    "use strict";
    function Family(dto) {
        var name = ko.observable(dto.name);
        var familyId = dto.familyId;
        var companions = ko.observableArray(dto.companions);
        var enemies = ko.observableArray(dto.enemies);
        var onRelationAdded = new gp.Event(this);
        var onRelationRemoved = new gp.Event(this);

        function addCompanion(companion) {
            companions.push(companion);
            onRelationAdded.notify(companion);
        }

        function removeCompanion(companion) {
            companions.remove(companion);
            onRelationRemoved.notify(companion);
        }

        function addEnemy(enemy) {
            enemies.push(enemy);
            onRelationAdded.notify(enemy);
        }

        function removeEnemy(enemy) {
            enemies.remove(enemy);
            onRelationRemoved.notify(enemy);
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
            onRelationAdded: onRelationAdded,
            onRelationRemoved: onRelationRemoved
        }

        return publicApi;
    }

    gp.Family = Family;

})(window.gp = window.gp || {}, ko);