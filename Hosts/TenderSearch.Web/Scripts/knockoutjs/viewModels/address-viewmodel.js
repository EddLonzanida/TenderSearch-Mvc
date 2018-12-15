function AddressViewModel(option) {

    var hasError = ko.observable(false);
    var streetAddress = ko.observable(option.initialStreetAddress);

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

    var barangayOption = {
        initialDataAsText: option.initialDataForBarangay,
        fetchSuggestions: fetchSuggestions,
        apiUrl: `${option.baseUrl}Users/Address/BarangaySuggestions`
    };
    var personOption = {
        initialDataAsText: option.initialDataForPerson,
        fetchSuggestions: fetchSuggestions,
        baseUrl: option.baseUrl,
        apiUrl: `${option.baseUrl}Users/Address/PersonSuggestions`
    };

    var barangaySuggestionsViewModel = ko.observable(new BarangaySuggestionsViewModel(barangayOption));
    var personSuggestionsViewModel = ko.observable(new PersonSuggestionsViewModel(personOption));
    var completeAddress = ko.pureComputed(function () {

        var street = "";

        if (streetAddress()) {
            street = streetAddress();
        }

        var brgyVm = barangaySuggestionsViewModel();

        if (!brgyVm.selectedItem) {
            return street;
        }

        var item = brgyVm.selectedItem();

        if (!item.barangayName
            || !item.cityMunicipalityName
            || !item.provinceName
            || !item.regionName
            || !item.zipCode) {
            return street;
        }

        var brgy = item.barangayName();
        var cityMunicipality = item.cityMunicipalityName();
        var province = item.provinceName();
        var region = item.regionName();
        var zip = item.zipCode();

        return `${street}, Brgy. ${brgy}, ${cityMunicipality}, ${province}, ${region} ${zip}`;
    });

    var vm = {
        hasError: hasError,
        streetAddress: streetAddress,
        barangaySuggestionsViewModel: barangaySuggestionsViewModel,
        personSuggestionsViewModel: personSuggestionsViewModel,
        completeAddress: completeAddress
    }

    return vm;
}
