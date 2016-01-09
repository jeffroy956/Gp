/// <reference path="../_references.js" />
describe("FamilyBuilder", function () {
    it("is globally defined", function () {
        expect(test.FamilyBuilder).toBeDefined();
    });

    it("creates a family", function () {
        var family = {
            familyId: 1,
            name: "lettuce"
        };

        var familyBuilder = new test.FamilyBuilder();

        expect(familyBuilder.withFamily(family).build()).toEqual([{
            familyId: 1,
            name: "lettuce"
        }]);
    });

});