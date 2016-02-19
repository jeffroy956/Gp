/// <reference path="_references.js" />
describe("PlanListViewModel", function () {
    it("is globally defined", function () {
        expect(gp.PlanViewModel).toBeDefined();
    });



    it("requests plans for default calendar on initialization", function () {
        var planRepo = new gp.PlanRepository(test.a.serverRequestStub());
        
        spyOn(planRepo, "getCalendarPlans");

        var vm = new gp.PlanViewModel(planRepo, test.a.defaultCalendars());

        expect(planRepo.getCalendarPlans).toHaveBeenCalledWith(2);
    });

    it("defines calendarPlans observable collection", function () {
        var planRepo = new gp.PlanRepository(test.a.serverRequestStub());

        var vm = new gp.PlanViewModel(planRepo, test.a.defaultCalendars());
        expect(vm.calendarPlans).toBeDefined();
        expect(vm.calendarPlans().length).toBe(0);
    });

    xit("maps list of plan for default calendar on initialization", function () {
        var planRepo = new gp.PlanRepository(test.a.serverRequestStub());
        var calendars = [
            {
                calendarId: 2,
                description: "2016 Calendar"
            },
            {
                calendarId: 1,
                description: "2015 Calendar"
            },
        ];

        spyOn(planRepo, "getCalendarPlans");

        var vm = new gp.PlanViewModel(planRepo, calendars);

        var plans = [];

        test.a.planBuilder().withEventDescription("first event");



        expect(planRepo.getCalendarPlans).toHaveBeenCalledWith(2);
    });
});