var currentMethod = "";
var CLIENT = $(function () {

    //#region utility methods
    var removePager = function (newHtml) {

        var myRegexp = /[\S\s]*<tbody.*>([\S\s]*)<\/tbody>[\S\s]*/ig;
        var tbody = newHtml.replace(myRegexp, "$1");

        return tbody;
    };

    var getPager = function (newHtml) {

        var myRegexp = /[\S\s]*(<div.+class="pagedList"[\S\s]*)/ig;
        var pager = newHtml.replace(myRegexp, "$1");

        return pager;
    };

    var showErrorMessage = function (currentModal, errMsg) {

        try {

            var errorMessageModalId = "#errorMessageModal";

            $(errorMessageModalId).draggable({
                handle: ".modal-header"
            });

            if (currentModal) {

                resetModalBody(modalId);

            }

            var newMsg = errMsg;

            //check for destructive msgs
            if (newMsg.indexOf("<html") > 0) {

                newMsg = newMsg.replace(/\<style[\s\S]*style\>/gim, ""); //remove buildin styling and use the default bootstrap

                var searchStringStart = "<title>";
                var searchStringEnd = "</title>";
                var positionStart = newMsg.indexOf(searchStringStart);
                var positionEnd = newMsg.indexOf(searchStringEnd);

                newMsg = newMsg.substring(positionStart + searchStringStart.length, positionEnd);

            }

            $(errorMessageModalId).find(".modal-body").first().html(newMsg);
            $(errorMessageModalId).modal("show");

        } catch (e) {

            console.warn(`Error in method: showErrorMessage - ${e.message}\n\n\n\nOriginal error:\n${errMsg}`);
            alert(`Error in method: showErrorMessage - ${e.message}`);
        }
    }
    var showPleaseWaitModal = function () {

        var modalId = "#pleaseWaitModal";

        $(modalId).modal("show");

    }

    var disableSortButtons = function (targetTbody) {

        var $sortButtons = targetTbody.parents(targetTableSelector).parent("td").siblings().find(sortTargetSelector);

        $sortButtons.addClass("disabled");
        $sortButtons.find("i").removeClass("animated pulse infinite");

    }

    var updatePageNumber = function (targetTbody, pageNumber) {

        targetTbody.parents(targetTableSelector).parent("td").siblings().find(sortRoleHandlerSelector).find("#page").val(pageNumber);

    }

    var selectRow = function (td) {

        var selectionColor = "warning";

        td.closest("tbody").find("tr").removeClass(selectionColor);
        td.closest("tr").addClass(selectionColor);

        var $sortButtons = td.parents(targetTableSelector).parent("td").siblings().find(sortDirectionSelector);

        $sortButtons.removeClass("disabled");
        $sortButtons.find("i").addClass("animated pulse");
    }

    var rowSelectHandler = function () {

        selectRow($(this));

    }

    var setBusyIndicatorBig = function (modalId, targetElement) {

        if (!targetElement) targetElement = modalBodyId;

        var $busyIndicator = $("#busyIndicator");
        var $modalBody = $(modalId).find(targetElement);

        $modalBody.html($busyIndicator.html());
    }

    var updateRowDeleteCounter = function (item) {

        var rowLabel = " row(s) found.";
        var $rowCounter = $(item).parents().eq(11).find(`span:contains('${rowLabel}')`);

        if ($rowCounter.length > 0) {

            $rowCounter = $rowCounter.first();
            $rowCounter.removeClass("animated rubberBand  ");

            var rowCounterHtml = $rowCounter.html();
            var counter = rowCounterHtml.replace(rowLabel, "").trim();

            counter = parseFloat(counter);
            counter--;
            $rowCounter.html(counter + rowLabel);

            $rowCounter.addClass("animated rubberBand  ");

        } else console.warn("=====$rowCounter not updated... ");
    }

    var isKnockoutLoaded = function () {

        try {

            if (ko) return true;

            return false;

        } catch (e) {

            return false;

        }
    }

    var resetModalBody = function (modalId) {

        if (isKnockoutLoaded()) {

            var $layoutCreateEditContent = $("#LayoutCreateEditContent");

            if ($layoutCreateEditContent.length > 0) {

                ko.cleanNode($layoutCreateEditContent);

            }
        }

        $(modalId).modal("hide");
        $(modalId).find(modalBodyId).html("");

    }

    //#endregion // utility methods

    //#region Copy
    var ajaxCopy = function () {

        var $form = $(this);

        if (!$form.valid()) return false;

        var modalId = "#copyItemModal";
        var url = $form.attr("action");
        var options = {
            url: url,
            type: $form.attr("method"),
            data: $form.serialize() //important!!! this will prevent error: -> The required anti-forgery form field "__RequestVerificationToken" is not present
        };

        $("#busyindicatorCopy").show();

        $.ajax(options)
            .done(function (data) {

                $(modalId).modal("hide");
                window.location = data;

            })
            .fail(function (err, status) {

                showErrorMessage(modalId, err.responseText);

            });

        return false;
    }

    var copyLinkHandler = function () {

        var modalId = "#copyItemModal";

        $(modalId).draggable({
            handle: ".modal-header"
        });

        selectRow($(this));

        var copyLinkName = $(this).attr("data-eml-copyname");

        $(modalId).find("#modaltitle").html(copyLinkName);

        var copyLinkObj = $(this);
        var url = copyLinkObj[0].href;

        setBusyIndicatorBig(modalId);
        $(modalId).modal("show"); //show busy indicator

        //Ajaxify here
        var options = {
            url: url,
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options).done(function (data) {

            var $target = $(modalId).find(modalBodyId);
            var $newHtml = $(data);

            $newHtml.find(".form-group").addClass("animated fadeIn");
            $target.html($newHtml);
            //TODO use Json response to insert the new row 
            //   $("form[data-eml-createconfirmation='true']").validationEngine();
            $.validator.unobtrusive.parse(copyConfirmationSelector); //refresh jqueryval
            $(copyConfirmationSelector).submit(ajaxCopy); //refresh event hook-up

        })
            .fail(function (err, status) {

                showErrorMessage(modalId, err.responseText);

            });

        return false;
    }
    //#endregion // Copy

    //#region Edit
    var ajaxEdit = function () {

        var modalId = editItemModalId;
        var $form = $(this);

        if (!$form.valid()) return false;

        var url = $form.attr("action");
        var options = {
            url: url,
            type: $form.attr("method"),
            data: $form.serialize() //important!!! this will prevent error: -> The required anti-forgery form field "__RequestVerificationToken" is not present
        };

        $("#busyindicatorEdit").show();

        var $submitButton = $form.find(submitButtonSelector);

        $submitButton.addClass("disabled");
        //TODO: prevent from closing if still busy

        $.ajax(options)
            .done(function (data) {

                // resetModalBody(modalId);
                window.location = data;
                $(ajaxEditHandlerSelector).click(editLinkHandler);
                $submitButton.removeClass("disabled");

            })
            .fail(function (err, status) {

                showErrorMessage(modalId, err.responseText);
                $submitButton.removeClass("disabled");

            });

        return false;
    }

    var editLinkHandler = function () {

        var modalId = editItemModalId;

        $(modalId).draggable({
            handle: ".modal-header"
        });

        selectRow($(this));

        var editLinkName = $(this).attr("data-eml-editname");

        $(modalId).find("#modaltitle").html(editLinkName);

        var editLinkObj = $(this);
        var url = editLinkObj[0].href;

        setBusyIndicatorBig(modalId);
        $(modalId).modal("show"); //show busy indicator

        //Ajaxify here
        var options = {
            url: url,
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options)
            .done(function (data) {

                var $target = $(modalId).find(modalBodyId);
                var $newHtml = $(data);
                //animated fadeInDown

                $newHtml.find(".form-horizontal").addClass("animated fadeIn");
                $target.html($newHtml);
                $.validator.unobtrusive.parse(editConfirmationSelector); //refresh jqueryval
                $(editConfirmationSelector).submit(ajaxEdit); //refresh event hook-up
                setDatePickerHandler();

            })
            .fail(function (err, status) {

                showErrorMessage(modalId, err.responseText);

            });

        return false; // prevent default behaviour
    }
    //#endregion // Edit

    //TODO Refactor EditCreate

    //#region Create
    var ajaxCreate = function () {

        var modalId = createItemModalId;
        var $form = $(this);

        if (!$form.valid()) return false;

        var url = $form.attr("action");
        var options = {
            url: url,
            type: $form.attr("method"),
            data: $form.serialize() //important!!! this will prevent error: -> The required anti-forgery form field "__RequestVerificationToken" is not present
        };

        $("#busyindicatorCreate").show();

        var $submitButton = $form.find(submitButtonSelector);

        $submitButton.addClass("disabled");
        //TODO: prevent from closing if still busy

        $.ajax(options)
            .done(function (data) {

                //resetModalBody(modalId);
                window.location = data;
                $(ajaxCreateHandlerSelector).click(createLinkHandler);
                $submitButton.removeClass("disabled");

            })
            .fail(function (err, status) {

                showErrorMessage(modalId, err.responseText);
                $submitButton.removeClass("disabled");

            });

        return false;
    }

    var createLinkHandler = function () {

        var modalId = createItemModalId;

        $(modalId).draggable({
            handle: ".modal-header"
        });

        var createLinkName = $(this).attr("data-eml-createname");

        $(modalId).find("#modaltitle").html(createLinkName);

        var createLinkObj = $(this);
        var url = createLinkObj[0].href;

        setBusyIndicatorBig(modalId);
        $(modalId).modal("show"); //show busy indicator
        //TODO: prevent from closing if still busy

        //Ajaxify here
        var options = {
            url: url,
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options)
            .done(function (data) {

                var $target = $(modalId).find(modalBodyId);
                var $newHtml = $(data);

                $newHtml.find(".form-horizontal").addClass("animated fadeIn");
                $target.html($newHtml);
                $.validator.unobtrusive.parse(createConfirmationSelector); //refresh jqueryval
                $(createConfirmationSelector).submit(ajaxCreate); //refresh event hook-up
                setDatePickerHandler();

            })
            .fail(function (err, status) {

                showErrorMessage(modalId, err.responseText);

            });

        return false; // prevent default behaviour
    }
    //#endregion // Create

    //#region Delete
    var deleteLinkObj; //global variable used to visually delete the selected row
    // var replacePager = false;
    var ajaxDelete = function () {

        var modalId = "#deleteConfirmationModal";
        var $form = $(this);

        if (!$form.valid()) return false;

        var url = $form.attr("action");
        var options = {
            url: url,
            type: $form.attr("method"),
            data: $form.serialize() //important!!! this will prevent error: -> The required anti-forgery form field "__RequestVerificationToken" is not present
        };

        $.ajax(options)
            .done(function (data) {

                $(modalId).modal("hide");
                deleteLinkObj.closest("tr").hide("fast"); //Hide Row
                //disableSortButtons(deleteLinkObj);
                updateRowDeleteCounter(deleteLinkObj);
                $(ajaxDeleteHandlerSelector).click(deleteLinkHandler);

            })
            .fail(function (err, status) {

                showErrorMessage(modalId, err.responseText);

            });

        return false;
    };

    var deleteLinkHandler = function () {

        var modalId = "#deleteConfirmationModal";

        $(modalId).draggable({
            handle: ".modal-header"
        });

        selectRow($(this));

        deleteLinkObj = $(this);  //for ajaxDelete use. See ajaxDelete

        var deleteLinkName = $(this).attr("data-eml-deletename");

        $("#data-eml-deletename").html(deleteLinkName);//update message box

        var url = deleteLinkObj[0].href;

        setBusyIndicatorBig(modalId, ".modal-footer");

        $(modalId).modal("show");
        //TODO: prevent from closing if still busy
        $("#data-eml-deletename").addClass("animated swing");

        //Ajaxify here
        var options = {
            url: url,
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options)
            .done(function (data) {
                var $target = $(modalId).find(".modal-footer");
                var $newHtml = $(data);

                $newHtml.find(".form-group, button").addClass("animated fadeIn");
                $target.html($newHtml);
                $.validator.unobtrusive.parse(deleteConfirmationSelector); //refresh jqueryval
                $(deleteConfirmationSelector).submit(ajaxDelete); //refresh event hook-up

            })
            .fail(function (err, status) {

                showErrorMessage(modalId, err.responseText);

            });

        return false; // prevent default behaviour
    }
    //#endregion // Delete

    //#region Unlock
    var unlockLinkObj; //global variable used to visually unlock the selected row
    // var replacePager = false;
    var ajaxUnlock = function () {

        var modalId = "#unlockConfirmationModal";
        var $form = $(this);
        var url = $form.attr("action");
        var options = {
            url: url,
            type: $form.attr("method"),
            data: $form.serialize() //important!!! this will prevent error: -> The required anti-forgery form field "__RequestVerificationToken" is not present
        };
        // var $target =  $form.attr("[data-valmsg-for='DeletionReason']") ;
        //$("[data-valmsg-for='DeletionReason']").cl() ;
        //return false;
        $("#busyindicatorunlock").show();

        $.ajax(options)
            .done(function (data) {

                $(modalId).modal("hide");
                window.location = data;
                $(unlockLinkHandlerSelector).click(unlockLinkHandler);

            })
            .fail(function (err, status) {

                showErrorMessage(modalId, err.responseText);

            });

        return false;
    };

    var unlockLinkHandler = function () {

        var modalId = "#unlockConfirmationModal";

        $(modalId).draggable({
            handle: ".modal-header"
        });

        unlockLinkObj = $(this);  //for ajaxUnlock use. See ajaxUnlock

        var unlockLinkName = $(this).attr("data-eml-unlockname");

        $("#data-eml-unlockname").html(unlockLinkName);//update message box

        var url = unlockLinkObj[0].href;

        setBusyIndicatorBig(modalId, ".modal-footer");
        $(modalId).modal("show");
        $("#data-eml-unlockname").addClass("animated swing");
        //Ajaxify here
        var options = {
            url: url,
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options).done(function (data) {

            var $target = $(modalId).find(".modal-footer");
            var $newHtml = $(data);

            $newHtml.find("button").addClass("animated fadeIn");
            $target.html($newHtml);

            $(unlockConfirmationSelector).submit(ajaxUnlock); //refresh event hook-up

        })
            .fail(function (err, status) {

                showErrorMessage(modalId, err.responseText);

            });

        return false; // prevent default behaviour
    }
    //#endregion // Unlock

    //#region Email
    var ajaxEmail = function () {

        var modalId = "#emailItemModal";
        var $form = $(this);

        if (!$form.valid()) return false;

        var url = $form.attr("action");
        var options = {
            url: url,
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $("#busyindicatorEmail").show();

        $.ajax(options)
            .done(function (data) {

                $(modalId).modal("hide");
                $(modalId).find(modalBodyId).html(""); // clear contents
                // window.location = data;
                $(emailLinkHandlerSelector).click(emailLinkHandler);

            })
            .fail(function (err, status) {

                showErrorMessage(modalId, err.responseText);

            });

        return false;
    }

    var emailLinkHandler = function () {

        var modalId = "#emailItemModal";

        $(modalId).draggable({
            handle: ".modal-header"
        });

        var emailLinkObj = $(this);
        var url = emailLinkObj[0].href;

        setBusyIndicatorBig(modalId);
        $(modalId).modal("show"); //show busy indicator

        //Ajaxify here
        var options = {
            url: url,
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options)
            .done(function (data) {

                var $target = $(modalId).find(modalBodyId);
                var $newHtml = $(data);

                $newHtml.find(".form-horizontal").addClass("animated fadeIn");
                $target.html($newHtml);
                $.validator.unobtrusive.parse(emailConfirmationSelector); //refresh jqueryval
                $(emailConfirmationSelector).submit(ajaxEmail); //refresh event hook-up

            })
            .fail(function (err, status) {

                showErrorMessage(modalId, err.responseText);

            });

        return false; // prevent default behaviour
    }
    //#endregion // Email

    //#region Search

    var ajaxSearch = function (url, targetTableBody) { ///Will return the pager html

        var $form = $("form[data-eml-ajax='true']").filter(`[data-eml-targettablebody='${targetTableBody}']`);

        if ($form.length === 0) {

            console.warn("ajaxSearch - Unable to find: form[data-eml-ajax='true'].");

            return false;
        }

        showBusySearch();

        var pagerHtml = "";
        var options = {
            url: url,
            data: $form.serialize(),
            type: $form.attr("method")
        };

        $.ajax(options)
            .done(function (newHtml) {

                pagerHtml = getPager(newHtml);

                var $target = $(targetTableBody);
                var $newPagerHtmlJQuery = $(pagerHtml);
                var noPagerTableHtml = removePager(newHtml);
                var rowCountId = $newPagerHtmlJQuery.attr("data-eml-rowcount");

                $target.html(noPagerTableHtml); //replace rows
                $target.find(ajaxDeleteHandlerSelector).click(deleteLinkHandler);
                $target.find("tr td").click(rowSelectHandler);
                $target.find(ajaxCreateHandlerSelector).click(createLinkHandler);
                $target.find(ajaxEditHandlerSelector).click(editLinkHandler);
                $target.find(copyLinkHandlerSelector).click(copyLinkHandler);
                $target.find(unlockLinkHandlerSelector).click(unlockLinkHandler);
                $target.effect("highlight");

                var $pagedList = $target.parents().find("div .pagedList").filter(`[data-eml-targettablebody='${targetTableBody}']`);
                var $newRowCount = $newPagerHtmlJQuery.find(rowCountId);
                var $oldRowCount = $(rowCountId);
                var $tmpOldRowCount = $oldRowCount.prevAll("i.animated.rubberBand.bounce:first");

                $tmpOldRowCount.toggleClass(" animated rubberBand bounce ");
                $oldRowCount.html($newRowCount.html());
                $tmpOldRowCount.toggleClass(" animated rubberBand bounce ");

                if (!$pagedList || $pagedList.length === 0) {

                    alert("Unable to find pagedList");

                } else {

                    if (pagerHtml == null) {

                        pagerHtml = ""; //default to empty string
                        alert("pagerHtml is empty");

                    }
                    else {

                        $pagedList.html($newPagerHtmlJQuery.html());
                        $pagedList.find("a").filter("[href]").click(pageClickHandler);

                    }

                    disableSortButtons($target);
                }

                var $pageNum = $pagedList.find(".active a, .active span");

                if ($pageNum) {

                    if ($pageNum.length > 0 && url) {

                        $pageNum.addClass("animated bounce");

                        var matches = url.match(/page=([^&]+)/);

                        if (matches && matches.length > 0) {

                            var page = url.match(/page=([^&]+)/)[1]; //get page number

                            updatePageNumber($target, page);
                        }
                    }
                }

                hideBusySearch();

                return pagerHtml;
            })
            .fail(function (err, status) {

                hideBusySearch();

                console.warn(err);
                showErrorMessage(null, err.responseText);
                pagerHtml = "";
                window.location = url; //redirect to non-ajax page

                return false;
            });
    }

    var ajaxSearchSubmit = function () {

        var $form = $(this);
        var url = $form.attr("action");
        var targetTableBody = $form.attr(targetTableBodySelector);

        ajaxSearch(url, targetTableBody);

        return false;
    };

    var ajaxSubmitAutoCompleteSearch = function (event, ui) {

        var $input = $(this);

        $input.val(ui.item.label);

        var $form = $input.parent("form :first");

        $form.submit();
    };

    var createSearchAutoComplete = function () {

        var $input = $(this);
        var options = {
            source: function (request, response) {
                getIntellisense(request, response, $input);
            },
            select: ajaxSubmitAutoCompleteSearch,
            open: function (event, ui) {
                hideBusySearch();
            }
        };

        $input.autocomplete(options);
    };

    var showBusySearch = function () {

        //fa fa-spinner fa-pulse
        var $searchButton = $(searchButtonSelector);

        $searchButton.removeClass("fa-search-plus");
        $searchButton.addClass("fa-spinner");
        $searchButton.addClass("fa-pulse");
    }

    var hideBusySearch = function () {

        var $searchButton = $(searchButtonSelector);

        $searchButton.removeClass("fa-spinner");
        $searchButton.removeClass("fa-pulse");
        $searchButton.addClass("fa-search-plus");
    }

    var getIntellisense = function (request, callback, input) {
        // var $input = $(input); $form.serialize()
        var $form = input.parent("form :first");
        var url = input.attr("data-eml-autocomplete");
        var options = {
            url: url,
            data: { search: request.term },
            type: $form.attr("method")
        };

        showBusySearch();

        $.ajax(options)
            .done(function (data) {

                var suggestions = [];

                $.each(data, function (i, val) {
                    suggestions.push(val.label);
                });

                hideBusySearch();
                callback(suggestions);

            })
            .fail(function (err, status) {

                hideBusySearch();
                showErrorMessage(null, err.responseText);

            });
    }
    //#endregion // Search

    //#region Pager Handlers

    var pageClickHandler = function () {

        var $a = $(this);
        var url = $a.attr("href");
        var busyIndicatorPaging = $("#busyindicatorPaging");

        if (busyIndicatorPaging) $a.prepend(busyIndicatorPaging.html());

        var targetTableBody = $a.parents(".pagedList").first().attr(targetTableBodySelector);

        ajaxSearch(url, targetTableBody);

        return false;
    };
    //#endregion // Pager Handlers

    //#region sortMove Handlers
    var sortMoveHandler = function () {

        var $sortButton = $(this);
        var targetTableBody = $sortButton.attr("data-eml-sorttarget");
        var sortDirection = $sortButton.attr("data-eml-sortdirection");
        //var pageNumber = $(targetTableBody).attr("data-eml-pagenumber");
        var $selectedRow = $(targetTableBody).find("tr").filter(".warning"); //get selected row
        var fromPosition = $selectedRow.attr("data-eml-positionfrom");
        var toPositon = $selectedRow.attr("data-eml-positionto");

        if (!toPositon) toPositon = fromPosition;

        var canUpdate = false;
        var isTargetRowVisible = true;

        if (sortDirection === "up") {
            if ($selectedRow.prev().find("th").length === 0) {

                var prevRow = $selectedRow.prev();

                $selectedRow.prev().attr("data-eml-positionto", toPositon);
                isTargetRowVisible = prevRow.is(":visible");

                toPositon--;
                $selectedRow.insertBefore(prevRow);

                if (!isTargetRowVisible) {

                    prevRow.show();
                    $selectedRow.hide();

                }

                canUpdate = true;
            }
        }

        if (sortDirection === "down") {

            if ($selectedRow.next().html()) {

                var nextRow = $selectedRow.next();

                nextRow.attr("data-eml-positionto", toPositon);
                isTargetRowVisible = nextRow.is(":visible");

                toPositon++;
                $selectedRow.insertAfter(nextRow);

                if (!isTargetRowVisible) {

                    nextRow.show();
                    $selectedRow.hide();
                }

                canUpdate = true;
            }
        }

        if (canUpdate) {

            $selectedRow.attr("data-eml-positionto", toPositon);

            var $sortRoles = $sortButton.closest("td").find("[data-eml-sortrole]");

            if ($sortRoles && $sortRoles.length > 0) {

                setTimeout(function () {

                    if ($sortRoles.hasClass("disabled")) $sortRoles.removeClass("disabled");

                    $sortRoles.find("i").addClass("animated pulse infinite");

                }, 100);
            }
        }
    };
    //#endregion // sortMove Handlers  

    //#region sortRole Handlers
    var getSortItems = function (rows) {

        var sortItems = [];

        $(rows).each(function (index, value) {

            var id = $(this).attr("id");
            var positionTo = $(this).attr("data-eml-positionto");
            var item = { 'ID': id, "Order": positionTo };

            sortItems.push(item);

        });

        return JSON.stringify(sortItems);
    }

    var sortRoleHandler = function () {

        var $button = $(this).find("[data-eml-sortrole]");
        var role = $button.attr("data-eml-sortrole");

        if (role) {

            $button.addClass("disabled");
            $button.find("i").removeClass("animated pulse infinite");

        }

        // data - eml - sorttarget
        var modalId = "#pleaseWaitModal";

        if (role === "save") {

            $(modalId).modal("show"); //

            var $form = $(this);
            var rows = $($button.attr(sortTargetSelector)).find("[data-eml-positionto]");
            var sortItems = getSortItems(rows);

            $form.find("#sortItems").val(sortItems);

            var url = $form.attr("action");
            var options = {
                url: url,
                type: $form.attr("method"),
                data: $form.serialize()
            };

            $.ajax(options)
                .done(function (data) {

                    //clear all data-eml-positionto values
                    $(modalId).modal("hide");

                })
                .fail(function (err, status) {

                    showErrorMessage(modalId, err.responseText);

                });
        }

        return false;
    };
    //#endregion // sortRole Handlers

    //#region downloadReport
    var downloadReport = function () {

        var modalId = "#pleaseWaitModal";

        showPleaseWaitModal();

        var $form = $(this);
        var url = $form.attr("action");
        var options = {
            url: url,
            type: $form.attr("method"),
            data: $form.serialize()
        };

        //  var deferred = $q.defer();
        $.ajax(options)
            .done(function (data) {

                $(modalId).modal("hide");
                window.location = data;
                //  deferred.resolve(data);
                //  alert("downloadReport touchdown test!");
            })
            .fail(function (err, status) {

                showErrorMessage(modalId, err.responseText);

            });

        return false;
    };

    var downloadReportHandler = function (reportItemId, reportKey) {

        var $form = $(downloadReportSelector).first();

        $form.find("#reportItemId").val(reportItemId);
        $form.find("#reportKey").val(reportKey);

        $form.submit();
    };

    //#endregion // downloadReport

    //#region menuclick
    var menucClickHandler = function () {

        $("ul.nav > li").removeClass("active");

        $(this).addClass("active");
    };
    //#endregion // menuclick

    //#region datepicker
    var setDatePickerHandler = function () {

        $("input[type=datetime]").datepicker({
            todayBtn: "linked",
            clearBtn: true,
            forceParse: false,
            autoclose: true,
            todayHighlight: true,
            dateFormat: "dd/mm/yy"
        });
    };

    setDatePickerHandler();

    $.validator.addMethod("date",

        function (value, element) {

            $.culture = Globalize.culture("en-AU");

            var date = Globalize.parseDate(value, "dd/MM/yyyy", "en-AU");

            return this.optional(element) ||
                !/Invalid|NaN/.test(new Date(date).toString());
        });
    //#endregion // datepicker

    //#region EVENT HANDLERS
    var pageClickHandlerSelector = ".pagedList a";
    var ajaxDeleteHandlerSelector = "[data-eml-ajaxdelete='true']";
    var ajaxSearchSubmitSelector = "form[data-eml-ajax='true']";
    var createSearchAutoCompleteSelector = "input[data-eml-autocomplete]";
    var rowSelectHandlerSelector = "[data-eml-tablecontainer='true'] table tbody tr td";
    var ajaxCreateHandlerSelector = "[data-eml-ajaxcreate='true']";
    var ajaxEditHandlerSelector = "[data-eml-ajaxedit='true']";
    var copyLinkHandlerSelector = "[data-eml-copylink='true']";
    var unlockLinkHandlerSelector = "[data-eml-unlocklink='true']";
    var emailLinkHandlerSelector = "[data-eml-emaillink='true']";
    var sortMoveHandlerSelector = "[data-eml-sortdirection]";
    var sortRoleHandlerSelector = "form[data-eml-sortorderconfirmation='true']";
    var downloadReportSelector = "form[data-eml-downloadreportconfirmation='true']";
    var menuClickHandlerSelector = "ul.nav > li, a.navbar-brand";
    var targetTableSelector = "[data-eml-tablecontainer='true']";
    var sortTargetSelector = "[data-eml-sorttarget]";
    var sortDirectionSelector = "[data-eml-sortdirection]";
    var copyConfirmationSelector = "form[data-eml-copyconfirmation='true']";
    var submitButtonSelector = "button[type='submit']";
    var editConfirmationSelector = "form[data-eml-editconfirmation='true']";
    var createConfirmationSelector = "form[data-eml-createconfirmation='true']";
    var deleteConfirmationSelector = "form[data-eml-deleteconfirmation='true']";
    var unlockConfirmationSelector = "form[data-eml-unlockconfirmation='true']";
    var emailConfirmationSelector = "form[data-eml-emailconfirmation='true']";
    var searchButtonSelector = "i[data-eml-searchbutton='true']";
    var targetTableBodySelector = "data-eml-targettablebody";
    var editItemModalId = "#editItemModal";
    var createItemModalId = "#createItemModal";
    var modalBodyId = "#modalbody";

    $(pageClickHandlerSelector).filter("[href]").click(pageClickHandler);
    $(ajaxDeleteHandlerSelector).click(deleteLinkHandler);
    $(ajaxSearchSubmitSelector).submit(ajaxSearchSubmit);
    $(createSearchAutoCompleteSelector).each(createSearchAutoComplete);
    $(rowSelectHandlerSelector).click(rowSelectHandler);
    $(ajaxCreateHandlerSelector).click(createLinkHandler);
    $(ajaxEditHandlerSelector).click(editLinkHandler);
    $(copyLinkHandlerSelector).click(copyLinkHandler);
    $(unlockLinkHandlerSelector).click(unlockLinkHandler);
    $(emailLinkHandlerSelector).click(emailLinkHandler);
    $(sortMoveHandlerSelector).click(sortMoveHandler);
    $(sortRoleHandlerSelector).submit(sortRoleHandler);
    $(downloadReportSelector).submit(downloadReport);
    $(menuClickHandlerSelector).click(menucClickHandler);

    $(editItemModalId).on("hidden.bs.modal", function (e) {
        resetModalBody(editItemModalId);
    });

    $(createItemModalId).on("hidden.bs.modal", function (e) {
        resetModalBody(createItemModalId);
    });

    //#endregion // EVENT HANDLERS 

    //#region public methods

    // this.LayoutDeleteSubmitButtonAjaxOnLoadHandler = layoutDeleteSubmitButtonAjaxOnLoadHandler;
    //#endregion // public methods
})[0];