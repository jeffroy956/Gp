/// <reference path="../_references.js" />
(function (test) {
    "use strict";
    function FamilyBuilder() {

        var _families = [];

        this.withFamily = function (family) {
            _families.push(family);
            return this;
        };

        this.build = function () {
            return _families;
        };
    }

    test.FamilyBuilder = FamilyBuilder;

})(window.test = window.test || {});