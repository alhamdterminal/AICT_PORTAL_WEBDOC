


    var AssignedTariffs = [];
    var portandterminalslist = [];

    $(function () {



        getportandterminal();

    setTimeout(function () {
        LoadAgentTariffGrid()
    }, 3000);

       
         
         
    });




    function getportandterminal() {
        $.get('/APICalls/GetAllPortAndTerminals', function (res) {
            portandterminalslist = res;
        });
    }

    function LoadAgentTariffGrid() {


        //$("#ShippingAgentdatagrid").dxDropDownBox("instance")._options.value = [""];


        //console.log($("#ShippingAgentdatagrid").dxDropDownBox("instance")._options.value)


        var collectionid = $('#transportCollectionId').val();


    if (collectionid) {

        $.get('/Tariff/GetTransportCollectionBy?collectionid=' + collectionid, function (data) {
            console.log("Data", data);

            if (data) {

                $('#transportCollectionName').val(data.transportCollectionName);
                $('#indexCargoType').val(data.cargoType);

                $('#shipperdropdown').val(data.shipperCode).trigger('change.select2');
                $('#portAndTerminalId').val(data.portAndTerminalCode).trigger('change.select2');
                $('#isocodesdropdown').val(data.isoCodeHeadCode).trigger('change.select2');
                $('#gooddropdown').val(data.goodHeadCode).trigger('change.select2');
                $('#consigneedropdown').val(data.consigneeCode).trigger('change.select2');
                $('#clearingAgentdropdown').val(data.clearningAgentCode).trigger('change.select2');

                //var agents = data.shippingagentCode;

                //console.log("agents", agents)
                //var array = agents.split(',');

                //console.log("array", array)




                //if (agents) {


                //    console.log("test1")
                //    console.log($("#ShippingAgentdatagrid").dxDropDownBox("instance")) 

                //    $("#ShippingAgentdatagrid").dxDropDownBox("instance")._options.value = [1, 3]


                //    var ShippingAgentdatagrid = $("#ShippingAgent-datagrid");

                //    console.log("one ", ShippingAgentdatagrid);

                //    var dataGridff = ShippingAgentdatagrid.dxDataGrid("instance");

                //    console.log("two", dataGridff);

                //} else {
                //    console.log("test2")

                //    //$("#ShippingAgentdatagrid").dxDropDownBox("instance")._options.value = [""]

                //}


            }

        });
        }

    else {

        //console.log("ads", $("#ShippingAgentdatagrid").dxDropDownBox("instance"));

        // $("#ShippingAgentdatagrid").dxDropDownBox("instance")._options.value = [0] 
        //var ShippingAgentdatagrid = $("#ShippingAgent-datagrid");

        //var dataGridff = ShippingAgentdatagrid.dxDataGrid("instance");

        //if (dataGridff) {

        //    dataGridff.clearSelection();
        //    console.log("dataGridff ", dataGridff)
        //}


        $('#transportCollectionName').val('');
    $('#indexCargoType').val('LCL');
    $('#shipperdropdown').val('').trigger('change.select2');
    $('#portAndTerminalId').val('').trigger('change.select2');
    $('#isocodesdropdown').val('').trigger('change.select2');
    $('#gooddropdown').val('').trigger('change.select2');
    $('#consigneedropdown').val('').trigger('change.select2');
    $('#clearingAgentdropdown').val('').trigger('change.select2');


        }


    $.get('/APICAlls/GetTariffsByCollectionId?collectionid=' + collectionid, function (data) {

        AssignedTariffs = data;

    console.log("AssignedTariffs", AssignedTariffs)

    var grid = $("#agentTariffGrid").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    var dataGrid = $("#agentTariffGrid").dxDataGrid({
        dataSource: AssignedTariffs,
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
    allowDeleting: true,
    allowUpdating: true,
                },
    columns: [
    {
        dataField: "name",
    caption: "Name",
    formItem: {
        visible: false
                        }
                    },
    {
        dataField: "description",
    caption: "Description",
    formItem: {
        visible: false
                        }
                    },


    {
        dataField: "portAndTerminalId",
    caption: "port",
    lookup: {
        dataSource: portandterminalslist,
    valueExpr: "value",
    displayExpr: "text"
                        }
                    },

    {
        dataField: "rate20",
    caption: "Rate 20",
    dataType: "number",
    format: "#,##0.##",
                    },
    {
        dataField: "rate40",
    caption: "Rate 40",
    dataType: "number",
    format: "#,##0.##",
                    },
    {
        dataField: "rate45",
    caption: "Rate 45",
    dataType: "number",
    format: "#,##0.##",
                    },
    {
        dataField: "cbmRate",
    caption: "CBM",
    dataType: "number",
    format: "#,##0.##",
                    },
    {
        dataField: "weightRate",
    caption: "Weight",
    dataType: "number",
    format: "#,##0.##",
                    },
    {
        dataField: "perIndexRate",
    caption: "Per Index",
    dataType: "number",
    format: "#,##0.##",
                    },
    {
        dataField: "devededIndexAmount",
    caption: "Deveded Index",
    dataType: "number",
    format: "#,##0.##",
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

    $.post('/Tariff/updateAgentTariff', {tariff: tariff }, function (data) {

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


                    $.post('/Tariff/DeleteTransportCollectionTariffCondition?TariffId=' + e.data.tariffInofrmationTariffId, function (data) {
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



    function formfiled() {



        document.getElementById('ischeckupdate').onchange = function () {

            console.log(this.checked)

            if (this.checked == true) {
                var buttn = document.getElementById("btnSubmit");
                buttn.style.display = 'none';

                var buttn = document.getElementById("btnSubmitUpdate");
                buttn.style.display = 'block';

                var buttn = document.getElementById("btnSubmitDelete");
                buttn.style.display = 'block';
            }

            if (this.checked == false) {
                var buttn = document.getElementById("btnSubmit");
                buttn.style.display = 'block';

                var buttn = document.getElementById("btnSubmitUpdate");
                buttn.style.display = 'none';

                var buttn = document.getElementById("btnSubmitDelete");
                buttn.style.display = 'none';
            }
        }


        $('#transportCollectionId').on('change', function (data) {

        LoadAgentTariffGrid();

        });

    }

    function addInfo() {

        var strff = "";
    var strconsignee = "";
    var strcagnet = "";
    var strshipper = "";
    var strgood = "";
    var strcontype = "";
    var strportandterminal = "";
    var ShippingAgentdatagrid = $("#ShippingAgent-datagrid");

    if (ShippingAgentdatagrid.length) {
            var dataGridff = ShippingAgentdatagrid.dxDataGrid("instance");


    var listofff = dataGridff.getSelectedRowsData()

            let resultff = listofff.map(a => a.value);

    strff = Object.values(resultff).join(',');

        }

    var consigneedropdownid = $('#consigneedropdown').val();
    var clearingAgentdropdownid = $('#clearingAgentdropdown').val();
    var gooddropdownid = $('#gooddropdown').val();
    var shipperdropdownid = $('#shipperdropdown').val();
    var isocodesdropdownid = $('#isocodesdropdown').val();
    var portAndTerminalId = $('#portAndTerminalId').val();

    console.log("strff", strff)
    console.log("consigneedropdownid", consigneedropdownid)
    console.log("clearingAgentdropdownid", clearingAgentdropdownid)
    console.log("gooddropdownid", gooddropdownid)
    console.log("shipperdropdownid", shipperdropdownid)
    console.log("isocodesdropdownid", isocodesdropdownid)
    console.log("portAndTerminalId", portAndTerminalId)

    var transName = $('#transportCollectionName').val();
    var cargotype = $('#indexCargoType').val();

    if (transName && cargotype) {

        //if (strff || clearingAgentdropdownid || consigneedropdownid || shipperdropdownid || gooddropdownid || portAndTerminalId || isocodesdropdownid) {
        let res = {
        transportCollectionName: transName,
    shippingagentCode: strff,
    clearningAgentCode: clearingAgentdropdownid,
    consigneeCode: consigneedropdownid,
    shipperCode: shipperdropdownid,
    goodHeadCode: gooddropdownid,
    portAndTerminalCode: portAndTerminalId,
    isoCodeHeadCode: isocodesdropdownid,
    cargoType: cargotype,

                }

    console.log(res);

    $.post('/Tariff/AddTransportCollect', {res: res }, function (data) {
                    if (data.error == true) {
        showToast(data.message, "error");

                    }
    else {

        showToast(data.message, "success");
    LoadAgentTariffGrid();
    window.location.href = window.location.href;
                    }
                });
            //}
            //else {
        //    showToast("atlease select one type of parameter", "error")
        //}


    }
    else {
        showToast("please add name and cargo type", "error")
    }

    }



    function updateInfo() {

        var transportCollectionId = $('#transportCollectionId').val();

    if (transportCollectionId) {



            var strff = "";
    var strconsignee = "";
    var strcagnet = "";
    var strshipper = "";
    var strgood = "";
    var strcontype = "";
    var strportandterminal = "";
    var ShippingAgentdatagrid = $("#ShippingAgent-datagrid");

    if (ShippingAgentdatagrid.length) {
                var dataGridff = ShippingAgentdatagrid.dxDataGrid("instance");

    var listofff = dataGridff.getSelectedRowsData()

                let resultff = listofff.map(a => a.value);

    strff = Object.values(resultff).join(',');

            }



    var consigneedropdownid = $('#consigneedropdown').val();
    var clearingAgentdropdownid = $('#clearingAgentdropdown').val();
    var gooddropdownid = $('#gooddropdown').val();
    var shipperdropdownid = $('#shipperdropdown').val();
    var isocodesdropdownid = $('#isocodesdropdown').val();
    var portAndTerminalId = $('#portAndTerminalId').val();

    console.log("consigneedropdownid", consigneedropdownid)
    console.log("clearingAgentdropdownid", clearingAgentdropdownid)
    console.log("gooddropdownid", gooddropdownid)
    console.log("shipperdropdownid", shipperdropdownid)
    console.log("isocodesdropdownid", isocodesdropdownid)
    console.log("portAndTerminalId", portAndTerminalId)


    var transName = $('#transportCollectionName').val();
    var cargotype = $('#indexCargoType').val();

    if (transName && cargotype) {

        /*if (strff || clearingAgentdropdownid || consigneedropdownid || shipperdropdownid || gooddropdownid || portAndTerminalId || isocodesdropdownid) {*/
        let res = {
        transportCollectionName: transName,
    shippingagentCode: strff,
    clearningAgentCode: clearingAgentdropdownid,
    consigneeCode: consigneedropdownid,
    shipperCode: shipperdropdownid,
    goodHeadCode: gooddropdownid,
    portAndTerminalCode: portAndTerminalId,
    isoCodeHeadCode: isocodesdropdownid,
    cargoType: cargotype,

                    }

    console.log(res);

    $.post('/Tariff/UpdateTransportCollect?transportCollectionId=' + transportCollectionId, {res: res }, function (data) {
                        if (data.error == true) {
        showToast(data.message, "error");

                        }
    else {

        showToast(data.message, "success");
    LoadAgentTariffGrid();
    window.location.href = window.location.href;
                        }
                    });
                //}
                //else {
        //    showToast("atlease select one type of parameter", "error")
        //}


    }
    else {
        showToast("please add name and cargo type", "error")
    }

        } else {
        showToast("please select contract name")
    }
    }

    function createtariff() {

        var model = $('#tariffCharegsForm').serialize();

    console.log("tariff", model);

    var tarifhead = $('#tariffHead').val()
    var transcollectionid = $('#transportCollectionId').val()

    if (tarifhead) {

            if (transcollectionid) {

        $.post('/Tariff/SaveTransportCollectionTariffCondition?' + model + '&tarifhead=' + tarifhead + '&transcollectionid=' + transcollectionid, function (data) {

            //  LoadAgentTariffGrid();

            if (data.error == true) {
                showToast(data.message, "error");

            }
            else {

                showToast(data.message, "success");
                LoadAgentTariffGrid();

            }

        });


            } else {
        showToast("please select contract", "error")
    }

        } else {
        showToast("please select head", "error")
    }
    }


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function DeleteInfo() {
        var transcollectionid = $('#transportCollectionId').val()



    if (transcollectionid) {

        $.post('/Tariff/DeleteTransportCollectionDetail?transcollectionid=' + transcollectionid, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

                window.location.href = window.location.href;
            }
            else {

                showToast(data.message, "success");
                window.location.href = window.location.href;
            }

        });


            } else {
        showToast("please select contract", "error")
    }

         
    }

