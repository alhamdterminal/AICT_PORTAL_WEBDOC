


    $(function () {
        getsealPurchases();
    });

    var purchaseData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getsealPurchases() {
        $.get('/Import/Setup/GetSealPurchases', function (data) {
            purchaseData = data;
            console.log("Sealpurchase", purchaseData);
            sealPurchaseDatagrid();
        });
    }


    function refresh() {
        window.location.reload();
    }

    function sealPurchaseDatagrid() { 

        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    $("#gridContainer").dxDataGrid({
        dataSource: purchaseData,
    keyExpr: "sealPurchaseId",
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
        mode: "form",
    allowAdding: true,
    allowDeleting: true,
    allowUpdating: false,

            },
    columns: [
    {
        dataField: "sealPurchaseNo",
    caption: "Purchase No",
    allowEditing: false,
                },
    {
        dataField: "purchaseDate",
    caption: "Purchase Date",
    dataType: 'date',
    format: 'dd/MM/yyyy',
    allowEditing: false,
                },
    {
        dataField: "sealFrom",
    caption: "From",
    dataType: "number",
    validationRules: [{type: "required" }],
    editorOptions: {
        step: 0
                    }

                },

    {
        dataField: "sealTo",
    caption: "To",
    dataType: "number",
    validationRules: [{type: "required" }],
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "rate",
    validationRules: [{type: "required" }],
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                }

    ],


    onRowInserted: function (e) {

        console.log(e)
                 console.log(e.data)
    var data = e.data;

    $.post('/Import/Setup/AddSealPurchase', {data, data}, function (result) {

        console.log("result", result)
                        if (result.error) {
        showToast(result.message, "warning");
                        }

    else {

        showToast(result.message, "success");

                        }

    window.setInterval('refresh()', 4000);
                });



            },

    onRowUpdated: function (e) {
        console.log(e);
    console.log(e.data);
    $.post('/Import/Setup/UpdateSealPurchase', {data : e.data}, function (result) {
        console.log("result", result)
                    if (result.error) {
        showToast(result.message, "warning");
                    }

    else {

        showToast(result.message, "success");

                    }

    window.setInterval('refresh()', 4000);
                });
            },
    onRowRemoving: function (e) {
    },
    onRowRemoved: function (e) {
        console.log(e)
                var purchaseId = e.data.sealPurchaseId;
    $.post('/Import/Setup/DeleteSealPurchase?key=' + purchaseId, function (result) {
        console.log("result", result)
                    if (result.error) {
        showToast(result.message, "warning");
                    }

    else {

        showToast(result.message, "success");

                    }

    window.setInterval('refresh()', 4000);
                });
            }
        }).dxDataGrid("instance");


    grid.on('editorPrepared', function (e) {
            if (e.parentType !== 'dataRow') {
                return;
            }

            //$('input[type=text]').on('wheel', function (ad) {

        //}); 


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
    }



    function formfiled() {


    }
