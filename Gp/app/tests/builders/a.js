/// <reference path="../_references.js" />
(function (test) {
    "use strict";
    test.a = test.a || {};

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
            var promiseAll = Promise.all([promise]);

            if (done) {
                promiseAll
                    .then(function () {
                        done();
                    });
            }

            resolveHandler(data);

            return promiseAll;
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

    test.a.defaultCalendars = function () {
        return [
            {
                calendarId: 2,
                description: "2016 Calendar"
            },
            {
                calendarId: 1,
                description: "2015 Calendar"
            },
        ];
    };

    test.a.serverRequestStub = function () {
        function sendRequest(verb, url, data) {
            return test.a.promiseStub();
        }

        return {
            sendRequest: sendRequest
        };
    }

})(window.test = window.test || {});