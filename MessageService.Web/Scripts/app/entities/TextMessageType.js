var mq = mq || {};

(function() {
    mq.TextMessageType = {
        1: 'Email',
        2: 'Sms',
        3: 'Push',

        Email: 1,
        Sms: 2,
        Push: 3
    };

    mq.TextMessageType.options = function() {
        return [
            { key: 'Email', value: 1 },
            { key: 'Sms', value: 2 },
            { key: 'Push', value: 3 }
        ];
    }
})();