

    var AssignedTariffs = [];

    function LoadExaminationTariffGrid() {

        var agentdropdownid = $('#agentdropdown').val();

    var gooddropdownid = $('#gooddropdown').val();

    var indexCargoType = $('#indexCargoType').val();

    var examinationType = $('#examinationType').val();

    console.log("agentdropdownid", agentdropdownid)
    console.log("gooddropdownid", gooddropdownid)
    console.log("indexCargoType", indexCargoType)
    console.log("examinationType", examinationType)

    $.get('/APICAlls/GetExaminationTariffsByPerametersId?ShippingAgentId=' + agentdropdownid + '&GoodId=' + gooddropdownid + '&indexCargoType=' + indexCargoType
    + '&examinationType=' + examinationType, function (data) {

        AssignedTariffs = data;

    console.log("AssignedTariffs", AssignedTariffs)

    var grid = $("#examinationTariffGrid").dxDataGrid(this.gridSettings).dxDataGrid('instance');



    var dataGrid = $("#examinationTariffGrid").dxDataGrid({
        dataSource: AssignedTariffs,
    keyExpr: "examinationTariffId",
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
        dataField: "examinationChargeName",
    caption: "Name",
    allowEditing: false,
                        },

    {
        dataField: "rate20",
    caption: "Rate 20",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                            }

                        },
    {
        dataField: "rate40",
    caption: "Rate 40",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                            }

                        },
    {
        dataField: "rate45",
    caption: "Rate 45",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                            }

                        },
    {
        dataField: "cbmRate",
    caption: "CBM",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                            }

                        },
    {
        dataField: "weightRate",
    caption: "Weight",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                             }

                        },
    {
        dataField: "perIndexRate",
    caption: "Per Index",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                             }

                        },
    {
        dataField: "devededIndexAmount",
    caption: "Deveded Index",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                              }

                        },


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
                            },
    {
        column: "devededIndexAmount",
    summaryType: "sum"
                            }]
                    },


    onRowUpdated: function (e) {
        console.log(e);
    var tariff = e.data;
    console.log(tariff)

    $.post('/Invoice/updateExaminationTariff', {tariff: tariff }, function (data) {

                                if (data.error == true) {
        showToast(data.message, "error");

                                }
    else {

        showToast(data.message, "success");

                                }
                            });
                         
                    },
    onRowRemoved: function (e) {

        console.log("e", e)


                        $.post('/Invoice/DeleteSaveTariffCondition?TariffId=' + e.data.examinationTariffInformationTariffId, function (data) {
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

    else if (event.keyCode === 189) {
                        if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                        }

                    }
                });


            });

            });
         


    }


    function addTariff() {

        var model = $('#tariffCharegsForm').serialize();

    console.log("tariff", model);

    var headid = $('#examinationdown').val();

    if (headid) {



            var agentdropdownid = $('#agentdropdown').val();
    var gooddropdownid = $('#gooddropdown').val();
    var indexCargoType = $('#indexCargoType').val();
    var examinationType = $('#examinationType').val();

    console.log("agentdropdownid", agentdropdownid);
    console.log("gooddropdownid", gooddropdownid);
    console.log("indexCargoType", indexCargoType);
    console.log("examinationType", examinationType);

    if (indexCargoType && examinationType) {
                if (gooddropdownid) {

        console.log("ok model", model)

                    var tariff = {
        shippingAgentId: agentdropdownid,
    goodsHeadId: gooddropdownid,
    indexCargoType: indexCargoType,
    examinationType: examinationType,
                    };



    $.post('/Invoice/SaveTariffCondition?' + model, {tariff: tariff }, function (data) {

        LoadExaminationTariffGrid();

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
    $('#devededIndexAmount').val('')

                        }

                    });

                }
    else {
        showToast("please Select at  good head type option", "error");
                }
            } else {
        showToast("must select cargo type and examination type", "error");
            }







        }

    else {
        showToast("please select Examination head", "error");
        }


    }



    function formfiled() {
        $('#agentdropdown').on('change', function (data) {
            LoadExaminationTariffGrid();

        });

    $('#gooddropdown').on('change', function (data) {
        LoadExaminationTariffGrid();

        });

    $('#indexCargoType').on('change', function (data) {
        LoadExaminationTariffGrid();

        });

    $('#examinationType').on('change', function (data) {
        LoadExaminationTariffGrid();

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
