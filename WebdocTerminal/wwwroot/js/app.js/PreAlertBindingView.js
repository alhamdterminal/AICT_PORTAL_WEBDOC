

    var igm;
    var vessel;


    function igm_changed(data) {
        igm = data.value;         
    }

    function vessel_changed(data) {

        $("#voyageid").empty();
    vessel = data.value;

    $.get('/APICalls/GetPreAlertVoyage?vesselname=' + vessel, function (res) {

        res.filter(x => {
            $('#voyageid').append($('<option>').val(x).text(x))

        }); 
        });
    }

    //var virnumberList = [];

    //$(function () {
        //$.get('/APICalls/GetALlVirnumber', function (res) {
        //    if (res) {
        //        virnumberList = res;
        //    }
        //    else {
        //        virnumberList = []
        //    }
        //});
        //});

        function find() {


            var shippingAgent = $("#shippingAgent").val();
            var line = $("#line").val();
            var portAndTerminal = $("#portAndTerminal").val();
            var fromdate = document.getElementById("fromdate").value;
            var todate = document.getElementById("todate").value;
            var voyage = $("#voyageid").val();
            console.log("vessel", vessel);
            console.log("voyageid", $("#voyageid").val());
            if (vessel == undefined || vessel == null || vessel == "null") {
                vessel = "";
            }
            if (voyage == undefined || voyage == null || voyage == "null") {
                voyage = "";
            }


            listGrid(shippingAgent, line, portAndTerminal, vessel, voyage, fromdate, todate)

        }

    var resdata = [];

    function listGrid(shippingAgent, line, portAndTerminal, vessel, voyage, fromdate, todate) {

        $.get('/APICalls/GetUnBindPreAlert?shippingAgent=' + shippingAgent + '&line=' + line + '&vessel=' + vessel + '&voyage=' + voyage + '&portAndTerminal=' + portAndTerminal + '&from=' + fromdate + '&to=' + todate, function (data) {

            if (data) {

                resdata = data;
                console.log("resdata", resdata)
                gridData(resdata);
            }

            else {
                resdata = []
                gridData(resdata);

            }


        });
    }



    function gridData(res) {

        resdata = res;

    console.log("containers", resdata);
    //console.log("virnumberList", virnumberList);


    var grid = $("#unbindgrid").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    var dataGrid = $("#unbindgrid").dxDataGrid({
        dataSource: resdata,
    keyExpr: "preAlterDetailId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    columnResizingMode: 'widget',
    allowColumnReordering: true,
    paging: {
        //enabled: false
        pageSize: 100,
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

    columns: [
    {
        dataField: "containerNo",
    caption: "Container No",
    allowEditing: false,
    fixed: true
                },
    {
        dataField: "masterBLNumber",
    caption: "MBL",
    allowEditing: false,
    fixed: true
                },
    {
        dataField: "size",
    caption: "Size",
    allowEditing: false,
    fixed: true
                },
    {
        dataField: "type",
    caption: "Type",
    allowEditing: false,
    fixed: true
                },
    {
        dataField: "cargoType",
    caption: "Cargo Type",
    allowEditing: false,
    fixed: true
                },
    {
        dataField: "lineFFName",
    caption: "FF / Line Name",
    allowEditing: false,
    fixed: true

                },
    {
        dataField: "soccoc",
    caption: "Good Type",
    allowEditing: false,
    fixed: true
                },
    {
        dataField: "portAndTerminalName",
    caption: "POL",
    allowEditing: false,
    fixed: true
                },
    {
        dataField: "preAlertDate",
    caption: "Alert Date",
    allowEditing: false,
    dataType: 'date',
    format: 'dd/MM/yyyy', 
                },


                //{
        //    datafield: "virnumber",
        //    caption: "igm #",
        //    width: 250,
        //    allowEditing: false,
        //    //lookup: {
        //    //    datasource: {
        //    //        store: $('/apicalls/getvirno', function (res) {
        //    //            console.log("res", res);
        //    //        }),
        //    //        requiretotalcount: true,
        //    //        pagesize: 4,
        //    //        paginate: true,
        //    //    },

        //    //lookup: {
        //    //    datasource: {
        //    //        store: virnumberlist,
        //    //        requiretotalcount: true,
        //    //        pagesize: 4,
        //    //        paginate: true,
        //    //    },
        //    //    displayexpr: "text",
        //    //    valueexpr: "value"
        //    //}

        //},

        {
            dataField: "isCheck",
            caption: "Check",
            dataType: "boolean"
        },

            ]


        }).dxDataGrid("instance");


    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }




    function formfiled() {

    }


    function selectAll() {

        resdata.forEach(c => c.isCheck = true);

    gridData(resdata);

    }
    function unselectAll() {

        resdata.forEach(c => c.isCheck = false);

    gridData(resdata);

    }

    function updateAlert() {

        console.log("igm", igm);
    console.log("resdata", resdata);

        var datalist = resdata.filter(c => c.isCheck == true);


    if (igm) {

            if (datalist.length) {
        $.post('/Import/PreAlert/BindWithIGM?igm=' + igm, { datalist: datalist }, function (result) {

            if (result.error) {
                return alert(result.message);
            }

            else {
                alert(result.message);

            }

            window.setInterval('refresh()', 3000);

        })
    }
    else {
        alert("please select data");
            }
            
        }
    else {
        alert("please select igm")
    }
    }


    function refresh() {
        window.location.reload();
    }

