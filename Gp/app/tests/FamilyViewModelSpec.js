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

        repoPromise.resolve([
        {
            familyId: 1,
            name: "beans"
        }
        ]);

        repoPromise.waitFor(function () {
            return vm.families().length === 1;
        }, function () {
            expect(vm.families().length).toBe(1);
            done();
        });
    });

});