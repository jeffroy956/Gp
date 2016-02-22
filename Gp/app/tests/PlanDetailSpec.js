/// <reference path="_references.js" />
describe("PlanDetail", function () {
    it("is globally defined", function () {
        expect(gp.PlanDetail).toBeDefined();
    });

    it("constructs a new PlanDetail with simple observable properties mapped", function () {
        var planDetailDto = test.a.planBuilder()
        .withEventDescription("start tomatoes")
        .withPlanDate("3/15/2016")
        .withActualDate("3/16/2016")
        .build();

        var mappedDetail = new gp.PlanDetail(planDetailDto);

        expect(mappedDetail.eventDescription()).toBe("start tomatoes");
        expect(mappedDetail.planDate()).toBe("3/15/2016");
        expect(mappedDetail.actualDate()).toBe("3/16/2016");
    });

    //TODO: map varietyId and make variety object observable property on planDetail
    //TODO: flatten family name, companion, and enemy information into variety

});