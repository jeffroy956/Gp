﻿/// <reference path="../_references.js" />
describe("PlanBuilder", function () {
    it("is globally defined", function () {
        expect(test.a.planBuilder).toBeDefined();
    });

    it("creates a plan entry", function () {
        var family = {
            familyId: 1,
            name: "lettuce"
        };
        
        var plan = test.a.planBuilder()
        .withEventDescription("Start tomato seeds")
        .withVariety({
            varietyId: 1,
            name: "tomato"
        })
        .withPlanDate("2/5/2016")
        .withActualDate("2/6/2016")
        .build();

        expect(plan).toEqual({
            eventDescription: "Start tomato seeds",
            variety: {
                varietyId: 1,
                name: "tomato"
            },
            planDate: "2/5/2016",
            actualDate: "2/6/2016"
        });
    });

});