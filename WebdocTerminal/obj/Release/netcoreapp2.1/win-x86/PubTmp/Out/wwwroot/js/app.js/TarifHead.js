

    var grid;

    var tariffs = [];

    var tarfftype = [
    {Name: "TariffContract", Text: "Tariff Contract" },
    {Name: "TariffHead", Text: "Tariff Percentage" }
    ];


    function loadGrid() {

        $.get('/APICalls/GetAllTariffHead', function (data) {

            tariffs = data;

            var dataGrid = $("#tariff").dxDataGrid({
                dataSource: tariffs,
                keyExpr: "tariffHeadId",
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
                        dataField: "tariffHeadType",
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

                    $.post('/Tariff/UpdateTariffHead', { res: res }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error");
                        }
                        else {

                            showToast(data.message, "success");
                            window.location.href = window.location.href;
                        }
                    });

                },
                onRowRemoved: function (e) {

                    console.log("e", e)

                    $.post('/Tariff/DeleteTariffHead?TariffHeadId=' + e.data.tariffHeadId, function (data) {
                        if (data.error == true) {
                            showToast(data.message, "error");

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


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function addTariffHead() {


        var formValues = $('#tariffHeadForm').serialize();

    var postUrl = '/Tariff/SaveTariffHead/tariffhead?' + formValues;

        if (tariffs.find(t => t.name == $('#name').val() && t.description == $('#descript').val() )) {

        showToast("Duplicate entry error! Changes will not be saved", "error");

    return;
        }

    console.log()
    if (document.getElementById('name').value) {

        $.post(postUrl, function (data) {

            showToast("Tarrif Informaiton Saved", "success");

            loadGrid();

        });

        } else {
        showToast("Must enter Name", "error");

    return;
        }


    }

    function formfiled() {


        loadGrid();



    }
