
    var grid;

    var containers = [];

    var gateinsummary = [];




    $(document).ready(function () {

        loadGrid();


    });




    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function loadGrid() {

        $.get('/APICalls/GetGateInGDs', function (data) {


            containers = data;

            console.log("containers", containers)

            var dataGrid = $("#gateinContainer").dxDataGrid({
                dataSource: containers,
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

                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },

                editing: {
                    mode: "batch",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                columns: [
                    {
                        dataField: "messageFrom",
                        caption: "Message Type",
                        allowEditing: false,
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "gdNumber",
                        caption: "GD Number",
                        allowEditing: false,
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "containerNo",
                        caption: "Container No",
                        allowEditing: false,
                    },
                    {
                        dataField: "clearingAgentName",
                        caption: "Clearing Agent Name",
                        allowEditing: false,
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "packageType",
                        caption: "Package Type",
                        allowEditing: false,
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "noOfPackages",
                        caption: "Manifest Packges",
                        allowEditing: false,
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "grossWeight",
                        caption: "Manifest Weight",
                        allowEditing: false,
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "remainingNoOfPackages",
                        caption: "Found / Remaining Packages",
                        validationRules: [{ type: "required" }],
                        cssClass: 'myClass'
                    },
                    {
                        dataField: "remainingGrossWeight",
                        caption: "Found / Remaining Weight",
                        validationRules: [{ type: "required" }],
                        cssClass: 'myClass'
                    },
                    {
                        dataField: "vehicleNo",
                        caption: "Vehicle No",
                        validationRules: [{ type: "required" }],

                        cssClass: 'myClass'
                    },

                    {
                        caption: "",
                        dataField: "isGateIn",
                        dataType: "boolean",
                        //onValueChanged: function (container, options) {
                        //    console.log("container", container)
                        //    console.log("options", options)
                        //    //$('<div>')
                        //    //    .appendTo(container)
                        //    //    .dxCheckBox({
                        //    //        onValueChanged: function (args) {
                        //    //            console.log("args", args)
                        //    //            //var dataSource = dataGrid.option("dataSource");
                        //    //            //$.each(dataSource, function (_, item) {
                        //    //            //    item.isHired = args.value;
                        //    //            //});
                        //    //            //dataGrid.cancelEditData()
                        //    //            //dataGrid.refresh();
                        //    //        }
                        //    //    })
                        //    //    .on("dxclick", function (e) {
                        //    //        e.stopPropagation();
                        //    //    })
                        //}

                    },
                    {
                        width: 100,
                        alignment: 'center',
                        cellTemplate: function (container, options) {
                            $('<a/>').addClass('dx-link')
                                .text('Detail')
                                .on('dxclick', function () {
                                    showGdarrivalSummary(options.row.data)
                                })
                                .appendTo(container);
                        }
                    },

                ],




            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');




        });

    }






    function refresh() {
        window.location.reload();
    }


    function gateIn() {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

    console.log("gate in", containers);


        var gateInContainers = containers.filter(c => c.isGateIn == true);


    console.log("ready to gate in", gateInContainers)


    $.post('GateInGds', {cargoList: gateInContainers }, function (data) {

            if (data.error) {
        //showToast(data.message, "error");

        alert(data.message);

            }
    else {
        showToast(data.message, "success");
    window.setInterval('refresh()', 3000);

            }
        });

    }


    function formfiled() {


    }


    function showGdarrivalSummary(data) {

        console.log("data", data);

    $.get('/Export/GateInOut/GetGateInGdInfo?gdnumber=' + data.gdNumber, function (data) {
        gateinsummary = data;
    var dataGrid = $("#GdArrivalSummary").dxDataGrid({
        dataSource: gateinsummary,
    keyExpr: "gateInExportId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
                },
    editing: {
        mode: "cell",
    allowUpdating: true,
    //allowDeleting: true,
    selectTextOnEditStart: true,
    startEditAction: "click"
                },
    columns: [
    {
        dataField: "gdNumber",
    caption: "GD Number",
    allowEditing: false,
                    },
    {
        dataField: "packageType",
    caption: "Package Type",
    allowEditing: false,
                    },
    {
        dataField: "numberofPackages",
    caption: "No of Packages",
    allowEditing: false,
                    },
    {
        dataField: "weight",
    caption: "Package Type",
    allowEditing: false,
                    },
    {
        dataField: "gateInDate",
    caption: "Gate In Date",
    dataType: "date",
    format: "dd/MM/yyyy hh:mm:ss",
    allowEditing: false,
                    },
    {
        dataField: "vehicleNumber",
    caption: "Vehicle No",
    allowEditing: false,
                    },
    {
        dataField: "gateInStatus",
    caption: "Status",
    allowEditing: false,
                    },

    ],
                

            }).dxDataGrid("instance");


        });
    }

