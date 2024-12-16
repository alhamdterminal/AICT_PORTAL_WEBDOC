

    var grid;

    var onOptionChanged = function (e) {
        console.log(e);
    };

    var containers = [];

    var shippingAgents = [];

    var shippingLines = [];

    $(function () {


        $.get('/APICalls/GetShippingAgents', function (agents) {
            console.log("agents", agents)
            shippingAgents = agents;
        });

    $.get('/APICalls/GetShippingLines', function (lines) {
        console.log(lines)
            shippingLines = lines;
        });
    loadGrid();
    });

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "containerNumber");
    checkColumn(e, "containerSize");
    checkColumn(e, "shippingAgentId");
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

        $.get('/APICalls/GetEmptyGateOutContainer', function (data) {
            if (dataSource)
                containers = dataSource

            else
                containers = data;

            console.log(containers);
            console.log("s", shippingAgents);
            console.log("l", shippingLines);



            var dataGrid = $("#emptyOutContainers").dxDataGrid({
                dataSource: containers,
                keyExpr: "containerId",

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
                    mode: "batch",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                columns: [

                    "containerNumber",
                    "containerSize",
                    "shiftedYard",
                    "vehicleNumber",

                    {
                        dataField: "shippingAgentId",
                        caption: "Agent",
                        width: 200,
                        lookup: {
                            dataSource: shippingAgents,
                            displayExpr: "name",
                            valueExpr: "shippingAgentId"
                        }
                    },
                    "containerCondition",
                    {
                        dataField: "shippingLineId",
                        caption: "Shipping Line",
                        width: 200,
                        lookup: {
                            dataSource: shippingLines,
                            displayExpr: "shippingLineName",
                            valueExpr: "shippingLineId"
                        }
                    },
                    {
                        dataField: "isEmptyOut",
                        caption: "",
                        dataType: "boolean"
                    }
                ],
                onEditorPreparing: function (e) {
                    console.log(e);
                    hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {

                    $("#btnSubmit").attr("disabled", false);
                }

            }).dxDataGrid("instance");





        });
    }


    function emptyOut() {

        $(this).children('#btnSubmit').prop('disabled', true);

        var emptyGateOut = containers.filter(c => c.isEmptyOut == true);
    console.log(emptyGateOut)

    $.post('AddEmptyGateOutContainers', {model: emptyGateOut }, function (data) {

        showToast("Empty Gate-Out Created", "success");

                loadGrid(containers.filter(c => c.isEmptyOut == false));
             
        });
    }


