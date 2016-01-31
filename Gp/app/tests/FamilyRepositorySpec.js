/// <reference path="_references.js" />

describe("FamilyRepository", function () {
    var asyncTimeout = 100;

    it("is globally defined", function () {
        expect(gp.FamilyRepository).toBeDefined();
    });

    it("gets all families", function () {
        var request = new gp.ServerRequest();
        spyOn(request, "sendRequest").and.returnValue(test.a.promiseStub());

        var repo = new gp.FamilyRepository(request);

        var families = repo.getAll();

        expect(request.sendRequest).toHaveBeenCalledWith(
            "GET", "/api/Families");
    });

    it("saves modified family", function () {
        var request = new gp.ServerRequest();
        spyOn(request, "sendRequest").and.returnValue(test.a.promiseStub());

        var repo = new gp.FamilyRepository(request);

        var families = test.a.familyBuilder().withFamily("corn").buildKo();

        repo.save(families);

        expect(request.sendRequest).toHaveBeenCalledWith(
            "POST", "/api/Families", ko.toJSON([{
                name: "corn",
                familyId: 1,
                companions: [],
                enemies: []
            }]));
    });

    it("saving a family resets its dirty flag", function (done) {
        var request = new gp.ServerRequest();
        var promiseFake = test.a.promiseFake();
        spyOn(request, "sendRequest").and.returnValue(promiseFake.promise);

        var repo = new gp.FamilyRepository(request);

        var families = test.a.familyBuilder()
            .withFamily("corn")
            .buildKo();

        families[0].isDirty(true);

        repo.save(families);

        promiseFake.resolveNow({})
        .then(function () {
            expect(families[0].isDirty()).toBe(false);
            done();
        });

    }, asyncTimeout);

    it("gets all families returns promise", function () {
        var request = new gp.ServerRequest();
        spyOn(request, "sendRequest").and.returnValue(test.a.promiseStub());

        var repo = new gp.FamilyRepository(request);

        var promise = repo.getAll();

        expect(promise).toBeDefined();
        expect(promise.then).toBeDefined();
    });

    it("maps family into a knockout object", function (done) {
        var request = new gp.ServerRequest();

        var families = test.a.familyBuilder().withFamilyObject({
            familyId: 1,
            name: "lettuce",
        }).build();


        var successCallback;
        spyOn($, "ajax").and.callFake(function (options) {
            successCallback = options.success;
        });

        var repo = new gp.FamilyRepository(request);

        repo.getAll().then(function(data){
            expect(data).toBeDefined();
            expect(data.length).toBe(1);

            expect(data[0].name()).toBe("lettuce");
            done();
        });

        successCallback(families);
    }, asyncTimeout);

    it("maps family with companions", function (done) {
        var request = new gp.ServerRequest();

        var families = test.a.familyBuilder().withFamilyObject({
            familyId: 1,
            name: "lettuce",
            companions: [{
                familyId: 2,
                name: "spinach"
            }]
        }).build();


        var successCallback;
        spyOn($, "ajax").and.callFake(function (options) {
            successCallback = options.success;
        });

        var repo = new gp.FamilyRepository(request);

        repo.getAll().then(function (families) {
            expect(families[0].companions).toBeDefined();
            expect(families[0].companions()[0].name).toBe("spinach");
            done();
        });

        successCallback(families);

    }, asyncTimeout);

    it("maps family with enemies", function (done) {
        var request = new gp.ServerRequest();

        var families = test.a.familyBuilder().withFamilyObject({
            familyId: 1,
            name: "lettuce",
            enemies: [{
                familyId: 2,
                name: "spinach"
            }]
        }).build();


        var successCallback;
        spyOn($, "ajax").and.callFake(function (options) {
            successCallback = options.success;
        });

        var repo = new gp.FamilyRepository(request);

        repo.getAll().then(function (families) {
            expect(families[0].enemies).toBeDefined();
            expect(families[0].enemies()[0].name).toBe("spinach");
            done();
        });

        successCallback(families);

    }, asyncTimeout);

});