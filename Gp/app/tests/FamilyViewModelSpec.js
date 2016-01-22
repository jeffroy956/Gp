/// <reference path="_references.js" />

describe("FamilyViewModel", function () {
    it("is globally defined", function () {
        expect(gp.FamilyViewModel).toBeDefined();
    });

    it("requests all families when constructed", function () {

        var repo = new gp.FamilyRepository();
        spyOn(repo, "getAll").and.returnValue(test.a.promiseStub());

        var vm = new gp.FamilyViewModel(repo);

        expect(repo.getAll).toHaveBeenCalled();
    });

    it("populates list of families when constructed", function (done) {
        var repo = new gp.FamilyRepository();
        var repoPromise = test.a.promiseFake();

        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        repoPromise.resolveNow(
            test.a.familyBuilder().withFamily("beans").buildKo(),
            function () {
                expect(vm.families().length).toBe(1);
                done();
            });
    });

    it("availableRelations for family does not contain self", function (done) {
        var repo = new gp.FamilyRepository();
        var repoPromise = test.a.promiseFake();

        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        repoPromise.resolveNow(
            test.a.familyBuilder()
            .withFamily("beans")
            .withFamily("spinach")
            .buildKo(),
            function () {
                vm.selectFamily(vm.families()[0]);

                expect(vm.availableRelations().length).toBe(1);
                done();
            });
    });

    it("availableRelations for family does not contain already selected companions", function (done) {
        var repo = new gp.FamilyRepository();
        var repoPromise = test.a.promiseFake();

        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        repoPromise.resolveNow(
            test.a.familyBuilder()
            .withFamily("beans")
            .withFamily("spinach")
            .withFamily("carrot")
            .linkCompanion("beans", "spinach")
            .buildKo(),
            function () {
                vm.selectFamily(vm.families()[0]);

                expect(ko.unwrap(vm.availableRelations()[0].name)).toBe("carrot");
                done();
            });
    });

    it("availableRelations for family does not contain already selected enemies", function (done) {
        var repo = new gp.FamilyRepository();
        var repoPromise = test.a.promiseFake();

        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        repoPromise.resolveNow(
            test.a.familyBuilder()
            .withFamily("beans")
            .withFamily("spinach")
            .withFamily("carrot")
            .linkEnemy("beans", "spinach")
            .buildKo(),
            function () {
                vm.selectFamily(vm.families()[0]);

                expect(ko.unwrap(vm.availableRelations()[0].name)).toBe("carrot");
                done();
            });
    });


    it("adds a new companion to current family", function (done) {
        var repo = new gp.FamilyRepository();

        var repoPromise = test.a.promiseFake();
        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        repoPromise.resolveNow(
            test.a.familyBuilder()
            .withFamily("beans")
            .withFamily("spinach")
            .buildKo(),
            function () {
                vm.selectFamily(vm.families()[0]);

                vm.addCompanionToCurrentFamily(vm.families()[1]);

                expect(vm.currentFamily().companions().length).toBe(1);

                done();
            });
    });

    it("adding a new companion removes it from list of available companions", function (done) {
        var repo = new gp.FamilyRepository();

        var repoPromise = test.a.promiseFake();
        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        repoPromise.resolveNow(
            test.a.familyBuilder()
            .withFamily("beans")
            .withFamily("spinach")
            .buildKo(),
            function () {
                vm.selectFamily(vm.families()[0]);

                vm.addCompanionToCurrentFamily(vm.families()[1]);

                expect(_.some(vm.availableRelations(), function (companion) {
                    return ko.unwrap(companion.name) === "spinach";
                })).toBe(false);

                done();
            });
    });

    it("adds a new enemy to current family", function (done) {
        var repo = new gp.FamilyRepository();

        var repoPromise = test.a.promiseFake();
        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        repoPromise.resolveNow(
            test.a.familyBuilder()
            .withFamily("beans")
            .withFamily("spinach")
            .buildKo(),
            function () {
                vm.selectFamily(vm.families()[0]);

                vm.addEnemyToCurrentFamily(vm.families()[1]);

                expect(vm.currentFamily().enemies().length).toBe(1);

                done();
            });
    });

    it("adding a new companion removes it from list of available companions", function (done) {
        var repo = new gp.FamilyRepository();

        var repoPromise = test.a.promiseFake();
        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        repoPromise.resolveNow(
            test.a.familyBuilder()
            .withFamily("beans")
            .withFamily("spinach")
            .buildKo(),
            function () {
                vm.selectFamily(vm.families()[0]);

                vm.addEnemyToCurrentFamily(vm.families()[1]);

                expect(_.some(vm.availableRelations(), function (companion) {
                    return ko.unwrap(companion.name) === "spinach";
                })).toBe(false);

                done();
            });
    });

});