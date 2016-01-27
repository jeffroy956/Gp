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

    it("adding companion raises onRelationChanged event", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        var changedRelationArgs;
        family.onRelationChanged.attach(function (sender, args) {
            changedRelationArgs = args;
        });

        family.addCompanion({ familyId: 2, name: "cukes" });

        expect(changedRelationArgs).toEqual({ action: "added", family: { familyId: 2, name: "cukes" } });
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

    it("adding enemy raises onRelationChanged event", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        var changedRelationArgs;
        family.onRelationChanged.attach(function (sender, args) {
            changedRelationArgs = args;
        });

        family.addEnemy({ familyId: 2, name: "cukes" });

        expect(changedRelationArgs).toEqual({ action: "added", family: { familyId: 2, name: "cukes" }});
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

    it("removing a companion raises onRelationChanged event", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        var changedRelationArgs;

        family.onRelationChanged.attach(function (sender, args) {
            changedRelationArgs = args;
        });

        var companion = { familyId: 2, name: "cukes" };

        family.addCompanion(companion);

        family.removeCompanion(companion);

        expect(changedRelationArgs).toEqual({ action: "removed", family: { familyId: 2, name: "cukes" }});
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

    it("removing an enemy raises onRelationChanged event", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        var changedRelationArgs;

        family.onRelationChanged.attach(function (sender, args) {
            changedRelationArgs = args;
        });

        var enemy = { familyId: 2, name: "cukes" };

        family.addEnemy(enemy);

        family.removeEnemy(enemy);

        expect(changedRelationArgs).toEqual({ action: "removed", family: { familyId: 2, name: "cukes" } });
    });

    it("dirty flag is initially false when initially constructed from dto", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        expect(family.isDirty()).toBe(false);
    });

    it("adding companion marks family as dirty", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        family.addCompanion({ familyId: 2, name: "cukes" });

        expect(family.isDirty()).toBe(true);
    });

    it("adding enemy marks family as dirty", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        family.addEnemy({ familyId: 2, name: "cukes" });

        expect(family.isDirty()).toBe(true);
    });

    it("changing name marks family as dirty", function () {
        var dto = {
            familyId: 1,
            name: "lettuce",
        };

        var family = new gp.Family(dto);

        family.name("new name");

        expect(family.isDirty()).toBe(true);
    });

});