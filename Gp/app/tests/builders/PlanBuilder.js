/// <reference path="../_references.js" />
(function (test, gp, _) {
    "use strict";
    function PlanBuilder() {
        var _eventDescription, _variety, _planDate, _actualDate;
        
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

        this.withActualDate = function (actualDate) {
            _actualDate = actualDate;
            return this;
        }

        this.build = function () {
            return {
                eventDescription: _eventDescription,
                variety: _variety,
                planDate: _planDate,
                actualDate: _actualDate
            };
        }
    }

    test.a = test.a || {};
    test.a.planBuilder = function () {
        return new PlanBuilder();
    };

})(window.test = window.test || {}, gp, _);