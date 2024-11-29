

    var billNo;


    var type;

    var invoiceType;
    var invoices = [];
    var Indexinvoices = [];

    function invoice_changed(data)
    {
        billNo = data.value;

        //$.get('/APICalls/GetInvoiceType?BillNo=' + billNo, function (res) {

        //    invoiceType = res;
        //})

        loadGrid(billNo)
    }


    function loadGrid(billNo) {

        console.log("billNo", billNo)

        $.get('/APICalls/GetinvoicesByInvoiceNo?InvoiceNo=' + billNo, function (data) {

        console.log("data", data)
            invoices = data;


    var dataGrid = $("#gateinContainer").dxDataGrid({
        dataSource: invoices,
    keyExpr: "invoiceId",
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
    {
        dataField: "totalCharges",
    caption: "Total Charges",
    dataType: "number",
    format: "#,##0.##",
                    },
    "cbm",
    "weight",
    "storageDays",
    /*  "storageTotal", */

    {
        dataField: "grandTotal",
    caption: "Grand Total",
    dataType: "number",
    format: "#,##0.##",
                    },
    {
        caption: "Bill Type",
    dataField: "billType" 
                    },
    {
        caption: "Invoice Category",
    dataField: "invoiceCategory"
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
                    //{
                    //    width: 100,
                    //    alignment: 'center',
                    //    cellTemplate: function (container, options) {
                    //        $('<a />').addClass('dx-link')
                    //            .text('Delete')
                    //            .on('dxclick', function () {
                    //                deleteInvoice(options.row.data)
                    //            })
                    //            .appendTo(container);
                    //    }
                    //} 
                    
                    

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

        });

    }

    function print_invoice(data) {

        console.log("print_invoice data", data)


        if (data.billType == "Auction") {

        window.open('/import/reports/SalesTaxInvoiceAution?BillNo=' + data.invoiceNo + '&BillType=' + "Auction" + '&InvoiceCategory=' + data.invoiceCategory, "_blank");
              
        }

    else {
        //  window.open('/import/reports/BillInvoice?BillNo=' + data.invoiceNo, "_blank");

        window.open('/import/reports/SalesTaxInvoice?BillNo=' + data.invoiceNo + '&BillType=' + data.billType + '&InvoiceCategory=' + data.invoiceCategory, "_blank");

        //if (data.type == "CFS") {

        //    window.open('/import/reports/BillInvoice?BillNo=' + data.invoiceNo, "_blank");
        //}
        //else if (data.type == "CY") {

        //    window.open('/import/reports/BillInvoiceCyContainer?BillNo=' + data.invoiceNo, "_blank");
        //}
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
                window.location.href = window.location.href;
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

    function loadGridindexwise() {
 

        var indexnumber = $("#indexdb option:selected").text();



    if (indexnumber) {

        $.get('/APICAlls/ReprintGetMultipleInvoicesByIgmIndex?igm=' + igm + "&index=" + indexnumber, function (data) {

            console.log("data", data)
            Indexinvoices = data;


            var dataGrid = $("#indexinvoices").dxDataGrid({
                dataSource: Indexinvoices,
                keyExpr: "invoiceId",
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
                    "weight",
                    "storageDays",
                    "grandTotal",
                    {
                        caption: "Bill Type",
                        dataField: "billType"
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
                    //{
                    //    width: 100,
                    //    alignment: 'center',
                    //    cellTemplate: function (container, options) {
                    //        $('<a/>').addClass('dx-link')
                    //            .text('Delete')
                    //            .on('dxclick', function () {
                    //                deleteInvoice(options.row.data)
                    //            })
                    //            .appendTo(container);
                    //    }
                    //}



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

        });

        }
    else {
        showToast("PLease Select Index", "error");
        }
       
    }


    var igm;



    function igm_changed(data) {

        igm = data.value;

    $.get('/APICalls/GetIGMOIndexDropdown?IGM='+ igm, function (indexdb) {

        $('#indexdiv').html(indexdb);
                     

                });
           
         
    }
