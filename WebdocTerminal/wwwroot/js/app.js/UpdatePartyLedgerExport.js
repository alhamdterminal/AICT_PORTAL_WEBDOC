

    var PartyLedger = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function callChangefunc() {
        var partyId = $("#prty option:selected").val();
    console.log("partyId", partyId)

    getPartyLedgerByPatry(partyId);



    }


    function getPartyLedgerByPatry(partyId) {
        console.log(partyId);
    $.get('/Export/PartyLedgerExport/GetLedgerByParty?PatryId='+partyId, function (data) {
        console.log("data", data)

            PartyLedger = data;

    var dataGrid = $("#PartyLedgersGrid").dxDataGrid({
        dataSource: PartyLedger,
    keyExpr: "partyLedgerExportId",

    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
                },
    editing: {
        mode: "row",
    allowUpdating: true,
    allowDeleting: true,
    selectTextOnEditStart: true,
    startEditAction: "click"
                },
    columns: [

    {
        dataField: "debit",
    caption: "Debit"
                    },
    {
        dataField: "credit",
    caption: "Credit"
                    },
    {
        dataField: "balance",
    caption: "Balance"
                    }
    ],
    onRowUpdated: function (e) {
        console.log(e);
    var model = e.data;
    $.post('/Export/PartyLedgerExport/UpdateLedger', {model: model }, function (data) {
        console.log("data", data)
    });
                },
    onRowRemoved: function (e) {
        console.log(e);
    var model = e.data;
    $.post('/Export/PartyLedgerExport/DeletePartyLedger', {model: model }, function (data) {
        console.log("data", data)
    });
                }


            }).dxDataGrid("instance");




        });
    }
