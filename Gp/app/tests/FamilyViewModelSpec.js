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
            name: ko.observable("beans")
        }
        ]);

        repoPromise.waitFor(function () {
            return vm.families().length === 1;
        }, function () {
            expect(vm.families().length).toBe(1);
            done();
        });
    });

    it("availableCompanions for family does not contain self", function (done) {
        var repo = new gp.FamilyRepository();
        var repoPromise = test.a.promiseFake();

        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        repoPromise.resolve(
            [
                {
                    familyId: 1,
                    name: ko.observable("beans"),
                    companions: ko.observableArray(),
                    enemies: ko.observableArray()
                },
                {
                    familyId: 2,
                    name: ko.observable("spinach"),
                    companions: ko.observableArray(),
                    enemies: ko.observableArray()
                }
            ]);


        repoPromise.waitFor(function () {
            return vm.families().length > 0;
        }, function () {
            vm.selectFamily(vm.families()[0]);

            expect(vm.availableCompanions).toBeDefined();
            expect(vm.availableCompanions().length).toBe(1);
            done();
        });
    });

    it("availableCompanions for family does not contain already selected family", function (done) {
        var repo = new gp.FamilyRepository();
        var repoPromise = test.a.promiseFake();

        spyOn(repo, "getAll").and.returnValue(repoPromise.promise);

        var vm = new gp.FamilyViewModel(repo);

        repoPromise.resolve(
            [
                {
                    familyId: 1,
                    name: ko.observable("beans"),
                    companions: ko.observableArray([
                        {
                            familyId: 2,
                            name: "spinach"
                        }
                        ]),
                    enemies: ko.observableArray()
                },
                {
                    familyId: 2,
                    name: ko.observable("spinach"),
                    companions: ko.observableArray(),
                    enemies: ko.observableArray()
                },
                {
                    familyId: 3,
                    name: ko.observable("carrot"),
                    companions: ko.observableArray(),
                    enemies: ko.observableArray()
                },
            ]);


        repoPromise.waitFor(function () {
            return vm.families().length > 0;
        }, function () {
            vm.selectFamily(vm.families()[0]);

            expect(vm.availableCompanions).toBeDefined();
            expect(ko.unwrap(vm.availableCompanions()[0].name)).toBe("carrot");
            done();
        });

    });

});