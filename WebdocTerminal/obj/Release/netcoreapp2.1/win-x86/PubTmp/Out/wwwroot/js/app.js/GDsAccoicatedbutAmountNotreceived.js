

    $(function () {
        listGrid();

    });
    var resultdata = [];
    function listGrid() {


        $.get('/APICalls/GetGDsAccoicatedbutAmountNotreceivedViewModel', function (data) {

            if (data) {

                resultdata = data;


                console.log("resultdata", resultdata)


                containerIndexesGrid(resultdata)
            }

            else {
                resultdata = []
                containerIndexesGrid(resultdata);

            }


        });


    }




    function formfiled() {

    }



    function containerIndexesGrid(res) {


        var grid = $("#CargoInformation").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    resultdata = res;

    console.log("resultdata", resultdata)


    var grid = $("#CargoInformation").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    var dataGrid = $("#CargoInformation").dxDataGrid({
        dataSource: resultdata,
    keyExpr: "gdNumber",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
            },

    export: {
        enabled: true
            },
    onExporting(e) {

        console.log("e", e)

                const workbook = new ExcelJS.Workbook();

    const worksheet = workbook.addWorksheet('Un Settled Invoices');

    DevExpress.excelExporter.exportDataGrid({
        component: e.component,
    worksheet,
    autoFilterEnabled: true,
                }).then(() => {
        workbook.xlsx.writeBuffer().then((buffer) => {


            // var griddata = $("#CargoInformation").dxDataGrid("option", "dataSource");
            //var resffandContainerno = "";


            //console.log("griddata", griddata)
            //if (griddata.length) {

            //    resffandContainerno  = griddata[0].containerNumber + "-" + griddata[0].shippingAgent + ".xlsx";
            // }

            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), "Un Settled Invoices.xlsx");

        });
                });
    e.cancel = true;


            },
    editing: {
        allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click",
    mode: "cell"

            },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

            },
    filterRow: {
        visible: true,
    applyFilter: 'auto',
            },

    scrolling: {
        columnRenderingMode: 'virtual',
            },
    columns: [
    {

        caption: "Serial No",
    dataField: "serialNumber",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass',
                    //calculateCellValue: function (rowData , e) {

        //    console.log("rowData", rowData)
        //    console.log("e", e)

        //    return this.defaultCalculateCellValue(rowData);
        //}

    },
    {
        dataField: "shippingAgent",
    caption: "Shipping Agent",
    allowEditing: false,
    cssClass: 'myClass',

                },
    {
        dataField: "clearingAgent",
    caption: "Clearing Agent",
    allowEditing: false,
    cssClass: 'myClass',

                },
    {
        dataField: "shipper",
    caption: "Shipper",
    allowEditing: false,
    cssClass: 'myClass',

                },
    {
        dataField: "gdNumber",
    caption: "GD No",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                },
    {
        dataField: "invoiceNumber",
    caption: "Invoice No",
    allowEditing: false,
    cssClass: 'myClass'
                },
    {
        dataField: "invoiceDate",
    caption: "Invoice Date",
    allowEditing: false,
    dataType: 'date',
    format: 'dd/MM/yyyy',
    cssClass: 'myClass'
                },

    {
        dataField: "amountExTax",
    caption: "Amount Ex Tax",
    allowEditing: false,
    cssClass: 'myClass'

                },
    {
        dataField: "salesTax",
    caption: "Sales Tax",
    allowEditing: false,
    cssClass: 'myClass'

                },
    {
        dataField: "amountIncTax",
    caption: "Amount Inc Tax",
    allowEditing: false,
    cssClass: 'myClass'

                },
    {
        dataField: "approvedBy",
    caption: "Approved -  By",
    allowEditing: false,
    cssClass: 'myClass'

                },

    ],



        }).dxDataGrid("instance");




    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }

