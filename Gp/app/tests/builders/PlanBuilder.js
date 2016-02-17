/// <reference path="../_references.js" />
(function (test, gp, _) {
    "use strict";
    function PlanBuilder() {
        var _eventDescription, _variety, _planDate;
        
        this.withEventDescription = function (description) {
            _eventDescription = description;
            return this;
        }

        this.withVariety = function (variety) {
            _variety = variety;
            return this;
        }

        this.withPlanDate = function (planDate) {
            _planDate = planDate;
            return this;
        }

        this.build = function () {
            return {
                eventDescription: _eventDescription,
                variety: _variety,
                planDate: _planDate
            };
        }
    }

    test.PlanBuilder = function () {
        return new PlanBuilder();
    };

})(window.test = window.test || {}, gp, _);