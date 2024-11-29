

    var shippingAgentId = 0;

    var tariffs = [];

    var AssignedTariffs = [];

    var portandterminalslist = [];

    var tariffId = 0;
    var StorageSlabs = [];


    var BillDateType = [
    {Name: "GateIn" },
    {Name: "ETD" }
    ];


    var BillDateTypeFCL = [
    {Name: "GateIn" },
    {Name: "ETD" }
    ];

    var ListTypeOfImplementation = [
    {Name: "All" },
    {Name: "Arrival" },
    {Name: "Delivery" }
    ];

    var MultiplyByWeight = [
    {Name: "Yes" },
    {Name: "No" }
    ];

    var VehcileAmountAllow = [
    {Name: "Yes" },
    {Name: "No" }
    ];
    $(function () {

        getconsigneelist();

    setTimeout(function () {
        loadmethods();
        }, 7000);



    });

    function loadmethods() {

        console.log("test")

        LoadAgentTariffGrid();
    getshippingagnetdetail();
    $('#storageDays').val('');
    $('#dgfreeDays').val('');
    resetAllValues();

    getportandterminal();
    }



    var consigneeList = [];
    function getconsigneelist() {
        $.get('/Export/TariffExport/GetALlConsignees', function (res) {
            if (res) {
                consigneeList = res;
                $("#searchBoxForConsigne").dxSelectBox({
                    dataSource: {
                        store: consigneeList,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ExportConsigneeId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                    onValueChanged: function (e) {
                        console.log("e", e);

                        LoadAgentTariffGrid();
                        getsharerates();
                    }
                })
            }
            else {
                consigneeList = []
                $("#searchBoxForConsigne").dxSelectBox({
                    dataSource: {
                        store: consigneeList,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ExportConsigneeId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,

                    onValueChanged: function (e) {
                        console.log("e", e);
                    }

                })
            }
        });
    }



    function getportandterminal() {
        $.get('/Export/TariffExport/GetAllPortAndTerminals', function (res) {
            portandterminalslist = res;
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




    function LoadAgentTariffGrid() {

        var agentdropdownid = $('#agentdropdown').val();
    var consigneedropdownid = $("#searchBoxForConsigne").dxSelectBox('instance').option("value");
    var clearingAgentdropdownid = $('#clearingAgentdropdown').val();
    var gooddropdownid = $('#gooddropdown').val();
    var shipperdropdownid = $('#shipperdropdown').val();
    var indexCargoTypeid = $('#indexCargoType').val();
    var portAndTerminalId = $('#portAndTerminalId').val();

    console.log("agentdropdownid", agentdropdownid)
    console.log("consigneedropdownid", consigneedropdownid)
    console.log("portAndTerminalId ", portAndTerminalId)
    console.log("clearingAgentdropdownid", clearingAgentdropdownid)
    console.log("gooddropdownid", gooddropdownid)
    console.log("shipperdropdownid", shipperdropdownid)
    console.log("indexCargoTypeid", indexCargoTypeid)

    $.get('/Export/TariffExport/GetTariffsByPerametersId?ShippingAgentId=' + agentdropdownid + '&ConsigneId=' + consigneedropdownid + '&ClearingAgentId=' + clearingAgentdropdownid
    + '&GoodId=' + gooddropdownid + '&ShipperId=' + shipperdropdownid  + '&portAndTerminalId=' + portAndTerminalId  + '&indexCargoType=' + indexCargoTypeid, function (data) {

        AssignedTariffs = data;

    console.log("AssignedTariffs", AssignedTariffs)


    if (AssignedTariffs.length) {

                    if (AssignedTariffs[0].isEnableDisabled == false) {
        $('#contractStatus').val("Enabled");
                    }
    if (AssignedTariffs[0].isEnableDisabled == true) {
        $('#contractStatus').val("Disabled");
                    }



    $('#tariffCode').val(AssignedTariffs[0].tariffCode);
    $('#storageDays').val(AssignedTariffs[0].storageFreeDays);
    $('#dgfreeDays').val(AssignedTariffs[0].dgFreeDays);

                    var storageres = AssignedTariffs.find(x => x.name.includes("STORAGE"));

    if (storageres) {
        console.log("storageres", storageres);

    $('#tariff').val(storageres.exportTariffId).trigger('change.select2');

    getstorageDetailbytariffId(storageres.exportTariffId)

                    }
    else {
        $('#tariff').select2().val('').trigger('change.select2');
    getstorageDetailbytariffId(0)
                    }


                }
    else {
        $('#contractStatus').val('');
    $('#tariffCode').val('');
    $('#storageDays').val('');
    $('#dgfreeDays').val('');
    $('#tariff').select2().val('').trigger('change.select2');
    getstorageDetailbytariffId(0);
                }


    var grid = $("#agentTariffGrid").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    var dataGrid = $("#agentTariffGrid").dxDataGrid({
        dataSource: AssignedTariffs,
    keyExpr: "exportTariffId",
    wordWrapEnabled: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: true
                    },
    editing: {
        mode: "row",
    allowDeleting: true,
    allowUpdating: true,
                    },
                    //filterRow: {
        //    visible: true,
        //    applyFilter: "auto"
        //},


        columns: [
    {
        dataField: "name",
    caption: "Name",
    allowEditing: false,
    formItem: {
        visible: false
                            }
                        },
    {
        dataField: "description",
    caption: "Description",
    allowEditing: false,
    formItem: {
        visible: false
                            }
                        },
    {
        dataField: "typeOfTariff",
    caption: "Type Of Tariff",
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

    {
        dataField: "portAndTerminalExportId",
    caption: "port",
    width:"300",
    lookup: {
        dataSource: portandterminalslist,
    valueExpr: "value",
    displayExpr: "text"
                            }
                        },
    {
        dataField: "typeOfImplementation",
    caption: "Type Of Imp",
    width: "300",
    lookup: {
        dataSource: ListTypeOfImplementation,
    valueExpr: "Name",
    displayExpr: "Name"
                            }
                        },
    {
        caption: "Implement From",
    dataField: "implementFrom",
    dataType: "date",
    format: 'dd/MM/yyyy',
                        },
    {
        caption: "Implement To",
    dataField: "implementTo",
    dataType: "date",
    format: 'dd/MM/yyyy',
                        },
    {
        caption: "Is General",
    dataField: "isGeneral",
    dataType: "boolean"
                        },
    {
        caption: "Is Fixed",
    dataField: "isFixedRate",
    dataType: "boolean"
                        },
    {
        caption: "Is Doller",
    dataField: "isDollerRate",
    dataType: "boolean"
                        },

    ],
    summary: {
        totalItems: [
    {
        column: "rate20",
    summaryType: "sum",
    customizeText(data) {
                                    return `  ${data.value.toLocaleString()}`;
                                },
                            },
    {
        column: "rate40",
    summaryType: "sum",
    customizeText(data) {
                                    return `  ${data.value.toLocaleString()}`;
                                },
                            },
    {
        column: "rate45",
    summaryType: "sum",
    customizeText(data) {
                                    return `  ${data.value.toLocaleString()}`;
                                },
                            },
    {
        column: "cbmRate",
    summaryType: "sum",
    customizeText(data) {
                                    return `  ${data.value.toLocaleString()}`;
                                },
                            },
    {
        column: "weightRate",
    summaryType: "sum",
    customizeText(data) {
                                    return `  ${data.value.toLocaleString()}`;
                                },
                            },
    {
        column: "perIndexRate",
    summaryType: "sum",
    customizeText(data) {
                                    return `  ${data.value.toLocaleString()}`;
                                },
                            },
    {
        column: "devededIndexAmount",
    summaryType: "sum",
    customizeText(data) {
                                    return `  ${data.value.toLocaleString()}`;
                                },
                            }]
                    },


    onRowUpdated: function (e) {
        console.log(e);
    var tariff = e.data;
    console.log(tariff)

    console.log("tariff", tariff)


    if (tariff.typeOfImplementation != "All" && tariff.implementFrom == null) {
        showToast("please select from date", "error");
    LoadAgentTariffGrid();
                            }

    else if (tariff.typeOfImplementation == "All" && tariff.implementFrom != null) {
        showToast("please  Time Line Type", "error");
    LoadAgentTariffGrid();
                            }
    else if (tariff.typeOfImplementation == "All" && tariff.implementTo != null) {
        showToast("please  Time Line Type", "error");
    LoadAgentTariffGrid();
                            }
    else {
        $.post('/Export/TariffExport/updateAgentTariff', { tariff: tariff }, function (data) {

            if (data.error == true) {
                alert(data.message);

            }
            else {

                alert(data.message);

            }
        });
                            }



                        
                    },
    onRowRemoved: function (e) {

        console.log("e", e)


                        $.post('/Export/TariffExport/DeleteSaveTariffCondition?TariffId=' + e.data.tariffInofrmationTariffExportId, function (data) {
                            if (data.error == true) {
        alert(data.message);

                            }
    else {

        alert(data.message);


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


    function getsharerates() {


        var agentdropdownid = $('#agentdropdown').val();

    var gooddropdownid = $('#gooddropdown').val();
    var indexCargoTypeid = $('#indexCargoType').val();

    console.log("agentdropdownid", agentdropdownid)
    console.log("gooddropdownid", gooddropdownid)
    console.log("indexCargoTypeid", indexCargoTypeid)

    $.get('/Export/TariffExport/GetShareRateByPerametersId?ShippingAgentId=' + agentdropdownid + '&GoodId=' + gooddropdownid + '&indexCargoType=' + indexCargoTypeid, function (data) {

        console.log("data", data)
            if (data) {

        $('#aictrate20').val(data.aictRate20);
    $('#aictrate40').val(data.aictRate40);
    $('#aictrate45').val(data.aictRate45);
    $('#aictcbmRate').val(data.aictcbmRate);
    $('#aictPerIndexRate').val(data.aictPerIndexRate);

    $('#ffRate20').val(data.ffRate20);
    $('#ffRate40').val(data.ffRate40);
    $('#ffRate45').val(data.ffRate45);
    $('#ffCBMRate').val(data.ffcbmRate);
    $('#ffPerIndexRate').val(data.ffPerIndexRate);

    $('#totalAICTRate20').val(data.totalAICTRate20);
    $('#totalAICTRate40').val(data.totalAICTRate40);
    $('#totalAICTRate45').val(data.totalAICTRate45);
    $('#totalAICTCBMRate').val(data.totalAICTCBMRate);
    $('#totalAICTPerIndexRate').val(data.totalAICTPerIndexRate);


            } else {
        $('#aictrate20').val(0);
    $('#aictrate40').val(0);
    $('#aictrate45').val(0);
    $('#aictcbmRate').val(0);
    $('#aictPerIndexRate').val(0);

    $('#ffRate20').val(0);
    $('#ffRate40').val(0);
    $('#ffRate45').val(0);
    $('#ffCBMRate').val(0);
    $('#ffPerIndexRate').val(0);


    $('#totalAICTRate20').val(0);
    $('#totalAICTRate40').val(0);
    $('#totalAICTRate45').val(0);
    $('#totalAICTCBMRate').val(0);
    $('#totalAICTPerIndexRate').val(0);

            }

        });
    }



    function formfiled() {

        $('#agentdropdown').on('change', function (data) {
            LoadAgentTariffGrid();
            getsharerates();

            getshippingagnetdetail();
        });


    $('#clearingAgentdropdown').on('change', function (data) {
        LoadAgentTariffGrid();
    getsharerates();
        });

    $('#gooddropdown').on('change', function (data) {
        LoadAgentTariffGrid();
    getsharerates();
        });

    $('#shipperdropdown').on('change', function (data) {
        LoadAgentTariffGrid();
    getsharerates();
        });

    $('#indexCargoType').on('change', function (data) {
        LoadAgentTariffGrid();
    getsharerates();
        });

    $('#portAndTerminalId').on('change', function (data) {
        LoadAgentTariffGrid();
    getsharerates();
        });


    $('#tariff').on('change', function (e) {

        tariffId = this.value;

    getstorageDetailbytariffId(tariffId)
         });

    }


    function savedays() {


        var tariffid = $('#tariffCode').val();
    var storageDays = $('#storageDays').val();
    var dgfreeDays = $('#dgfreeDays').val();

    console.log("tariffid", tariffid)

    if (tariffid) {
        console.log("storageDays", storageDays)
            $.post('/Export/TariffExport/AddTariffStorageDays?tariffid=' + tariffid + '&storageDays=' + storageDays + '&dgfreeDays=' + dgfreeDays, function (data) {


                if (data.error == true) {
        alert(data.message);
                } else {
        alert(data.message);
                }


            });
        }
    else {
        alert("please select tariff code");
        }
         
    }


    function addTariff() {

        var model = $('#tariffCharegsForm').serialize();

    console.log("tariff", model);

    var tariffheadid = $('#tariffHead').val();

    var typeOfImplementation = $('#typeOfImplementation').val();

    var fromdate = document.getElementById("implementFrom").value;
    var todate = document.getElementById("implementto").value;

    console.log("typeOfImplementation", typeOfImplementation);
    console.log("fromdate", fromdate);
    console.log("todate", todate);



    if (typeOfImplementation != "All" && fromdate == "") {
        showToast("please  select from date", "error");
        }

    else if (fromdate != "" && typeOfImplementation == "All") {
        showToast("please   Time Line Type", "error");
        }


    else if (todate != "" && typeOfImplementation == "All") {
        showToast("please   Time Line Type", "error");
        }

    else {
            if (tariffheadid) {

                var percentAgeShippingAgentId = $('#percentAgeShippingAgentId').val();

    console.log("percentAgeShippingAgentId", percentAgeShippingAgentId)

    var GeneralTariff = $("#isGeneral").val();

    console.log("GeneralTariff", GeneralTariff);

    var percentAgeShippingAgentId = $('#percentAgeShippingAgentId').val();

    console.log("percentAgeShippingAgentId", percentAgeShippingAgentId)


    if (GeneralTariff == "true") {



                    if (percentAgeShippingAgentId) {

                        var agentdropdownid = $('#agentdropdown').val();
    var consigneedropdownid = $("#searchBoxForConsigne").dxSelectBox('instance').option("value");;
    var clearingAgentdropdownid = $('#clearingAgentdropdown').val();
    var gooddropdownid = $('#gooddropdown').val();
    var shipperdropdownid = $('#shipperdropdown').val();
    var indexCargoTypeid = $('#indexCargoType').val();
    var portAndTerminalId = $('#portAndTerminalId').val();

    console.log("agentdropdownid", agentdropdownid)
    console.log("consigneedropdownid", consigneedropdownid)
    console.log("clearingAgentdropdownid", clearingAgentdropdownid)
    console.log("gooddropdownid", gooddropdownid)
    console.log("shipperdropdownid", shipperdropdownid)
    console.log("indexCargoTypeid", indexCargoTypeid)
    console.log("percentAgeShippingAgentId", percentAgeShippingAgentId)
    console.log("portAndTerminalId", portAndTerminalId)

    if (indexCargoTypeid) {


                            if (indexCargoTypeid == "LCL") {
                                if (consigneedropdownid || clearingAgentdropdownid || shipperdropdownid  || portAndTerminalId) {
                                    return alert("for lcl you can only select FF or Good")
                                }

    if (!agentdropdownid && !gooddropdownid) {
                                    return alert("for lcl you have to select one option FF or Good")
                                }
                            }

    if (agentdropdownid || consigneedropdownid || clearingAgentdropdownid || gooddropdownid || shipperdropdownid  || portAndTerminalId) {

        console.log("ok")

                                var tariff = {
        shippingAgentExportId: agentdropdownid,
    exportConsigneeId: consigneedropdownid,
    clearingAgentExportId: clearingAgentdropdownid,
    goodsHeadExportId: gooddropdownid,
    shippingLineExportId: shipperdropdownid,
    indexCargoType: indexCargoTypeid,
    portAndTerminalExportId: portAndTerminalId,
    percentAgeShippingAgentExportId: percentAgeShippingAgentId,
                                };

    $.post('/Export/TariffExport/SaveTariffCondition?' + model, {tariff: tariff }, function (data) {
        LoadAgentTariffGrid();

    if (data.error == true) {
        alert(data.message);

                                    }
    else {

        alert(data.message);

                                    }

                                });

                            }
    else {
        alert("please Select at least one option");
                            }

                        }
    else {
        alert("must select cargo type");
                        }




                    } else {

        alert("please select percent agent tariff");
                    }

                }
    if (GeneralTariff == "false") {



                    if (percentAgeShippingAgentId) {
        alert("please un select percent Agent tariff");
                    } else {

                        var agentdropdownid = $('#agentdropdown').val();
    var consigneedropdownid = $("#searchBoxForConsigne").dxSelectBox('instance').option("value");
    var clearingAgentdropdownid = $('#clearingAgentdropdown').val();
    var gooddropdownid = $('#gooddropdown').val();
    var shipperdropdownid = $('#shipperdropdown').val();
    var indexCargoTypeid = $('#indexCargoType').val();
    var percentAgeShippingAgentId = $('#percentAgeShippingAgentId').val();
    var portAndTerminalId = $('#portAndTerminalId').val();

    console.log("agentdropdownid", agentdropdownid)
    console.log("consigneedropdownid", consigneedropdownid)
    console.log("clearingAgentdropdownid", clearingAgentdropdownid)
    console.log("gooddropdownid", gooddropdownid)
    console.log("shipperdropdownid", shipperdropdownid)
    console.log("indexCargoTypeid", indexCargoTypeid)
    console.log("percentAgeShippingAgentId", percentAgeShippingAgentId)
    console.log("portAndTerminalId", portAndTerminalId)

    if (indexCargoTypeid) {

                            if (indexCargoTypeid == "LCL") {
                                if (consigneedropdownid || clearingAgentdropdownid || shipperdropdownid   || portAndTerminalId) {
                                    return alert("for lcl you can only select FF or Good")
                                }

    if (!agentdropdownid && !gooddropdownid) {
                                    return alert("for lcl you have to select one option FF or Good")
                                }
                            }

    if (agentdropdownid || consigneedropdownid || clearingAgentdropdownid || gooddropdownid || shipperdropdownid  || portAndTerminalId) {

        console.log("ok model", model)

                                var tariff = {
        shippingAgentExportId: agentdropdownid,
    exportConsigneeId: consigneedropdownid,
    clearingAgentExportId: clearingAgentdropdownid,
    goodsHeadExportId: gooddropdownid,
    shippingLineExportId: shipperdropdownid,
    portAndTerminalExportId: portAndTerminalId,
    indexCargoType: indexCargoTypeid,
    percentAgeShippingAgentExportId: percentAgeShippingAgentId,
                                };



    $.post('/Export/TariffExport/SaveTariffCondition?' + model, {tariff: tariff }, function (data) {

        LoadAgentTariffGrid();

    if (data.error == true) {
        alert(data.message);

                                    }
    else {

        alert(data.message);


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
        alert("please Select at least one option");
                            }
                        } else {
        alert("must select cargo type");
                        }



                    }




                }

            }

    else {
        alert("please select tariff head");
            }

        }



    }

    function saveshareRate() {


        var agentdropdownid = $('#agentdropdown').val();
    var gooddropdownid = $('#gooddropdown').val();

    var indexCargoTypeid = $('#indexCargoType').val();

    //share rate aict
    var aictrate20 = $('#aictrate20').val();
    var aictrate40 = $('#aictrate40').val();
    var aictrate45 = $('#aictrate45').val();
    var aictcbmRate = $('#aictcbmRate').val();
    var aictPerIndexRate = $('#aictPerIndexRate').val();

    //share rate ff
    var ffRate20 = $('#ffRate20').val();
    var ffRate40 = $('#ffRate40').val();
    var ffRate45 = $('#ffRate45').val();
    var ffCBMRate = $('#ffCBMRate').val();
    var ffPerIndexRate = $('#ffPerIndexRate').val();



    console.log("aictrate20", aictrate20)
    console.log("aictrate40", aictrate40)
    console.log("aictrate45", aictrate45)
    console.log("aictcbmRate", aictcbmRate)
    console.log("aictPerIndexRate", aictPerIndexRate)
    console.log("ffRate20", ffRate20)
    console.log("ffRate40", ffRate40)
    console.log("ffRate45", ffRate45)
    console.log("ffCBMRate", ffCBMRate)
    console.log("ffPerIndexRate", ffPerIndexRate)

    console.log("agentdropdownid", agentdropdownid)
    console.log("gooddropdownid", gooddropdownid)
    console.log("indexCargoTypeid", indexCargoTypeid)

    if (indexCargoTypeid) {
            if (agentdropdownid || gooddropdownid) {


                var tariff = {
        shippingAgentExportId: agentdropdownid,
    goodsHeadExportId: gooddropdownid,
    indexCargoType: indexCargoTypeid,
    aictRate20: aictrate20,
    aictRate40: aictrate40,
    aictRate45: aictrate45,
    aictcbmRate: aictcbmRate,
    aictPerIndexRate: aictPerIndexRate,
    ffRate20: ffRate20,
    ffRate40: ffRate40,
    ffRate45: ffRate45,
    ffcbmRate: ffCBMRate,
    ffPerIndexRate: ffPerIndexRate,


                };

    console.log("tariff", tariff)

    $.post('/Export/TariffExport/SaveShareCondition', {tariff: tariff }, function (data) {

        getsharerates();

    if (data.error == true) {
        alert(data.message);

                    }
    else {

        alert(data.message);

                    }

                });

            }
    else {
        alert("please Select at least one option FF Or Good");

            }

        }
    else {
        alert("must select cargo type");

        }

    }

    function resetAllValues() {

        $('#aictrate20').val(0);
    $('#aictrate40').val(0);
    $('#aictrate45').val(0);
    $('#aictcbmRate').val(0);
    $('#aictPerIndexRate').val(0);

    $('#ffRate20').val(0);
    $('#ffRate40').val(0);
    $('#ffRate45').val(0);
    $('#ffCBMRate').val(0);
    $('#ffPerIndexRate').val(0);


    $('#totalAICTRate20').val(0);
    $('#totalAICTRate40').val(0);
    $('#totalAICTRate45').val(0);
    $('#totalAICTCBMRate').val(0);
    $('#totalAICTPerIndexRate').val(0);
    }



    function loadStorageSlabGrid(StorageSlabs) {

        console.log("StorageSlabs", StorageSlabs)

        var grid = $("#gridContainerstorage").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#gridContainerstorage").dxDataGrid({
        dataSource: StorageSlabs,
    keyExpr: "storageSlabExportId",
    wordWrapEnabled: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    allowMouseWheel: false,
    paging: {
        enabled: false
            },
    editing: {
        mode: "row",
    allowUpdating: true,
    allowDeleting: true,
    allowAdding: true,
            },
    filterRow: {
        visible: true,
    applyFilter: "auto"
            },
    columns: [


    {
        dataField: "aictRate",
    caption: "AICT Rate",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },

    {
        dataField: "ffRate",
    caption: "FF Rate",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },


    {
        dataField: "rate",
    caption: "Rate",
    format: "#,##0.##",
    allowEditing: false,


                },


    //////////////////20 40 45//////////////////


    // AICT


    {
        dataField: "aictRate20",
    caption: "AICT Rate20",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "aictRate40",
    caption: "AICT Rate40",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "aictRate45",
    caption: "AICT Rate45",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },

    // ff

    {
        dataField: "ffRate20",
    caption: "FF Rate20",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "ffRate40",
    caption: "FF Rate40",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "ffRate45",
    caption: "FF Rate45",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },

    // normal

    {
        dataField: "rate20",
    caption: "Rate20",
    dataType: "number",
    format: "#,##0.##",
    allowEditing: false,

                },
    {
        dataField: "rate40",
    caption: "Rate40",
    dataType: "number",
    format: "#,##0.##",
    allowEditing: false,

                },
    {
        dataField: "rate45",
    caption: "Rate45",
    dataType: "number",
    format: "#,##0.##",
    allowEditing: false,

                },
    {
        dataField: "description",
    caption: "Description",
    validationRules: [{type: "required" }],
                },
    {
        dataField: "noOfDays",
    caption: "Number Of Days",
    validationRules: [{type: "required" }],
                },


    ],

    onRowUpdated: function (e) {
        console.log(e);
    var res = e.data;

    $.post('/Export/TariffExport/updateStorageSlab', {res: res }, function (data) {

                    if (data.error == true) {
        alert(data.message);

                    }
    else {

        alert(data.message); 
                    }
                });

            },
    onRowRemoved: function (e) {

        console.log("e", e)

                $.post('/Export/TariffExport/DeleteupdateStorageSlab?StorageSlabId=' + e.data.storageSlabExportId, function (data) {
                    if (data.error == true) {
        //showToast(data.message, "error");
        alert(data.message);

                    }
    else {

        //showToast(data.message, "success");
        alert(data.message);
    getstorageDetailbytariffId(e.data.exportTariffId);

                    }
                });
            },
    onRowInserted: function (e) {
        console.log("ee", e);
    addStorageSlab(e);
            }
        }).dxDataGrid("instance");

    console.log("grid", grid)

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

    }




    function addStorageSlab(e) {

        var data = e.data;

    var tariffresid = $('#tariff').val();
    if (tariffresid) {

        console.log("data", data)
            $.post('/Export/TariffExport/AddStorageSlab?tariffid=' + tariffresid, {slab: data }, function (data) {


                if (data.error == true) {
        //showToast(data.message, "error");
        alert(data.message);
                } else {
        //showToast(data.message, "success");
        alert(data.message);
                    //getstorageDetailbytariffId(tariffId)
                }


            });

        }
    else {
        //showToast("please select tariff", "error");
        alert("please select tariff");
        }
         
    }


    function getstorageDetailbytariffId(tariffId) {


        $.get('/Export/TariffExport/GetStorageSlabsByTariff?TariffId=' + tariffId, function (data) {

            if (data) {
                StorageSlabs = data;
            }
            else {
                StorageSlabs = [];
            }

            loadStorageSlabGrid(StorageSlabs);
        })

    }



    function getshippingagnetdetail() {

        var aggentid = 0;
    var agentdropdownid = $('#agentdropdown').val();
    console.log("agentdropdownid", agentdropdownid)


    if (agentdropdownid) {
        aggentid = agentdropdownid
    }
    console.log("aggentid", aggentid)

    $.get('/Export/TariffExport/GetShippingAgentById?shippingagentid=' + agentdropdownid, function (data) {


        console.log("data", data);

    var grid = $("#shippingAgentGrid").dxDataGrid(this.gridSettings).dxDataGrid('instance');


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
    allowEditing: false,
    caption: "Name"
                    },
    {
        dataField: "category",
    caption: "Category"
                    },
    {
        dataField: "ntnNumber",
    validationRules: [{type: "required" }],
    caption: "NTN"

                    },
    {
        dataField: "billDateType",
    caption: "Storage applicable LCL",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: BillDateType,
    valueExpr: "Name",
    displayExpr: "Name"
                        }
                    },
    {
        dataField: "billDateTypeFCL",
    caption: "Storage applicable FCL",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: BillDateTypeFCL,
    valueExpr: "Name",
    displayExpr: "Name"
                        }
                    },

    {
        dataField: "weightAllow",
    caption: "Multiply By Weight",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: MultiplyByWeight,
    valueExpr: "Name",
    displayExpr: "Name"
                        }
                    },
    {
        dataField: "vehcileAmountAllow",
    caption: "Allow Vechile Amount",
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: VehcileAmountAllow,
    valueExpr: "Name",
    displayExpr: "Name"
                        }
                    },
    {
        dataField: "weightAmount",
    validationRules: [{type: "required" }],
    caption: "Weight Amount",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                        }
                    },


    ],



    onRowUpdated: function (e) {
        console.log(e);
    var values = e.data;
    console.log(values)


    $.post('/Export/ShippingAgentExport/UpdateShippingAgentExport', {values: values }, function (data) {

                        if (data.error == true) {
        //showToast(data.message, "error");
        alert(data.message);

                        }
    else {

        //showToast(data.message, "success");
        alert(data.message);

                        }
                    });

                },


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

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });
    }

    function findrecordbycode() {

        var tariffCode = $('#tariffCode').val();

    console.log(tariffCode);

    if (tariffCode) {

        $.get('/Export/TariffExport/findrecordbycode?tariffCode=' + tariffCode, function (res) {

            console.log("res", res);

            if (res) {

                $('#agentdropdown').val(res.shippingAgentExportId).trigger('change.select2');
                //$('#consigneedropdown').val(res.consigneId).trigger('change.select2');
                if (res.exportConsigneeId) {

                    var ab = $("#searchBoxForConsigne").dxSelectBox('instance').option().dataSource.store.find(x => x.value == res.exportConsigneeId)

                    console.log("AB", ab);
                    $("#searchBoxForConsigne").dxSelectBox('instance').option("value", ab.value);
                    $("#searchBoxForConsigne").dxSelectBox('instance').option("text", ab.text);


                } else {
                    $("#searchBoxForConsigne").dxSelectBox('instance').option("value", "");
                    $("#searchBoxForConsigne").dxSelectBox('instance').option("text", "");
                }

                if (res.isEnableDisabled == false) {
                    $('#contractStatus').val("Enabled");
                }
                if (res.isEnableDisabled == true) {
                    $('#contractStatus').val("Disabled");
                }
                $('#storageDays').val(res.storageFreeDays);
                $('#dgfreeDays').val(res.dgFreeDays);

                $('#clearingAgentdropdown').val(res.clearingAgentExportId).trigger('change.select2');
                $('#gooddropdown').val(res.goodsHeadExportId).trigger('change.select2');
                $('#shipperdropdown').val(res.shippingLineExportId).trigger('change.select2');
                $('#indexCargoType').val(res.indexCargoType).trigger('change.select2');
                $('#portAndTerminalId').val(res.portAndTerminalExportId).trigger('change.select2');
                $('#percentAgeShippingAgentId').val(res.percentAgeShippingAgentExportId).trigger('change.select2');

                LoadAgentTariffGrid();
                getsharerates();
                getshippingagnetdetail();

            }

            else {
                $('#agentdropdown').select2().val('').trigger('change.select2');
                $('#contractStatus').val('');
                $('#storageDays').val('');
                $('#dgfreeDays').val('');
                //$('#consigneedropdown').select2().val('').trigger('change.select2');

                $("#searchBoxForConsigne").dxSelectBox('instance').option("value", "");
                $("#searchBoxForConsigne").dxSelectBox('instance').option("text", "");

                $('#clearingAgentdropdown').select2().val('').trigger('change.select2');
                $('#gooddropdown').select2().val('').trigger('change.select2');
                $('#shipperdropdown').select2().val('').trigger('change.select2');
                $('#indexCargoType').select2().val('').trigger('change.select2');
                $('#portAndTerminalId').select2().val('').trigger('change.select2');
                $('#percentAgeShippingAgentId').select2().val('').trigger('change.select2');

                LoadAgentTariffGrid();
                getsharerates();
                getshippingagnetdetail();


            }


        })
    }
    else {
        //showToast("please give tariff code ", "error");
        alert("please give tariff code ");
        }


    }

    function deleterecordbycode() {
        if (confirm("Press a button!") == true) {


        var tariffCode = $('#tariffCode').val();

    console.log(tariffCode);

    if (tariffCode) {

        $.post('/Export/TariffExport/Deleterecordbycode?tariffCode=' + tariffCode, function (data) {
            if (data.error == true) {
                //showToast(data.message, "error");
                alert(data.message);

            }
            else {

                //showToast(data.message, "success");
                alert(data.message);
                window.location.href = window.location.href;
                LoadAgentTariffGrid();
                getsharerates();
                getshippingagnetdetail();
            }
        });

        }
    else {
        //showToast("please give tariff code ", "error");
        alert("please give tariff code ");
        }

        }

    }

    function enableDisable() {

        if (confirm("alert !") == true) {

        var tariffCode = $('#tariffCode').val();

    console.log(tariffCode);

    if (tariffCode) {

        $.post('/Export/TariffExport/EnableDisableContract?tariffCode=' + tariffCode, function (data) {
            if (data.error == true) {

                //showToast(data.message, "error");
                alert(data.message);

            }
            else {

                //showToast(data.message, "success");
                alert(data.message);
                window.location.href = window.location.href;
            }
        });

        }

    else {

        //showToast("please give tariff code ", "error");
        alert("please give tariff code ");
        }


        }

    }

    function Updaterecordbycode() {
        if (confirm("alert !") == true) {
        var tariffCode = $('#tariffCode').val();

    console.log(tariffCode);

    if (tariffCode) {


            var agentdropdownid = $('#agentdropdown').val();
    //var consigneedropdownid = $('#consigneedropdown').val();
    var consigneedropdownid = $("#searchBoxForConsigne").dxSelectBox('instance').option("value");

    var clearingAgentdropdownid = $('#clearingAgentdropdown').val();
    var gooddropdownid = $('#gooddropdown').val();
    var shipperdropdownid = $('#shipperdropdown').val();
    var indexCargoTypeid = $('#indexCargoType').val();
    var percentAgeShippingAgentId = $('#percentAgeShippingAgentId').val();
    var portAndTerminalId = $('#portAndTerminalId').val();


    console.log("agentdropdownid", agentdropdownid)
    console.log("consigneedropdownid", consigneedropdownid)
    console.log("clearingAgentdropdownid", clearingAgentdropdownid)
    console.log("gooddropdownid", gooddropdownid)
    console.log("shipperdropdownid", shipperdropdownid)
    console.log("indexCargoTypeid", indexCargoTypeid)
    console.log("percentAgeShippingAgentId", percentAgeShippingAgentId)
    console.log("portAndTerminalId", portAndTerminalId)

            // if (agentdropdownid || consigneedropdownid || clearingAgentdropdownid || gooddropdownid || shipperdropdownid || isocodesdropdownid || portAndTerminalId) {
            var tariff = {
        shippingAgentExportId: agentdropdownid,
    exportConsigneeId: consigneedropdownid,
    clearingAgentExportId: clearingAgentdropdownid,
    goodsHeadExportId: gooddropdownid,
    shippingLineExportId: shipperdropdownid,
    portAndTerminalExportId: portAndTerminalId,
    indexCargoType: indexCargoTypeid,
    //tariffId: tariffId,
    percentAgeShippingAgentExportId: percentAgeShippingAgentId,
            };

    $.post('/Export/TariffExport/Updaterecordbycode?tariffCode=' + tariffCode, {tariff: tariff }, function (data) {
                if (data.error == true) {
        //showToast(data.message, "error");
        alert(data.message);
    LoadAgentTariffGrid();
    getsharerates();
    getshippingagnetdetail();

                }
    else {

        //showToast(data.message, "success");
        alert(data.message);

    LoadAgentTariffGrid();
    getsharerates();
    getshippingagnetdetail();
                }
            });

            //}
            //else {
        //    showToast("please Select at least one option", "error");
        //}




    }
    else {
        //showToast("please give tariff code ", "error");
        alert("please give tariff code ");

        }

    }
    }


    function printpage() {

        document.getElementById("section-to-print").style.display = "none";
    window.print();
    document.getElementById("section-to-print").style.display = "block";
    }

    function createnewcontract() {


        if (confirm("alert !") == true) {

        var createdCode = $("#createdCode").val()

    if (createdCode) {
            var percentAgeShippingAgentId = $('#percentAgeShippingAgentId').val();
    var agentdropdownid = $('#agentdropdown').val();
    var consigneedropdownid = $("#searchBoxForConsigne").dxSelectBox('instance').option("value");;
    var clearingAgentdropdownid = $('#clearingAgentdropdown').val();
    var gooddropdownid = $('#gooddropdown').val();
    var shipperdropdownid = $('#shipperdropdown').val();
    var indexCargoTypeid = $('#indexCargoType').val();
    var portAndTerminalId = $('#portAndTerminalId').val();

    console.log("percentAgeShippingAgentId", percentAgeShippingAgentId)
    console.log("agentdropdownid", agentdropdownid)
    console.log("consigneedropdownid", consigneedropdownid)
    console.log("clearingAgentdropdownid", clearingAgentdropdownid)
    console.log("gooddropdownid", gooddropdownid)
    console.log("shipperdropdownid", shipperdropdownid)
    console.log("indexCargoTypeid", indexCargoTypeid)
    console.log("percentAgeShippingAgentId", percentAgeShippingAgentId)
    console.log("portAndTerminalId", portAndTerminalId)

    if (agentdropdownid || consigneedropdownid || clearingAgentdropdownid || gooddropdownid || shipperdropdownid  || portAndTerminalId) {


                var tariff = {
        shippingAgentExportId: agentdropdownid,
    exportConsigneeId: consigneedropdownid,
    clearingAgentExportId: clearingAgentdropdownid,
    goodsHeadExportId: gooddropdownid,
    shippingLineExportId: shipperdropdownid,
    indexCargoType: indexCargoTypeid,
    portAndTerminalExportId: portAndTerminalId,
    percentAgeShippingAgentExportId: percentAgeShippingAgentId,
                };

    $.post('/Export/TariffExport/SaveTariffConditionByTariffCode?createdCode=' + createdCode, {tariff: tariff }, function (data) {
        LoadAgentTariffGrid();

    if (data.error == true) {
        //showToast(data.message, "error");
        alert(data.message);

                    }
    else {

        //showToast(data.message, "success");
        alert(data.message);

                    }

                });

            }
    else {
        //showToast("please Select at least one option", "error");
        alert("please Select at least one option");
            }

        } else {
        //showToast("please add Created Tariff Code", "error");
        alert("please add Created Tariff Code");
        }


    }
    }



