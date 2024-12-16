
    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function submitRefundAmount() {
        var resinvocie = $('#knockofinvoice').val();

    var res = $('#excessAmount').val();

    if (resinvocie) {
            if (res > 0) {
                var form = document.getElementById('RefoundAmountForm');

    var model = $('#RefoundAmountForm').serialize();


    form.reportValidity();


    if (form.checkValidity()) {
        $.post('/Import/PartyLedger/SaveExcessRefund?' + model, function (data) {
            if (data.error == true) {
                showToast(data.message, "error");
            }
            else {
                showToast(data.message, "success");
                GetexcessRefundDetail();
                FindRecord();
            }
        });
                }
            }
    else {
        showToast("Insufficient excess amount ", "error")
    }

        } else {
        showToast("Add Invoice no", "error")
    }
        
    }


    function GetexcessRefundDetail() {

        var res = $('#knockofinvoice').val();

    if (res) {
        $.get('/APICAlls/GetexcessRefundDetail?invoiceno=' + res, function (data) {


            console.log("data", data)

            var dataGrid = $("#detailGrid").dxDataGrid({
                dataSource: data,
                keyExpr: "excessAmountRefundSettlementId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    enabled: true
                },
                editing: {
                    mode: "inline",
                    allowDeleting: false,
                    allowUpdating: false,
                },
                columns: [

                    {
                        dataField: "invoiceNo",
                        caption: "Invoice No",

                    },
                    {
                        dataField: "type",
                        caption: "Type",

                    },
                    {
                        dataField: "refoundAmount",
                        caption: "Refound Amount",

                    },
                    {
                        dataField: "chequeNumber",
                        caption: "Cheque No",
                        allowEditing: false,
                    },
                    {
                        caption: "Cheque Date",
                        dataField: "chequeDate",
                        dataType: "date",
                        allowEditing: false,
                    },
                    {
                        dataField: "aictServiceCharges",
                        caption: "AICT Service Charges",
                        dataType: "number",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    }, {
                        dataField: "amount",
                        caption: "Amount",
                        dataType: "number",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    }, {
                        dataField: "onllineAccountAmount",
                        caption: "Onlline Account Amount",
                        dataType: "number",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    },

                    {
                        dataField: "inFavorof",
                        caption: "inFavorof",

                    },


                ],

                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {
                    console.log(e);

                },
                onRowRemoved: function (e) {


                }

            }).dxDataGrid("instance");


        });

        } else {
        showToast("please add invoice no", "error");
        }

    

    }


    function restformvalues() {

        $('#bank').select2().val('').trigger('change.select2');
    $('#chequeNumber').val('');
    $('#amount').val('');
    $('#inFavorof').val('');
    $('#aictserviceCharges').val('');
    $('#onllineAccountTitle').val('');
    $('#onllineAccountAmount').val('');
    $('#onllineAccountNumber').val('');
    $('#excessAmount').val(0);
    }

    function FindRecord() {
        restformvalues();
    var res = $('#knockofinvoice').val();

    if (res) {

        $.get('/APICalls/GetexcessamounteByInvoiceNo?invoiceno=' + res, function (res) {

            if (res) {
                $("#excessAmount").val(res)
            } else {
                $("#excessAmount").val(0)
            }
            GetexcessRefundDetail();
        });

        } else {
        showToast("please add invoice no", "error");
        }

    }


    function formfiled() {
        $("#excessAmount").val(0)
    }
