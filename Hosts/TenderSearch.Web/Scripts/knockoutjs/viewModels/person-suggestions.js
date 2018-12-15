function PersonSuggestionsViewModel(option) {

    var initialDataAsText = JSON.parse(option.initialDataAsText);
    var initialData = ko.mapping.fromJS(initialDataAsText);
    var selectedItem = ko.observable(initialData);
    var itemsFromServer = ko.observableArray();

    function getApiClient() {
        ////TOD append OwnerType param
        //if (selectedItem()) {

        //    //Get from dropdown selection: ownertype
        //}
        return `${option.apiUrl}`;
    }

    function resetData(skipItemsFromServer) {

        selectedItem(initialData);

        if (skipItemsFromServer !== true) itemsFromServer([]);
    }

    function getJqueryUiData(ajaxData) {

        var uiData = $.map(ajaxData, function (value, key) {

            return {
                label: value.displayName,
                value: value.id
            };
        });

        return uiData;
    }

    function fetchSuggestions(request, callback) {

        var ajaxOption = {
            url: getApiClient(),
            data: { search: request.term },
            type: 'GET'
        };

        var fetchOption = {
            itemsFromServerAsObservable: itemsFromServer,
            ajaxOption: ajaxOption,
            resetData: resetData,
            getJqueryUiData: getJqueryUiData
        };

        return option.fetchSuggestions(fetchOption, callback);
    }

    // user clicked on a auto complete item
    function onItemSelected(event, ui) {

        $(event.target).val(ui.item.label);

        var id = ui.item.value;

        var firstItem = ko.utils.arrayFirst(itemsFromServer(), function (item) {
            return item["id"]() === id;
        });

        selectedItem(firstItem);

        return false;
    }

    var vm = {
        selectedItem: selectedItem,
        fetchSuggestions: fetchSuggestions,
        onItemSelected: onItemSelected
    }

    return vm;
}