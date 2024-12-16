


    $(function () {
        getPrealertVesselData();
    });

    var PrealertVesselData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getPrealertVesselData() {
        $.get('/Import/Setup/GetPreAlertVessels', function (data) {
            PrealertVesselData = data;
            console.log("PrealertVesselData", PrealertVesselData);
            PrealertVesselgrid();
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
    function PrealertVesselgrid() {


        console.log("PrealertVesselData", PrealertVesselData)

        $("#gridContainer").dxDataGrid({
        dataSource: PrealertVesselData,
    keyExpr: "preAlertVesselId",
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
        dataField: "preAlertVesselName",
    validationRules: [{type: "required" }],
    caption: "Vessel"
                }

    ],


    onRowInserted: function (e) {

        console.log(e)
                var data = e.data;

    $.post('/Import/Setup/AddpreAlertVessel', {data, data}, function (result) {

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
    $.post('/Import/Setup/UpdatePreAlertVessel', {data, data}, function (result) {
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
