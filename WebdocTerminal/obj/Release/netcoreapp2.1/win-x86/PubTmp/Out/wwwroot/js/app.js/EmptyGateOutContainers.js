

    var grid;

    var onOptionChanged = function (e) {
        console.log(e);
    };

    var containers = [];

    var shippingAgents = [];
    var deliveredYards = [];
    var vehicles = [];
    var  transporters = [];

    $(function () {


        $.get('/APICalls/GetShippingAgents', function (agents) {
            console.log(agents)
            shippingAgents = agents;
        });


    $.get('/APICalls/GetDeliveredYards', function (dYards) {
        console.log(dYards)
            deliveredYards = dYards;
        });


    $.get('/APICalls/GetAllVehicles', function (data) {

        transporters = data;

    console.log(transporters)

        });


      //  formfiled();
       
    });



    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "virNumber");
    checkColumn(e, "containerNumber");
    checkColumn(e, "containerSize");
    checkColumn(e, "shippingAgentId");
    checkColumn(e, "shippingLine");
    checkColumn(e, "type");

        if (PermissionInputs.find(x => x.fieldName == "IsEmptyOut" && x.isChecked == false)) {
        checkColumn(e, "isEmptyOut");
        }
        if (PermissionInputs.find(x => x.fieldName == "ShiftedYard" && x.isChecked == false)) {
        checkColumn(e, "shiftedYard");
        }
        if (PermissionInputs.find(x => x.fieldName == "VehicleNumber" && x.isChecked == false)) {
        checkColumn(e, "vehicleNumber");
        }
        if (PermissionInputs.find(x => x.fieldName == "ContainerCondition" && x.isChecked == false)) {
        checkColumn(e, "containerCondition");
        }
        if (PermissionInputs.find(x => x.fieldName == "ShippingLineId" && x.isChecked == false)) {
        checkColumn(e, "shippingLineId");
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

        $.get('/APICalls/GetEmptyGateOutContainer', function (data) {


            console.log("data", data);

            if (dataSource)
                containers = dataSource

            else
                containers = data;

            var dataGrid = $("#emptyOutContainers").dxDataGrid({
                dataSource: containers,
                keyExpr: "key",

                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    pageSize: 10
                },
                editing: {
                    mode: "form",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },
                columns: [

                    "type",
                    "virNumber",
                    "containerNumber",
                    "containerSize",
                    {
                        dataField: "deliveredYard",
                        caption: "Delivered Yard",
                        width: 200,
                        lookup: {
                            dataSource: deliveredYards,
                            displayExpr: "deliveredYardName",
                            valueExpr: "deliveredYardName"
                        }
                    },
                    {
                        dataField: "deliveredYardDate",
                        caption: "Delivered  Date",
                        dataType: 'date',
                        format: 'dd/MM/yyyy',
                        width: 100,
                        validationRules: [{ type: "required" }]

                    },
                    {
                        dataField: "vehicleNumber",
                        caption: "Vehicle",
                        width: 200,
                        //setCellValue: function (rowData, value) {
                        //    rowData.vehicleNumber = value;
                        //    var t = vehicles.find(d => d.vehicleNumber == value);
                        //    console.log(t, "t");
                        //    if (t != null || t != undefined) {
                        //        rowData.transporterName = t.transporterName;
                        //    }
                        //},
                        //lookup: {
                        //    dataSource: vehicles,
                        //    displayExpr: "vehicleNumber",
                        //    valueExpr: "vehicleNumber"
                        //},
                        validationRules: [{ type: "required" }]

                    },
                    {
                        dataField: "transporterId",
                        caption: "Transporter",
                        lookup: {
                            dataSource: transporters,
                            displayExpr: "transporterName",
                            valueExpr: "transporterId"
                        },
                        validationRules: [{ type: "required" }]

                    },


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
                    {
                        dataField: "containerCondition",
                        caption: "Container Condition",
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "shippingLine",
                        caption: "Shipping Line",

                    }
                ],
                onEditorPreparing: function (e) {

                    hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {
                    console.log("hello", e)


                    $.post('AddEmptyGateOutContainersCFS', { model: e.data }, function (data) {
                        if (data.error == true) {
                            showToast(data.message, "error")

                        } else {

                            var Status = "EMPTY DELIVERY";

                            showToast(data.message, "success");

                            window.open('/Import/Reports/EquipmentInterchangeAndInspectionReport?igm=' + e.data.virNumber + '&ContainerNumber=' + e.data.containerNumber + '&Status=' + Status, "_blank");

                            window.open('/import/reports/EmptyGatePassReport?id=' + data.emptyGateOutContainerno, "_blank");


                        }


                        window.location.href = window.location.href;
                    });

                }

            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


        });
    }


    function emptyOut() {

        $(this).children('#btnSubmit').prop('disabled', true);

        var emptyGateOut = containers.filter(c => c.isEmptyOut == true);

    console.log(emptyGateOut)

    $.post('AddEmptyGateOutContainers', {model: emptyGateOut }, function (data) {

             

            if (data.error == true) {
        showToast(data.message, "error")

    } else {
                var res = emptyGateOut.pop();

    console.log("res res res", res)


    showToast(data.message, "success")

    console.log("res",res)

    if (res) {


                    var Containernumber = res.containerNumber
    var VirNo = res.virNumber
    var Status = "EMPTY DELIVERY"

    console.log("container number ", Containernumber)
    console.log("VirNo", VirNo)
    console.log("Status ", Status)

    window.open('/Import/Reports/EquipmentInterchangeAndInspectionReport?igm=' + VirNo + '&ContainerNumber=' + Containernumber + '&Status=' + Status, "_blank");


                }

    window.location.href = window.location.href;
                

            }

        });
    }



    function formfiled() {

        loadGrid();

    }



