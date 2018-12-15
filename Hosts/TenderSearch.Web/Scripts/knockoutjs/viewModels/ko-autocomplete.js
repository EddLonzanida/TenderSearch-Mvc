
ko.bindingHandlers.autocomplete = {
    init: function (element, params) {

        $(element).autocomplete(params());
        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).autocomplete("destroy");
        });

    },
    update: function (element, params) {

        $(element).autocomplete("option", "source", params().source);

    }
};

