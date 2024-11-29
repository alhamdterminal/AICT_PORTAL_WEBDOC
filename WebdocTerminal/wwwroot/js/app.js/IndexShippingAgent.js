

    $(function () {

        getmaterlineData();

    });



    var ShippingAgents = [];

    var masterlines = [];

    var BillDateType = [
    {Name: "GateIn" },
    {Name: "Destuffed" },
    {Name: "VesselArrival" }
    ];

    var BillDateTypeFCL = [
    {Name: "GateIn" },
    {Name: "Destuffed" },
    {Name: "VesselArrival" }
    ];


    var BillDateTypeCY = [
    {Name: "GateIn" },
    {Name: "VesselArrival" }
    ];

    var MultiplyByWeight = [
    {Name: "Yes" },
    {Name: "No" }
    ];

    var VehcileAmountAllow = [
    {Name: "Yes" },
    {Name: "No" }
    ];


    var AllowSpecialCharge = [
    {Name: "Yes" },
    {Name: "No" }
    ];


    var AllowExaminationAutoChrge = [
    {Name: "Yes" },
    {Name: "No" }
    ];

    function getmaterlineData() {
        $.get('/ShippingAgent/GetMasterLine', function (data) {
            masterlines = data;
            console.log("masterlines", masterlines);

        });
    }


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getshippingAgents() {
        $.get('/ShippingAgent/Get', function (data) {
            ShippingAgents = data;
            console.log(ShippingAgents);
            Agentgrid();
        });
    }


    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {


        if (PermissionInputs.find(x => x.fieldName == "Name" && x.isChecked == false)) {

        checkColumn(e, "name");
        }
        if (PermissionInputs.find(x => x.fieldName == "Line" && x.isChecked == false)) {

        checkColumn(e, "line");
        }
        if (PermissionInputs.find(x => x.fieldName == "LineCode" && x.isChecked == false)) {

        checkColumn(e, "lineCode");
        }
    }
    function Agentgrid() {
        function logEvent(eventName) {
            var logList = $("#events ul"),
                newItem = $("<li>", { text: eventName });
            logList.prepend(newItem);
        }

        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#gridContainer").dxDataGrid({
        dataSource: ShippingAgents,
    keyExpr: "shippingAgentId",
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

    editing: {
        mode: "row",
    allowUpdating: true,
    allowDeleting: false,
    allowAdding: true
            },
    columns: [
    {
        dataField: "name",
    validationRules: [{type: "required" }],
    caption: "Name"
                },
    {
        dataField: "line",
    caption: "Line"
                },
    {
        dataField: "masterShippingAgentId",
    caption: "Master Line",
    lookup: {
        dataSource: masterlines,
    displayExpr: "masterShippingAgentName",
    valueExpr: "masterShippingAgentId"
                    },
    validationRules: [{type: "required" }]

                },
    {
        dataField: "ntnNumber",
    validationRules: [{type: "required" }],
    caption: "NTN"

                },
    {
        dataField: "billDateType",
    caption: "Storage applicable CFS / LCL",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: BillDateType,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },

    {
        dataField: "billDateTypeFCL",
    caption: "Storage applicable CFS / FCL",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: BillDateTypeFCL,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },
    {
        dataField: "billDateTypeCY",
    caption: "Storage applicable CY",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: BillDateTypeCY,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },

    {
        dataField: "allowSpecialChargeLCL",
    caption: "Special Charge for LCL",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: AllowSpecialCharge,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },
    {
        dataField: "allowSpecialChargeFCL",
    caption: "Special Charge for FCL",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: AllowSpecialCharge,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },
    {
        dataField: "allowSpecialChargeCY",
    caption: "Special Charge for CY",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: AllowSpecialCharge,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },

    {
        dataField: "weightAllow",
    caption: "Multiply By Weight",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: MultiplyByWeight,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },
    {
        dataField: "vehcileAmountAllow",
    caption: "12%",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: VehcileAmountAllow,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },

    {
        dataField: "allowExaminationAutoChrges",
    caption: "Examination Auto Charge",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: AllowExaminationAutoChrge,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },

    {
        dataField: "weightAmount",
    validationRules: [{type: "required" }],
    caption: "Weight Amount",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },



    {
        dataField: "lineCode",
    caption: "Line Code", 
                },
    ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
            },
    onEditingStart: function (e) {
        logEvent("EditingStart");
            },
    onInitNewRow: function (e) {
        logEvent("InitNewRow");
            },
    onRowInserting: function (e) {
        console.log(e)
                logEvent("RowInserting");

            },
    onRowInserted: function (e) {
        logEvent("RowInserted");
    console.log(e)
    console.log(e.data)
    var shippingAgent = e.data;

    if (shippingAgent.name || shippingAgent.line || shippingAgent.lineCode) {
        $.post('/ShippingAgent/Post', { shippingAgent, shippingAgent }, function (data) {


            if (data.error == true) {
                showToast(data.message, "error")

            } else {
                showToast(data.message, "success")
                getshippingAgents();


            }


        });
                }


            },
    onRowUpdating: function (e) {
        logEvent("RowUpdating");
            },
    onRowUpdated: function (e) {
        logEvent("RowUpdated");
    console.log(e);
    var shippingAgent = e.data;
    $.post('/ShippingAgent/updateShippingAgent', {shippingAgent, shippingAgent}, function (data) {
        showToast("ShippingAgent Updated", "success");
                });
            },
    onRowRemoving: function (e) {
        logEvent("RowRemoving");
            },
    onRowRemoved: function (e) {
        logEvent("RowRemoved");
    console.log(e)
    var key = e.data.shippingAgentId;
    $.post('/ShippingAgent/Delete?key=' + key, function (data) {
        showToast("ShippingAgent Deleted", "success");
                });
            }
        });


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
    }



    function formfiled() {
        getshippingAgents();

    }
