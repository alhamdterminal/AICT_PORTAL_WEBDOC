

    $(function () {
        listGrid();

    });
    var resultdata = [];
    function listGrid() {


        $.get('/APICalls/GetExportAlongSideData', function (data) {

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

    const worksheet = workbook.addWorksheet('Along Side Data');

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

            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), "Along Side Data.xlsx");

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
        dataField: "gdNumber",
    caption: "GD No",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                },
    {
        dataField: "cargoReceivedDate",
    caption: "Cargo Received",
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
        dataField: "portOfDischarge",
    caption: "Port Of Discharge",
    allowEditing: false,
    cssClass: 'myClass'
                },


    {
        dataField: "vesselVoyage",
    caption: "Vessel / Voyage",
    allowEditing: false,
    cssClass: 'myClass'

                },
    {
        dataField: "numberofPackge",
    caption: "No of Packge",
    allowEditing: false,
    cssClass: 'myClass'

                },
    {
        dataField: "packgeType",
    caption: "Packge Type",
    allowEditing: false,
    cssClass: 'myClass'

                },
    {
        dataField: "cbm",
    caption: "CBM",
    allowEditing: false,
    cssClass: 'myClass'

                },
    {
        dataField: "weight",
    caption: "Weight",
    allowEditing: false,
    cssClass: 'myClass'

                },
    {
        dataField: "allowLoading",
    caption: "Allow Loading",
    allowEditing: false,
    cssClass: 'myClass'

                },

    {
        dataField: "noc",
    caption: "NOC",
    allowEditing: false,
    cssClass: 'myClass'

                },
    {
        dataField: "paperReady",
    caption: "Paper Ready",
    allowEditing: false,
    dataType: 'date',
    format: 'dd/MM/yyyy',
    cssClass: 'myClass'
                },

    {
        dataField: "anf",
    caption: "ANF",
    allowEditing: false,
    dataType: 'date',
    format: 'dd/MM/yyyy',
    cssClass: 'myClass'
                },
    {
        dataField: "remarks",
    caption: "Remarks",
    allowEditing: false,
    cssClass: 'myClass'
                },

    ],



        }).dxDataGrid("instance");




    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }

