


    var gatepassesList = [];



    var gdnumberres = "";

    function gdNumber_changed(data) {

        console.log("data2", data.value)


        var gdno = data.value;
    gdnumberres = gdno;
    if (gdno) {


        getgatepasses(gdno);

        }
    else {



        getgatepasses("");
        }


    }


    function getgatepasses(gdno) {

        $.get('/APICalls/GetExportGatePassInfo?gdnumber=' + gdno, function (data) {

            gatepassesList = data

            var dataGrid = $("#gridgds").dxDataGrid({
                dataSource: gatepassesList,
                keyExpr: "gatePassExportId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                columnAutoWidth: true,
                allowColumnResizing: true,
                paging: {
                    enabled: false
                },
                editing: {
                    mode: "row",
                    allowUpdating: false,
                    allowDeleting: false,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                columns: [
                    "gdNumber",
                    "truckNumber",
                    "remarks",
                    "totalDelivered",
                    "status",
                    {
                        dataField: "gatePassExportId",
                        caption: 'S No.',
                        cellTemplate: function (container, options) {
                            $("<button type='button' class='btn btn-success' onClick='routeToReport(" + options.value + ")'>Print</button>")
                                .appendTo(container);
                        }, allowEditing: false
                    },
                ],
                onRowRemoved: function (e) {
                    console.log(e)
                    var key = e.data.doItemExportId;
                    $.post('/Export/Delivery/DeleteGatePass?key=' + key, function (data) {
                        showToast(data.message, data.error ? "error" : "success");

                        var gdno = e.data.gdNumber;

                        getgatepasses(gdno);
                    });
                }


            }).dxDataGrid("instance");

        });

    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function routeToReport(gatePassId) {

        window.open('/Export/Reports/ExportGatePassDrayOff?gatePassExportId=' + gatePassId, '_blank');

    }



    function formfiled() {


    }
