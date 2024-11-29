

    function loadingBar() {
        $("#large-indicator").dxLoadIndicator({
            height: 60,
            width: 60
        });
    }

    function loadingBarHide() {
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }

    function myFunction() {

        loadingBar();


    var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    //var uploadfromdate = document.getElementById("single_cal4").value;
    //var uploadtodate = document.getElementById("single_cal1").value;
    var igm = $("#virnolist").dxAutocomplete("instance").option("value");
    var Containerno = $("#containerlist").dxAutocomplete("instance").option("value");;
    var masterline = document.getElementById("masterShippingAgentId").value;;
    var line = document.getElementById("shippingagent").value;;
    var Delivered = document.getElementById("isDelivered").value;
    var billtoline = document.getElementById("isBillToLine").value;


    var uploadfromdate = document.getElementById("fromdateUpload").value;
    var uploadtodate = document.getElementById("todateUpload").value;


    console.log("uploadfromdate", uploadfromdate);
    console.log("uploadtodate", uploadtodate);


    console.log("igm", igm)
    console.log("Containerno", Containerno)

    if (igm == undefined || igm == "" || igm == "null") {
        igm = null;
        }
    if (Containerno == undefined || Containerno == "" || Containerno == "null") {
        Containerno = null;
        }

    $.get('/Import/Reports/UploadAictLineIndexReport?fromdate=' + fromdate + '&todate=' + todate + '&uploadfromdate=' + uploadfromdate + '&uploadtodate=' + uploadtodate + '&igm=' + igm
    + '&Containerno=' + Containerno + '&masterline=' + masterline + '&line=' + line + '&Delivered=' + Delivered + '&billtoline=' + billtoline, function (data) {

        loadingBarHide();
    griddata(data);
            });

    }


    function griddata(data) {


        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#gridContainer").dxDataGrid({
        dataSource: data,
    keyExpr: "containerIndexId",
    allowColumnReordering: true,
    showBorders: true,
    grouping: {
        autoExpandAll: true,
            },
    searchPanel: {
        visible: true,
            },
    paging: {
        pageSize: 40,
            },
    groupPanel: {
        visible: true,
            },
    export: {
        enabled: true
            },
    onExporting(e) {

        console.log("e", e)
                const workbook = new ExcelJS.Workbook();

    const worksheet = workbook.addWorksheet('LineBoxIndexWiseDetail');

    DevExpress.excelExporter.exportDataGrid({
        component: e.component,
    worksheet,
    autoFilterEnabled: true,
                }).then(() => {
        workbook.xlsx.writeBuffer().then((buffer) => {


            var resffandContainerno = "LineBoxIndexWiseDetail.xlsx";


            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), resffandContainerno);

        });
                });
    e.cancel = true;


            },
    columns: [

    {
        dataField: "masterLineName",
    caption: "Master Line",
    cssClass: "WrappedColumnClass"
                },
    {
        dataField: "lineName",
    caption: "Line Name",
    cssClass: "WrappedColumnClass"
                },
    {
        dataField: "containerNumber",
    caption: "Container No",
    cssClass: "WrappedColumnClass"
                },
    {
        dataField: "arrivalDate",
    caption: "Arrival Date",
    dataType: "date",
    format: "dd/MMM/yyyy",
    cssClass: "WrappedColumnClass"
                },
    {
        dataField: "size",
    caption: "Size",
    cssClass: "WrappedColumnClass"
                },
    {
        dataField: "igmNo",
    caption: "IGM",
    cssClass: "WrappedColumnClass"
                },
    {
        dataField: "indexNo",
    caption: "Index No",
    cssClass: "WrappedColumnClass"
                },
    {
        dataField: "higherCBM",
    caption: "CBM",
    cssClass: "WrappedColumnClass",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "perBoxRate",
    caption: "Per Box Rate",
    cssClass: "WrappedColumnClass aictsharecolor",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },{
        dataField: "aictPerCBMRate",
    caption: "Per CBM",
    cssClass: "WrappedColumnClass aictsharecolor",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "aictPerIndexRate",
    caption: "Per Index",
    cssClass: "WrappedColumnClass  aictsharecolor",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "additionalChargeAICTPerCBMRate",
    caption: "Additional Per CBM",
    cssClass: "WrappedColumnClass  aictsharecolor",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "additionalChargeAICTPerIndexRate",
    caption: "Additional Per Index",
    cssClass: "WrappedColumnClass  aictsharecolor",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "totalAICT",
    caption: "Aict Total",
    cssClass: "WrappedColumnClass  aictsharecolor",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "ffPerCBMRate",
    caption: "FF Per CBM",
    cssClass: "WrappedColumnClass  ffsharecolor",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "ffPerIndexRate",
    caption: "FF Per Index",
    cssClass: "WrappedColumnClass ffsharecolor",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "additionalChargeFFPerCBMRate",
    caption: "additional FF Per CBM",
    cssClass: "WrappedColumnClass  ffsharecolor",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "additionalChargeFFPerIndexRate",
    caption: "additional FF Per Index",
    cssClass: "WrappedColumnClass  ffsharecolor",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "totalFF",
    caption: "FF Total Amount",
    cssClass: "WrappedColumnClass  ffsharecolor",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "totalCBMRate",
    caption: "Total CBM Rate",
    cssClass: "WrappedColumnClass",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "totalPerIndexRate",
    caption: "Total Per Index",
    cssClass: "WrappedColumnClass",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "totalCharges",
    caption: "Total Amount",
    cssClass: "WrappedColumnClass",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "specialCharges",
    caption: "Other Charges",
    cssClass: "WrappedColumnClass",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "portCharges",
    caption: "Port Charges",
    cssClass: "WrappedColumnClass",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "billToLine",
    caption: "Bill To Line",
    cssClass: "WrappedColumnClass",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "deliveryDate",
    caption: "Delivery  Date",
    dataType: "date",
    format: "dd/MMM/yyyy",
    cssClass: "WrappedColumnClass"
                },

    {
        dataField: 'masterLineName',
    groupIndex: 0,
                },
    {
        dataField: 'containerNumber',
    groupIndex: 1,
                },
    ],

            //sortByGroupSummaryInfo: [{
        //    summaryItem: 'count',
        //}],
        summary: {

        groupItems: [
    {
        column: "indexNo",
    summaryType: "count",
    showInGroupFooter: true,
    alignByColumn: true,

                    },
    {
        column: "higherCBM",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,

                    },
    {
        column: "perBoxRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,

                    },
    {
        column: "aictPerCBMRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,

                    },
    {
        column: "aictPerIndexRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,

                    },
    {
        column: "additionalChargeAICTPerCBMRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,

                    },
    {
        column: "additionalChargeAICTPerIndexRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,

                    },
    {
        column: "totalAICT",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,

                    },
    {
        column: "ffPerCBMRate",
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
    showInGroupFooter: true,
    alignByColumn: true,
                    },
    {
        column: "additionalChargeFFPerCBMRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,
                    },
    {
        column: "additionalChargeFFPerIndexRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,

                    },
    {
        column: "totalFF",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,
                    },
    {
        column: "totalCBMRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,
                    },
    {
        column: "totalPerIndexRate",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,
                    },
    {
        column: "totalCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,
                    },
    {
        column: "specialCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,
                    },
    {
        column: "portCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,
                    },
    {
        column: "billToLine",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
    showInGroupFooter: true,
    alignByColumn: true,
                    },
    ]

            },




        }).dxDataGrid('instance');


    $('#autoExpand').dxCheckBox({
        value: true,
    text: 'Expand All Groups',
    onValueChanged(data) {
        dataGrid.option('grouping.autoExpandAll', data.value);
            },
        });


    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');
    }



    function formfiled() {

    }


