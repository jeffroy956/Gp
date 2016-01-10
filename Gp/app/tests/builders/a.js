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

        function waitFor(predicate, done) {
            var maxWait = 25;
            var numWaits = 0;
            function keepWaiting() {
                setTimeout(function () {
                    if (numWaits < maxWait && !predicate()) {
                        numWaits++;
                        keepWaiting();
                    }
                    else {
                        done();
                    }
                }, 1);
            }

            keepWaiting();
        }

        return {
            resolve: resolveHandler,
            reject: rejectHandler,
            promise: promise,
            waitFor: waitFor
        }
    }

})(window.test = window.test || {});