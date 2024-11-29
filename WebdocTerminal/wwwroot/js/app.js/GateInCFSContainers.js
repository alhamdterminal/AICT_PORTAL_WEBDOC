

    var grid;

    var containers = [];

    var shippingAgents = [];

    var IPaosCFS = [];

    var TransporterLists = [];



    $(document).ready(function () {


        getAllTransporter();
    loadGrid();
       
        
     });


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
    checkColumn(e, "weight");
        if (PermissionInputs.find(x => x.fieldName == "ContainerSize" && x.isChecked == false)) {

        checkColumn(e, "containerSize");
        }

         if (PermissionInputs.find(x=> x.fieldName == "DPTareweight" && x.isChecked ==  false)) {
        checkColumn(e, "dpTareweight");
             
        }
         if (PermissionInputs.find(x=> x.fieldName == "DPManifestWeight" && x.isChecked ==  false)) {
        checkColumn(e, "dpManifestWeight");
             
        }

        
        if (PermissionInputs.find(x=> x.fieldName == "ShippingAgentId" && x.isChecked ==  false)) {
        checkColumn(e, "shippingAgentId");
             
        } 
         if (PermissionInputs.find(x=> x.fieldName == "Weight" && x.isChecked ==  false)) {
        checkColumn(e, "foundWeight");
             
        }
          if (PermissionInputs.find(x=> x.fieldName == "IsGateIn" && x.isChecked ==  false)) {
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

    function getAllTransporter() {
        $.get('/Import/Setup/GetTransporters', function (data) {
            TransporterLists = data;
        });
    }

    function loadGrid(dataSource) {

        $.get('/APICalls/GetGateInCFSContainers', function (data) {

            $.get('/APICalls/GetShippingAgents', function (agents) {

                containers = data;

                shippingAgents = agents;

                if (dataSource)
                    containers = dataSource
                else
                    containers = data;

                console.log("containers", containers)

                var grid = $("#gateinContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');

                var dataGrid = $("#gateinContainer").dxDataGrid({
                    dataSource: containers,
                    keyExpr: "key",
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

                        allowUpdating: true,
                        selectTextOnEditStart: true,
                        startEditAction: "click",
                        mode: "cell"
                    },
                    columns: [
                        {
                            dataField: "containerTypeStatus",
                            caption: "Container Status",
                            allowEditing: false,
                            validationRules: [{ type: "required" }],

                        },

                        "containerNo",
                        {
                            dataField: "status",
                            caption: "Status",
                            allowEditing: false,
                            validationRules: [{ type: "required" }],

                        },
                        {
                            dataField: "containerSize",
                            caption: "Container Size",
                            allowEditing: false,
                            validationRules: [{ type: "required" }],

                        },
                        "isoCode",
                        "virNo",
                        "carrierId",
                        "carrierName",
                        {
                            dataField: "pacssSealNo",
                            caption: "PCCSS Seal No"
                        },

                        "vehicleNo",
                        {
                            dataField: "weight",
                            caption: "Manifest Weight"
                        },
                        {
                            dataField: "foundWeight",
                            dataType: "number",
                            validationRules: [{ type: "required" }],
                            caption: "Found Weight"
                        },
                        {
                            dataField: "dpTareweight",
                            dataType: "number",
                            validationRules: [{ type: "required" }],
                            caption: "Container Tare Weight"
                            //validationRules: [{ type: "required" }]
                        },
                        {
                            dataField: "dpManifestWeight",
                            dataType: "number",
                            validationRules: [{ type: "required" }],
                            caption: "Port Manifest Weight"
                        },
                        {
                            dataField: "transporterId",
                            caption: "Transporter",
                            width: 300,
                            validationRules: [{ type: "required" }],
                            lookup: {
                                dataSource: TransporterLists,
                                valueExpr: "transporterId",
                                displayExpr: "transporterName"
                            }
                        },
                        {
                            dataField: "shippingAgentId",
                            width: 200,
                            caption: "Shipping Agent",
                            allowEditing: false,
                            lookup: {
                                dataSource: shippingAgents,
                                displayExpr: "name",
                                valueExpr: "shippingAgentId"
                            }
                        },
                        {
                            caption: "Location",
                            dataField: "containerLocation",

                        },
                        {
                            caption: "",
                            dataField: "isGateIn",
                            dataType: "boolean"

                        }


                    ],

                    onCellClick: function (e) {

                        if (e.column.dataField == "isGateIn") {

                            //if (PermissionInputs.find(x => x.fieldName == "IsGateIn" && x.isChecked == false)) {


                            //}
                            //else {

                            if (e.row.data.containerSize == null || e.row.data.weight == null || e.row.data.dpTareweight == null || e.row.data.dpManifestWeight == null) {
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
                                    var y = Number(e.row.data.foundWeight);

                                    var z = ((x / y) * 100)

                                    if ((z > 105) || (z < 95)) {
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
        checkCell(e, "containerNo");
    checkCell(e, "containerSize");
    checkCell(e, "isoCode");
    checkCell(e, "virNo");
    checkCell(e, "carrierId");
    checkCell(e, "carrierName");
    checkCell(e, "pacssSealNo");
    checkCell(e, "vehicleNo");
    checkCell(e, "weight");
    checkCell(e, "foundWeight");
    checkCell(e, "dpTareweight");
    checkCell(e, "dpManifestWeight");
    checkCell(e, "shippingAgentId");

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

    console.log("gate in", containers);


        var gateInContainers = containers.filter(c => c.isGateIn == true);


    console.log("ready to gate in" ,gateInContainers)

    $.post('SaveGateInContainers', {containers: gateInContainers }, function (data) {

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

    function GetCFSPaiosByDate() {
        var fromdate = document.getElementById("single_cal2").value;
    console.log(fromdate);
    $.get('/APICalls/GetCFSIPAOSByDate?fromdate=' + fromdate, function (data) {
        IPaosCFS = data;
    var dataGrid = $("#ipoascfs").dxDataGrid({
        dataSource: IPaosCFS,
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
    //allowDeleting: true,
    selectTextOnEditStart: true,
    startEditAction: "click"
                },
    columns: [
    "containerNumber",
    "virNumber",
    "bondedCarrierId",
    "bondedCarrierName",
    "vehicleNumber",


    ],
    onRowUpdated: function (e) {

    },
    onRowRemoved: function (e) {
                    
                    var containerNumber = e.data.containerNumber;


    $.post('SaveDeletedContainersCFS?containerNumber=' + containerNumber, function (data) {

        showToast("Container Removed ", "successdev ");
    loadGrid();

                    });
                }

            }).dxDataGrid("instance");


        });
    }

    function formfiled() {


    }

