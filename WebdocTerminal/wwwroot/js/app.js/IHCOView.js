


    $(function () {
        getIHCOData();
    });

    var IHCOData = [];

    var HandlingCode = [
    {Name: "OC" },
    {Name: "DD" },
    {Name: "IB" },
    {Name: "RF" },
    {Name: "BR" }

    ];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getIHCOData() {
        $.get('/Import/Setup/GetIHCOs', function (data) {
            IHCOData = data;
            console.log("IHCOData", IHCOData);
            IHCOgrid();
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
    function IHCOgrid() {

        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    $("#gridContainer").dxDataGrid({
        dataSource: IHCOData,
    keyExpr: "ihcoId",
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
        dataField: "virNumber",
    validationRules: [{type: "required" }],
    caption: "IGM"
                },
    {
        dataField: "blNumber",
    validationRules: [{type: "required" }],
    caption: "BL No"
                },
    {
        dataField: "indexNumber",
    dataType: "number",
    validationRules: [{type: "required" }],
    caption: "Index No",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "handlingCode",
    validationRules: [{type: "required" }],
    caption: "Handling Code",
    lookup: {
        dataSource: HandlingCode,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },
    ],


    onRowInserted: function (e) {

        console.log(e)
                 console.log(e.data)
    var data = e.data;

    $.post('/Import/Setup/AddIHCO', {data, data}, function (result) {

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
    $.post('/Import/Setup/UpdateIHCO', {data, data}, function (result) {
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
