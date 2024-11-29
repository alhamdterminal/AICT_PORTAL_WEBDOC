
    var billNo;

    var invoiceType;
    var invoices = [];

    function invoice_changed(data) {
        billNo = data.value;

        //$.get('/APICalls/GetInvoiceType?BillNo=' + billNo, function (res) {

        //    invoiceType = res;
        //})

        loadGrid(billNo)
    }


    function FindInvoice() {
        var res = $('#invoiceNo').val();


    loadGrid(res)
    }
    function FindInvoicebygd() {

        var res = $('#gdno').val();

    loadGridgdno(res);


    }



    function loadGridgdno(gdno) {

        console.log("gdno", gdno)

        $.get('/APICalls/GetinvoicesExportBygdno?gdno=' + gdno, function (data) {

        console.log("data", data)
            invoices = data;

    printGrid(invoices);

        });

    }


    function loadGrid(billNo) {

        console.log("billNo", billNo)

        $.get('/APICalls/GetinvoicesExportByInvoiceNo?InvoiceNo=' + billNo, function (data) {

        console.log("data", data)
            invoices = data;

    printGrid(invoices);

        });

    }


    function printGrid(invoices) {

        var dataGrid = $("#gateinContainer").dxDataGrid({
        dataSource: invoices,
    keyExpr: "invoiceExportId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
            },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."
            },
    editing: {
        mode: "row",
    allowUpdating: false,
    selectTextOnEditStart: false,
    startEditAction: "click"
            },
    columns: [
    {
        dataField: "invoiceDate",
    caption: "Invoice Date",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    "totalCharges",
    "cbm",
    "cbmTotal",
    "weight",
    "indexTotal",
    "storageDays",
    "storageTotal",
    "totalAmount",
    "grandTotal",
                //{
        //    width: 100,
        //    alignment: 'center',
        //    cellTemplate: function (container, options) {
        //        $('<a/>').addClass('dx-link')
        //            .text('Delete')
        //            .on('dxclick', function () {
        //                deleteInvoiceonly(options.row.data)
        //            })
        //            .appendTo(container);
        //    }
        //},
        {
            width: 100,
            alignment: 'center',
            cellTemplate: function (container, options) {
                $('<a/>').addClass('dx-link')
                    .text('DeleteAll')
                    .on('dxclick', function () {
                        deleteInvoice(options.row.data)
                    })
                    .appendTo(container);
            }
        },
        {
            width: 100,
            alignment: 'center',
            cellTemplate: function (container, options) {
                $('<a/>').addClass('dx-link')
                    .text('DeleteInvoice')
                    .on('dxclick', function () {
                        deletefromInvoice(options.row.data)
                    })
                    .appendTo(container);
            }
        }



            ],
    onEditorPreparing: function (e) {
        //  hideMenifestedColumns(e);
    },
    onRowUpdated: function (e) {

        $("#btnSubmit").attr("disabled", false);
            }

        }).dxDataGrid("instance");

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }


    function printChequeDetailGrid(data) {


        var dataGrid = $("#chequeDetailGrid").dxDataGrid({
        dataSource: data,
    keyExpr: "chequeRecivedExportId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
            },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."
            },
    editing: {
        mode: "row",
    allowUpdating: false,
    selectTextOnEditStart: false,
    startEditAction: "click"
            },
    columns: [
    {
        dataField: "date",
    caption: "Date",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },

    "amount",
    "type",
    "number",
    {
        dataField: "chequeDate",
    caption: "Cheque Date",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    {
        width: 100,
    alignment: 'center',
    cellTemplate: function (container, options) {
        $('<a/>').addClass('dx-link')
            .text('DeleteChequeReceived')
            .on('dxclick', function () {
                DeleteChequeReceived(options.row.data)
            })
            .appendTo(container);
                    }
                }



    ],
    onEditorPreparing: function (e) {
        //  hideMenifestedColumns(e);
    },
    onRowUpdated: function (e) {

        $("#btnSubmit").attr("disabled", false);
            }

        }).dxDataGrid("instance");

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


    }


    function FindInvoicebyPartyAndInstrumentNo() {

        var instrumentNo = $('#instrumentNo').val();

    var partyId = $("#partyId option:selected").val();

    console.log("partyId", partyId)

    if (partyId && instrumentNo) {

        $.get('/APICalls/GetChequeReceivedDetailsexport?partyId=' + partyId + '&instrumentNo=' + instrumentNo, function (data) {

            console.log("data", data)

            printChequeDetailGrid(data);

        });
        }
    else {

        showToast("please select party and instrument no", "error")

    }

    }



    function print_invoice(data) {

        console.log("print_invoice data", data)
        if (data.type == "CFS") {

        window.open('/import/reports/BillInvoice?BillNo=' + data.invoiceNo, "_blank");
        }
    else if (data.type == "CY") {

        window.open('/import/reports/BillInvoiceCyContainer?BillNo=' + data.invoiceNo, "_blank");
        }

    }

    function deleteInvoice(data) {
        console.log("deleteInvoice data", data)

        var x = confirm("Are You Sure");
    if (x == true) {

        $.post("/Export/InvoiceExport/DeleteInvoiceAll?invoiceId=" + data.invoiceExportId, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");

            }
        })
    }

    }


    function deletefromInvoice(data) {
        console.log("deletefromInvoice data", data)

        var x = confirm("Are You Sure");
    if (x == true) {

        $.post("/Export/InvoiceExport/DeleteInvoiceFromInvoice?invoiceId=" + data.invoiceExportId, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");

            }
        })
    }

    }

    function deleteInvoiceonly(data) {
        console.log("deleteInvoice data", data)

        var x = confirm("Are You Sure");
    if (x == true) {

        $.post("/Invoice/DeleteInvoiceOnly?invoiceId=" + data.invoiceId, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");

            }
        })
    }


    }

    function DeleteChequeReceived(data) {
        console.log("deleteInvoice data", data)

        var x = confirm("Are You Sure");
    if (x == true) {

        $.post("/Export/InvoiceExport/DeleteChequeReceived?ChequeReceivedId=" + data.chequeRecivedExportId, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");

            }
        })
    }


    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function formfiled() {
        if (PermissionInputs.find(x => x.fieldName == "InvoiceNo" && x.isChecked == false)) {
        document.getElementById("SearchDOForm").style.display = "none";
    console.log();
        }


    }