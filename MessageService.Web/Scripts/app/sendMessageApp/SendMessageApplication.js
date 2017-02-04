var mq = mq || {};

(function () {
    mq.SendMessageApplication = SendMessageApplication;

    function SendMessageApplication(options) {
        if (!(this instanceof SendMessageApplication))
            return new SendMessageApplication(options);
        this.client = new mq.MessageServiceClient();

        this.message = {
            messageType: ko.observable(null),
            recipients: ko.observable(''),
            text: ko.observable('')
        }

        this.error = ko.observable();
        this.busy = ko.observable(false);
        return this;
    }

    var ctor = SendMessageApplication;
    var proto = ctor.prototype;

    proto.clearMessage = function() {
        this.message
            .messageType(null)
            .recipients('')
            .text('');
    }
    proto.isValid = function() {
        var m = this.message;
        return m.messageType() && m.recipients() && m.text();
    }
    proto.sendMessage = function() {
        if (!this.isValid()) {
            return;
        }
        var payload = {
            messageType: this.message.messageType(),
            recipients: this.message.recipients(),
            text: this.message.text()
        }
        this.client.sendMessage(payload)
            .then(function() {
                this.clearMessage();
            }.bind(this))
            .withProgressFlag(this.busy)
            .catch(function(err) {
                this.error(err);
            }.bind(this));
    }
})();