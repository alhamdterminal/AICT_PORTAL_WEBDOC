


    function Finddetail() {

        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    var ShippingAgent = $("#shippingAgentId option:selected").val();
    var nature = $("#nature").val();
    if (ShippingAgent) {

        $.get('/Import/Setup/GetAictBillingSettleUnSettleInvoicesList?ShippingAgentId=' + ShippingAgent + '&nature=' + nature + '&fromdate=' + fromdate + '&todate=' + todate, function (res) {
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
    export: {
        enabled: true
            },
    onExporting(e) {

        console.log("e", e)
                const workbook = new ExcelJS.Workbook();

    const worksheet = workbook.addWorksheet('SettleUnSettleInvoices');

    DevExpress.excelExporter.exportDataGrid({
        component: e.component,
    worksheet,
    autoFilterEnabled: true,
                }).then(() => {
        workbook.xlsx.writeBuffer().then((buffer) => {

            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), "SettleUnSettleInvoices.xlsx");

        });
                });
    e.cancel = true;


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
    allowEditing: false,
    validationRules: [{type: "required" }],
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "serviceChargesPercentToLine",
    caption: "% of Other Service Charges to be Billed To MSk",
    format: "#,##0.##",
    allowEditing: false,
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


                },
    {
        dataField: "aictBillingNumber",
    caption: "Inv #",
    allowEditing: false,
                },
    ],



        }).dxDataGrid("instance");

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

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


