/// <reference path="_references.js" />
describe("VarietyViewModel", function () {
    it("is globally defined", function () {
        expect(gp.VarietyViewModel).toBeDefined();
    });

    it("initializes a variety with a Family", function () {
        var families = test.a.familyBuilder()
            .withFamily("bush beans")
            .build();

        var variety = {
            name: "beans",
            family: families[0]
        }

        var vm = new gp.VarietyViewModel(variety, families);

        expect(vm.family().name).toBe("bush beans");
    });

    it("initializes a variety without a Family set", function () {
        var families = test.a.familyBuilder()
            .withFamily("bush beans")
            .build();

        var variety = {
            name: "beans"
        }

        var vm = new gp.VarietyViewModel(variety, families);

        expect(vm.family()).not.toBeDefined();
    });

    it("initializes availableFamilies", function () {
        var families = test.a.familyBuilder()
            .withFamily("bush beans")
            .withFamily("pole beans")
            .build();

        var variety = {
            name: "beans"
        }

        var vm = new gp.VarietyViewModel(variety, families);

        expect(vm.availableFamilies.length).toBe(2);
    });

    it("selects a new family for a variety", function () {
        var families = test.a.familyBuilder()
            .withFamily("bush beans")
            .withFamily("pole beans")
            .build();

        var variety = {
            name: "beans"
        }

        var vm = new gp.VarietyViewModel(variety, families);

        vm.selectFamily(families[1]);

        expect(vm.family().name).toBe("pole beans");
        expect(variety.family.name).toBe("pole beans");
    });
});