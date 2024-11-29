




    var ShippingAgents = [];


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getshippingAgents() {
        $.get('/ShippingAgent/GetMasterLine', function (data) {
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

         
        if (PermissionInputs.find(x => x.fieldName == "MasterShippingAgentCode" && x.isChecked == false)) {

        checkColumn(e, "masterShippingAgentCode");
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
    keyExpr: "masterShippingAgentId",
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
        dataField: "masterShippingAgentName",
    validationRules: [{type: "required" }],
    caption: "Name"

                },

    {
        dataField: "masterShippingAgentCode",
    caption: "Line Code",
    allowEditing: false,
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

    if ( shippingAgent.masterShippingAgentName || shippingAgent.masterShippingAgentCode) {
        $.post('/ShippingAgent/MasterLinePost', { shippingAgent, shippingAgent }, function (data) {


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
    $.post('/ShippingAgent/updateMasterShippingAgent', {shippingAgent, shippingAgent}, function (data) {
        showToast("Updated", "success");
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
