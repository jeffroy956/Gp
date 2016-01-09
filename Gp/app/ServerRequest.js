/// <reference path="_references.js" />
(function (gp, $) {
    "use strict";

    function ServerRequest() {
        function get(url, data) {
            return sendRequest("GET", url, data);
        }

        function post(url, data) {
            return sendRequest("POST", url, data);
        }

        function sendRequest(verb, url, data) {
            var promise = new Promise(function (resolve, reject) {

                $.ajax({
                    type: verb,
                    url: url,
                    data: data,
                    success: function (data) {
                        resolve(data);
                    },
                    error: function (xhr, errorType, error) {
                        reject({
                            xhr: xhr,
                            errorType: errorType,
                            error: error
                        });
                    }
                });

            });

            return promise;
        }

        var publicApi = {
            get: get,
            post: post,
            sendRequest: sendRequest
        };

        return publicApi;
    }



    gp.ServerRequest = ServerRequest;

})(window.gp = window.gp || {}, Zepto);