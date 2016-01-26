/// <reference path="_references.js" />
describe("Event", function () {
    it("is globally defined", function () {
        expect(gp.Event).toBeDefined();
    });

    it("notifies of change", function () {
        var event = new gp.Event({
            name: "me"
        });

        var handledSender, handledArgs;

        event.attach(function (sender, args) {
            handledSender = sender;
            handledArgs = args;
        });

        event.notify({
            arg: "arg!!"
        });

        expect(handledSender).toEqual({
            name: "me"
        });

        expect(handledArgs).toEqual({
            arg: "arg!!"
        });
    });

    it("notifies multiple listeners", function () {
        var event = new gp.Event({
            name: "me"
        });

        var listener1Called = false;
        function listener1(sender, args) {
            listener1Called = true;
        }

        var listener2Called = false;
        function listener2(sender, args) {
            listener2Called = true;
        }

        event.attach(listener1);
        event.attach(listener2);

        event.notify();

        expect(listener1Called).toBe(true);
        expect(listener2Called).toBe(true);
    });

    it("detaches an event", function () {
        var event = new gp.Event({
            name: "me"
        });

        var listener1Called = false;
        function listener1(sender, args) {
            listener1Called = true;
        }

        var listener2Called = false;
        function listener2(sender, args) {
            listener2Called = true;
        }

        event.attach(listener1);
        event.attach(listener2);

        event.detach(listener1);

        event.notify();

        expect(listener1Called).toBe(false);
        expect(listener2Called).toBe(true);
    });
});