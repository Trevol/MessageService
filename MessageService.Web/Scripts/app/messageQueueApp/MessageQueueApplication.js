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

            .catch(function (err) {
                this.queueError(err);
                console.log(err);
            }.bind(this));
    }

    proto.queueStep = function () {
        this.client.queueStep()

            .then(function (attempt) {

                return this.loadQueue(10);
            }.bind(this))

            .catch(function(err) {
                this.queueStepError(err);
                console.log(err);
            }.bind(this));
    }
    function processAttempt(attempt) {
        if (!attempt) {
            throw new Error('No attempt returned/ May be queue is empty');
        }
        if (!attempt.success) {
            throw new Error(attempt.errorInfo);
        }
    }
})();