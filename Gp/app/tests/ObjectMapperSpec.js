/// <reference path="_references.js" />
describe("ObjectMapper", function () {
    it("is globally defined", function () {
        expect(gp.ObjectMapper).toBeDefined();
    });

    it("maps plan dto to an observable collection", function () {
        var plans = [];

        plans.push(
            test.a.planBuilder()
            .withEventDescription("plant corn")
            );

        var mapper = new gp.ObjectMapper();
        var observablePlans = mapper.mapPlans(plans);

        expect(observablePlans().length).toBe(1);
    });
});