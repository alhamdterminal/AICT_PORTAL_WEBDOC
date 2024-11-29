

    var container = "";


    var GateIncontainers = [];

    function container_changed(data) {
        console.log("data", data)
        if (data.container) {
        container = data.igm;
        }
    if (data.value) {
        container = data.value;
        }

    getgateinContainersDetail(container);

    }


    function getgateinContainersDetail(container) {
        $.get('/APICalls/GetGateInContainersDetail?container=' + container, function (data) {

            console.log("data", data);

            GateIncontainers = data;


            var dataGrid = $("#gateincontainers").dxDataGrid({
                dataSource: GateIncontainers,
                keyExpr: "gateInId",

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
                    mode: "batch",
                    allowUpdating: false,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                columns: [
                    {
                        dataField: "virNo",
                        caption: "VIR No",
                        allowEditing: false,
                    },
                    {
                        dataField: "containerNo",
                        caption: "Container No",
                        allowEditing: false,
                    },
                    {
                        dataField: "containerSize",
                        caption: "Size",
                        allowEditing: false,
                    },
                    {
                        dataField: "isoCode",
                        caption: "ISO Code",
                        allowEditing: false,
                    },
                    {
                        dataField: "gateInDateTime",
                        caption: "Arrive Date",
                        dataType: 'date',
                        format: "MM/dd/yyyy HH:mm",
                        allowEditing: false
                    },
                    {
                        dataField: "carrierName",
                        caption: "Carrier Name",
                        allowEditing: false
                    },
                    {
                        dataField: "menifestedSealNo",
                        caption: "MenifestedSealNo",
                        allowEditing: false
                    },
                    {
                        dataField: "vehicleNo",
                        caption: "VehicleNo",
                        allowEditing: false
                    },
                    {
                        dataField: "weight",
                        caption: "Weight",
                        allowEditing: false
                    }
                ],

                onRowUpdated: function (e) {

                    $("#btnSubmit").attr("disabled", false);
                }

            }).dxDataGrid("instance");


        });
    }


    function formfiled() { }

