

    var AutoGrounding = [
    {Name: "YES" },
    {Name: "NO" }
    ];

    var AutoGroundingLCL = [
    {Name: "YES" },
    {Name: "NO" }
    ];

    var AutoGroundingFCL = [
    {Name: "YES" },
    {Name: "NO" }
    ];

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
        $.get('/Import/Setup/GetGoodHeads', function (data) {
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
    keyExpr: "goodsHeadId",
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
    {
        dataField: "allowAutoGrounding",
    caption: "Allow Auto Grounding",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: AutoGrounding,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },
    {
        dataField: "allowAutoGroundingLCL",
    caption: "Allow Auto Grounding LCL",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: AutoGroundingLCL,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },
    {
        dataField: "allowAutoGroundingFCL",
    caption: "Allow Auto Grounding FCL",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: AutoGroundingFCL,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },
    ],


    onRowInserted: function (e) {

        console.log(e)
                 console.log(e.data)
    var data = e.data;

    $.post('/Import/Setup/AddGoodshead', {data, data}, function (result) {

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
    $.post('/Import/Setup/UpdateGoodHeads', {data, data}, function (result) {
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
