


    $(function () {
        getTransporterData();
    });

    var TransporterData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getTransporterData() {
        $.get('/Import/Setup/GetTransporters', function (data) {
            TransporterData = data;
            console.log("TransporterData", TransporterData);
            Transportergrid();
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
    function Transportergrid() {


        console.log("TransporterData", TransporterData)

        $("#gridContainer").dxDataGrid({
        dataSource: TransporterData,
    keyExpr: "transporterId",
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
        dataField: "transporterName",
    validationRules: [{type: "required" }],
    caption: "Name"
                },
    {
        dataField: "vehicleNumber",
    validationRules: [{type: "required" }],
    caption: "Vehicle No"
                },

    ],


    onRowInserted: function (e) {

        console.log(e) 
                var data = e.data;

    $.post('/Import/Setup/AddTransporters', {data, data}, function (result) {

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
    $.post('/Import/Setup/UpdateTransporter', {data, data}, function (result) {
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
