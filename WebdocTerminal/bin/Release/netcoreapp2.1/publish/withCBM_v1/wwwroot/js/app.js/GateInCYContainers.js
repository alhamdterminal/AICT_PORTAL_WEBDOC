

    var grid;

    var containers = [];

    var shippingAgents = [];

    //$(function () {
        //    formfiled();


        //});

        function checkColumn(e, field) {

            if (e.parentType === "dataRow" && e.dataField !== field) {
                return;
            }

            e.editorOptions.disabled = true;
        }

    function hideMenifestedColumns(e) {

        checkColumn(e, "containerNo");
    checkColumn(e, "isoCode");
    checkColumn(e, "virNo");
    checkColumn(e, "carrierId");
    checkColumn(e, "carrierName");
    checkColumn(e, "pacssSealNo");
    checkColumn(e, "vehicleNo");
    checkColumn(e, "berthAt");

        if (PermissionInputs.find(x => x.fieldName == "ContainerSize" && x.isChecked == false)) {

        checkColumn(e, "containerSize");
        }
        if (PermissionInputs.find(x => x.fieldName == "FoundSealNumber" && x.isChecked == false)) {

        checkColumn(e, "foundSealNumber");
        }
        if (PermissionInputs.find(x => x.fieldName == "Weight" && x.isChecked == false)) {

        checkColumn(e, "weight");
        }
        if (PermissionInputs.find(x => x.fieldName == "ShippingAgentId" && x.isChecked == false)) {

        checkColumn(e, "shippingAgentId");
        }
        if (PermissionInputs.find(x => x.fieldName == "DPTareweight" && x.isChecked == false)) {

        checkColumn(e, "dpTareweight");
        }
        if (PermissionInputs.find(x => x.fieldName == "DPManifestWeight" && x.isChecked == false)) {

        checkColumn(e, "dpManifestWeight");
        }
        if (PermissionInputs.find(x => x.fieldName == "IsGateIn" && x.isChecked == false)) {

        checkColumn(e, "isGateIn");
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

        $.get('/APICalls/GetGateInCYContainers', function (data) {

            console.log(data)

            $.get('/APICalls/GetShippingAgents', function (agents) {

                if (dataSource)
                    containers = dataSource
                else
                    containers = data;

                shippingAgents = agents;

                console.log(containers)

                var dataGrid = $("#gateinContainer").dxDataGrid({
                    dataSource: containers,
                    keyExpr: "containerId",
                    allowColumnResizing: true,
                    columnAutoWidth: true,
                    wordWrapEnabled: true,
                    showBorders: true,
                    showBorders: true,


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

                        allowUpdating: true,
                        selectTextOnEditStart: true,
                        startEditAction: "click",
                        mode: "cell"
                    },
                    columns: [
                        "containerNo",
                        {
                            dataField: "containerSize",
                            caption: "Container Size",
                            validationRules: [{ type: "required" }],

                            lookup: {
                                dataSource: [{ containerSize: 20 }, { containerSize: 40 }, { containerSize: 45 }],
                                displayExpr: "containerSize",
                                valueExpr: "containerSize"
                            }
                        },
                        //"containerSize",
                        "isoCode",
                        "virNo",
                        "carrierId",
                        "carrierName",
                        "pacssSealNo",
                        {
                            dataField: "foundSealNumber",
                            validationRules: [{ type: "required" }],

                            caption: "Shipper Seal"
                        },
                        "berthAt",
                        "vehicleNo",
                        "weight",
                        {
                            dataField: "shippingAgentId",
                            caption: "Shipping Agent",
                            width: 200,
                            validationRules: [{ type: "required" }],

                            lookup: {
                                dataSource: shippingAgents,
                                displayExpr: "name",
                                valueExpr: "shippingAgentId"
                            }
                        },
                        {
                            dataField: "dpTareweight",
                            caption: "Container Tare Weight",
                            validationRules: [{ type: "required" }]
                        },
                        {
                            dataField: "dpManifestWeight",
                            caption: "Port Manifest Weight",
                            validationRules: [{ type: "required" }]
                        },

                        {
                            caption: "",
                            dataField: "isGateIn",
                            dataType: "boolean"
                        }
                    ],
                    onEditorPreparing: function (e) {
                        if (aa == true) {

                            hideCell(e)
                        }
                        else {
                            hideMenifestedColumns(e);
                        }

                    },
                    onRowUpdated: function (e) {

                    },
                    onCellClick: function (e) {

                        if (e.column.dataField == "isGateIn") {
                            //if (PermissionInputs.find(x => x.fieldName == "IsGateIn" && x.isChecked == false)) {


                            //}
                            //else {
                            if (e.row.data.containerSize == null || e.row.data.foundSealNumber == null || e.row.data.weight == null ||
                                e.row.data.shippingAgentId == null || e.row.data.dpTareweight == null || e.row.data.dpManifestWeight == null) {
                                var ad = document.getElementsByClassName("dx-checkbox")[e.rowIndex]
                                ad.classList.remove('dx-checkbox-checked')
                                $(".dx-checkbox :input")[e.rowIndex].value = false
                                e.value = false;
                                e.data.isGateIn = false;
                                showToast("Please Select All Required Fields ", "error");
                            }
                            else {

                                if ((e.row.data.dpManifestWeight != 0) && (e.row.data.dpTareweight != 0) && (e.row.data.foundWeight != 0)) {
                                    var x = Number(e.row.data.dpManifestWeight) + Number(e.row.data.dpTareweight);

                                    var y = Number(e.row.data.weight);

                                    var z = ((x / y) * 100)



                                    if (((z > 105) || (z < 95))) {
                                        var r = confirm("The gate in weight is exceeding the +-/5% limit. Are you Sure you want to gate in?");


                                        if (r == true) {
                                            var ad = document.getElementsByClassName("dx-checkbox")[e.rowIndex]

                                            ad.classList.add('dx-checkbox-checked')
                                            $(".dx-checkbox :input")[e.rowIndex].value = true
                                            e.data.isGateIn = true;
                                            e.value = true;

                                        } else {
                                            var ad = document.getElementsByClassName("dx-checkbox")[e.rowIndex]
                                            ad.classList.remove('dx-checkbox-checked')
                                            $(".dx-checkbox :input")[e.rowIndex].value = false
                                            e.value = false;
                                            e.data.isGateIn = false;

                                        }

                                    }


                                }
                                if (e.value == true) {
                                    onclickonCell(true)
                                }
                                else {
                                    onclickonCell(false)
                                }
                                indexNo = [e.rowIndex][0]
                            }

                            // }




                        }

                    },

                }).dxDataGrid("instance");

                $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
                $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

            });

        });

    }
    var indexNo = "";
    function checkCell(e, field) {

        if (indexNo != null) {

            if (e.row.dataIndex == indexNo && e.dataField == field) {
        e.editorOptions.disabled = true;
            }

        }



    }


    function hideCell(e) {
        checkCell(e, "foundWeight");
    checkCell(e, "dpTareweight");
    checkCell(e, "dpManifestWeight");
    checkCell(e, "containerNo");
    checkCell(e, "isoCode");
    checkCell(e, "virNo");
    checkCell(e, "carrierId");
    checkCell(e, "carrierName");
    checkCell(e, "pacssSealNo");
    checkCell(e, "vehicleNo");
    checkCell(e, "berthAt");
    checkCell(e, "weight");

    }

    var aa = "";
    function onclickonCell(x) {

        if (x) {
        aa = x
    }
    else {
        aa = x
    }

    }
    function gateIn() {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

        var gateInContainers = containers.filter(c => c.isGateIn == true);

    console.log(gateInContainers)
    $.post('SaveGateInContainersCY', {containers: gateInContainers }, function (data) {

            if (data.error) {
        showToast(data.message, "error");
    window.location.href = window.location.href;

    return;
            }
    else {
        showToast("Gate In Created ", "success");
    window.location.href = window.location.href;

            }

    window.location.href = window.location.href;
            loadGrid(containers.filter(c => c.isGateIn == false));
        });
    }

    function GetCYPaiosByDate() {
        var IPaos = []
    var fromdate = document.getElementById("single_cal2").value;
    console.log(fromdate);
    $.get('/APICalls/GetCYIPAOSByDate?fromdate=' + fromdate, function (data) {
        IPaos = data;
    console.log(IPaos);
    var dataGrid = $("#ipoas").dxDataGrid({
        dataSource: IPaos,
    keyExpr: "ipaoId",
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
    selectTextOnEditStart: true,
    startEditAction: "click"
                },
    columns: [
    "totalRecords",
    "recordSequence",
    "virNumber",
    "bondedCarrierId",
    "bondedCarrierName",
    "vehicleNumber",
    ],
    onRowUpdated: function (e) {

    },
    onRowRemoved: function (e) {
        console.log(e)
                    var containerNumber = e.data.containerNumber;

    $.post('SaveDeletedContainersCY?containerNumber=' + containerNumber, function (data) {

        showToast("Container Removed ", "successdev ");

    loadGrid();
                    });
                }

            }).dxDataGrid("instance");


        });
    }




    function formfiled() {
        loadGrid();
    }

