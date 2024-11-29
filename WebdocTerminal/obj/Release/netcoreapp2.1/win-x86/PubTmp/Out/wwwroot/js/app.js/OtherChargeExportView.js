

    $(function () {
        getOtherChargeData();
    });

    var OtherChargeData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getOtherChargeData() {
        $.get('/Export/ExportSetup/GetOtherCharge', function (data) {
            OtherChargeData = data;
            console.log("OtherChargeData", OtherChargeData);
            OtherChargegrid();
        });
    }



    function refresh() {
        window.location.reload();
    }
    function OtherChargegrid() {

        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#gridContainer").dxDataGrid({
        dataSource: OtherChargeData,
    keyExpr: "otherChargeExportId",
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
    allowAdding: true
            },
    columns: [
    {
        dataField: "chargeName",
    validationRules: [{type: "required" }],
    caption: "Name"
                },
    {
        dataField: "chargeAmount",
    dataType: "number",
    validationRules: [{type: "required" }],
    caption: "Amount",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },

    ],


    onRowInserted: function (e) {

        console.log(e)
                 console.log(e.data)
    var data = e.data;

    $.post('/Export/ExportSetup/AddOtherCharge', {data, data}, function (result) {

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
    var data = e.data;
    $.post('/Export/ExportSetup/UpdateOtherCharge', {data, data}, function (result) {
        console.log("result", result)
                    if (result.error) {
        showToast(result.message, "warning");
                    }

    else {

        showToast(result.message, "success");

                    }

    window.setInterval('refresh()', 3000);
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
