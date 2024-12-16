

    var grid;

    var tariffs = [];

    var tarfftype = [
    {Name: "TariffContract", Text: "Tariff Contract" },
    {Name: "TariffHead", Text: "Tariff Percentage" }
    ];


    function loadGrid() {

        $.get('/Export/TariffExport/GetAllTariffHeadExport', function (data) {

            tariffs = data;

            var dataGrid = $("#tariff").dxDataGrid({
                dataSource: tariffs,
                keyExpr: "tariffHeadExportId",
                wordWrapEnabled: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                showBorders: true,
                RowAlternationEnabled: true,
                paging: {
                    pageSize: 10
                },
                editing: {
                    mode: "inline",
                    allowDeleting: true,
                    allowUpdating: true,
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },
                columns: [

                    "name",
                    "description",
                    {
                        dataField: "tariffHeadExportType",
                        validationRules: [{ type: "required" }],
                        caption: "Tariff Head",
                        lookup: {
                            dataSource: tarfftype,
                            valueExpr: "Name",
                            displayExpr: "Text"
                        }
                    },
                ],
                onRowUpdated: function (e) {
                    console.log(e);
                    var res = e.data;

                    $.post('/Export/TariffExport/UpdateTariffHead', { res: res }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error");
                            window.location.href = window.location.href;

                        }
                        else {

                            showToast(data.message, "success");
                            window.location.href = window.location.href;
                        }
                    });

                },
                onRowRemoved: function (e) {

                    console.log("e", e)

                    $.post('/Tariff/DeleteTariffHead?TariffHeadId=' + e.data.tariffHeadExportId, function (data) {
                        if (data.error == true) {
                            showToast(data.message, "error");
                            window.location.href = window.location.href;

                        }
                        else {

                            showToast(data.message, "success");

                            window.location.href = window.location.href;

                        }
                    });
                },
            }).dxDataGrid("instance");



        });
    }
