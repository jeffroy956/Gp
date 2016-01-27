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

    it("selecting a family attaches onRelationChanged event", function (done) {
        var repo = new gp.FamilyRepository();

        var repoPromise = test.a.promiseFake();
        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        var families = test.a.familyBuilder()
            .withFamily("beans")
            .withFamily("spinach")
            .buildKo();

        repoPromise.resolveNow(
            families,
            function () {
                spyOn(families[0].onRelationChanged, "attach");
                vm.selectFamily(vm.families()[0]);
                expect(families[0].onRelationChanged.attach).toHaveBeenCalled();

                done();
            });
    }, asyncTimeout);

    it("selecting another family dettaches the onRelationChanged event", function (done) {
        var repo = new gp.FamilyRepository();

        var repoPromise = test.a.promiseFake();
        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        var families = test.a.familyBuilder()
            .withFamily("beans")
            .withFamily("spinach")
            .buildKo();

        repoPromise.resolveNow(
            families,
            function () {
                spyOn(families[0].onRelationChanged, "detach");
                vm.selectFamily(vm.families()[0]);
                vm.selectFamily(vm.families()[1]);
                expect(families[0].onRelationChanged.detach).toHaveBeenCalled();

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