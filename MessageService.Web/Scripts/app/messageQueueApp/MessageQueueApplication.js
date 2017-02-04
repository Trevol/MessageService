var mq = mq || {};

(function () {
    mq.MessageQueueApplication = MessageQueueApplication;

    function MessageQueueApplication(options) {
        if (!(this instanceof MessageQueueApplication))
            return new MessageQueueApplication(options);
        this.client = new mq.MessageServiceClient();

        this.queue = ko.observableArray();
        this.selectedMessage = ko.observable();

        this.queueError = ko.observable();
        this.queueStepError = ko.observable();

        this.busy = ko.observable(false);

        this.loadQueue();
        return this;
    }

    var ctor = MessageQueueApplication;
    var proto = ctor.prototype;

    proto.loadQueue = function () {
        this.queueError('').queue([]).selectedMessage(null);
        this.client.queue(10)

            .then(function (messages) {
                this.queue(messages);
            }.bind(this))

            .withProgressFlag(this.busy)

            .catch(function (err) {
                this.queueError(err);
                console.log(err);
            }.bind(this));
    }

    proto.queueStep = function () {
        this.client.queueStep()

            .then(function (attempt) {
                processAttempt(attempt);
                return this.loadQueue(10);
            }.bind(this))

            .withProgressFlag(this.busy)

            .catch(function(err) {
                //debugger;
                this.queueStepError(err);
                console.log(err);
            }.bind(this));
    }
    function processAttempt(attempt) {
        if (!attempt) {
            throw 'No attempt returned. May be queue is empty or message send prevented by policy';
        }
        if (!attempt.success) {
            throw attempt.errorInfo;
        }
    }
})();