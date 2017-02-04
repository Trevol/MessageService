(function() {
    //jQuery ajax config
    $.ajaxSetup({ cache: false });

    //Q config
    Q.makePromise.prototype.withProgressFlag = function (progressFlag) {
        if (!ko.isObservable(progressFlag)) {
            return this;
        }
        progressFlag(true);
        return this.finally(function () {
            progressFlag(false);
        });
    };

    //ko config
    ko.options.deferUpdates = true;
    ko.punches.enableAll();
    ko.unwrapOrInvoke = function (value, target) {
        if (_(value).isFunction()) {
            return value.call(target);
        }
        return value;
    };
})();