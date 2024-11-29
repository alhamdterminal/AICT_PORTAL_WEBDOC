




    var VehicleMeasurementData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getVehicleMeasurement() {
        $.get('/Import/Setup/GetVehicleMeasurement', function (data) {
            VehicleMeasurementData = data;
            console.log(VehicleMeasurementData);
            VehicleMeasurementgrid();
        });
    }



    function VehicleMeasurementgrid() {

        $("#gridContainer").dxDataGrid({
            dataSource: VehicleMeasurementData,
            keyExpr: "vehicleMeasurementId",
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
                    dataField: "vehicleName",
                    validationRules: [{ type: "required" }],
                    caption: "Vehicle"
                },
                {
                    dataField: "model",
                    validationRules: [{ type: "required" }],
                    caption: "Model"
                },
                {
                    dataField: "length",
                    validationRules: [{ type: "required" }],
                    caption: "Length"
                },
                {
                    dataField: "width",
                    validationRules: [{ type: "required" }],
                    caption: "Width"
                },
                {
                    dataField: "height",
                    validationRules: [{ type: "required" }],
                    caption: "Height"
                },
                {
                    dataField: "vehicleMeasurementCBM",
                    validationRules: [{ type: "required" }],
                    caption: "CBM"
                },

            ],

            onRowInserted: function (e) {


                var model = e.data;

                $.post('/Import/Setup/AddtVehicleMeasurement', { model, model }, function (data) {
                    showToast("Created", "success");
                    getVehicleMeasurement();
                });

            },

            onRowUpdated: function (e) {

                var model = e.data;

                $.post('/Import/Setup/UpdateVehicleMeasurement', { model, model }, function (data) {
                    showToast("Updated", "success");
                    getVehicleMeasurement();
                });
            }

        });
    }



    function formfiled() {
        getVehicleMeasurement();

    }
