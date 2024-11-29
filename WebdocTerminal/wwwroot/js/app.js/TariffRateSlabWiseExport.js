


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

        loadStorageSlabGrid();

    $('#tariff').on('change', function (e) {

        tariffExportId = this.value;


    $.get('/Export/TariffExport/GetlabsWiseRateExportByTariff?TariffExportId=' + tariffExportId, function (data) {

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



        $("#gridContainer").dxDataGrid({
            dataSource: StorageSlabs,
            keyExpr: "tariffRateSlabWiseId",
            wordWrapEnabled: true,
            showBorders: true,
            allowColumnResizing: true,
            columnAutoWidth: true,
            paging: {
                enabled: false
            },
            editing: {
                mode: "row",
                allowUpdating: true,
                allowDeleting: true,
                allowAdding: true
            },
            columns: [
                {
                    dataField: "rate",
                    caption: "Rate",
                    dataType: "number",
                    validationRules: [{ type: "required" }],

                },
                {
                    dataField: "fromCBM",
                    caption: "From",
                    validationRules: [{ type: "required" }],

                },
                {
                    dataField: "toCBM",
                    caption: "To",
                    validationRules: [{ type: "required" }],

                },
            ],
            onEditorPreparing: function (e) {

                disableColumns(e);

            },
            onRowInserted: function (e) {

                addStorageSlab(e);
            },

            onRowUpdated: function (e) {

                console.log(e);
                var slab = e.data;
                $.post('/Export/TariffExport/UpdateSlabWiseTariffExport', { slab, slab }, function (data) {
                    showToast(" Updated", "success");
                });
            },
            onRowRemoved: function (e) {

                var key = e.data.tariffRateSlabWiseId;
                $.post('/Export/TariffExport/DeleteParty?key=' + key, function (data) {
                    showToast(" Deleted", "success");
                });
            }


        });

    }



    function addStorageSlab(e) {



        var resdata = $("#tariff").val();

    console.log("resdata", resdata);

    if (resdata) {

                    var data = e.data;
    var storageSlab = {rate: data.rate, fromCBM: data.fromCBM, toCBM: data.toCBM, tariffExportId: tariffExportId };
    $.post('/Export/TariffExport/AddSlabWiseTariffExport', {slab: storageSlab }, function (data) {
        showToast("Save Slab Rate", "success");
            });

        }
    else {
        showToast("please select tariff head", "error");
        }

        //if (tariffExportId == 0) {
        //    alert("Please select tariff")
        //}
        //else {
        //    var data = e.data;
        //    var storageSlab = { rate: data.rate, rate20: data.rate20, rate40: data.rate40, rate45: data.rate45, description: data.description, noOfDays: data.noOfDays, tariffExportId: tariffExportId };
        //    $.post('/Export/TariffExport/AddStorageSlabExport', { slab: storageSlab }, function (data) {
        //        showToast("Storage Tariff", "success");
        //    });
        //}

    }




    function formfiled() {


    }
    /**/
