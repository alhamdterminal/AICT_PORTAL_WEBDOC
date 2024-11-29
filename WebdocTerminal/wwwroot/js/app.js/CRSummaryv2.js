

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
    $(function () {


        $.get('/APICalls/GetALlConsignees', function (res) {
            if (res) {
                $("#consigneeselectbox").dxSelectBox({
                    dataSource: {
                        store: res,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ConsigneId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                })
            }
            else {
                $("#consigneeselectbox").dxSelectBox({
                    dataSource: {
                        store: [],
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ConsigneId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                })
            }
        });


    });

    var resValues = [];
    var coloasda = [];
    function myFunction() {

        griddata([]);

    var consid = $("#consigneeselectbox").dxSelectBox('instance').option("value");

    console.log("consid", consid)
    var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    var cargotype = document.getElementById("cargoType").value;
    var type = document.getElementById("type").value;
    var shippingAgent = document.getElementById("shippingAgent").value;
    var clearingagent = document.getElementById("clearingagent").value;
    var goodsHeads = document.getElementById("goodsHeads").value;

    loadingBar();

    $.get('/Import/Reports/ViewCRSummaryv2?shippingAgent=' + shippingAgent + '&clearingagent=' + clearingagent + '&consignee=' + consid + '&goodsHeads=' + goodsHeads
    + '&type=' + type + '&cargotype=' + cargotype + '&fromdate=' + fromdate + '&todate=' + todate, function (data) {
        loadingBarHide();

    console.log("data", data);

    var jsonObj = $.parseJSON(data);

    var newobj = jsonObj.Table;

    let asdasd = newobj[0];

    var asdasdas = Object.keys(asdasd);

    asdasdas = asdasdas.filter(function (el) { return el != "InvoiceId"; });
             
            asdasdas.filter(x => {

        let re = {
        dataField: x,
                }

    coloasda.push(re);
            });



    let rasFFCollection = {

        dataField: "FFCollection",
    caption: "FFCollection",
    format: "#,##0.##",
    calculateCellValue: function (rowData) {

                    var calculatedval =
    (

    (
    (
    (parseFloat(rowData.FFPerCBMRate.replace(/,/g, '')))
    +
    (parseFloat(rowData.FFPerCBMRateShareRate.replace(/,/g, '')))
    )
    * rowData.DLVCBMWt
    )
    + (parseFloat(rowData.FFPerIndexRate.replace(/,/g, '')))
    + (parseFloat(rowData.FFPerIndexRateShareRate.replace(/,/g, '')))
    + (parseFloat(rowData.Party.replace(/,/g, '')))
    + (parseFloat(rowData.FFLine.replace(/,/g, '')))
    + (parseFloat(rowData.ClearingAgent.replace(/,/g, '')) )



    )

    return rowData.FFCollection = calculatedval;


                },

            }
    coloasda.push(rasFFCollection);

    let rasAICTSharePerBox = {

        dataField: "AICTSharePerBox",
    caption: "AICTSharePerBox",
    format: "#,##0.##",
    calculateCellValue: function (rowData) {

                    var calculatedvalue = (rowData.AICTPeIndex != null ? Number(rowData.AICTPeIndex) : Number(0))  * -1
    return rowData.AICTSharePerBox = calculatedvalue;
                },

            }

    coloasda.push(rasAICTSharePerBox);

    let rasFreightForwarderStorageShare = {

        dataField: "FreightForwarderStorageShare",
    caption: "FreightForwarderStorageShare",
    format: "#,##0.##",
    calculateCellValue: function (rowData) {
                    
                  var calculatedvalue = ((parseFloat(rowData.StorageInvo.replace(/,/g, '')) * rowData.FFStorageShare) / 100)

    return rowData.FreightForwarderStorageShare = calculatedvalue;
                },

            }

    coloasda.push(rasFreightForwarderStorageShare);

    let rasTotalFF = {

        dataField: "TotalFF",
    caption: "TotalFF",
    format: "#,##0.##",
    calculateCellValue: function (rowData) {

                    var calculatedvalue =
    (rowData.FFCollection != null ? Number(rowData.FFCollection) : Number(0))
    +
    (rowData.AICTSharePerBox != null ? Number(rowData.AICTSharePerBox) : Number(0))
    +
    (rowData.FreightForwarderStorageShare != null ? Number(rowData.FreightForwarderStorageShare) : Number(0))

    return rowData.TotalFF = calculatedvalue;
                },

            }

    coloasda.push(rasTotalFF);


    let rasAICTColletion = {

        dataField: "AICTColletion",
    caption: "AICTColletion",
    format: "#,##0.##",
    calculateCellValue: function (rowData) {

                    var calculatedvalue =
    (parseFloat(rowData.TotalInvo.replace(/,/g, '')))
    -
    (rowData.FFCollection != null ? Number(rowData.FFCollection) : Number(0))
    -
    (rowData.FreightForwarderStorageShare != null ? Number(rowData.FreightForwarderStorageShare) : Number(0))
    -
    ((parseFloat(rowData.StorageInvo.replace(/,/g, ''))
    -
    (rowData.FreightForwarderStorageShare != null ? Number(rowData.FreightForwarderStorageShare) : Number(0))
    ))

    return rowData.AICTColletion = calculatedvalue;
                },

            }

    coloasda.push(rasAICTColletion);

    let rasAICTSharePerBoxSec = {

        dataField: "AICTSharePerBoxSec",
    caption: "AICTSharePerBoxSec",
    format: "#,##0.##",
    calculateCellValue: function (rowData) {
                     
                   var calculatedvalue = (rowData.AICTPeIndex != null ? Number(rowData.AICTPeIndex) : Number(0))
    return rowData.AICTSharePerBoxSec = calculatedvalue;
                     
                },

            }

    coloasda.push(rasAICTSharePerBoxSec);

    let rasAICTStorageShare = {

        dataField: "AICTStorageShare",
    caption: "AICTStorageShare",
    format: "#,##0.##",
    calculateCellValue: function (rowData) {
                     
                    var calculatedvalue = (parseFloat(rowData.StorageInvo.replace(/,/g, ''))
    -
    (rowData.FreightForwarderStorageShare != null ? Number(rowData.FreightForwarderStorageShare) : Number(0))
    )
    return rowData.AICTStorageShare = calculatedvalue;
                     
                },

            }

    coloasda.push(rasAICTStorageShare);

    let rasTotalAICT = {

        dataField: "TotalAICT",
    caption: "TotalAICT",
    format: "#,##0.##",
    calculateCellValue: function (rowData) {

                    var calculatedvalue = (


    (rowData.AICTColletion != null ? Number(rowData.AICTColletion) : Number(0))
    +
    (rowData.AICTSharePerBoxSec != null ? Number(rowData.AICTSharePerBoxSec) : Number(0))
    +
    (rowData.AICTStorageShare != null ? Number(rowData.AICTStorageShare) : Number(0))
    )
    return rowData.TotalAICT = calculatedvalue;

                },

            }

    coloasda.push(rasTotalAICT);

    let rasTotal = {

        dataField: "Total",
    caption: "Total",
    format: "#,##0.##",
    calculateCellValue: function (rowData) {

                    var calculatedvalue = (


    (rowData.TotalFF != null ? Number(rowData.TotalFF) : Number(0))
    +
    (rowData.TotalAICT != null ? Number(rowData.TotalAICT) : Number(0))
    )
    return rowData.Total = calculatedvalue;

                },

            }

    coloasda.push(rasTotal);

    let rasDailySales = {

        dataField: "DailySales",
    caption: "DailySales",
    format: "#,##0.##",
    calculateCellValue: function (rowData) {

                    var calculatedvalue = ( parseFloat(rowData.TotalInvo.replace(/,/g, '')) )
    return rowData.DailySales = calculatedvalue;

                },

            }

    coloasda.push(rasDailySales);

    let rasDiff = {

        dataField: "Diff",
    caption: "Diff",
    format: "#,##0.##",
    calculateCellValue: function (rowData) {

                    var calculatedvalue = (
    (rowData.Total != null ? Number(rowData.Total) : Number(0))
    -
    (rowData.DailySales != null ? Number(rowData.DailySales) : Number(0))
    )
    return rowData.Diff = calculatedvalue;

                },

            }

    coloasda.push(rasDiff);






    griddata(newobj);

                //$('#repotdata').html(data);
            });

    }



    function griddata(data) {


        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#gridContainer").dxDataGrid({
        dataSource: data,
    keyExpr: "InvoiceId",
    allowColumnReordering: true,
    showBorders: true,
    wordWrapEnabled: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    columnResizingMode: 'widget',
    columns: coloasda,
    grouping: {
        autoExpandAll: true,
            },
    searchPanel: {
        visible: true,
            },
            //paging: {
        //    pageSize: 100,
        //},

        scrolling: {
        mode: 'virtual',
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

    const worksheet = workbook.addWorksheet('CRSummaryv2');

    DevExpress.excelExporter.exportDataGrid({
        component: e.component,
    worksheet,
    autoFilterEnabled: true,
                }).then(() => {
        workbook.xlsx.writeBuffer().then((buffer) => {


            var resffandContainerno = "CRSummaryv2.xlsx";


            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), resffandContainerno);

        });
                });
    e.cancel = true;


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

    function exporttoexcel() {

         var PaymentReceiveGriddata = $("#gridContainer").dxDataGrid("option", "dataSource");
    console.log("PaymentReceiveGriddata", PaymentReceiveGriddata);

    const worksheet = XLSX.utils.json_to_sheet(PaymentReceiveGriddata);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, "ShortCrSummary");
    XLSX.writeFile(workbook, "ShortCrSummary.xlsx", {compression: true });

    }