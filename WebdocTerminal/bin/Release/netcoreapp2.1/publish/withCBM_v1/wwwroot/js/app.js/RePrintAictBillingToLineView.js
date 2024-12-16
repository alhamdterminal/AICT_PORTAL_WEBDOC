
    var billNo;




    function invoice_changed(data)
    {
        billNo = data.value;

    loadGrid(billNo)
    }


    function loadGrid(billNo) {

        console.log("billNo", billNo)

        $.get('/APICalls/GetinvoicesAictBillingtoLineByInvoiceNo?InvoiceNo=' + billNo, function (data) {

        gridData(data)

    });

    }

    function gridData(res) {

        console.log("res", res);

    var dataGrid = $("#gateinContainer").dxDataGrid({
        dataSource: res,
    keyExpr: "aictBillingId",
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
        dataField: "performedDate",
    caption: "Invoice Date",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    {
        caption: "Invoice no",
    dataField: "aictBillingNumber"
                },
    {
        dataField: "fromDate",
    caption: "From Date",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    {
        dataField: "toDate",
    caption: "To Date",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    {
        caption: "Billing From",
    dataField: "billingFrom"
                },
    {
        caption: "Sales Tax",
    dataField: "salesTax"
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
                },



    ],

    onRowUpdated: function (e) {

        $("#btnSubmit").attr("disabled", false);
            }

        }).dxDataGrid("instance");

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }

    function print_invoice(data) {

        console.log("print_invoice data", data);

    window.open('/import/reports/AICTInvoiceToLineView?BillNo=' + data.aictBillingNumber, "_blank");
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


    }

    function Finddetail() {
        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;

    $.get('/Import/Setup/GetAictBillingList?fromdate=' + fromdate + '&todate=' + todate, function (res) {
            if (res) {
        gridData(res)
    }
    else {
        Griddata([])
    }

        });
    }
