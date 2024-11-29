

    var shippingAgentId = 0;

    var tariffs = [];

    var AssignedTariffs = [];


    var tariffId = 0;


    $(function () {
        getnatureoftarifftypes();

    LoadAgentTariffGrid();
    getshippingagnetdetail();

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

        var duplicateTariff = tariffs.find(t => ((t.rate20 == tariff.rate20 && t.rate20 != 0)
    || (t.rate40 == tariff.rate40 && t.rate40 != 0)
    || (t.rate45 == tariff.rate45 && t.rate45 != 0)
    || (t.cbmRate == tariff.cbmRate && t.cbmRate != 0)
    || (t.weightRate == tariff.weightRate && t.weightRate != 0)
    || (t.perIndexRate == tariff.perIndexRate && t.perIndexRate != 0)
    || (t.devededIndexAmount == tariff.devededIndexAmount && t.devededIndexAmount != 0))
    && t.tariffHead.name == tariff.tariffHead.name && t.tariffId != tariff.tariffId);

    if (duplicateTariff)
    return false;
    else
    return true;
    }


    var ListOfTariffType = [
    {Name: "Nominated" },
    {Name: "FreeHand" }
    ];

    function LoadAgentTariffGrid() {

        var agentdropdownid = $('#agentdropdown').val();


    console.log("agentdropdownid", agentdropdownid)

    $.get('/APICAlls/GetExportTariffsByPerametersId?ShippingAgentId=' + agentdropdownid, function (data) {

        AssignedTariffs = data;

    console.log("AssignedTariffs", AssignedTariffs)

    var grid = $("#agentTariffGrid").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    var dataGrid = $("#agentTariffGrid").dxDataGrid({
        dataSource: AssignedTariffs,
    keyExpr: "tariffExportId",
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
    allowDeleting: true,
    allowUpdating: true,
                },
    columns: [
    {
        dataField: "tariffHeadName",
    caption: "Name",
    allowEditing: false,
    formItem: {
        visible: false
                        }
                    },
    {
        dataField: "tariffHeadDescription",
    caption: "Description",
    allowEditing: false,
    formItem: {
        visible: false
                        }
                    },
    {
        dataField: "rate20",
    caption: "Rate 20",
    dataType: "number",
    format: "#,##0.##"
                    },

    {
        dataField: "rate40",
    caption: "Rate 40",
    dataType: "number",
    format: "#,##0.##"
                    },
    {
        dataField: "rate45",
    caption: "Rate 45",
    dataType: "number",
    format: "#,##0.##"
                    },
    {
        dataField: "cbmRate",
    caption: "CBM",
    dataType: "number",
    format: "#,##0.##"
                    },
    {
        dataField: "weightRate",
    caption: "Weight",
    dataType: "number",
    format: "#,##0.##"
                    },
    {
        dataField: "perIndexRate",
    caption: "Per Index",
    dataType: "number",
    format: "#,##0.##"
                    },
    {
        dataField: "cbmFixedRate",
    caption: "CBM Fixed Rate",
    dataType: "number",
    format: "#,##0.##"
                    },
    {
        caption: "Multiply if greater than",
    dataField: "cbmMultiplyValue",
    dataType: "number"
                    },
    {
        caption: "Implement From",
    dataField: "implementFrom",
    dataType: "date",
    allowEditing: false,
                    },

                    //{
        //    dataField: "tariffType",
        //    caption: "Tariff Type",
        //    validationRules: [{ type: "required" }],
        //    lookup: {
        //        dataSource: ListOfTariffType,
        //        valueExpr: "Name",
        //        displayExpr: "Name"
        //    }
        //},

        {
            dataField: "natureOfTariffId",
            caption: "Tariff Type",
            validationRules: [{ type: "required" }],
            lookup: {
                dataSource: NatureOfTariffTypesList,
                valueExpr: "natureOfTariffId",
                displayExpr: "natureOfTariffName"
            }
        },


                ],
    summary: {
        totalItems: [
    {
        column: "rate20",
    summaryType: "sum",
    displayFormat: '{0}',
                        },
    {
        column: "rate40",
    summaryType: "sum",
    displayFormat: '{0}',
                        },
    {
        column: "rate45",
    summaryType: "sum",
    displayFormat: '{0}',
                        },
    {
        column: "cbmRate",
    summaryType: "sum",
    displayFormat: '{0}',
                        },
    {
        column: "weightRate",
    summaryType: "sum",
    displayFormat: '{0}',
                        },
    {
        column: "perIndexRate",
    summaryType: "sum",
    displayFormat: '{0}',
                        }]
                },

    onEditorPreparing: function (e) {

    },
    onRowUpdated: function (e) {
        console.log(e);
    var tariff = e.data;
    // var tariff = Object.assign(e.oldData, e.newData);;
    console.log(tariff)
    if (checkDuplicationCharges(tariff)) {

        $.post('/Export/TariffExport/updateAgentTariff', { tariff: tariff }, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            }
            else {

                showToast(data.message, "success");

            }
        });
                    }
    else {
        showToast("Duplicate entry error! Changes will not be saved", "error");
                    }
                },
    onRowRemoved: function (e) {

        console.log("e", e)

                    var agentdropid = $('#agentdropdown').val();

    console.log("agentdropid", agentdropid);

    $.post('/Export/TariffExport/DeleteSaveTariffCondition?TariffId=' + e.data.tariffExportId + '&agentdropid=' + agentdropid, function (data) {
                        if (data.error == true) {
        showToast(data.message, "error");

                        }
    else {

        showToast(data.message, "success");


                        }
                    });

                }

            }).dxDataGrid("instance");



    grid.on('editorPrepared', function (e) {
                if (e.parentType !== 'dataRow') {
                    return;
                }

    e.editorElement[e.editorName]('instance').on('keyDown', function (arg) {

                    var gridComponent = e.component;

    var event = arg.jQueryEvent;

    if (event.keyCode === 38) {
                        if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                        }

                    } else if (event.keyCode === 40) {
                        if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                        }

                    }
                });


            });
        });




    }

    var ImplementFrom = [
    {Name: "OPIA" },
    {Name: "GateInDate" }

    ];

    var NatureOfTariff = [];

    var ImplementTo = [
    {Name: "ETD" },
    {Name: "CurrentDate" }
    ];

    function getshippingagnetdetail() {

        var aggentid = 0;
    var agentdropdownid = $('#agentdropdown').val();
    console.log("agentdropdownid", agentdropdownid)


    if (agentdropdownid) {
        aggentid = agentdropdownid
    }
    console.log("aggentid", aggentid)

    $.get('/APICAlls/GetShippingAgentExportById?shippingagentid=' + agentdropdownid, function (data) {


        console.log("data", data)

            var dataGrid = $("#shippingAgentGrid").dxDataGrid({
        dataSource: data,
    keyExpr: "shippingAgentExportId",
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
                },
    columns: [
    {
        dataField: "shippingAgentName",
    validationRules: [{type: "required" }],
    allowEditing: true,
    caption: "Name"
                    },
    {
        dataField: "implementFrom",
    Caption: "Imlement From",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: ImplementFrom,
    valueExpr: "Name",
    displayExpr: "Name"
                        }
                    },
    {
        dataField: "implementTo",
    Caption: "Implement To",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: ImplementTo,
    valueExpr: "Name",
    displayExpr: "Name"
                        }
                    },
                    //{
        //     dataField: "tariffType",
        //     caption: "Tariff Type",
        //     validationRules: [{ type: "required" }],
        //     lookup: {
        //         dataSource: ListOfTariffType,
        //         valueExpr: "Name",
        //         displayExpr: "Name"
        //     }
        // },

        {
            dataField: "natureOfTariffId",
            caption: "Tariff Type",
            validationRules: [{ type: "required" }],
            lookup: {
                dataSource: NatureOfTariffTypesList,
                valueExpr: "natureOfTariffId",
                displayExpr: "natureOfTariffName"
            }
        },



                ],



    onRowUpdated: function (e) {
        console.log(e);
    var values = e.data;

    console.log("values", values)

    $.post('/Export/ShippingAgentExport/UpdateShippingAgentExport', {values, values}, function (data) {

                        if (data.error == true) {
        showToast(data.message, "error");

                        }
    else {

        showToast(data.message, "success");

                        }
                    });

                },


            }).dxDataGrid("instance");

        });
    }




    function formfiled() {
        if (PermissionInputs.find(x => x.fieldName == "ShippingAgent" && x.isChecked == false)) {

        document.getElementById('agentdropdown').disabled = true;

        }

    $('#agentdropdown').on('change', function (data) {
        LoadAgentTariffGrid();
    getshippingagnetdetail();
        });

    }


    function addTariff() {




        var tariffType = $('#tariffType').val();

    console.log("tariffType", tariffType)

    if (tariffType) {
            var model = $('#tariffCharegsForm').serialize();

    console.log("tariff", model);

    var tariffheadid = $('#tariffHead').val();

    if (tariffheadid) {


                var agentdropdownid = $('#agentdropdown').val();

    console.log("agentdropdownid", agentdropdownid)


    if (agentdropdownid) {

        console.log("ok model", model)

                    var tariff = {
        shippingAgentId: agentdropdownid,
                    };



    $.post('/Export/TariffExport/SaveTariffCondition?' + model + '&shippingagentid=' + agentdropdownid, function (data) {

        LoadAgentTariffGrid();

    if (data.error == true) {
        showToast(data.message, "error");

                        }
    else {

        showToast(data.message, "success");

    $('#rate20').val('')
    $('#rate40').val('')
    $('#rate45').val('')
    $('#cbmRate').val('')
    $('#weightRate').val('')
    $('#perIndexRate').val('')

                        }

                    });

                }
    else {
        showToast("please Select at least one option", "error");
                }
            }

    else {
        showToast("please select tariff head", "error");
            }
        }
    else {
            return showToast("please select tariff nature", "error");
        }


    }

    var NatureOfTariffTypesList = [];
    function getnatureoftarifftypes() {

        $.get('/APICalls/GetNatureOftariffType', function (resdata) {

            NatureOfTariffTypesList = resdata;
        });
    }


    function AssignToNewAgent() {

        var agentdropdown = $('#agentdropdown').val();
    var secagentdropdown = $('#secagentdropdown').val();

    if (agentdropdown && secagentdropdown) {

        $.post('/Export/TariffExport/SaveTariffConditionByagent?agentdropdown=' + agentdropdown + '&secagentdropdown=' + secagentdropdown, function (data) {
            LoadAgentTariffGrid();

            if (data.error == true) {
                showToast(data.message, "error");

            }
            else {

                showToast(data.message, "success");

            }

        });


        } else {
        showToast("please select both agents", "error")
    }

    }
