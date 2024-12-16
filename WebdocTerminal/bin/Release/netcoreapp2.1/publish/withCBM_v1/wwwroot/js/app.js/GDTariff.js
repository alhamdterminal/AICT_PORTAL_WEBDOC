




    var tariffs = [];

    var containerIndexTariffs = [];

    var newdata = null;

    var type = "CFS";



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





    var gd;

    function gdNumber_changed(data) {


        gd = data.value;
    console.log(gd);


    $('#shippingAgentName').val('');
    $('#tariffType').val('');


    containerCallback();
        //LoadContainerIndexTariffGrid();


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
    && t.tariffHeadName == tariff.tariffHeadName && t.tariffId != tariff.tariffId);

    if (duplicateTariff)
    return false;
    else
    return true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "tariffHeadName");
    checkColumn(e, "tariffHeadDescription");




    }

    function containerCallback() {

        console.log("gd", gd);

    if (gd) {

        $.get('/APICalls/GetShippingAgentBygd?gdnumber=' + gd, function (data) {

            $('#shippingAgentName').val(data);
        });

    $.get('/APICalls/GetTariffTypeBygd?gdnumber=' + gd, function (data) {

        console.log("data", data)
                if (data) {
        console.log("if")
                    $('#tariffType').select2().val(data).trigger('change.select2');
                } else {
        console.log("else")
                    $('#tariffType').select2().val('').trigger('change.select2');
                }


            });


    LoadContainerIndexTariffGrid();
        }


    }



    function addTariff() {

        console.log("gdnumber ", gd)


        if (gd) {


            var model = $('#tariffCharegsForm').serialize();

    console.log("tariff", model);

    var tariffheadid = $('#tariffHead').val();

    if (tariffheadid) {



        $.post('/Export/TariffExport/SaveTariffConditionForGDtariff?' + model + '&gdnumber=' + gd, function (data) {

            LoadContainerIndexTariffGrid();

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
        showToast("please select tariff head", "error");
            }


        } else {
        showToast("please select gd", "error");
        }

    }



    function LoadContainerIndexTariffGrid() {


        $.get('/APICAlls/GetGDTariffList?gdnumber=' + gd, function (data) {
            containerIndexTariffs = data;

            console.log("containerIndexTariffs", containerIndexTariffs)

            var dataGrid = $("#indexTariffGrid").dxDataGrid({
                dataSource: containerIndexTariffs,
                keyExpr: "tariffExportId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    enabled: true,
                    pageSize: 10
                },
                editing: {
                    allowUpdating: false,
                    allowDeleting: function (e) {


                        if (e.row.data.type == "Agent Tariff") {
                            return false;
                        }
                        else {
                            return true;
                        }
                        //return !e.row.data.invoiceCreated;
                    },
                    //   allowDeleting: true
                },
                columns: [
                    "type",
                    {
                        dataField: "tariffHeadName",
                        caption: "Name",
                        formItem: {
                            visible: false
                        }
                    },
                    {
                        dataField: "tariffHeadDescription",
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
                    "cbmMultiplyValue",
                    "tariffType",
                    {
                        caption: "Implement From",
                        dataField: "implementFrom",
                        dataType: "date"
                    },
                    //{
                    //    dataField: "agentTariffId",
                    //    caption: "",
                    //    cellTemplate: function (container, options) {

                    //        console.log(options.data.type == "Agent Tariff");

                    //        console.log(options.data.invoiceCreated == false);

                    //        if (options.data.type == "Agent Tariff" && options.data.invoiceCreated == false) {

                    //            $("<button class='btn btn-danger' onClick='RemoveAgentTariff(" + options.value + ")'>Remove</button>")
                    //                .appendTo(container);
                    //        }

                    //    }
                    //}
                    {
                        dataField: "agentTariffId",
                        caption: "",
                        cellTemplate: function (container, options) {


                            if (options.data.type == "Agent Tariff") {

                                $("<button class='btn btn-danger' onClick='RemoveAgentTariff(" + options.value + " , " + options.data.tariffExportId + ")'>Remove</button>")
                                    .appendTo(container);
                            }

                        }
                    }

                ], summary: {
                    totalItems: [
                        {
                            column: "rate20",
                            summaryType: "sum",
                            diplayFormat: "{0}"
                        },
                        {
                            column: "rate40",
                            summaryType: "sum",
                            diplayFormat: "{0}"
                        },
                        {
                            column: "rate45",
                            summaryType: "sum",
                            diplayFormat: "{0}"
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
                },
                onRowRemoved: function (e) {

                    console.log("gdnumber ", gd)

                    if (gd) {

                        $.post('/Export/TariffExport/RemoveGDTariff?TariffId=' + e.data.tariffExportId + '&gdnumber=' + gd, function (data) {

                            if (data.error == true) {
                                showToast(data.message, "error");

                            }
                            else {

                                showToast(data.message, "success");
                            }
                        });
                    }
                    else {
                        showToast("please select gd number", "error");
                    }



                }

            }).dxDataGrid("instance");


        });
    }


    function formfiled() {

    }


    function RemoveAgentTariff(value, tariffExportId) {

        console.log("gdnumber ", gd);
    console.log("value", value);
    console.log("value2", tariffExportId);


    if (gd) {

            if (value != null || tariffExportId) {
        console.log("ok");
    var agentTariffId = value;

    $.post('/Export/TariffExport/SaveDisabledAgentTariffExport?gdnumber=' + gd + "&agentTariffId=" + agentTariffId + "&tariffexportid=" + tariffExportId, function (data) {

        console.log("data", data);

    if (data.error == false) {

        showToast(data.message, "success");

                    }
    else {

        showToast(data.message, "error");
                    }


                });

            }
    else {
        showToast("Only Remove Agent Tariff", "error");

            }
        }

    else {
        showToast("please select gd number", "error");

        }

    }


    function refershIndextariff() {

        console.log("gdnumber ", gd)

        if (gd) {

        $.post('/Export/TariffExport/RefershAgentTariff?gdnumber=' + gd, function (data) {

            console.log("data", data)

            if (data.error == false) {

                showToast(data.message, "success");

            }
            else {

                showToast(data.message, "error");
            }

        });
        }
    }


    //function RemoveAgentTariff(value) {

        //    console.log("value", value);
        //    if (value != null) {
        //        console.log("ok");
        //        var agentTariffId = value;

        //        if (type == "CFS") {
        //            $.post('/Tariff/SaveDisabledAgentTariff?containerIndexId=' + containerIndexId + "&agentTariffId=" + agentTariffId, function (data) {

        //                console.log("data", data)

        //                if (data.error == false) {

        //                    showToast(data.message, "success");

        //                    //window.location.href = window.location.href;


        //                }
        //                else {

        //                    showToast(data.message, "error");

        //                    window.location.href = window.location.href;

        //                }

        //            });
        //        }
        //        if (type == "CY") {
        //            $.post('/Tariff/SaveDisabledAgentTariffCY?igm=' + igm + '&indexNo=' + $("#containerIndexCYdb option:selected").val() + "&agentTariffId=" + agentTariffId, function (data) {

        //                console.log("data", data);

        //                if (data.error == false) {

        //                    showToast(data.message, "success");

        //                }
        //                else {

        //                    showToast(data.message, "error");
        //                }


        //            });
        //        }
        //    }
        //    else {
        //        showToast("Only Remove Agent Tariff", "error");

        //    }


        //}



        //function refershIndextariff() {
        //    if (type == "CFS") {
        //        console.log("containerIndexId", containerIndexId)
        //        if (containerIndexId) {

        //            console.log("ok")

        //            $.post('/Tariff/IndexRemoveDiabledTariff?containerIndexId=' + containerIndexId, function (data) {

        //                console.log("data", data)

        //                if (data.error == false) {

        //                    timer();

        //                    showToast(data.message, "success");


        //                }
        //                else {

        //                    showToast(data.message, "error");
        //                }

        //            });
        //        }
        //        else {
        //            showToast("Please Select Index Or Container", "error");
        //        }

        //    }
        //    if (type == "CY") {
        //        $.post('/Tariff/IndexRemoveDiabledTariffCY?igm=' + igm + '&indexNo=' + $("#containerIndexCYdb option:selected").val(), function (data) {

        //            console.log("data", data)

        //            if (data.error == false) {

        //                timer();

        //                showToast(data.message, "success");

        //            }
        //            else {

        //                showToast(data.message, "error");
        //            }

        //        });
        //    }
        //}

        function timer() {
            setTimeout(pageRelod, 3000);

        }

    function pageRelod() {
        window.location.href = window.location.href;
    }

    function updatetarifftypeforcontainer() {


        console.log("gdnumber ", gd)

        if (gd) {


            var tarftype = $("#tariffType option:selected").val();

    console.log("tarftype", tarftype)

    if (tarftype) {

        $.post('/Export/TariffExport/SaveTariffTariffTypeForLP?tarftype=' + tarftype + '&gdnumber=' + gd, function (data) {


            if (data.error == true) {
                showToast(data.message, "error");
                LoadContainerIndexTariffGrid();
            }
            else {

                showToast(data.message, "success");
                LoadContainerIndexTariffGrid();
            }

        });
            }
    else {
        showToast("please select tariff type", "error")
    }





        } else {
        showToast("please select index", "error");
        }





    }
