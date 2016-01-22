/// <reference path="../_references.js" />
(function (test) {
    "use strict";
    function a() {

        this.familyBuilder = function () {
            return new test.FamilyBuilder();
        }
    }

    test.a = new a();

    test.a.promiseStub = function () {
        return new Promise(function (resolve, reject) {
        });
    }

    test.a.promiseFake = function () {
        var resolveHandler, rejectHandler;
        var promise = new Promise(function (resolve, reject) {
            resolveHandler = resolve;
            rejectHandler = reject;
        });

        function resolveNow(data, done) {
            Promise.all([promise])
                .then(function () {
                    done();
                });

            resolveHandler(data);
        }
        
        function rejectNow(data, done) {
            Promise.all([promise])
                .catch(function () {
                    done();
                });

            rejectHandler(data);
        }

        return {
            resolve: resolveHandler,
            reject: rejectHandler,
            resolveNow: resolveNow,
            rejectNow: rejectNow,
            promise: promise
        }
    }

})(window.test = window.test || {});