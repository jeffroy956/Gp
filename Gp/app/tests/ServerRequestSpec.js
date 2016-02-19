/// <reference path="_references.js" />

describe("ServerRequest", function () {
    it("is globally defined", function () {
        expect(gp.ServerRequest).toBeDefined();
    });

    it("returns a promise when making request", function () {
        var request = new gp.ServerRequest();

        spyOn($, "ajax");

        var promise = request.sendRequest("GET", "/api/Families", {});

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

        var promise = request.sendRequest("GET", "/api/Families", {})
        .then(function (data) {
            expect(data).toEqual({});
            done();
        });

        successCallback({});
    });

    it("completes server request with an error", function (done) {
        var request = new gp.ServerRequest();

        var errorCallback;
        spyOn($, "ajax").and.callFake(function (options) {
            //error(xhr, errorType, error)
            errorCallback = options.error;
        });

        var promise = request.sendRequest("GET", "/api/Families", {})
        .catch(function (data) {
            expect(data).toEqual({
                xhr: {},
                errorType: "bad",
                error: "message"
            });
            done();
        });

        errorCallback({}, "bad", "message");
    });


    it("makes serverRequest with parameters", function () {
        var request = new gp.ServerRequest();

        var options;
        spyOn($, "ajax").and.callFake(function (parm) {
            options = parm;
        });

        request.sendRequest("PUT", "/someurl", { p1: "hi" })

        expect(options).toBeDefined();
        expect(options.url).toBe("/someurl");
        expect(options.type).toBe("PUT");
        expect(options.data).toEqual({ p1: "hi" });
    });

});