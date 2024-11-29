

    var LineSeals = [];

    var ShippingAgents = [];



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }







    $(function () {

        $.get('/Import/Setup/GetLineSealAmounts', function (data) {


            console.log("data ", data);

            loadGrid(data)

        })


    });

    function formfiled() {


    }


    function loadGrid(dataSource) {


        $.get('/APICalls/GetShippingAgentName', function (data) {
            console.log("ShippingAgent", data)

            ShippingAgents = data;

            var grid = $("#linesealAmountGrid").dxDataGrid(this.gridSettings).dxDataGrid('instance');

            var dataGrid = $("#linesealAmountGrid").dxDataGrid({
                dataSource: dataSource,
                keyExpr: "lineSealAmountId",

                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    pageSize: 10
                },
                editing: {
                    mode: "row",
                    allowDeleting: true,
                    allowAdding: true
                },
                columns: [



                    {
                        dataField: "shippingAgentId",
                        caption: "Line",

                        lookup: {
                            dataSource: ShippingAgents,
                            displayExpr: "text",
                            valueExpr: "value"
                        },
                        validationRules: [{ type: "required" }]

                    },
                    {
                        dataField: "amount",
                        width: 120,
                        caption: "Charge Amount",
                        validationRules: [{ type: "required" }],
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
                    var model = e.data;


                    $.post('/Import/Setup/AddLineSealAmount', { model: model }, function (result) {


                        console.log("result", result)
                        if (result.error) {
                            showToast(result.message, "warning");
                            refresh()
                        }

                        else {

                            showToast(result.message, "success");
                            refresh()
                        }

                    });



                },


                onRowRemoved: function (e) {

                    console.log(e)
                    console.log(e.data)
                    var data = e.data.lineSealAmountId;

                    $.post('/Import/Setup/DeleteSealLineAmount?key=' + data, function (result) {

                        console.log("result", result)
                        if (result.error) {
                            showToast(result.message, "warning");
                        }

                        else {

                            showToast(result.message, "success");

                        }

                        refresh();
                    });



                }

            }).dxDataGrid("instance");


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


    function refresh() {
        window.location.reload();
    }



