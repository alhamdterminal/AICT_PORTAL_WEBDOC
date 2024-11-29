

    $(function () {
        getGoodsHeadData();
    });

    var GoodHeadsData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getGoodsHeadData() {
        $.get('/Export/GoodsHeadExport/GetGoodsHeadExportExport', function (data) {
            GoodHeadsData = data;
            console.log("GoodHeadsData", GoodHeadsData);
            GoodsHeadgrid();
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
    function GoodsHeadgrid() {

        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    $("#gridContainer").dxDataGrid({
        dataSource: GoodHeadsData,
    keyExpr: "goodsHeadExportId",
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
        dataField: "goodsHeadName",
    validationRules: [{type: "required" }],
    caption: "Name"
                },
    {
        dataField: "storageFreeDays",
    validationRules: [{type: "required" }],
    caption: "Free Days",
    dataType: "number",
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

    $.post('/Export/GoodsHeadExport/AddGoodsHeadExport', {data, data}, function (result) {

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
    $.post('/Export/GoodsHeadExport/updateGoodsHeadExport', {data, data}, function (result) {
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
