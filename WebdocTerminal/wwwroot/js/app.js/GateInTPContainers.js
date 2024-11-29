


    var containers = [];

    var shippingAgents = [];


    //$(function () {

        //    loadGrid();

        //})

        function showToast(message, icon) {

            $.toast({
                heading: message,
                showHideTransition: 'slide',
                position: 'bottom-right',
                icon: icon
            });
        }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {


        if (PermissionInputs.find(x => x.fieldName == "ContainerNo" && x.isChecked == false)) {

        checkColumn(e, "containerNo");
        }

        if (PermissionInputs.find(x => x.fieldName == "ContainerSize" && x.isChecked == false)) {

        checkColumn(e, "containerSize");
        }

        if (PermissionInputs.find(x => x.fieldName == "IsoCode" && x.isChecked == false)) {

        checkColumn(e, "isoCode");
        }
        if (PermissionInputs.find(x => x.fieldName == "VirNo" && x.isChecked == false)) {

        checkColumn(e, "virNo");
        }
        if (PermissionInputs.find(x => x.fieldName == "CarrierId" && x.isChecked == false)) {

        checkColumn(e, "carrierId");
        }
        if (PermissionInputs.find(x => x.fieldName == "CarrierName" && x.isChecked == false)) {

        checkColumn(e, "carrierName");
        }

        if (PermissionInputs.find(x => x.fieldName == "FoundSealNumber" && x.isChecked == false)) {

        checkColumn(e, "foundSealNumber");
        }

        if (PermissionInputs.find(x => x.fieldName == "PacssSealNo" && x.isChecked == false)) {

        checkColumn(e, "pacssSealNo");
        }
        if (PermissionInputs.find(x => x.fieldName == "VehicleNo" && x.isChecked == false)) {

        checkColumn(e, "vehicleNo");
        }
        if (PermissionInputs.find(x => x.fieldName == "Weight" && x.isChecked == false)) {

        checkColumn(e, "weight");
        }
        if (PermissionInputs.find(x => x.fieldName == "ShippingAgentId" && x.isChecked == false)) {

        checkColumn(e, "shippingAgentId");
        }
        if (PermissionInputs.find(x => x.fieldName == "IsGateOut" && x.isChecked == false)) {

        checkColumn(e, "isGateOut");
        }

    }
    function loadGrid() {

        $.get('/APICalls/GetGateInTPContainers', function (data) {
            console.log("tedtData", data)

            containers = data;

            var grid = $("#gateinContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


            var dataGrid = $("#gateinContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "svmoId",
                wordWrapEnabled: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                showBorders: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    enabled: true,
                    pageSize: 10
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
                        dataField: "virNo",
                        caption: "Vir No",
                        allowEditing: false,

                    },
                    {
                        dataField: "indexNo",
                        caption: "Index No",
                        allowEditing: false,

                    },
                    {
                        dataField: "containerNo",
                        caption: "Container No",
                        allowEditing: false,

                    },

                    {
                        dataField: "gateOutTime",
                        caption: "Out Time",
                        dataType: 'date',
                        format: 'dd/MM/yyyy',
                        allowEditing: false,
                        width: 100
                    },
                    {
                        dataField: "status",
                        caption: "Status",
                        allowEditing: false,

                    },
                    {
                        dataField: "vehicleNo",
                        caption: "Vehicle No",
                        allowEditing: false,

                    },
                    {
                        dataField: "pccssSealNumber",
                        caption: "Seal Number",
                        allowEditing: false,

                    },
                    {
                        dataField: "bondedCarrier",
                        caption: "Bonded Carrier",
                        allowEditing: false,

                    },
                    {
                        dataField: "countryOfExit",
                        caption: "Country Of Exit",
                        allowEditing: false,

                    },

                    {
                        dataField: "grossWeight",
                        caption: "Weight",
                        dataType: "number",
                        validationRules: [{ type: "required" }],
                        editorOptions: {
                            step: 0
                        }
                    },
                    {
                        caption: "",
                        dataField: "isGateOut",
                        dataType: "boolean"
                    }
                ],
                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {

                    $("#btnSubmit").attr("disabled", false);
                }

            }).dxDataGrid("instance");



            grid.on('editorPrepared', function (e) {
                if (e.parentType !== 'dataRow') {
                    return;
                }
                e.editorElement[e.editorName]('instance').on('keyDown', function (arg) {

                    var gridComponent = e.component;

                    var event = arg.jQueryEvent;

                    if (event.keyCode === 38) {
                        if (e.editorName == "dxNumberBox") {
                            event.stopPropagation();
                            event.preventDefault();
                        }

                    } else if (event.keyCode === 40) {
                        if (e.editorName == "dxNumberBox") {
                            event.stopPropagation();
                            event.preventDefault();
                        }
                    }

                    else if (event.keyCode === 189) {
                        if (e.editorName == "dxNumberBox") {
                            event.stopPropagation();
                            event.preventDefault();
                        }

                    }
                });

            });

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });

    }

    function gateout() {

        var gateInContainers = containers.filter(c => c.isGateOut == true);
    //SaveGateOUTTPContainers
    $.post('AddGetOutTP', {model: gateInContainers }, function (data) {

            if (data.error == true) {

        loadGridhold(data.message);
    $('#HoldStatusModal').modal('toggle');

            }
    else {
        loadGridhold([]);
    showToast(data.message, "success");

    window.location.href = window.location.href;

            } 


        });
    }

    function loadGridhold(data) {

        $("#holdGrid").dxDataGrid({
            dataSource: data,
            keyExpr: "holdId",
            showBorders: true,
            allowColumnResizing: true,
            columnAutoWidth: true,
            paging: {
                pageSize: 10
            },
            searchPanel: {
                visible: true,
                width: 240,
                placeholder: "Search..."
            },

            columns: [
                {
                    dataField: "virNo",
                    caption: "IGM",
                    allowEditing: false,
                },
                {
                    dataField: "indexNo",
                    caption: "Index No",
                    allowEditing: false,
                },
                {
                    dataField: "auctionLotNo",
                    caption: "Auction Lot No",
                    allowEditing: false,
                },
                {
                    dataField: "holdDate",
                    caption: "Hold Date",
                    allowEditing: false,
                    dataType: 'date',
                    format: 'dd/MM/yyyy',
                    allowEditing: false,
                },
                {
                    dataField: "holdType",
                    caption: "hold Type",
                    allowEditing: false,
                },
                {
                    dataField: "specialInstructions",
                    caption: "special Instructions",
                    allowEditing: false,
                },
                {
                    dataField: "role",
                    caption: "Role",
                    allowEditing: false,
                },
                {
                    dataField: "isHold",
                    caption: "Hold Status",
                    allowEditing: false,
                },
                {
                    dataField: "ro",
                    caption: "RO Number",

                },
                {
                    dataField: "removeInstruction",
                    caption: "Remove Instruction",
                    validationRules: [{
                        type: "required",
                        message: "Remove Instruction Is Required"
                    }],

                },
            ],

        });

    }


    //function GetCFSPaiosByDate() {
        //    var IPaosCFS = []
        //    var fromdate = document.getElementById("single_cal2").value;
        //    console.log(fromdate);
        //    $.get('/APICalls/GetCFSIPAOSByDate?fromdate=' + fromdate, function (data) {
        //        IPaosCFS = data;
        //        var dataGrid = $("#ipoascfs").dxDataGrid({
        //            dataSource: IPaosCFS,
        //            keyExpr: "ipaoId",
        //            showBorders: true,
        //            showBorders: true,
        //            dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
        //            paging: {
        //                enabled: false
        //            },
        //            editing: {
        //                mode: "batch",
        //                allowUpdating: true,
        //                selectTextOnEditStart: true,
        //                startEditAction: "click"
        //            },
        //            columns: [
        //                "containerNumber",
        //                "virNumber",
        //                "bondedCarrierId",
        //                "bondedCarrierName",
        //                "vehicleNumber",
        //            ],
        //            onRowUpdated: function (e) {

        //                $("#btnSubmit").attr("disabled", false);
        //            }

        //        }).dxDataGrid("instance");


        //    });
        //}

        function formfiled() {
            loadGrid();
        }

