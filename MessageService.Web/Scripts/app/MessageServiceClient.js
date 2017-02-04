var mq = mq || {};

(function () {
    mq.MessageServiceClient = MessageServiceClient;

    function MessageServiceClient(controller) {
        if (!(this instanceof MessageServiceClient))
            return new MessageServiceClient(controller);
        this.controllerUri = mq.env.appRoot + 'api/' + (controller || 'mqclient') + '/';
        return this;
    }

    var ctor = MessageServiceClient;
    var proto = ctor.prototype;

    proto.queue = function (take, skip) {
        return get(this.actionUri('queue'), { take: take, skip: skip || null });
    }
    proto.queueStep = function () {
        return get(this.actionUri('queueStep'));
    }
    proto.sendMessage = function (textMessage) {
        return post(this.actionUri('sendMessage'), textMessage);
    }
    proto.actionUri = function (action) {
        return this.controllerUri + action;
    }

    function get(uri, data) {
        return ajax(uri, 'GET', data);
    }
    function post(uri, data) {
        return ajax(uri, 'POST', data && JSON.stringify(data)||null);
    }
    function ajax(uri, method, data) {
        var jqXhr = $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data || null
        });
        return Q(jqXhr);
        /*
        ? JSON.stringify(data)
        .fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        })
        */
    }
})();