

    var shippingAgentId = 0;

    //$(function () {



        //})

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
        if (PermissionInputs.find(x => x.fieldName == "CBMRate" && x.isChecked == false)) {
        checkColumn(e, "cbmRate");
        }
          if (PermissionInputs.find(x => x.fieldName == "WeightRate" && x.isChecked == false)) {
        checkColumn(e, "weightRate");
        }
          if (PermissionInputs.find(x => x.fieldName == "PerIndexRate" && x.isChecked == false)) {
        checkColumn(e, "perIndexRate");
        }
        if (PermissionInputs.find(x => x.fieldName == "PerIndexRate" && x.isChecked == false)) {
        checkColumn(e, "perIndexRate");
        }
        if (PermissionInputs.find(x => x.fieldName == "IsActive" && x.isChecked == false)) {
        checkColumn(e, "isActive");
        }
        if (PermissionInputs.find(x => x.fieldName == "ImplementFrom" && x.isChecked == false)) {
        checkColumn(e, "implementFrom");
        }



    }

    var tariffs = [];
    var tariffId;

    function loadTariffGrid() {
        console.log("agentGrid", shippingAgentId)
        $.get('/APICAlls/GetShippingAgentTariffs?ShippingAgentId=' + shippingAgentId, function (data) {

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
    selectTextOnEditStart: true,
    startEditAction: "click"
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
                    }
    ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
                },
    onRowUpdating: function (e) {
                    var tariff = Object.assign(e.oldData, e.newData);

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

    function addTariff() {

        var tariff = $('#tariffCharegsForm').serialize();

    var postUrl = '/Tariff/SaveCustomerTariff?' + tariff + "&ShippingAgentId=" + shippingAgentId;

    var tariffJson = {
        rate20: $('#rate20').val(),
    rate40: $('#rate40').val(),
    rate45: $('#rate45').val(),
    cbmRate: $('#cbmRate').val(),
    perIndexRate: $('#perIndexRate').val(),
    weightRate: $('#weightRate').val(),
    tariffHead: {
        name: $("#tariffHead option:selected").text()
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


        if (PermissionInputs.find(x => x.fieldName == "IsActive" && x.isChecked == false)) {

        document.getElementById("Isactive").style.display = "none";

        }
        if (PermissionInputs.find(x => x.fieldName == "DailyCharges" && x.isChecked == false)) {

        document.getElementById('Dailycharges').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "RoundCBMCode" && x.isChecked == false)) {

        document.getElementById('roundCBMCode').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "RoundCBMCode" && x.isChecked == false)) {

        document.getElementById('roundCBMCode').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "PerIndexRate" && x.isChecked == false)) {

        document.getElementById('perIndexRate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "WeightRate" && x.isChecked == false)) {

        document.getElementById('weightRate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "CBMRate" && x.isChecked == false)) {

        document.getElementById('cbmRate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate45" && x.isChecked == false)) {

        document.getElementById('rate45').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate40" && x.isChecked == false)) {

        document.getElementById('rate40').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate20" && x.isChecked == false)) {

        document.getElementById('rate20').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ImplementFrom" && x.isChecked == false)) {

        document.getElementById('implementFrom').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "TariffHead" && x.isChecked == false)) {

        document.getElementById('tariffHead').disabled = true;
        }

    $.get('/APICAlls/GetUser', function (user) {

        // console.log("shippingAgentId",user)


        shippingAgentId = user.shippingAgentId;
    loadTariffGrid();
        });
    }





