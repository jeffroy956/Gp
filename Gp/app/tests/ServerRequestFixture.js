/// <reference path="_references.js" />

describe("ServerRequest", function () {
    it("is globally defined", function () {
        expect(gp.ServerRequest).toBeDefined();
    });

    it("makes a get request to server", function () {
        var request = new gp.ServerRequest();

        var options;
        spyOn($, "ajax").and.callFake(function (parm) {
            options = parm;
        });
        
        request.get("/api/Families");


        expect(options).toBeDefined();
        expect(options.url).toBe("/api/Families");
        expect(options.type).toBe("GET");
    });

    it("returns a promise when making request", function () {
        var request = new gp.ServerRequest();

        spyOn($, "ajax");

        var promise = request.get("/api/Families");

        expect(promise).toBeDefined();
        expect(promise.then).toBeDefined();
    });

    it("completing request resolves promise with success", function (done) {
        var request = new gp.ServerRequest();

        var successCallback;
        spyOn($, "ajax").and.callFake(function (options) {
            //success(data, status, xhr)
            successCallback = options.success;
        });

        var promise = request.get("/api/Families")
        .then(function (data) {
            expect(data).toEqual({});
            done();
        });

        successCallback({});


    });

    //error(xhr, errorType, error)
});