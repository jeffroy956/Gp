/// <reference path="_references.js" />
(function (gp, $) {
    function ServerRequest() {
        function get(url) {
            return sendRequest("GET", url);
        }

        function sendRequest(method, url) {
            var promise = new Promise(function (resolve, reject) {

                $.ajax({
                    type: method,
                    url: url,
                    success: function (data) {
                        resolve(data);
                    }
                });

            });

            return promise;
        }

        var publicApi = {
            get: get
        }

        return publicApi;
    }



    gp.ServerRequest = ServerRequest;

})(window.gp = window.gp || {}, Zepto);