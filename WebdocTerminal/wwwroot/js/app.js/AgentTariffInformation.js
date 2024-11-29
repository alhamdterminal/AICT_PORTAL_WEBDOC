

    var shippingAgentExportId = 0;

    var tariffs = [];

    var agentTariffs = [];

    $(function () {

        LoadTariffGrid();

    $('#agentdropdown').on('change', function (data) {

        shippingAgentExportId = this.value;

    LoadTariffGrid();

    LoadAgentTariffGrid();

        });


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

        console.log("1tariff", tariff);
    console.log("tariffs", tariffs);

        var duplicateTariff = tariffs.find(t => ((t.rate20 == tariff.rate20 && t.rate20 != 0)
    || (t.rate40 == tariff.rate40 && t.rate40 != 0)
    || (t.rate45 == tariff.rate45 && t.rate45 != 0)
    || (t.cbmRate == tariff.cbmRate && t.cbmRate != 0)
    || (t.cbmFixedRate == tariff.cbmFixedRate && t.cbmFixedRate != 0)
    || (t.weightRate == tariff.weightRate && t.weightRate != 0)
    || (t.perIndexRate == tariff.perIndexRate && t.perIndexRate != 0))
    && t.tariffHeadExport.name == tariff.tariffHeadExport.name && t.tariffExportId != tariff.tariffExportId);

    console.log("duplicateTariff", duplicateTariff)
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
        if (PermissionInputs.find(x => x.fieldName == "GDRate" && x.isChecked == false)) {
        checkColumn(e, "perIndexRate");
        }
        if (PermissionInputs.find(x => x.fieldName == "CBMFixedRate" && x.isChecked == false)) {
        checkColumn(e, "cbmFixedRate");
        }
        if (PermissionInputs.find(x => x.fieldName == "MultiplyCBM" && x.isChecked == false)) {
        checkColumn(e, "cbmMultiply");
        }
        if (PermissionInputs.find(x => x.fieldName == "MultiplyIfGreaterthen" && x.isChecked == false)) {
        checkColumn(e, "cbmMultiplyValue");
        }
        if (PermissionInputs.find(x => x.fieldName == "ImplementFrom" && x.isChecked == false)) {
        checkColumn(e, "implementFrom");
        }
        if (PermissionInputs.find(x => x.fieldName == "IsActive" && x.isChecked == false)) {
        checkColumn(e, "isActive");
        }
        if (PermissionInputs.find(x => x.fieldName == "Traiff" && x.isChecked == false)) {
        checkColumn(e, "tariffExportId");
        }
    }

    function addTariff(tariffExportId) {

        if ($('#agentdropdown').val() == "") {

        alert("Select Agent");
        }
    else {
            var tariff = {shippingAgentExportId: shippingAgentExportId, tariffExportId: tariffExportId };

    $.post('/Export/TariffExport/SaveAgentTariff', {tariff: tariff }, function (data) {

        tariffs = tariffs.filter(t => t != tariffExportId);

    LoadTariffGrid();

    LoadAgentTariffGrid();
            });
        }
    }

    function LoadTariffGrid() {

        $.get('/APICAlls/GetUnassignedExportShippingAgentTariffs?shippingAgentExportId=' + shippingAgentExportId, function (data) {

            tariffs = data;

            var dataGrid = $("#tariffGrid").dxDataGrid({
                dataSource: tariffs,
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
                    {
                        caption: "GD Rate",
                        dataField: "perIndexRate",
                    },
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
                    },
                    {
                        dataField: "tariffExportId",
                        caption: "Tariff",

                        cellTemplate: function (container, options) {
                            $("<button class='btn btn-success' onClick='addTariff(" + options.value + ")'>Add</button>")
                                .appendTo(container);
                        }
                    }
                ],

                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onRowUpdating: function (e) {
                    var tariff = Object.assign(e.oldData, e.newData);;

                    if (checkDuplicationCharges(tariff)) {

                        $.post('/Export/Tariffexport/updateAgentTariff', { tariff: tariff }, function (data) {

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

    function LoadAgentTariffGrid() {

        $.get('/APICAlls/GetShippingAgentExportTariffs?shippingAgentExportId=' + shippingAgentExportId, function (data) {

            agentTariffs = data;

            var dataGrid = $("#agentTariffGrid").dxDataGrid({
                dataSource: agentTariffs,
                keyExpr: "shippingAgentTariffExportId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    enabled: true
                },
                editing: {
                    mode: "batch",
                    allowUpdating: true,
                    allowDeleting: true,

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
                    {
                        caption: "GD Rate",
                        dataField: "perIndexRate",
                    },
                    "cbmFixedRate",
                    {
                        caption: "Multiply CBM",
                        dataField: "cbmMultiply",
                        dataType: "boolean"
                    },
                    {
                        caption: "Multiply if greater than",
                        dataField: "cbmMultiplyValue",
                        dataType: "number"
                    },
                    {
                        caption: "Implement From",
                        dataField: "implementFrom",
                        dataType: "date"
                    }
                ],
                summary: {
                    totalItems: [
                        {
                            column: "rate20",
                            summaryType: "sum"
                        },
                        {
                            column: "rate40",
                            summaryType: "sum"
                        },
                        {
                            column: "rate45",
                            summaryType: "sum"
                        },
                        {
                            column: "cbmRate",
                            summaryType: "sum"
                        },
                        {
                            column: "weightRate",
                            summaryType: "sum"
                        },
                        {
                            column: "perIndexRate",
                            summaryType: "sum"
                        }]
                },

                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {
                    var tariff = e.data;
                    // var tariff = Object.assign(e.oldData, e.newData);;
                    console.log("tariff", tariff);

                    if (checkDuplicationCharges(tariff)) {

                        $.post('/Export/Tariffexport/updateAgentTariffExport', { tariff: tariff }, function (data) {

                            showToast("Charges Updated!", "success");
                        });
                    }
                    else {
                        showToast("Duplicate entry error! Changes will not be saved", "error");
                    }
                },
                onRowRemoved: function (e) {

                    $.post('/Export/Tariffexport/RemoveAgentTariff?tariffExportId=' + e.data.tariffExportId + '&agentId=' + shippingAgentExportId, function (data) {

                    });
                }

            }).dxDataGrid("instance");

        });
    }

    function formfiled() {
        if (PermissionInputs.find(x => x.fieldName == "AgentName" && x.isChecked == false)) {

        document.getElementById('agentdropdown').disabled = true;
        }
    }
    function FromOneAgentToAnother() {

        console.log("ShippingAgent", $('#agentdropdown').val());

    if ($('#agentdropdown').val() == "" || $('#agentdropdown').val() == "Select Agent") {

        alert("Please Select From  Agent Name");
        }
    else {
            
            if ($('#toagentdropdown').val() == "" || $('#toagentdropdown').val() == "Select Agent") {

        alert("Please Select To  Agent Name");
            }

    else {
                var shippingAgent = $('#agentdropdown').val();
    var ToshippingAgent = $('#toagentdropdown').val();
    console.log("shippingAgent", shippingAgent);
    console.log("ToshippingAgent", ToshippingAgent);

    $.post('/Export/TariffExport/AssignFromOneAgentToAnother?shippingAgent=' + shippingAgent + '&ToshippingAgent=' + ToshippingAgent, function (data) {
                    if (data.error == true) {
        showToast(data.message, "error");
                    }
    else {
        showToast(data.message, "success");
                          
                    }
                })

            }



        }
    }