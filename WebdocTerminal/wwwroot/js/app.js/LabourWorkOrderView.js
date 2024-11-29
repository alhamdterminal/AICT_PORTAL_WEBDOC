


    $(function () {
        getLabourData();
    });

    var LabourData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    var Shift = [
    {Name: "Day" },
    {Name: "Night" }

    ];


    function getLabourData() {
        $.get('/Import/Setup/GetLabourWorkOrders', function (data) {
            LabourData = data;
            console.log("LabourData", LabourData);
            LabourDatagrid();
        });
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
    function LabourDatagrid() {


        console.log("LabourData", LabourData);

    var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#gridContainer").dxDataGrid({
        dataSource: LabourData,
    keyExpr: "labourWorkOrderId",
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
    allowAdding: true,
    allowDeleting: true,

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
        dataField: "shift",
    validationRules: [{type: "required" }],
    caption: "Shift",
    lookup: {
        dataSource: Shift,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },
    {
        dataField: "numberOfLabour",
    validationRules: [{type: "required" }],
    dataType:"number",
    caption: "Labour Engage",
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "reason",
    validationRules: [{type: "required" }],
    caption: "Reason"
                },
    ],


    onRowInserted: function (e) {

        console.log(e)
                 console.log(e.data)
    var data = e.data;

    $.post('/Import/Setup/AddLabourWorkOrder', {data, data}, function (result) {

        console.log("result", result)
                        if (result.error) {
        showToast(result.message, "warning");
                        }

    else {

        showToast(result.message, "success");

                        }

    window.setInterval('refresh()', 3000);
                });



            },

    onRowUpdated: function (e) {
        console.log(e);                
            },
    onRowRemoving: function (e) {
    },
    onRowRemoved: function (e) {
        console.log(e)  
                var xxs = e.data.labourWorkOrderId;
    $.post('/Import/Setup/DeleteWorkorder?key=' + xxs, function (data) {
        showToast("Deleted", "success");
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


    }
