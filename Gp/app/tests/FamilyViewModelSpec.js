/// <reference path="_references.js" />

describe("FamilyViewModel", function () {
    var asyncTimeout = 100;

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
    }, asyncTimeout);

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
    }, asyncTimeout);

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
    }, asyncTimeout);

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
    }, asyncTimeout);

    it("adding a new companion removes it from list of available relations", function (done) {
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

                vm.addCompanion(vm.families()[1]);

                expect(_.some(vm.availableRelations(), function (relation) {
                    return ko.unwrap(relation.name) === "spinach";
                })).toBe(false);

                done();
            });
    }, asyncTimeout);

    it("adding a new enemy removes it from list of available relations", function (done) {
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
                vm.addEnemy(vm.families()[1]);

                expect(_.some(vm.availableRelations(), function (relation) {
                    return ko.unwrap(relation.name) === "spinach";
                })).toBe(false);

                done();
            });
    }, asyncTimeout);

    it("removing a companion adds it back into list of available relations", function (done) {
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

                vm.addCompanion(vm.families()[1]);
                vm.removeCompanion(vm.families()[1]);

                expect(_.some(vm.availableRelations(), function (relation) {
                    return ko.unwrap(relation.name) === "spinach";
                })).toBe(true);

                done();
            });
    }, asyncTimeout);

    it("removing a companion adds it back into list of available relations at first index", function (done) {
        var repo = new gp.FamilyRepository();

        var repoPromise = test.a.promiseFake();
        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        repoPromise.resolveNow(
            test.a.familyBuilder()
            .withFamily("alfalfa")
            .withFamily("beans")
            .withFamily("corn")
            .withFamily("spinach")
            .buildKo(),
            function () {
                vm.selectFamily(vm.families()[1]);

                vm.addCompanion(vm.families()[0]);
                vm.removeCompanion(vm.families()[0]);

                expect(ko.unwrap(vm.availableRelations()[0].name)).toBe("alfalfa");

                done();
            });
    }, asyncTimeout);

    it("removing a companion adds it back into list of available relations at last index", function (done) {
        var repo = new gp.FamilyRepository();

        var repoPromise = test.a.promiseFake();
        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        repoPromise.resolveNow(
            test.a.familyBuilder()
            .withFamily("alfalfa")
            .withFamily("beans")
            .withFamily("corn")
            .withFamily("spinach")
            .buildKo(),
            function () {
                vm.selectFamily(vm.families()[0]);

                vm.addCompanion(vm.families()[3]);
                vm.removeCompanion(vm.families()[3]);

                expect(ko.unwrap(vm.availableRelations()[2].name)).toBe("spinach");

                done();
            });
    }, asyncTimeout);

    it("removing a companion adds it back into list of available relations in middle", function (done) {
        var repo = new gp.FamilyRepository();

        var repoPromise = test.a.promiseFake();
        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        repoPromise.resolveNow(
            test.a.familyBuilder()
            .withFamily("alfalfa")
            .withFamily("beans")
            .withFamily("corn")
            .withFamily("spinach")
            .buildKo(),
            function () {
                vm.selectFamily(vm.families()[0]);

                vm.addCompanion(vm.families()[2]);
                vm.removeCompanion(vm.families()[2]);

                expect(ko.unwrap(vm.availableRelations()[1].name)).toBe("corn");

                done();
            });
    }, asyncTimeout);

    it("removing an enemy adds it back into list of available relations", function (done) {
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

                vm.addEnemy(vm.families()[1]);
                vm.removeEnemy(vm.families()[1]);

                expect(_.some(vm.availableRelations(), function (relation) {
                    return ko.unwrap(relation.name) === "spinach";
                })).toBe(true);

                done();
            });
    }, asyncTimeout);

});