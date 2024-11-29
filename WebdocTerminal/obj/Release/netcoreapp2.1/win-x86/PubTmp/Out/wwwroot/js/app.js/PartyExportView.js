

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    var partyData = [];



    function getPartys() {
        $.get('/Export/PartyExport/GetPartyExport', function (data) {
            partyData = data;

            partygrid();
        });
    }



    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function disableColumns(e) {
  
        if (PermissionInputs.find(x => x.fieldName == "PartyName" && x.isChecked == false)) {

        checkColumn(e, "partyName");
        }
        
        if (PermissionInputs.find(x => x.fieldName == "Consignee" && x.isChecked == false)) {

        checkColumn(e, "consignee");
        }
    }

    function partygrid() {
        function logEvent(eventName) {
            var logList = $("#events ul"),
                newItem = $("<li>", { text: eventName });
            logList.prepend(newItem);
        }

        $("#gridContainer").dxDataGrid({
        dataSource: partyData,
    keyExpr: "partyExportId",
    wordWrapEnabled: true,
    showBorders: true,
    paging: {
        pageSize: 10
            },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."
            },
    editing: {
        mode: "row",
    allowUpdating: true,
    allowDeleting: true,
    allowAdding: true
            },
    columns: [
    {
        dataField: "partyName",
    caption: "Name"
                },
    {
        dataField: "consignee",
    caption: "Consginee"
                }
    ],
    onEditorPreparing: function (e) {
        disableColumns(e);
            },
    onEditingStart: function (e) {
        logEvent("EditingStart");
            },
    onInitNewRow: function (e) {
        logEvent("InitNewRow");
            },
    onRowInserting: function (e) {
        logEvent("RowInserting");
            },
    onRowInserted: function (e) {
        logEvent("RowInserted");
    console.log(e.data)
    var party = e.data;
    $.post('/Export/PartyExport/AddParty', {party, party}, function (data) {

        showToast("Party Created", "success");

    getPartys();

                });
            },
    onRowUpdating: function (e) {
        logEvent("RowUpdating");
            },
    onRowUpdated: function (e) {
        logEvent("RowUpdated");
    console.log(e);
    var party = e.data;
    $.post('/Export/PartyExport/UpdateParty', {party, party}, function (data) {
        showToast("Party Updated", "success");
                });
            },
    onRowRemoving: function (e) {
        logEvent("RowRemoving");
            },
    onRowRemoved: function (e) {
        logEvent("RowRemoved");
    var key = e.data.partyExportId;
    $.post('/Export/PartyExport/DeleteParty?key=' + key, function (data) {
        showToast("Party Deleted", "success");
                });
            }
        });
    }


    function formfiled() {
        getPartys();

    }

