


    var ledgers = [];

    function refresh() {
        window.location.reload();
    }





    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function submitAmountReceived() {
        var party = $("#searchBoxForConsigne").dxSelectBox('instance').option("value");

    if (party) {

        console.log("dddd");
    $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

            var result = ledgers.filter(c => c.isSettled == true);
    console.log("result", result);
    if (result.length) {
        console.log("result", result)

                sendRequest(result);


            }
    }
    else {
        showToast("please select party", "error");


    }

    }


    function sendRequest(result) {


        console.log(" sendRequest(result)", result);
    $.post('UpdateSettledAmountBillToLine', {model: result }, function (data) {
        console.log(data);
    if (data.error == true) {
        showToast(data.message, "error");
    window.location.href = window.location.href;

            }
    else {

        showToast(data.message, "success");

    window.location.href = window.location.href;

            }

        });

    }



    var partyList = [];
    function getpartyList() {
        $.get('/Import/PartyLedger/GetALlpartiesFF', function (res) {
            if (res) {
                partyList = res;
                $("#searchBoxForConsigne").dxSelectBox({
                    dataSource: {
                        store: partyList,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'PartyId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                    onValueChanged: function (e) {
                        console.log("e", e);
                        onChangeParty(e.value)
                        //LoadAgentTariffGrid();
                        //getsharerates();
                    }
                })
            }
            else {
                consigneeList = []
                $("#searchBoxForConsigne").dxSelectBox({
                    dataSource: {
                        store: partyList,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'PartyId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,

                    onValueChanged: function (e) {
                        console.log("e", e);
                    }

                })
            }
        });
    }

    function onChangeParty(partyId) {
        //var partyId = $("#party option:selected").val();

        $.get('/APICalls/GetrPartyUnSettledAmount?partyId=' + partyId, function (data) {



            console.log("data", data);

            ledgers = data;

            var dataGrid = $("#unsettledparty").dxDataGrid({
                dataSource: ledgers,
                keyExpr: "partyLedgerId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    pageSize: 10
                },
                editing: {
                    mode: "batch",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },

                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },
                columns: [
                    {
                        dataField: "ledgerDate",
                        caption: "Ledger Date",
                        dataType: 'date',
                        format: 'dd/MM/yyyy',
                        allowEditing: false,

                    },
                    {
                        dataField: "billNo",
                        caption: "Bill No",
                        allowEditing: false,

                    },
                    {
                        dataField: "type",
                        caption: "Bill Type",
                        allowEditing: false,

                    },
                    {
                        dataField: "debit",
                        caption: "Debit",
                        dataType: "number",
                        format: "#,##0.##",
                        allowEditing: false,

                    },
                    {
                        dataField: "credit",
                        caption: "Credit",
                        dataType: "number",
                        format: "#,##0.##",
                        allowEditing: false,

                    },
                    {
                        caption: "",
                        dataField: "isSettled",
                        dataType: "boolean"
                    }
                ],


            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        })

    }

    function formfiled() {
        getpartyList();
       
    }




