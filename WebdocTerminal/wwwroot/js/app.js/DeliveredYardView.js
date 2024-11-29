


    $(function () {
        getDeliveredYardData();
    });

    var DeliveredYardData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getDeliveredYardData() {
        $.get('/Import/Setup/GetDeliveredYards', function (data) {
            DeliveredYardData = data;
            console.log("DeliveredYardData", DeliveredYardData);
            DeliveredYardgrid();
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
    function DeliveredYardgrid() {


        console.log("DeliveredYardData", DeliveredYardData)

        $("#gridContainer").dxDataGrid({
        dataSource: DeliveredYardData,
    keyExpr: "deliveredYardId",
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
        dataField: "deliveredYardName",
    validationRules: [{type: "required" }],
    caption: "Yard Name"
                }

    ],


    onRowInserted: function (e) {

        console.log(e)
                var data = e.data;

    $.post('/Import/Setup/AddDeliveredYards', {data, data}, function (result) {

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
    $.post('/Import/Setup/UpdateDeliveredYard', {data, data}, function (result) {
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
    }



    function formfiled() {


    }



