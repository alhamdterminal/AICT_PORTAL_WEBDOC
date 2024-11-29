

    var grid;

    var onOptionChanged = function (e) {
        console.log(e);
    };

    var containers = [];

    //var shippingAgents = [];

    //$(function () {

        //    loadGrid();
        //});

        function checkColumn(e, field) {

            if (e.parentType === "dataRow" && e.dataField !== field) {
                return;
            }

            e.editorOptions.disabled = true;
        }

    function hideMenifestedColumns(e) {

        checkColumn(e, "containerNumber");
    checkColumn(e, "virNumber");
    checkColumn(e, "bondedCarrier");
    checkColumn(e, "grossWeight");

        if (PermissionInputs.find(x => x.fieldName == "GateOutDate" && x.isChecked == false)) {

        checkColumn(e, "gateOutDate");
        }
        if (PermissionInputs.find(x => x.fieldName == "TruckNumber" && x.isChecked == false)) {

        checkColumn(e, "truckNumber");
        }
        if (PermissionInputs.find(x => x.fieldName == "SealNumber" && x.isChecked == false)) {

        checkColumn(e, "sealNumber");
        }
         if (PermissionInputs.find(x => x.fieldName == "PortOfExit" && x.isChecked == false)) {

        checkColumn(e, "portOfExit");
        }
        if (PermissionInputs.find(x => x.fieldName == "Country" && x.isChecked == false)) {

        checkColumn(e, "country");
        }
        if (PermissionInputs.find(x => x.fieldName == "TerminalWeight" && x.isChecked == false)) {

        checkColumn(e, "terminalWeight");
        }
        if (PermissionInputs.find(x => x.fieldName == "ShippingAgentId" && x.isChecked == false)) {

        checkColumn(e, "shippingAgentId");
        }
        if (PermissionInputs.find(x => x.fieldName == "IsGateOut" && x.isChecked == false)) {

        checkColumn(e, "isGateOut");
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
    function loadGrid(dataSource) {

        $.get('/APICalls/GetGateOutCYContainers', function (data) {



            if (dataSource)
                containers = dataSource
            else
                containers = data;


            console.log("containers", containers);


            //$.get('/APICalls/GetShippingAgents', function (agents) {
            //    console.log(containers)
            //    shippingAgents = agents;


            var grid = $("#gateoutContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');

            var dataGrid = $("#gateoutContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "cyContainerId",
                wordWrapEnabled: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                RowAlternationEnabled: true,
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
                        dataField: "type",
                        caption: "Type",
                        allowEditing: false,
                    },
                    {
                        dataField: "virNumber",
                        caption: "Vir No",
                        allowEditing: false,
                    },
                    {
                        dataField: "containerNumber",
                        caption: "Container Number",
                        allowEditing: false,
                    },
                    {
                        dataField: "oldContainerNumberForCS",
                        caption: "Old Container number",
                        allowEditing: false,
                    },
                    {
                        dataField: "gateOutDate",
                        caption: "Out Time",
                        dataType: 'date',
                        allowEditing: false,
                        format: 'dd/MM/yyyy'
                    },
                    {
                        caption: "Truck Number",
                        dataField: "truckNumber",
                    },
                    "sealNumber",
                    {
                        caption: "bonded Carrier",
                        dataField: "bondedCarrier",
                        allowEditing: false,
                    },
                    //"bondedCarrier",
                    {

                        caption: "GatePass Number",
                        allowEditing: false,
                        validationRules: [{ type: "required" }],
                        dataField: "gatePassNumber",
                    },
                    {
                        caption: "Port Of Exit",
                        allowEditing: false,
                        validationRules: [{ type: "required" }],
                        dataField: "portOfExit",
                    },
                    {
                        caption: "Country",
                        allowEditing: false,
                        validationRules: [{ type: "required" }],
                        dataField: "country",
                    },
                    {
                        caption: "Menifest Weight",
                        dataField: "grossWeight",
                    },
                    {
                        dataField: "terminalWeight",
                        dataType: "number",
                        validationRules: [{ type: "required" }],
                        caption: "Weight"
                    },
                    {
                        dataField: "status",
                        caption: "status",
                        allowEditing: false,
                        validationRules: [{ type: "required" }],

                    },
                    //{
                    //    dataField: "shippingAgentId",
                    //    caption: "Agent",
                    //    width: 200,
                    //    lookup: {
                    //        dataSource: shippingAgents,
                    //        displayExpr: "name",
                    //        valueExpr: "shippingAgentId"
                    //    }
                    //},
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
                });


            });

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

            //    })


        });
    }

    function gateOutCY() {

        $("#btnSubmit").attr("disabled", true);

        var gateOutCY = containers.filter(c => c.isGateOut == true);

    setTimeout(function(){$("#btnSubmit").attr("disabled", false); }, 120000);

    $.post('AddGetOutCY', {model: gateOutCY }, function (data) {
        console.log(data);
    if (data.error == true) {
        loadGridhold(data.message);
    $('#HoldStatusModal').modal('toggle');
            }
    else {
        loadGridhold([]);
    showToast(data.message, "success");
    window.location.href = window.location.href;
                loadGrid(containers.filter(c => c.isGateOut == false));


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


    function formfiled() {

        loadGrid();

    }



