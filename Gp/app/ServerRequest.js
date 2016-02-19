/// <reference path="_references.js" />
(function (gp, $) {
    "use strict";

    function ServerRequest() {

        function sendRequest(verb, url, data) {
            var promise = new Promise(function (resolve, reject) {

                $.ajax({
                    type: verb,
                    url: url,
                    data: data,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
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
            sendRequest: sendRequest
        };

        return publicApi;
    }



    gp.ServerRequest = ServerRequest;

})(window.gp = window.gp || {}, Zepto);