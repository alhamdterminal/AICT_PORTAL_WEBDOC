
    var containers = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function listGrid(virno, containerno, indexno, agent) {

        console.log("virno", virno)
        console.log("containerno", containerno)
    console.log("indexno", indexno)
    console.log("agent", agent)

    if (virno == undefined) {
        virno = "";
        }
    if (containerno == undefined) {
        containerno = "";
        }
    if (indexno == undefined) {
        indexno = "";
        }
    if (agent == undefined) {
        agent = "";
        }

    console.log("after virno", virno)
    console.log("after containerno", containerno)
    console.log("after indexno", indexno)
    console.log("after agent", agent)


    loadingBar();
    $.get('/APICalls/GetCargoCFSConatiner?virno=' + virno + '&containerno=' + containerno + '&indexno=' + indexno + '&agent=' + agent, function (data) {
        loadingBarHide();
    if (data) {

        containers = data;

    console.log("containers", containers)

    const groupByCategory = groupBy(containers);


    console.log("groupByCategory", groupByCategory)


    containerIndexesGrid(groupByCategory)
            }

    else {
        containers = []
                containerIndexesGrid(containers);

            }


        });


    }


    function groupBy(data) {
        var newarraydata = [];
    if (data) {
        data.forEach(c => {
            var result = newarraydata.find(t => t.key == c.key);

            if (result == null) {
                newarraydata.push(c);

            }

        });
        }
    return newarraydata;
    }


    function loadingBar() {

        console.log("load")
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'block';

    $("#large-indicator").dxLoadIndicator({
        height: 60,
    width: 60
        });
    }
    function loadingBarHide() {
        console.log("loaded")
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }

    function formfiled() {

    }



    function containerIndexesGrid(res) {


        var grid = $("#CargoInformation").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    containers = res;

    console.log("containers", containers)


    var grid = $("#CargoInformation").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    var dataGrid = $("#CargoInformation").dxDataGrid({
        dataSource: containers,
    keyExpr: "containerIndexId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
            },
            //editing: {
            //    //mode: "row",
            //    //allowUpdating: true,
            //    //selectTextOnEditStart: true,
            //    //startEditAction: "click"
            //    allowUpdating: true,
            //    selectTextOnEditStart: true,
            //    startEditAction: "click",
            //    mode: "cell"
            //},
            export: {
        enabled: true
            },
    onExporting(e) {

        console.log("e", e)
                const workbook = new ExcelJS.Workbook();

    const worksheet = workbook.addWorksheet('AICTAndLineIndexTariffList');

    DevExpress.excelExporter.exportDataGrid({
        component: e.component,
    worksheet,
    autoFilterEnabled: true,
                }).then(() => {
        workbook.xlsx.writeBuffer().then((buffer) => {


            var griddata = $("#CargoInformation").dxDataGrid("option", "dataSource");
            var resffandContainerno = "";


            console.log("griddata", griddata)

            if (griddata.length) {

                resffandContainerno = griddata[0].containerNumber + "-" + griddata[0].shippingAgent + ".xlsx";
            }

            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), resffandContainerno);

        });
                });
    e.cancel = true;


            },
    editing: {
        allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click",
    mode: "cell"

            },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

            },
    scrolling: {
        columnRenderingMode: 'virtual',
            },
    columns: [
    {
        dataField: "igmNo",
    caption: "IGM",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                },
    {
        dataField: "containerNumber",
    caption: "Container",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                },
    {
        dataField: "indexNo",
    caption: "Index",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                },
    {
        dataField: "shippingAgent",
    caption: "FF / Line Name",
    allowEditing: false,
    cssClass: 'myClass'

                },
    {
        dataField: "goodHeadName",
    caption: "Good Type",
    allowEditing: false,
    cssClass: 'myClass'
                },
    {
        dataField: "arrivalDate",
    caption: "Arrival",
    allowEditing: false,
    dataType: 'date',
    format: 'dd/MM/yyyy',
    cssClass: 'myClass'
                },
    {
        dataField: "isDelivered",
    caption: "Delivered",
    allowEditing: false,
    cssClass: 'myClass',
    width:0,
                },
    {
        dataField: "deliveryDate",
    caption: "Delivery",
    allowEditing: false,
    cssClass: 'myClass',
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    {
        dataField: "higherCBM",
    dataType: "number",
    caption: "CBM",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },

                //{
        //caption: 'AICT Charges',
        //columns: [
        {
            dataField: "aictPerCBMRate",
            //caption: "CBMRate",
            caption: "ShareAICTCBM",
            format: "#,##0.##",
            editorOptions: {
                step: 0
            }
        },
        {
            dataField: "aictPerIndexRate",
            //caption: "IndexRate",
            caption: "ShareAICTIndex",
            format: "#,##0.##",
            editorOptions: {
                step: 0
            }
        },
        //   ]
        //},

        //{
        //caption: 'FF Charges',
        //columns: [
        {
            dataField: "ffPerCBMRate",
            //caption: "CBMRate",
            caption: "ShareFFCBM",
            format: "#,##0.##",
            editorOptions: {
                step: 0
            }
        },
        {
            dataField: "ffPerIndexRate",
            //caption: "IndexRate",
            caption: "ShareFFIndex",
            format: "#,##0.##",
            editorOptions: {
                step: 0
            }
        },
        //]
        //},


        //{
        //    caption: 'Addtional Charges',
        //    cssClass: "my-class",
        //    columns: [

        {
            dataField: "additionalChargeAICTPerCBMRate",
            dataType: "number",
            format: "#,##0.##",
            caption: "AICTCBM",
            editorOptions: {
                step: 0
            }

        },
        {
            dataField: "additionalChargeAICTPerIndexRate",
            dataType: "number",
            format: "#,##0.##",
            caption: "AICTIndex",
            editorOptions: {
                step: 0
            }

        },
        {
            dataField: "additionalChargeFFPerCBMRate",
            dataType: "number",
            format: "#,##0.##",
            caption: "FFCBM",
            editorOptions: {
                step: 0
            }

        },
        {
            dataField: "additionalChargeFFPerIndexRate",
            dataType: "number",
            format: "#,##0.##",
            caption: "FFIndex",
            editorOptions: {
                step: 0
            }
        },
        //    ]
        //},
        {
            dataField: "billToLine",
            caption: "BTL",
            format: "#,##0.##",
            allowEditing: false,
            allowExporting: false,
            cssClass: 'myClass'
        },
        {
            dataField: "portCharges",
            caption: "Port",
            allowEditing: false,
            allowExporting: false,
            format: "#,##0.##",
            cssClass: 'myClass'
        },

        {
            dataField: "specialCharges",
            caption: "Special",
            format: "#,##0.##",
            allowEditing: false,
            allowExporting: false,
            cssClass: 'myClass'
        },
        {
            dataField: "storageCharges",
            caption: "Storage",
            allowEditing: false,
            allowExporting: false,
            format: "#,##0.##",
            cssClass: 'myClass'
        },
        {
            dataField: "totalCBMRate",
            caption: "TotalCBM",
            format: "#,##0.##",
            allowEditing: false,
            allowExporting: false,
            cssClass: 'myClass',
            calculateCellValue: function (rowData) {

                var calculatedvalue = (rowData.aictPerCBMRate != null ? Number(rowData.aictPerCBMRate) : Number(0))
                    +
                    (rowData.ffPerCBMRate != null ? Number(rowData.ffPerCBMRate) : Number(0))
                    +
                    (rowData.additionalChargeAICTPerCBMRate != null ? Number(rowData.additionalChargeAICTPerCBMRate) : Number(0))
                    +
                    (rowData.additionalChargeFFPerCBMRate != null ? Number(rowData.additionalChargeFFPerCBMRate) : Number(0))
                return rowData.totalCBMRate = calculatedvalue;
            },
        },
        {
            dataField: "totalPerIndexRate",
            caption: "TotalIndex",
            allowEditing: false,
            allowExporting: false,
            format: "#,##0.##",
            cssClass: 'myClass',
            calculateCellValue: function (rowData) {

                var calculatedvalue = (rowData.aictPerIndexRate != null ? Number(rowData.aictPerIndexRate) : Number(0))
                    +
                    (rowData.ffPerIndexRate != null ? Number(rowData.ffPerIndexRate) : Number(0))
                    +
                    (rowData.additionalChargeAICTPerIndexRate != null ? Number(rowData.additionalChargeAICTPerIndexRate) : Number(0))
                    +
                    (rowData.additionalChargeFFPerIndexRate != null ? Number(rowData.additionalChargeFFPerIndexRate) : Number(0))
                return rowData.totalPerIndexRate = calculatedvalue;
            },
        },
        {
            dataField: "totalCharges",
            caption: "Total",
            format: "#,##0.##",
            allowEditing: false,
            allowExporting: false,
            cssClass: 'myClass',
            calculateCellValue: function (rowData) {

                var calculatedval = (rowData.totalCBMRate != null ? Number(rowData.totalCBMRate) : Number(0))

                if (Number(rowData.higherCBM) > 0) {

                    calculatedval = calculatedval * Number(rowData.higherCBM)
                }

                calculatedval = calculatedval + (rowData.totalPerIndexRate != null ? Number(rowData.totalPerIndexRate) : Number(0))
                calculatedval = calculatedval + (rowData.storageCharges != null ? Number(rowData.storageCharges) : Number(0))
                calculatedval = calculatedval + (rowData.specialCharges != null ? Number(rowData.specialCharges) : Number(0))
                calculatedval = calculatedval + (rowData.portCharges != null ? Number(rowData.portCharges) : Number(0))
                calculatedval = calculatedval - (rowData.billToLine != null ? Number(rowData.billToLine) : Number(0))


                return rowData.totalCharges = calculatedval;

            },

        },




            ],

    summary: {
        totalItems: [
    {
        column: "indexNo",
    summaryType: "count",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "aictPerCBMRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "aictPerIndexRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "ffPerCBMRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "ffPerIndexRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "additionalChargeAICTPerCBMRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "additionalChargeAICTPerIndexRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "additionalChargeFFPerCBMRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "additionalChargeFFPerIndexRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "billToLine",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "portCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "specialCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "storageCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "totalCBMRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "totalPerIndexRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "totalCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    ]
            },


    onRowUpdated: function (e) {

        //var model = e.data;
        //console.log("model", model);

        //if (model.isDelivered == false) {
        //    $.post('/Tariff/SetIndexTariffAmount', { model, model }, function (data) {

        //        if (data.error == true) {
        //            showToast(data.message, "error")

        //        } else {
        //            showToast(data.message, "success");

        //        }

        //        var virno = $("#virnolist").dxAutocomplete("instance").option("value")
        //        var containerno = $("#containerlist").dxAutocomplete("instance").option("value");
        //        var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value")
        //        var agent = $("#agentdropdown").val();

        //        console.log('virno', virno);
        //        console.log('containerno', containerno);
        //        console.log('indexno', indexno);
        //        console.log('agent', agent);

        //        listGrid(virno, containerno, indexno, agent);


        //    });
        //} else {

        //    showToast("index deliverd", "error");

        //    var virno = $("#virnolist").dxAutocomplete("instance").option("value")
        //    var containerno = $("#containerlist").dxAutocomplete("instance").option("value");
        //    var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value")
        //    var agent = $("#agentdropdown").val();

        //    console.log('virno', virno);
        //    console.log('containerno', containerno);
        //    console.log('indexno', indexno);
        //    console.log('agent', agent);

        //    listGrid(virno, containerno, indexno, agent);

        //}

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

    }

    var keyCodes = {
        TAB: 9,
    PAGE_UP: 33,
    PAGE_DOWN: 34,
    LEFT: 37,
    UP: 38,
    RIGHT: 39,
    DOWN: 40,

    };

    function updateinfo(data) {
        console.log("data", data)

    }

    function showcargoDetaildesc() {

        var res = [];

    containerIndexesGrid(res);

    var virno = $("#virnolist").dxAutocomplete("instance").option("value")
    var containerno = $("#containerlist").dxAutocomplete("instance").option("value");
    var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value")
    var agent = $("#agentdropdown").val();

    console.log('virno', virno);
    console.log('containerno', containerno);
    console.log('indexno', indexno);
    console.log('agent', agent);

    listGrid(virno, containerno, indexno, agent);

    }



    function SaveInfo() {
        var rowObject = $("#CargoInformation").dxDataGrid("option", "dataSource");


    console.log("rowObject", rowObject)

    var resultvalue = true;

    if (rowObject.length) {

            var newresult = rowObject;
    //var newresult = rowObject.filter(c => c.isDelivered == false);
    //var deliverdresultdata = rowObject.filter(c => c.isDelivered == true);

    if (newresult.length) {

        console.log("ok newresult", newresult)

                newresult.forEach(c => {

                    if ($.isNumeric(c.additionalChargeAICTPerCBMRate) == false) {
        resultvalue = false;
    return alert("please add Additional Charge AICT Per CBM Rate in numbers " )
                    }
    if ($.isNumeric(c.additionalChargeAICTPerIndexRate) == false) {
        resultvalue = false;
    return alert("please add Additional Charge AICT Per Index Rate in numbers " )
                    }
    if ($.isNumeric(c.additionalChargeFFPerCBMRate) == false) {
        resultvalue = false;
    return alert("please add Additional Charge FF Per CBM Rate in numbers " )
                    }
    if ($.isNumeric(c.additionalChargeFFPerIndexRate) == false) {
        resultvalue = false;
    return alert("please add Additional Charge FF Per Index Rate in numbers " )
                    }
    if ($.isNumeric(c.aictPerCBMRate) == false) {
        resultvalue = false;
    return alert("please add aict Per CBM Rate in numbers " )
                    }
    if ($.isNumeric(c.aictPerIndexRate) == false) {
        resultvalue = false;
    return alert("please add aict Per Index Rate in numbers " )
                    }
    if ($.isNumeric(c.ffPerCBMRate) == false) {
        resultvalue = false;
    return alert("please add FF Per CBM Rate in numbers " )
                    }
    if ($.isNumeric(c.ffPerIndexRate) == false) {
        resultvalue = false;
    return alert("please add FF Per Index Rate in numbers " )
                    }


    if (c.additionalChargeAICTPerCBMRate < 0) {
        resultvalue = false;
                        return alert("please add Additional Charge AICT Per CBM Rate > 0  " )
                    }
    if (c.additionalChargeAICTPerIndexRate < 0) {
        resultvalue = false;
                        return alert("please add Additional Charge AICT Per Index Rate > 0 " )
                    }
    if (c.additionalChargeFFPerCBMRate < 0) {
        resultvalue = false;
                        return alert("please add Additional Charge FF Per CBM Rate > 0 ")
                    }
    if (c.additionalChargeFFPerIndexRate < 0) {
        resultvalue = false;
                        return alert("please add Additional Charge FF Per Index Rate > 0 ")
                    }
    if (c.aictPerCBMRate < 0) {
        resultvalue = false;
                        return alert("please add aict Per CBM Rate > 0 ")
                    }
    if (c.aictPerIndexRate < 0) {
        resultvalue = false;
                        return alert("please add aict Per Index Rate > 0 " )
                    }
    if (c.ffPerCBMRate < 0) {
        resultvalue = false;
                        return alert("please add FF Per CBM Rate > 0 " )
                    }
    if (c.ffPerIndexRate < 0) {
        resultvalue = false;
                        return alert("please add FF Per Index Rate > 0 " )
                    }


    var totalcbm = Number(c.additionalChargeAICTPerCBMRate) + Number(c.additionalChargeFFPerCBMRate) + Number(c.aictPerCBMRate) + Number(c.ffPerCBMRate);
    var totalindx = Number(c.additionalChargeAICTPerIndexRate) + Number(c.additionalChargeFFPerIndexRate) + Number(c.aictPerIndexRate) + Number(c.ffPerIndexRate);

    console.log("totalcbm", Number(totalcbm));
    console.log("totalindx", Number(totalindx));

    if (Number(totalcbm) <= 0 && Number(totalindx) <= 0) {

        resultvalue = false;
    return alert("please add amount in cbm or index in igm " + c.igmNo + " index " + c.indexNo )
                    }

                })

    if (resultvalue == true) {
                    var result = [];
                    newresult.forEach(c => {


        let x = {

        additionalChargeAICTPerCBMRate: c.additionalChargeAICTPerCBMRate,
    additionalChargeAICTPerIndexRate: c.additionalChargeAICTPerIndexRate,
    additionalChargeFFPerCBMRate: c.additionalChargeFFPerCBMRate,
    additionalChargeFFPerIndexRate: c.additionalChargeFFPerIndexRate,

    aictPerCBMRate: c.aictPerCBMRate,
    aictPerIndexRate: c.aictPerIndexRate,
    ffPerCBMRate: c.ffPerCBMRate,
    ffPerIndexRate: c.ffPerIndexRate,

    totalCBMRate: c.totalCBMRate,
    totalPerIndexRate: c.totalPerIndexRate,

    containerNumber: c.containerNumber,

    igmNo: c.igmNo,
    indexNo: c.indexNo,
    higherCBM: c.higherCBM

                        };

    result.push(x);

                    })

    console.log("result", result)


    $.post('/Tariff/SaveAictLineIndexAmount', {model: result }, function (data) {

                        if (data.error == true) {
        // showToast(data.message, "error")

        alert(data.message)
                            //alert(data.message + "and total " + deliverdresultdata.length + "are Delivered")



                            var virno = $("#virnolist").dxAutocomplete("instance").option("value")
    var containerno = $("#containerlist").dxAutocomplete("instance").option("value");
    var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value")
    var agent = $("#agentdropdown").val();

    console.log('virno', virno);
    console.log('containerno', containerno);
    console.log('indexno', indexno);
    console.log('agent', agent);

    listGrid(virno, containerno, indexno, agent);


                        } else {

        alert(data.message);
    //alert(data.message + "and total " + deliverdresultdata.length + "are Delivered");
    //showToast(, "success");


    var virno = $("#virnolist").dxAutocomplete("instance").option("value")
    var containerno = $("#containerlist").dxAutocomplete("instance").option("value");
    var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value")
    var agent = $("#agentdropdown").val();

    console.log('virno', virno);
    console.log('containerno', containerno);
    console.log('indexno', indexno);
    console.log('agent', agent);

    listGrid(virno, containerno, indexno, agent);

                        }

                    });
                }

            }

        }
    else {
        showToast("no record found", "error")
    }
    }
