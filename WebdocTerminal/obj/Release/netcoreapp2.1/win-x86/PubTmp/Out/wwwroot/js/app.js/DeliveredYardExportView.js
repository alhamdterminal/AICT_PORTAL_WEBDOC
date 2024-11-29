

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
        $.get('/Export/ExportSetup/GetDeliveredYardExpor', function (data) {
            DeliveredYardData = data;
            console.log("DeliveredYardData", DeliveredYardData);
            DeliveredYardgrid();
        });
    }




    function refresh() {
        window.location.reload();
    }
    function DeliveredYardgrid() {


        $("#gridContainer").dxDataGrid({
            dataSource: DeliveredYardData,
            keyExpr: "deliveredYardExportId",
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
                    validationRules: [{ type: "required" }],
                    caption: "Name"
                },

            ],


            onRowInserted: function (e) {

                console.log(e)
                console.log(e.data)
                var values = e.data;

                $.post('/Export/ExportSetup/AddDeliveredYardExport', { values, values }, function (result) {

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
                var values = e.data;
                $.post('/Export/ExportSetup/UpdateDeliveredYardExport', { values, values }, function (result) {
                    console.log("result", result);
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
