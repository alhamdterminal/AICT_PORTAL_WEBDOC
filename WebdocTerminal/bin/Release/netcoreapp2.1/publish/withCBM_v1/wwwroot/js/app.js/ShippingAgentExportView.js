


    var ShippingAgents = [];

    var BillDateType = [
    {Name: "GateIn" },
    {Name: "ETD" }
    ];

    var BillDateTypeFCL = [
    {Name: "GateIn" },
    {Name: "ETD" },
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

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getshippingAgents() {

        $.get('/Export/ShippingAgentExport/GetShippingAgentExport', function (data) {
            ShippingAgents = data;
            console.log(ShippingAgents);
            Agentgrid();
        });

    }

    function Agentgrid() {
        

        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#gridContainer").dxDataGrid({
        dataSource: ShippingAgents,
    keyExpr: "shippingAgentExportId",
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
        dataField: "shippingAgentName",
    validationRules: [{type: "required" }],
    caption: "Name"
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
        dataField: "weightAmount",
    validationRules: [{type: "required" }],
    caption: "Weight Amount",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                }
    ],

    onRowInserted: function (e) {
                 var values = e.data;
    $.post('/Export/ShippingAgentExport/AddShippingAgentExport', {values, values}, function (data) {
                        if (data.error == true) {
        showToast(data.message, "error")

    } else {
        showToast(data.message, "success")
                            getshippingAgents(); 
                        } 
                    }); 
            },

    onRowUpdated: function (e) {
                var values = e.data;

    $.post('/Export/ShippingAgentExport/UpdateShippingAgentExport', {values, values}, function (data) {
                    if (data.error == true) {
        showToast(data.message, "error")

    } else {
        showToast(data.message, "success")
                        getshippingAgents();


                    }
                });
            },
           
          
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
