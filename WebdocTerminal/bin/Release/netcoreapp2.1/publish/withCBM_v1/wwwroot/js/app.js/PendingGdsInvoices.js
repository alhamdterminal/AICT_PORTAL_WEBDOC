

    $(function () {
        listGrid();

    });
    var resultdata = [];
    function listGrid() {


        $.get('/APICalls/GetPendingGdsInvoicesViewModel', function (data) {

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

    const worksheet = workbook.addWorksheet('Pending Invoices - GDs');

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

            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), "Pending Invoices - GDs.xlsx");

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
        dataField: "serialNumber",
    caption: "Serial No",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                },
    {
        dataField: "gdNumber",
    caption: "GD No",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                },
    {
        dataField: "gateInDate",
    caption: "GateIn",
    allowEditing: false,
    dataType: 'date',
    format: 'dd/MM/yyyy',
    cssClass: 'myClass'
                },
    {
        dataField: "vessel",
    caption: "Vessel",
    allowEditing: false,
    cssClass: 'myClass'
                },
    {
        dataField: "voyage",
    caption: "Voyage",
    allowEditing: false,
    cssClass: 'myClass'

                },
    {
        dataField: "etd",
    caption: "ETD",
    allowEditing: false,
    dataType: 'date',
    format: 'dd/MM/yyyy',
    cssClass: 'myClass'
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
        dataField: "cbm",
    dataType: "number",
    caption: "CBM",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "weight",
    dataType: "number",
    caption: "Weight",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },


    ],



        }).dxDataGrid("instance");




    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }

