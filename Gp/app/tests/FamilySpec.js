/// <reference path="_references.js" />
describe("Family", function () {
    it("is globally defined", function () {
        expect(gp.Family).toBeDefined();
    });

    it("creates a new family from a DTO", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
            companions: [{ familyId: 2, name: "spinach" }],
            enemies: [{ familyId: 3, name: "carrots" }]
        };

        var family = new gp.Family(dto);

        expect(family.name()).toBe("lettuce");
        expect(family.familyId).toBe(1);
        expect(ko.unwrap(family.companions()[0].name)).toBe("spinach");
        expect(ko.unwrap(family.enemies()[0].name)).toBe("carrots");
    });
    
    it("adds a companion to family", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        family.addCompanion({ familyId: 2, name: "cukes" });

        expect(ko.unwrap(family.companions()[0].name)).toBe("cukes");
    });

    it("adding companion raises relationAdded event", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        var addedRelation;
        family.onRelationAdded.attach(function (sender, args) {
            addedRelation = args;
        });

        family.addCompanion({ familyId: 2, name: "cukes" });

        expect(addedRelation).toEqual({ familyId: 2, name: "cukes" });
    });

    it("adds an enemy to family", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        family.addEnemy({ familyId: 2, name: "cukes" });

        expect(ko.unwrap(family.enemies()[0].name)).toBe("cukes");
    });

    it("adding enemy raises relationAdded event", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        var addedRelation;
        family.onRelationAdded.attach(function (sender, args) {
            addedRelation = args;
        });

        family.addEnemy({ familyId: 2, name: "cukes" });

        expect(addedRelation).toEqual({ familyId: 2, name: "cukes" });
    });

    it("removes a companion from family", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        var companion = { familyId: 2, name: "cukes" };

        family.addCompanion(companion);

        family.removeCompanion(companion);

        expect(family.companions().length).toBe(0);
    });

    it("removing a companion raises onRelationRemoved event", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        var removedRelation;

        family.onRelationRemoved.attach(function (sender, args) {
            removedRelation = args;
        });

        var companion = { familyId: 2, name: "cukes" };

        family.addCompanion(companion);

        family.removeCompanion(companion);

        expect(removedRelation).toEqual({ familyId: 2, name: "cukes" });
    });

    it("removes an enemy from family", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        var enemy = { familyId: 2, name: "cukes" }
        family.addEnemy(enemy);
        family.removeEnemy(enemy);

        expect(ko.unwrap(family.enemies().length)).toBe(0);
    });

    it("removing an enemy raises onRelationRemoved event", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        var removedRelation;

        family.onRelationRemoved.attach(function (sender, args) {
            removedRelation = args;
        });

        var enemy = { familyId: 2, name: "cukes" };

        family.addEnemy(enemy);

        family.removeEnemy(enemy);

        expect(removedRelation).toEqual({ familyId: 2, name: "cukes" });
    });

});