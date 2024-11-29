

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
        $.get('/Import/Party/GetParty', function (data) {
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

    function hideMenifestedColumns(e) {
         

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
    keyExpr: "partyId",
    wordWrapEnabled: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
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
    allowDeleting: false,
    allowAdding: true
            },
    columns: [
    {
        dataField: "partyName",
    validationRules: [{type: "required" }],
    caption: "Name"
                },
    {
        dataField: "consignee",
    validationRules: [{type: "required" }],
    caption: "NTN"
                }
    ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
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

    var party = e.data;
    console.log(party)
    if (party.partyName || party.consignee) {
        $.post('/Import/Party/AddParty', { party, party }, function (data) {

            showToast("Party Created", "success");

            getPartys();

        });
                }


            },
    onRowUpdating: function (e) {
        logEvent("RowUpdating");
            },
    onRowUpdated: function (e) {
        logEvent("RowUpdated");
    console.log(e);
    var party = e.data;
    $.post('/Import/Party/UpdateParty', {party, party}, function (data) {
        showToast("Party Updated", "success");
                });
            },
    onRowRemoving: function (e) {
        logEvent("RowRemoving");
            },
    onRowRemoved: function (e) {
        logEvent("RowRemoved");
    var key = e.data.partyId;
    $.post('/Import/Party/DeleteParty?key=' + key, function (data) {
        showToast("Party Deleted", "success");
                });
            }
        });
    }


    function formfiled() {
        getPartys();

    }
