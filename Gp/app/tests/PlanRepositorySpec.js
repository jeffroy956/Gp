﻿/// <reference path="_references.js" />

describe("PlanRepository", function () {
    var asyncTimeout = 100;

    it("is globally defined", function () {
        expect(gp.PlanRepository).toBeDefined();
    });

    it("requests all events for calendar", function () {
        var request = new gp.ServerRequest();
        spyOn(request, "sendRequest").and.returnValue(test.a.promiseStub());

        var repo = new gp.PlanRepository(request);

        var promise = repo.getCalendarPlans(2);

        expect(request.sendRequest).toHaveBeenCalledWith(
            "GET", "/api/Plans/Calendar/2");

        expect(promise).toBeDefined();
        expect(promise.then).toBeDefined();
    });

    it("returns a list of plans for calendar from server", function (done) {
        var request = new gp.ServerRequest();
        var serverPromise = test.a.promiseFake();
        spyOn(request, "sendRequest").and.returnValue(serverPromise.promise);

        var repo = new gp.PlanRepository(request);

        var promise = repo.getCalendarPlans(2);

        promise.then(function (data) {
            expect(data).toBeDefined();
            if (data) {
                expect(data.length).toBe(1);
            }
            done();
        });

        var planListDto = [];
        planListDto.push(test.a.planBuilder().withEventDescription("Plant onions").build());

        serverPromise.resolveNow(planListDto);
    }, asyncTimeout);

    xit("maps family with enemies", function (done) {
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