/// <reference path="_references.js" />
(function (gp) {
    "use strict";
    function Event(sender) {
        var _listeners = [];

        function attach(listener) {
            _listeners.push(listener);
        }

        function detach(listener) {
            _listeners.splice(_listeners.indexOf(listener), 1);
        }
        
        function notify(args) {
            var index;

            for (index = 0; index < _listeners.length; index += 1) {
                _listeners[index](sender, args);
            }
        }

        var publicApi = {
            attach: attach,
            detach: detach,
            notify: notify
        }

        return publicApi;
    }

    gp.Event = Event;

})(window.gp = window.gp || {});