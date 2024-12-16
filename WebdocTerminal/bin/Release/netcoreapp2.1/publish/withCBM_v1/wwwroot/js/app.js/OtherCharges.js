

    var shippingAgents = [];

    //$(function () {



        //});

        function getAllShippingAgents() {
            $.get('/ShippingAgent/Get', function (data) {
                shippingAgents = data;
                console.log(shippingAgents);
                shippingAgentgrid();
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
        if (PermissionInputs.find(x => x.fieldName == "OtherCharges" && x.isChecked == false)) {

        checkColumn(e, "otherCharges");
        }
        if (PermissionInputs.find(x => x.fieldName == "ChargesName" && x.isChecked == false)) {

        checkColumn(e, "chargesName");
        }
    }

    function shippingAgentgrid() {
        function logEvent(eventName) {
            var logList = $("#events ul"),
                newItem = $("<li>", { text: eventName });
            logList.prepend(newItem);
        }

        $("#gridContainer").dxDataGrid({
        dataSource: shippingAgents,
    keyExpr: "shippingAgentId",
    wordWrapEnabled: true,
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
    allowDeleting: true
            },
    columns: [
    {
        dataField: "name",
    caption: "Name"
                },
    {
        dataField: "line",
    caption: "Line"
                },
    {
        dataField: "lineCode",
    caption: "Line Code"
                },
    {
        dataField: "otherCharges",
    caption: "Other Charges"
                },
    {
        dataField: "chargesName",
    caption: "Charges Name"
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
        logEvent("RowInserting");
            },
    onRowInserted: function (e) {
    },
    onRowUpdating: function (e) {
        logEvent("RowUpdating");
            },
    onRowUpdated: function (e) {
        logEvent("RowUpdated");
    console.log(e);
    var ShippingAgent = e.data;
    $.post('/ShippingAgent/updateShippingAgent', {ShippingAgent, ShippingAgent}, function (data) {
        showToast("Updated", "success");
                });
            },
    onRowRemoving: function (e) {
        logEvent("RowRemoving");
            },
    onRowRemoved: function (e) {
        logEvent("RowRemoved");
            }
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



    function formfiled() {

        getAllShippingAgents();

    }

