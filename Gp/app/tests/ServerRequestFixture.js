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

    it("completes server request with an error", function (done) {
        var request = new gp.ServerRequest();

        var errorCallback;
        spyOn($, "ajax").and.callFake(function (options) {
            //error(xhr, errorType, error)
            errorCallback = options.error;
        });

        var promise = request.get("/api/Families")
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

    it("makes a get request with parameters", function () {
        var request = new gp.ServerRequest();
        var options;
        spyOn($, "ajax").and.callFake(function (parm) {
            options = parm;
        });

        request.get("/someurl", { p1: "hi" });

        expect(options.data).toEqual({ p1: "hi" });
    });

    it("posts to server", function () {
        var request = new gp.ServerRequest();

        var options;
        spyOn($, "ajax").and.callFake(function (parm) {
            options = parm;
        });

        request.post("/someUrl", { p1: "hi" });

        expect(options).toBeDefined();
        expect(options.url).toBe("/someUrl");
        expect(options.type).toBe("POST");
    });
    
    it("posts to server", function () {
        var request = new gp.ServerRequest();

        var options;
        spyOn($, "ajax").and.callFake(function (parm) {
            options = parm;
        });

        request.post("/someUrl", { p1: "hi" });

        expect(options).toBeDefined();
        expect(options.url).toBe("/someUrl");
        expect(options.type).toBe("POST");
        expect(options.data).toEqual({ p1: "hi" });
    });

    it("uses general sendRequest to PUT to server", function () {
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