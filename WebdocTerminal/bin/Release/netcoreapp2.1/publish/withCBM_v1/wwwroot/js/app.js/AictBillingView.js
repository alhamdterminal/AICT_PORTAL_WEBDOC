


    $(function () {

        $('#salesTax').val(13);
    })

    function Finddetail() {

        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    var ShippingAgent = $("#shippingAgentId option:selected").val();
    if (ShippingAgent) {

        document.getElementById("single_cal2").setAttribute("disabled", true);
    document.getElementById("single_cal3").setAttribute("disabled", true);
    document.getElementById("shippingAgentId").setAttribute("disabled", true);
    document.getElementById("salesTax").setAttribute("disabled", true);

    $.get('/Import/Setup/GetAictBillingView?shippingAgentId=' + ShippingAgent + '&fromdate=' + fromdate + '&todate=' + todate, function (res) {
                if (res) {
        console.log(res)

                    Griddata(res)

                }
    else {
        Griddata([])

    }

            });
        }
    else {
        alert("please select Line");
        }

    }

    function Griddata(res) {
        var dataGrid = $("#gridlist").dxDataGrid({
        dataSource: res,
    keyExpr: "lineInvoiceno",
    wordWrapEnabled: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: true
            },
    editing: {
        allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click",
    mode: "batch"

            },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

            },
    hoverStateEnabled: true,
    columns: [
    {
        dataField: "virNo",
    caption: "IGM #",
    allowEditing: false,
                },
    {
        dataField: "indexNo",
    caption: "Index #",
    allowEditing: false,
                },
    {
        dataField: "lineInvoiceno",
    caption: "MSK Inv #",
    allowEditing: false,
                },
    {
        dataField: "lineInvoiceDate",
    caption: "MSK Inv Date",
    allowEditing: false,
    cssClass: 'myClass',
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    {
        dataField: "aictInvoiceNo",
    caption: "AICT Inv #",
    allowEditing: false,
                },
    {
        dataField: "aictInvoiceDate",
    caption: "AICT Inv Date",
    allowEditing: false,
    cssClass: 'myClass',
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    {
        dataField: "lineStorage",
    caption: "MSK Invoice Storage Charges",
    allowEditing: false,
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "lineServiceChages",
    caption: "MSK Invoice Service Charges",
    allowEditing: false,
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "lineTotalCharges",
    caption: "MSK Total Inv Amount",
    allowEditing: false,
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "storagePercentToLine",
    caption: "% of Storage To be Billed To MSK",
    format: "#,##0.##",
    validationRules: [{type: "required" }],
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "serviceChargesPercentToLine",
    caption: "% of Other Service Charges to be Billed To MSk",
    format: "#,##0.##",
    validationRules: [{type: "required" }],
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "aictBillingToLine",
    caption: "AICT Billing To MSk",
    format: "#,##0.##",
    allowEditing: false,
    editorOptions: {
        step: 0
                    },
    calculateCellValue: function (rowData) {

        let storage = ((rowData.lineStorage != null ? Number(rowData.lineStorage) : Number(0)) * (rowData.storagePercentToLine != null ? Number(rowData.storagePercentToLine) : Number(0)) / 100);
    let servicechagres = ((rowData.lineServiceChages != null ? Number(rowData.lineServiceChages) : Number(0)) * (rowData.serviceChargesPercentToLine != null ? Number(rowData.serviceChargesPercentToLine) : Number(0)) / 100);
    return Math.round(rowData.aictBillingToLine = storage + servicechagres);

                    },


                },
    {
        caption: "",
    dataField: "isChecked",
    dataType: "boolean"

                }
    ],
    summary: {
        totalItems: [


    {
        column: "aictBillingToLine",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },

    {
        name: 'SelectedRowCount',
    summaryType: 'custom',
    showInColumn: 'isChecked',
                    }
    ],
    calculateCustomSummary: function (options) {

                     if (options.name === "SelectedRowCount") {
                        if (options.summaryProcess === "finalize") {

                            var Selectednoofitems = 0;
    var totalselectedamount = 0;



    if (res.length) {
        res.forEach(c => {

            if (c.isChecked == true) {
                Selectednoofitems += 1;
                totalselectedamount += Math.round(c.aictBillingToLine);

            }
        });
                            }


    $('#TotalAmountOfBilling').val(totalselectedamount);
    $('#selectednoofitems').val(Selectednoofitems);
                           
                        }
                    }
                }
            },



        }).dxDataGrid("instance");

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }

    function selectAll() {

        var list = $("#gridlist").dxDataGrid("option", "dataSource");
    if (list.length) {
        list.forEach(c => c.isChecked = true);
    console.log("list", list)
    if (list.length) {
        Griddata(list);
            }

        }

    }
    function unselectAll() {

        var list = $("#gridlist").dxDataGrid("option", "dataSource");
    if (list.length) {
        list.forEach(c => c.isChecked = false);
    console.log("list", list)
    if (list.length) {
        Griddata(list);
            }

        }

    }

    function formfiled() {

    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function AddDeatil() {

        var reslist = [];
    reslist = $("#gridlist").dxDataGrid("option", "dataSource");
         
        var model = reslist.filter(c => c.isChecked == true);

    console.log("model", model);

    var ShippingAgent = $("#shippingAgentId option:selected").val();
    var salesTax = $("#salesTax").val();

    $.post('/Import/Setup/AddAictBillingToMSK?shippingagentId=' + ShippingAgent + '&SaleTax=' + salesTax, {model: model }, function (data) {

        billNo = data.invoiceNo;
    if (data.error == true) {
        alert(data.message);
            }
    else {
                if (billNo) {
        window.open('/import/reports/AICTInvoiceToLineView?BillNo=' + billNo, "_blank");
    window.location.href = window.location.href;
                }
            }

        });

    }

