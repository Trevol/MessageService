(function() {
    ko.bindingHandlers.busy = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var $element = $(element);
            var $layer = createLayer($element);
                        
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                removeLayer($element, $layer);                
            });
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var $layer = getLayer($(element));
            var busy = ko.unwrapOrInvoke(valueAccessor(), viewModel);
            if (busy) {
                $layer.show();
            } else {
                $layer.hide();
            }
        }
    };

    var layerElementKey = '__data-busy-effect-element';

    function createLayer($element) {
        var $layer = $('<div/>').addClass('busy-effect').appendTo($element);
        $element.data(layerElementKey, $layer);
        return $layer;
    }
    function getLayer($element) {
        return $element.data(layerElementKey);
    }
    function removeLayer($element, $layer) {
        $layer.remove();
        $element.removeData(layerElementKey);
    }
})()