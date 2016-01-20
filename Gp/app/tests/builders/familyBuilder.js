/// <reference path="../_references.js" />
(function (test, gp, _) {
    "use strict";
    function FamilyBuilder() {

        var _families = [];
        var _familyId = 1;
        this.withFamilyObject = function (family) {
            _families.push(family);
            return this;
        };

        this.withFamily = function (name) {
            _families.push({
                familyId: _familyId,
                name: name,
                companions: [],
                enemies: []
            });
            _familyId++;
            return this;
        }

        this.linkCompanion = function (targetName, companionName) {
            linkRelation(targetName, companionName, "companions");
            return this;
        }

        this.linkEnemy = function (targetName, enemyName) {
            linkRelation(targetName, enemyName, "enemies");
            return this;
        }

        function linkRelation(targetName, relationName, collectionName) {
            var foundFamily = _.find(_families, function (family) {
                return family.name === targetName;
            });

            if (foundFamily) {
                var foundRelation = _.find(_families, function (family) {
                    return family.name === relationName;
                });

                if (foundRelation) {
                    foundFamily[collectionName].push(foundRelation);
                }
                else {
                    throw new Error("Unable to find relation: " + relationName);
                }
            }
            else {
                throw new Error("Unable to find target family: " + targetName);
            }

        }

        this.build = function () {
            return _families;
        };

        this.buildKo = function () {
            return gp.familyMapper.mapFamilies(_families);
        }

    }

    test.FamilyBuilder = FamilyBuilder;

})(window.test = window.test || {}, gp, _);