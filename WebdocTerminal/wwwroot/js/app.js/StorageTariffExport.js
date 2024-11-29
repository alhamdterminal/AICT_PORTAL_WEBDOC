

    var type = "CFS";

    function showStorageSlab() {

        type = $("input[name='type']:checked").val();

    loadStorageSlabGrid();

    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    var tariffExportId = 0;

    var StorageSlabs = [];


    function containerCallback() {

    }


    $(function () {

        $('#tariff').on('change', function (e) {

            tariffExportId = this.value;

            getFreeDays();

            $.get('/Export/TariffExport/GetStorageSlabsExportByTariff?TariffExportId=' + tariffExportId, function (data) {

                StorageSlabs = data;

                loadStorageSlabGrid();
            })

        });

    });


    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function disableColumns(e) {
        if (PermissionInputs.find(x => x.fieldName == "Rate" && x.isChecked == false)) {

        checkColumn(e, "rate");
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate20" && x.isChecked == false)) {

        checkColumn(e, "rate20");
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate40" && x.isChecked == false)) {

        checkColumn(e, "rate40");
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate45" && x.isChecked == false)) {

        checkColumn(e, "rate45");
        }
        if (PermissionInputs.find(x => x.fieldName == "Description" && x.isChecked == false)) {

        checkColumn(e, "description");
        }
        if (PermissionInputs.find(x => x.fieldName == "NumberOfDays" && x.isChecked == false)) {

        checkColumn(e, "noOfDays");
        }
    }

    function loadStorageSlabGrid() {

        if (type == "CFS") {
        $("#gridContainer").dxDataGrid({
            dataSource: StorageSlabs,
            keyExpr: "tariffExportId",
            wordWrapEnabled: true,
            showBorders: true,
            allowColumnResizing: true,
            columnAutoWidth: true,

            paging: {
                enabled: false
            },
            editing: {
                mode: "row",
                allowUpdating: false,
                allowDeleting: false,
                allowAdding: true
            },
            columns: [
                {
                    dataField: "rate",
                    caption: "Rate",
                    dataType: "number"
                },
                {
                    dataField: "description",
                    caption: "Description"
                },
                {
                    dataField: "noOfDays",
                    caption: "Number Of Days"
                },
            ],
            onEditorPreparing: function (e) {

                disableColumns(e);

            },
            onRowInserted: function (e) {

                addStorageSlab(e);
            }
        });

        }
    else {

        $("#gridContainer").dxDataGrid({
            dataSource: StorageSlabs,
            keyExpr: "tariffExportId",
            wordWrapEnabled: true,
            showBorders: true,
            allowColumnResizing: true,
            columnAutoWidth: true,
            paging: {
                enabled: false
            },
            editing: {
                mode: "row",
                allowUpdating: false,
                allowDeleting: false,
                allowAdding: true
            },
            columns: [
                {
                    dataField: "rate20",
                    caption: "Rate 20",
                    dataType: "number"
                },
                {
                    dataField: "rate40",
                    caption: "Rate 40",
                    dataType: "number"
                },
                {
                    dataField: "rate45",
                    caption: "Rate 45",
                    dataType: "number"
                },
                {
                    dataField: "description",
                    caption: "Description"
                },
                {
                    dataField: "noOfDays",
                    caption: "Number Of Days"
                },
            ],
            onEditorPreparing: function (e) {

                disableColumns(e);

            },
            onRowInserted: function (e) {

                addStorageSlab(e);
            }
        });
        }
    }

    function getFreeDays() {

        if (tariffExportId == 0) {

        alert("Please select tariff")
    }
    else {

        $.get('/Export/TariffExport/GetExtraFreeDays?tariffExportId=' + tariffExportId, function (val) {


            $('#freedays').val(val)
            if (val > 0) {
                document.getElementById("freedays").readOnly = true;
            }
            else {
                document.getElementById("freedays").readOnly = false;
            }

        });
        }

    }

    function saveFreeDays() {

        if (tariffExportId == 0) {

        alert("Please select tariff")
    }
    else {

        $.post('/Export/TariffExport/SaveExtraFreeDays', { freedays: $('#freedays').val(), tariffExportId: tariffExportId }, function (data) {

            if ($('#freedays').val() > 0) {
                document.getElementById("freedays").readOnly = true;
            }
            else {
                document.getElementById("freedays").readOnly = false;
            }
            showToast("Extra free days saved!", "success");

        });
        }
    }

    function addStorageSlab(e) {

        if (tariffExportId == 0) {

        alert("Please select tariff")
    }
    else {
            var data = e.data;
    var storageSlab = {rate: data.rate, rate20: data.rate20, rate40: data.rate40, rate45: data.rate45, description: data.description, noOfDays: data.noOfDays, tariffExportId: tariffExportId };
    $.post('/Export/TariffExport/AddStorageSlabExport', {slab: storageSlab }, function (data) {

        showToast("Storage Tariff", "success");

            });
        }
    }




    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Tariff" && x.isChecked == false)) {
        document.getElementById('tariff').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ExtraFreeDays" && x.isChecked == false)) {
        document.getElementById('freedays').disabled = true;
        }
    }
