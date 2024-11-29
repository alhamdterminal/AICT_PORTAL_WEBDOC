
    $(function () {

        $('#voucherTypeId').val("GV").trigger('change.select2');
    $('#departmentId').val("1000").trigger('change.select2');

    });


    function showdetail() {

        Grid();
    }

    function formfiled() {

    }

    var VoucherDetail = [];


    function Grid() {


        var year = $("#financialYearId option:selected").text();



    $.get('/Account/YearClosure/GetClosureDetail?year=' + year, function (res) {

        console.log("res", res);

    $("#gridContainer").dxDataGrid({
        dataSource: res,
    keyExpr: "accountName",
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

    const worksheet = workbook.addWorksheet('Closure');

    DevExpress.excelExporter.exportDataGrid({
        component: e.component,
    worksheet,
    autoFilterEnabled: true,
                        }).then(() => {
        workbook.xlsx.writeBuffer().then((buffer) => {


            var resffandContainerno = "Closure.xlsx";


            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), resffandContainerno);

        });
                        });
    e.cancel = true;


                    },

    columns: [

    {
        dataField: "voucherType",
    caption: "Voucher Type",
                     
                },
    {
        dataField: "serviceType",
    caption: "Service Type",
                },
    {
        dataField: "accountName",
    caption: "COA",                                                             
                },
    {
        dataField: "departName",
    caption: "Depart",                    
                },

    {
        dataField: "debit",
    caption: "Debit",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }, 
                },
    {
        dataField: "credit",
    caption: "Credit",
    dataType: "number",
    format: "#,##0.##",
    width: 120,
    editorOptions: {
        step: 0
                    },
                },
    {
        dataField: "balance",
    caption: "Balance",
    dataType: "number",
    format: "#,##0.##",
    width: 120,
    editorOptions: {
        step: 0
                    },
                },

    {
        dataField: "narration",
    caption: "Narration",
                },

    ],
    summary: {
        totalItems: [
    {
        column: "debit",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "credit",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "balance",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    ]
            },

        });

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

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


    function formfiled() {

    }

    function PostClosureEntry() {

        var year = $("#financialYearId option:selected").text();
    var voucherType = $("#voucherTypeId").val();
    var department = $("#departmentId").val();

    $.post('/Account/YearClosure/PostClosureYear?year=' + year + '&voucherType=' + voucherType + '&department=' + department , function (data) {
            if (data.error == true) {
        loadGriderror(data.message);
    $('#ErroeStatusModal').modal('toggle');
            }
    else {
        loadGriderror([]);
    showToast(data.message.voucherId, "success");
    PrintDetail(data.message.voucherId);
            }
        });
    }

    function PrintDetail(voucherId) {

         
        if (voucherId != 0 && voucherId != "") {
        window.open('/Account/Reports/SingleVoucherView?VoucherId=' + voucherId, "_blank");
        }
    else {
        showToast("no voucher found", "error");
        }

    window.location.href = window.location.href;
    }

    function loadGriderror(data) {

        console.log("data", data)
        $("#errorGrid").dxDataGrid({
        dataSource: data,
    keyExpr: "voucherId",
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    paging: {
        pageSize: 10
            },
    columns: [
    {
        dataField: "voucherNo",
    caption: "Voucher No",
    allowEditing: false,
                },
    {
        dataField: "narration",
    caption: "Narration",
    allowEditing: false,
                },
    ],

        });

    }


