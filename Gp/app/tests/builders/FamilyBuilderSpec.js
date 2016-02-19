/// <reference path="../_references.js" />
describe("FamilyBuilder", function () {
    it("is globally defined", function () {
        expect(test.a.familyBuilder).toBeDefined();
    });

    it("creates a family", function () {
        var family = {
            familyId: 1,
            name: "lettuce"
        };

        expect(test.a.familyBuilder().withFamilyObject(family).build()).toEqual([{
            familyId: 1,
            name: "lettuce"
        }]);
    });

});