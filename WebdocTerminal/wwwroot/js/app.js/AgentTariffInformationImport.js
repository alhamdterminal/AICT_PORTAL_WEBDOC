

    var shippingAgentId = 0;

    var tariffs = [];

    var agentTariffs = [];

    //$(function () {




        //});


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

        var duplicateTariff = tariffs.find(t => ((t.rate20 == tariff.rate20 && t.rate20 != 0)
    || (t.rate40 == tariff.rate40 && t.rate40 != 0)
    || (t.rate45 == tariff.rate45 && t.rate45 != 0)
    || (t.cbmRate == tariff.cbmRate && t.cbmRate != 0)
    || (t.weightRate == tariff.weightRate && t.weightRate != 0)
    || (t.perIndexRate == tariff.perIndexRate && t.perIndexRate != 0))
    && t.tariffHead.name == tariff.tariffHead.name && t.tariffId != tariff.tariffId);

    if (duplicateTariff)
    return false;
    else
    return true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "tariffHead.name");
    checkColumn(e, "tariffHead.description");

        if (PermissionInputs.find(x => x.fieldName == "Rate20" && x.isChecked == false)) {
        checkColumn(e, "rate20");
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate40" && x.isChecked == false)) {
        checkColumn(e, "rate40");
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate45" && x.isChecked == false)) {
        checkColumn(e, "rate45");
        }
        if (PermissionInputs.find(x => x.fieldName == "CbmRate" && x.isChecked == false)) {
        checkColumn(e, "cbmRate");
        }
        if (PermissionInputs.find(x => x.fieldName == "WeightRate" && x.isChecked == false)) {
        checkColumn(e, "weightRate");
        }
        if (PermissionInputs.find(x => x.fieldName == "PerIndexRate" && x.isChecked == false)) {
        checkColumn(e, "perIndexRate");
        }
        if (PermissionInputs.find(x => x.fieldName == "ImplementFrom" && x.isChecked == false)) {
        checkColumn(e, "implementFrom");
        }
        if (PermissionInputs.find(x => x.fieldName == "IsActive" && x.isChecked == false)) {
        checkColumn(e, "isActive");
        }
        if (PermissionInputs.find(x => x.fieldName == "TariffId" && x.isChecked == false)) {
        checkColumn(e, "tariffId");
        }
 





    }

    function addTariff(tariffId) {
        if (PermissionInputs.find(x => x.fieldName == "TariffId" && x.isChecked == false)) {
        showToast("Permission Denied", "error");
    return
        } else {
               if ($('#agentdropdown').val() == "") {

        alert("Select Agent");
        }
    else {
            var tariff = {shippingAgentId: shippingAgentId, tariffId: tariffId };

    $.post('/Tariff/SaveAgentTariff', {tariff: tariff }, function (data) {

        tariffs = tariffs.filter(t => t != tariffId);

    LoadTariffGrid();

    LoadAgentTariffGrid();
            });
        }

        }

     
    }

    function LoadTariffGrid() {

        $.get('/APICAlls/GetUnassignedShippingAgentTariffs?ShippingAgentId=' + shippingAgentId, function (data) {

            tariffs = data;

            var dataGrid = $("#tariffGrid").dxDataGrid({
                dataSource: tariffs,
                keyExpr: "tariffId",
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
                        dataField: "tariffHead.name",
                        caption: "Name",
                        formItem: {
                            visible: false
                        }
                    },
                    {
                        dataField: "tariffHead.description",
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
                        dataField: "tariffId",
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

                        $.post('/Tariff/updateAgentTariff', { tariff: tariff }, function (data) {

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

        $.get('/APICAlls/GetShippingAgentTariffs?ShippingAgentId=' + shippingAgentId, function (data) {

            agentTariffs = data;

            var dataGrid = $("#agentTariffGrid").dxDataGrid({
                dataSource: agentTariffs,
                keyExpr: "tariffId",
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
                    mode: "inline",
                    allowUpdating: true,
                    allowDeleting: true,

                },
                columns: [
                    {
                        dataField: "tariffHead.name",
                        caption: "Name",
                        formItem: {
                            visible: false
                        }
                    },
                    {
                        dataField: "tariffHead.description",
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
                    console.log(e);
                    var tariff = e.data;
                    // var tariff = Object.assign(e.oldData, e.newData);;
                    console.log(tariff)
                    if (checkDuplicationCharges(tariff)) {

                        $.post('/Tariff/updateAgentTariff', { tariff: tariff }, function (data) {

                            showToast("Charges Updated!", "success");
                        });
                    }
                    else {
                        showToast("Duplicate entry error! Changes will not be saved", "error");
                    }
                },
                onRowRemoved: function (e) {

                    $.post('/Tariff/RemoveAgentTariff?TariffId=' + e.data.tariffId + '&agentId=' + shippingAgentId, function (data) {

                    });
                }

            }).dxDataGrid("instance");

        });
    }

    function formfiled() {
        if (PermissionInputs.find(x => x.fieldName == "ShippingAgent" && x.isChecked == false)) {

        document.getElementById('agentdropdown').disabled = true;

        }


    LoadTariffGrid();

    $('#agentdropdown').on('change', function (data) {

        shippingAgentId = this.value;

    LoadTariffGrid();

    LoadAgentTariffGrid();

        });
    }