




    var ClearingAgents = [];

    var BillDateTypeLCL = [
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


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getshippingAgents() {
        $.get('/ClearingAgent/Get', function (data) {
            ClearingAgents = data;
            console.log(ClearingAgents);
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
        if (PermissionInputs.find(x => x.fieldName == "PhoneNumber" && x.isChecked == false)) {

        checkColumn(e, "phoneNumber");
        }
        if (PermissionInputs.find(x => x.fieldName == "ChallanNumber" && x.isChecked == false)) {

        checkColumn(e, "challanNumber");
        }
    }
    function Agentgrid() {
        function logEvent(eventName) {
            var logList = $("#events ul"),
                newItem = $("<li>", { text: eventName });
            logList.prepend(newItem);
        }

        $("#gridContainer").dxDataGrid({
        dataSource: ClearingAgents,
    keyExpr: "clearingAgentId",
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
                     
                },
    {
        dataField: "phoneNumber",
    validationRules: [{type: "required" }],
    caption: "Phone Number"
                },
    {
        dataField: "ntnNumber",
    validationRules: [{type: "required" }],
    caption: "NTN Number"
                },
    {
        dataField: "challanNumber",
    validationRules: [{type: "required" }],
    caption: "Challan Number"
                },
    {
        dataField: "licenceNumber",
    validationRules: [{type: "required" }],
    caption: "Licence Number"
                },
    {
        dataField: "billDateTypeLCL",
    caption: "Storage applicable CFS / LCL",
    lookup: {
        dataSource: BillDateTypeLCL,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },

    {
        dataField: "billDateTypeFCL",
    caption: "Storage applicable CFS / FCL ",
    lookup: {
        dataSource: BillDateTypeFCL,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },
    {
        dataField: "billDateTypeCY",
    caption: "Storage applicable CY",
    lookup: {
        dataSource: BillDateTypeCY,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
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
    var clearingAgent = e.data;

    $.post('/ClearingAgent/Post', {clearingAgent, clearingAgent}, function (data) {




                        if (data.error == true) {
        showToast(data.message, "error")

    } else {
        showToast(data.message, "success")
                            window.location.href = window.location.href;
                        }
                         
                       
                    });
              
            },
    onRowUpdating: function (e) {
        logEvent("RowUpdating");
            },
    onRowUpdated: function (e) {
        logEvent("RowUpdated");

    var clearingAgent = e.data;
    console.log(clearingAgent);
    $.post('/ClearingAgent/Put', {clearingAgent, clearingAgent}, function (data) {
        showToast("Agent Updated", "success");
    window.location.href = window.location.href;
                });
            },
    onRowRemoving: function (e) {
        logEvent("RowRemoving");
            },
    onRowRemoved: function (e) {
        logEvent("RowRemoved");
    console.log(e)
    var key = e.data.clearingAgentId;
    $.post('/ClearingAgent/Delete?key=' + key, function (data) {
        showToast("  Deleted", "success");
    window.location.href = window.location.href;
                });
            }
        });
    }



    function formfiled() {
        getshippingAgents()

    }