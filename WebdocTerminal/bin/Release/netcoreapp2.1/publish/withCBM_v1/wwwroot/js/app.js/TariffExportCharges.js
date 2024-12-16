

    $(function () {

        loadTariffGrid();

    });

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function checkDuplicationCharges(tariff) {

        var duplicateTariff = tariffExports.find(t => ((t.rate20 == tariff.rate20 && t.rate20 != 0)
    || (t.rate40 == tariff.rate40 && t.rate40 != 0)
    || (t.rate45 == tariff.rate45 && t.rate45 != 0)
    || (t.cbmRate == tariff.cbmRate && t.cbmRate != 0)
    || (t.cbmFixedRate == tariff.cbmFixedRate && t.cbmFixedRate != 0)
    || (t.weightRate == tariff.weightRate && t.weightRate != 0)
    || (t.perIndexRate == tariff.perIndexRate && t.perIndexRate != 0))
    && t.tariffHeadExport.name == tariff.tariffHeadExport.name && t.tariffExportId != tariff.tariffExportId);

    if (duplicateTariff)
    return false;
    else
    return true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "tariffHeadExport.name");
    checkColumn(e, "tariffHeadExport.description");

        if (PermissionInputs.find(x => x.fieldName == "Rate20" && x.isChecked == false)) {
        checkColumn(e, "rate20");
        }

        if (PermissionInputs.find(x => x.fieldName == "Rate40" && x.isChecked == false)) {
        checkColumn(e, "rate40");
        }

        if (PermissionInputs.find(x => x.fieldName == "Rate45" && x.isChecked == false)) {
        checkColumn(e, "rate45");
        }

        if (PermissionInputs.find(x => x.fieldName == "CBMRate" && x.isChecked == false)) {
        checkColumn(e, "cbmRate");
        }

        if (PermissionInputs.find(x => x.fieldName == "WeightRate" && x.isChecked == false)) {
        checkColumn(e, "weightRate");
        }

        if (PermissionInputs.find(x => x.fieldName == "PerIndexRate" && x.isChecked == false)) {
        checkColumn(e, "perIndexRate");
        }

        if (PermissionInputs.find(x => x.fieldName == "CBMFixedRate" && x.isChecked == false)) {
        checkColumn(e, "cbmFixedRate");
        }

        if (PermissionInputs.find(x => x.fieldName == "ImplementFrom" && x.isChecked == false)) {
        checkColumn(e, "implementFrom");
        }

        if (PermissionInputs.find(x => x.fieldName == "IsActive" && x.isChecked == false)) {
        checkColumn(e, "isActive");
        }
    }

    var tariffExports = [];
    var tariffExportId;

    function loadTariffGrid() {

        $.get('/Export/TariffExport/GetTariffExprots', function (data) {

            tariffExports = data;

            var dataGrid = $("#tariffGrid").dxDataGrid({
                dataSource: tariffExports,
                keyExpr: "tariffExportId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    pageSize: 10
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },
                editing: {
                    mode: "inline",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                columns: [
                    {
                        dataField: "tariffHeadExport.name",
                        caption: "Name",
                        formItem: {
                            visible: false
                        }
                    },
                    {
                        dataField: "tariffHeadExport.description",
                        caption: "Description",
                        formItem: {
                            visible: false
                        }
                    },
                    "rate20",
                    "rate40",
                    "rate45",
                    "cbmRate",
                    "weightRate",
                    "perIndexRate",
                    "cbmFixedRate",
                    {
                        caption: "Implement From",
                        dataField: "implementFrom",
                        dataType: "date"
                    },
                    {
                        caption: "Is Active",
                        dataField: "isActive",
                        dataType: "boolean"
                    }
                ],
                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onRowUpdating: function (e) {
                    var tariff = Object.assign(e.oldData, e.newData);

                    if (checkDuplicationCharges(tariff)) {

                        $.post('/TariffExport/updateAgentTariffExport', { tariff: tariff }, function (data) {

                            showToast("Charges Updated!", "success");

                        });
                    }
                    else {
                        showToast("Duplicate entry error! Changes will not be saved", "error");
                    }
                }

            }).dxDataGrid("instance");

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });

    }

    function addTariff() {

        var tariff = $('#tariffCharegsForm').serialize();

    var postUrl = '/Export/TariffExport/SaveTariffExportInfo?' + tariff;

    var tariffJson = {
        rate20: $('#rate20').val(),
    rate40: $('#rate40').val(),
    rate45: $('#rate45').val(),
    cbmRate: $('#cbmRate').val(),
    perIndexRate: $('#perIndexRate').val(),
    weightRate: $('#weightRate').val(),
    tariffHeadExport: {
        name: $("#tariffHeadExport option:selected").text()
            }
        };

    if (checkDuplicationCharges(tariffJson)) {

        $.post(postUrl, function (data) {

            loadTariffGrid();
            showToast("Charges Added!", "success");
        });
        }
    else {
        showToast("Duplicate entry error! Changes will not be saved", "error");
        }
    }



    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "TariffHead" && x.isChecked == false)) {

        document.getElementById('tariffHeadExport').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ImplementFrom" && x.isChecked == false)) {

        document.getElementById('implementFrom').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Active" && x.isChecked == false)) {

        document.getElementById('active').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "DailyCharges" && x.isChecked == false)) {

        document.getElementById('dailycharge').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate20" && x.isChecked == false)) {

        document.getElementById('rate20').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate40" && x.isChecked == false)) {

        document.getElementById('rate40').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate45" && x.isChecked == false)) {

        document.getElementById('rate45').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "CBMRate" && x.isChecked == false)) {

        document.getElementById('cbmRate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "WeightRate" && x.isChecked == false)) {

        document.getElementById('weightRate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "PerIndexRate" && x.isChecked == false)) {

        document.getElementById('perIndexRate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "CBMFixedRate" && x.isChecked == false)) {

        document.getElementById('cbmFixedRate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "RoundCBMCode" && x.isChecked == false)) {

        document.getElementById('rccode').style.display = "none";
        }


    }


