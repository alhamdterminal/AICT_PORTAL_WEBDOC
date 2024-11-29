

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
    $.get('/Import/PartyLedger/GetLedgerByParty?PatryId='+partyId, function (data) {
        console.log("data", data)

            PartyLedger = data;

    var dataGrid = $("#PartyLedgersGrid").dxDataGrid({
        dataSource: PartyLedger,
    keyExpr: "partyLedgerId",

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
        dataField: "number",
    caption: "Cheque No"
                    },
    {
        dataField: "billNo",
    caption: "Bill No"
                    },
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
    onEditorPreparing: function (e) {

        hideMenifestedColumns(e);
                     
                },
    onRowUpdated: function (e) {
        console.log(e);
    var model = e.data;
    $.post('/Import/PartyLedger/UpdateLedger', {model: model }, function (data) {
        console.log("data", data)

                        showToast(data.message, 'success');

    window.location.href = window.location.href;
                    });
                },
    onRowRemoved: function (e) {
        console.log(e);
    var model = e.data;
    $.post('/Import/PartyLedger/DeletePartyLedger', {model: model }, function (data) {
        console.log("data", data)

                        showToast(data.message, 'success');

    window.location.href = window.location.href;
                    });
                }


            }).dxDataGrid("instance");




        });
    }




    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }



    function hideMenifestedColumns(e) {

        checkColumn(e, "billNo");
    checkColumn(e, "balance"); 
    }

