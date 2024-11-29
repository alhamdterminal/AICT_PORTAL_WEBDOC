

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



    function myFunction() {

        var consid = $("#consigneeselectbox").dxSelectBox('instance').option("value");

    console.log("consid", consid)
    var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    var cargotype = document.getElementById("cargoType").value;
    var type = document.getElementById("type").value;
    var shippingAgent = document.getElementById("shippingAgent").value;
    var clearingagent = document.getElementById("clearingagent").value;

    loadingBar();

    $.get('/Import/Reports/CRDataSummaryRes?shippingAgent=' + shippingAgent + '&clearingagent=' + clearingagent + '&consignee=' + consid
    + '&type=' + type + '&cargotype=' + cargotype + '&fromdate=' + fromdate + '&todate=' + todate, function (data) {
        loadingBarHide();
    console.log("data", data)
    griddata(data);

                //$('#repotdata').html(data);
            });

    }



    function griddata(data) {


        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#gridContainer").dxDataGrid({
        dataSource: data,
    keyExpr: "invoiceNo",
    allowColumnReordering: true,
    showBorders: true,



    wordWrapEnabled: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    columnResizingMode: 'widget',

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

    const worksheet = workbook.addWorksheet('CRShortSummary');

    DevExpress.excelExporter.exportDataGrid({
        component: e.component,
    worksheet,
    autoFilterEnabled: true,
                }).then(() => {
        workbook.xlsx.writeBuffer().then((buffer) => {


            var resffandContainerno = "CRShortSummary.xlsx";


            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), resffandContainerno);

        });
                });
    e.cancel = true;


            },
    columns: [

    {
        dataField: "srno",
    caption: "Sr No #",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },

    {
        dataField: "lineName",
    caption: "Line Name",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "containerNo",
    caption: "Container No",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "gateInDate",
    caption: "GateIn Date",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "portName",
    caption: "Port Name",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "virNo",
    caption: "Vir No",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "indexNo",
    caption: "Index No",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "containerdescription",
    caption: "Description",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "vesselName",
    caption: "Vessel",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "voyage",
    caption: "Voyage",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "invoiceNo",
    caption: "Invoice No",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "billType",
    caption: "Bill Type",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "invoiceDate",
    caption: "Invoice Date",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "cfsCy",
    caption: "CFS / CY",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "cargoType",
    caption: "Cargo Type",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "goodsHeadName",
    caption: "Goods Head",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "blNo",
    caption: "BL No",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "consigneName",
    caption: "Consigne",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "clearingAgentName",
    caption: "Clearing Agent",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "destuffDate",
    caption: "Destuff Date",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "totalCharges",
    caption: "Total Charges",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "qty20",
    caption: "Qty 20",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "qty40",
    caption: "qty 20",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "qty45",
    caption: "qty 45",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "totalArrivedCBM",
    caption: "Total Arrived CBM",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "totalArrivedIndexes",
    caption: "Total Arrived Indexes",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "totalDays",
    caption: "Total Days",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "freeDays",
    caption: "Free Days",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "storageDays",
    caption: "Storage Days",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "btlAmount",
    caption: "BTL Amount",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "btlRemarks",
    caption: "BTL Remarks",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "dlvcbmWt",
    caption: "DLVCBMWt",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
    {
        dataField: "auction",
    caption: "Auction",
    cssClass: "WrappedColumnClass  sumofbrakup",
    width: "auto"
                },
    {
        dataField: "examinationCharges",
    caption: "ExaminationCharges",
    cssClass: "WrappedColumnClass  sumofbrakup",
    width: "auto"
                },
    {
        dataField: "sealCharge",
    caption: "SealCharge",
    cssClass: "WrappedColumnClass  sumofbrakup",
    width: "auto"
                },
    {
        dataField: "portCharges",
    caption: "PortCharges",
    cssClass: "WrappedColumnClass  sumofbrakup",
    width: "auto"
                },
    {
        dataField: "specialHandlingCharges",
    caption: "SpecialHandlingCharges",
    cssClass: "WrappedColumnClass  sumofbrakup",
    width: "auto"
                },
    {
        dataField: "storage",
    caption: "Storage",
    cssClass: "WrappedColumnClass  sumofbrakup",
    width: "auto"
                },
    {
        dataField: "tariffRate",
    caption: "TariffRate",
    cssClass: "WrappedColumnClass  sumofbrakup",
    width: "auto"
                },
    {
        dataField: "fuelAdjcharges",
    caption: "FuelAdjcharges",
    cssClass: "WrappedColumnClass  maintariffbrakup ",
    width: "auto"
                },
    {
        dataField: "fuelAdjustmentcharges",
    caption: "FuelAdjustmentcharges",
    cssClass: "WrappedColumnClass  maintariffbrakup ",
    width: "auto"
                },
    {
        dataField: "additionalFUELCAF",
    caption: "AdditionalFUELCAF",
    cssClass: "WrappedColumnClass  maintariffbrakup ",
    width: "auto"
                },
    {
        dataField: "documentCharges",
    caption: "DocumentCharges",
    cssClass: "WrappedColumnClass  maintariffbrakup ",
    width: "auto"
                },
    {
        dataField: "documentionCharges",
    caption: "DocumentionCharges",
    cssClass: "WrappedColumnClass  maintariffbrakup ",
    width: "auto"
                },
    {
        dataField: "document",
    caption: "Document",
    cssClass: "WrappedColumnClass  maintariffbrakup ",
    width: "auto"
                },
    {
        dataField: "scannerDetention",
    caption: "ScannerDetention",
    cssClass: "WrappedColumnClass  maintariffbrakup ",
    width: "auto"
                },
    {
        dataField: "scanningQ",
    caption: "ScanningQ",
    cssClass: "WrappedColumnClass  maintariffbrakup ",
    width: "auto"
                },
    {
        dataField: "shifftingChargesQ",
    caption: "ShifftingChargesQ",
    cssClass: "WrappedColumnClass  maintariffbrakup ",
    width: "auto"
                },
    {
        dataField: "cargoHandling",
    caption: "CargoHandling",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "cfsCharges",
    caption: "CFSCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "currencyAdjustment",
    caption: "CurrencyAdjustment",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "customerCharges",
    caption: "CustomerCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "customsSealCharges",
    caption: "CustomsSealCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "dataProcessiong",
    caption: "DataProcessiong",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "destuffingCharges",
    caption: "DestuffingCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "doubleHaulageCharges",
    caption: "DoubleHaulageCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "examination",
    caption: "Examination",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "gateInCharges",
    caption: "GateInCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "generalCargoExamination",
    caption: "GeneralCargoExamination",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "handlingCharges",
    caption: "HandlingCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "insuranceCharges",
    caption: "InsuranceCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "lclCharges",
    caption: "LCLCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "measurementAndSurvey",
    caption: "MeasurementAndSurvey",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "measurementCharges",
    caption: "MeasurementCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "portTariffCharges",
    caption: "PortTariffCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "qictRAndD",
    caption: "QictRAndD",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "recevialDelivery",
    caption: "RecevialDelivery",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "serviceChargesDestuffing",
    caption: "ServiceChargesDestuffing",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "shiftingCharges",
    caption: "ShiftingCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "specialCharges",
    caption: "SpecialCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "surveyCharges",
    caption: "SurveyCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "terminalCharges",
    caption: "TerminalCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "transPortationDeliveryCharges",
    caption: "TransPortationDeliveryCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "weighment",
    caption: "Weighment",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "weighmentCharges",
    caption: "WeighmentCharges",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "wharfage",
    caption: "Wharfage",
    cssClass: "WrappedColumnClass  tariffbrakup ",
    width: "auto"
                },
    {
        dataField: "yardCharges",
    caption: "YardCharges",
    cssClass: "WrappedColumnClass  tariffbrakup",
    width: "auto"
                },
    {
        dataField: "aictPerCBMRate",
    caption: "AICTPerCBMRate",
    cssClass: "WrappedColumnClass  ffsharecolor",
    width: "auto"
                },
    {
        dataField: "aictPerIndexRate",
    caption: "AICTPerIndexRate",
    cssClass: "WrappedColumnClass  ffsharecolor",
    width: "auto"
                },
    {
        dataField: "ffPerCBMRate",
    caption: "FFPerCBMRate",
    cssClass: "WrappedColumnClass  ffsharecolor",
    width: "auto"
                },
    {
        dataField: "ffPerIndexRate",
    caption: "FFPerIndexRate",
    cssClass: "WrappedColumnClass  ffsharecolor",
    width: "auto"
                },
    {
        dataField: "aictPerCBMRateShareRate",
    caption: "AICTPerCBMRateShareRate",
    cssClass: "WrappedColumnClass  ffsharecolor",
    width: "auto"
                },
    {
        dataField: "aictPerIndexRateShareRate",
    caption: "AICTPerIndexRateShareRate",
    cssClass: "WrappedColumnClass  ffsharecolor",
    width: "auto"
                },
    {
        dataField: "ffPerCBMRateShareRate",
    caption: "FFPerCBMRateShareRate",
    cssClass: "WrappedColumnClass  ffsharecolor",
    width: "auto"
                },
    {
        dataField: "ffPerIndexRateShareRate",
    caption: "FFPerIndexRateShareRate",
    cssClass: "WrappedColumnClass  ffsharecolor",
    width: "auto"
                },
    {
        dataField: "perBoxRate",
    caption: "PerBoxRate",
    cssClass: "WrappedColumnClass  ffsharecolor",
    width: "auto"
                },
    {
        dataField: "otherchargesParty",
    caption: "otherchargesParty",
    cssClass: "WrappedColumnClass  otherchargebrakup",
    width: "auto"
                },
    {
        dataField: "otherchargesFFLine",
    caption: "otherchargesFFLine",
    cssClass: "WrappedColumnClass  otherchargebrakup",
    width: "auto"
                },
    {
        dataField: "otherchargesAict",
    caption: "otherchargesAict",
    cssClass: "WrappedColumnClass  otherchargebrakup",
    width: "auto"
                },
    {
        dataField: "otherchargesClearingAgent",
    caption: "otherchargesClearingAgent",
    cssClass: "WrappedColumnClass  otherchargebrakup",
    width: "auto"
                },
    {
        dataField: "storageWaiver",
    caption: "StorageWaiver",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },

    {
        dataField: "otherWaiver",
    caption: "OtherWaiver",
    cssClass: "WrappedColumnClass",
    width: "auto"
                },
                //{
                //    dataField: 'masterLineName',
                //    groupIndex: 0,
                //},
                //{
                //    dataField: 'containerNumber',
                //    groupIndex: 1,
                //},
            ],



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