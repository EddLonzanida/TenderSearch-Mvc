function DependentViewModel(option) {

    var hasError = ko.observable(false);

    function fetchSuggestions(fetchOption, callback) {

        hasError(false);

        return $.ajax(fetchOption.ajaxOption)
            .done(function (ajaxData) {

                ko.mapping.fromJS(ajaxData, {}, fetchOption.itemsFromServerAsObservable);

                var jQueryUiData = fetchOption.getJqueryUiData(ajaxData);

                callback(jQueryUiData);

                return ajaxData;

            }).fail(function (err, status) {

                console.warn("===getApiData ERROR");
                console.warn(err);
                fetchOption.resetData();
                hasError(true);

            });
    }

    var personOption = {
        initialDataAsText: option.initialDataForPerson,
        fetchSuggestions: fetchSuggestions,
        baseUrl: option.baseUrl,
        apiUrl: `${option.baseUrl}Users/Dependent/PersonSuggestions`
    };
    var personSuggestionsViewModel = ko.observable(new PersonSuggestionsViewModel(personOption));

    var vm = {
        hasError: hasError,
        personSuggestionsViewModel: personSuggestionsViewModel,
    }

    return vm;
}
