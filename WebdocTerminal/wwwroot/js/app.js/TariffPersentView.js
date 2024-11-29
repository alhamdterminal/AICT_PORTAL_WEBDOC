

    var tariffs = [];
    var tariffHead = [];

    $(function () {

        gettariffHeads();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function gettariffHeads() {

        $.get('/Tariff/GetTariffHeadsTypeWise', function (data) {

            tariffHead = data;

            console.log("tariffHead", tariffHead)

        });
    }




    function TariffPercentageGrid() {


        var shippingAgentId = $('#shippingAgentId').val();

    console.log("shippingAgentId", shippingAgentId);



    $.get('/APICAlls/GetTariffPercentages?ShippingAgentId=' + shippingAgentId, function (data) {

        tariffs = data;

    var grid = $("#tariffGrid").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    var grid = $("#tariffGrid").dxDataGrid(this.gridSettings).dxDataGrid('instance');
    var dataGrid = $("#tariffGrid").dxDataGrid({
        dataSource: tariffs,
    keyExpr: "tariffPercentageId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        pageSize: 10
                },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."
                },
    editing: {
        mode: "inline",

    allowUpdating: true,
    allowDeleting: true,
    allowAdding: true
                },
    columns: [
    {
        dataField: "tariffHeadId",
    caption: "Head",
    width: 300,
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: tariffHead,
    valueExpr: "tariffHeadId",
    displayExpr: "name"
                        }
                    },
    {

        dataField: "rateOnPersent",
    caption: "Percent",
    validationRules: [{type: "required" }],
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                        }

                    }


    ],
    summary: {
        totalItems: [
    {
        column: "rateOnPersent",
    summaryType: "sum"
                        },
    ]
                },
    onRowUpdated: function (e) {

        console.log("tariffs", tariffs)

                    var tariff = tariffs;

    console.log("tariff", tariff)

    console.log(tariff.reduce((n, {rateOnPersent}) => n + rateOnPersent, 0))

    var totalPercent = tariff.reduce((n, {rateOnPersent}) => n + rateOnPersent, 0);

    console.log("tariff", totalPercent)

                    //if (totalPercent == 100) {

        //    console.log("update", e.data);

        //    var model = e.data;
        //    $.post('/Tariff/UpdateTariffPersent', { model: model }, function (data) {

        //        if (data.error == true) {
        //            showToast(data.message, "error");
        //            window.location.href = window.location.href;
        //        }
        //        else {
        //            showToast(data.message, "success");
        //            window.location.href = window.location.href;
        //        }

        //    });

        //}
        //else {

        //    showToast("the percent total is  <> 100 please check again", "error");
        //    window.location.href = window.location.href;
        //}

    },

              

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

    function submitTariffPersent() {

        var tariff = tariffs;

    console.log("tariff", tariff)

    console.log(tariff.reduce((n, {rateOnPersent}) => n + rateOnPersent, 0))

    var totalPercent = tariff.reduce((n, {rateOnPersent}) => n + rateOnPersent, 0);

    if (totalPercent == 100) {
        console.log("ok")

            var ShippingAgentId = $('#shippingAgentId').val();

    if (ShippingAgentId) {
        $.post('/Tariff/SaveTariffPersent?ShippingAgentId=' + ShippingAgentId, { tariff, tariff }, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            }
            else {
                showToast(data.message, "success");

            }

        })

    }
    else {
        showToast("Please Select F.F", "error");

            }
          
             
        } else {

        showToast("the percent total is  <> 100 please check again", "error");
        }

    }

    function formfiled() {

        $('#shippingAgentId').on('change', function (data) {
            TariffPercentageGrid();
        });
    }




