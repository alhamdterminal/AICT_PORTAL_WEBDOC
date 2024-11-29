
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
        var res = $('#gdnumber').val();


    loadGridblgd(res)
    }


    function loadGridblgd(gdno) {

        console.log("gdno", gdno)

        $.get('/APICalls/GetinvoicesBygdnumber?gdnumber=' + gdno, function (data) {

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
    "invoiceNo",
    "totalCharges",
    "cbm",
    "cbmTotal",
    "weight",
    "storageDays",
    //"storageTotal",
    "grandTotal",
    {
        width: 100,
    alignment: 'center',
    cellTemplate: function (container, options) {
        $('<a/>').addClass('dx-link')
            .text('Delete')
            .on('dxclick', function () {
                deleteInvoiceonly(options.row.data)
            })
            .appendTo(container);
                    }
                },
    {
        width: 100,
    alignment: 'center',
    cellTemplate: function (container, options) {
        $('<a/>').addClass('dx-link')
            .text('Print')
            .on('dxclick', function () {
                print_invoice(options.row.data)
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

    function print_invoice(data) {

        console.log("print_invoice data", data)

        window.open('/Export/Reports/ExportBillInvoice?BillNo=' + data.invoiceNo, "_blank");


    }


    function deleteInvoiceonly(data) {
        console.log("deleteInvoice data", data)

        var x = confirm("Are You Sure");
    if (x == true) {

        $.post("/Export/InvoiceExport/DeleteInvoiceOnly?invoiceId=" + data.invoiceExportId, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");

            }
        })
    }


    }

    function deleteInvoice(data) {
        console.log("deleteInvoice data", data)

        var x = confirm("Are You Sure");
    if (x == true) {

        $.post("/Invoice/DeleteInvoice?invoiceId=" + data.invoiceId, function (data) {

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