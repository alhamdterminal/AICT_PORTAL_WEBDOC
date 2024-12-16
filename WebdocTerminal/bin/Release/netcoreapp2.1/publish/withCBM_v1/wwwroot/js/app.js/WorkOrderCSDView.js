



    var WorkOrderCDSData = [];
    var ShippingAgents = [];



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function containerChangeCallback() {

    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function refresh() {
        window.location.reload();
    }


    function WorkOrderCSDDatagrid() {


        $.get('/ShippingAgent/Get', function (data) {
            ShippingAgents = data;


            console.log("WorkOrderCDSData", WorkOrderCDSData);

            var grid = $("#gridContainerWorkForCSD").dxDataGrid(this.gridSettings).dxDataGrid('instance');


            $("#gridContainerWorkForCSD").dxDataGrid({
                dataSource: WorkOrderCDSData,
                keyExpr: "workOrderCSDId",
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
                    allowAdding: true
                },
                columns: [
                    {
                        dataField: "workOrderNumber",
                        allowEditing: false,
                        caption: "Work Order No"
                    },

                    {
                        dataField: "workOrderDate",
                        dataType: "date",
                        allowEditing: false,
                        caption: "Date"
                    },
                    {
                        dataField: "shippingAgentId",
                        caption: "Line / FF",
                        width: "250px",
                        lookup: {
                            dataSource: ShippingAgents,
                            displayExpr: "name",
                            valueExpr: "shippingAgentId"
                        },
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "cfS20",
                        dataType: "number",
                        validationRules: [{ type: "required" }],
                        caption: "CFS 20",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    },
                    {
                        dataField: "cfS40",
                        dataType: "number",
                        validationRules: [{ type: "required" }],
                        caption: "CFS 40",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    },
                    {
                        dataField: "cfS45",
                        dataType: "number",
                        validationRules: [{ type: "required" }],
                        caption: "CFS 45",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    },
                    {
                        dataField: "cY20",
                        dataType: "number",
                        validationRules: [{ type: "required" }],
                        caption: "CY 20",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    },
                    {
                        dataField: "cY40",
                        dataType: "number",
                        validationRules: [{ type: "required" }],
                        caption: "CY 40",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    },
                    {
                        dataField: "cY45",
                        dataType: "number",
                        validationRules: [{ type: "required" }],
                        caption: "CY 45",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    },
                    {
                        dataField: "documentAmount",
                        dataType: "number",
                        validationRules: [{ type: "required" }],
                        caption: "Document Amount",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    },
                    {
                        dataField: "sealAmount",
                        dataType: "number",
                        validationRules: [{ type: "required" }],
                        caption: "Seal Amount",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    },
                    {
                        dataField: "entryAmount",
                        dataType: "number",
                        validationRules: [{ type: "required" }],
                        caption: "Entry Amount",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    },
                    {
                        dataField: "remarks",
                        validationRules: [{ type: "required" }],
                        caption: "Remarks"
                    },
                ],


                onRowInserted: function (e) {

                    console.log(e)
                    console.log(e.data)
                    var data = e.data;

                    $.post('/Import/Setup/AddWorkOrderCSD?igm=' + igm, { data, data }, function (result) {

                        console.log("result", result)
                        if (result.error) {
                            showToast(result.message, "warning");
                        }

                        else {

                            showToast(result.message, "success");

                        }

                        window.setInterval('WorkForCSDdetails()', 3000);
                    });



                },

                onRowUpdated: function (e) {
                    console.log(e);
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
        });
    }



    function formfiled() {


    }



    function WorkForCSDdetails() {
        console.log("test igm", igm)
        $.get('/Import/Setup/GetWorkOrderCSDs?igm='+igm, function (data) {
        WorkOrderCDSData = data;
    console.log("WorkOrderCDSData", WorkOrderCDSData);
    WorkOrderCSDDatagrid();
        });

    }
