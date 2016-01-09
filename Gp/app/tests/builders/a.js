/// <reference path="../_references.js" />
(function (test) {
    "use strict";
    function a() {

        this.familyBuilder = function () {
            return new test.FamilyBuilder();
        }
    }

    test.a = new a();

})(window.test = window.test || {});