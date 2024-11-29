
    var VoucherList = [];

    function PrintDetail() {


        var fromdate = $('#single_cal2').val();
    var todate = $('#single_cal3').val();

    $.get('/Account/Voucher/GetVoucherTransactions?fromdate=' + fromdate + '&todate=' + todate, function (data) {
        console.log("data", data);
    if (data) {

        VoucherList = data;
    GridData(VoucherList)
            }
    else {

        VoucherList = [];
    GridData([])
            }
        });

    }



    function GridData(resdata) {


        $("#gridContainer").dxDataGrid({
            dataSource: resdata,
            keyExpr: "voucherDetailId",
            showBorders: true,
            selection: { mode: "single" },
            hoverStateEnabled: true,
            columnsAutoWidth: true,
            paging: {
                pageSize: 15
            },
            scrolling: {
                columnRenderingMode: "virtual"
            },
            filterRow: {
                visible: true,
                applyFilter: "auto"
            },
            searchPanel: {
                visible: true,
                width: 240,
                placeholder: "Search..."
            },
            editing: {
                mode: "row",
                allowDeleting: false,
                allowUpdating: false,
                useIcons: true
            },
            export: {
                enabled: true
            },
            onExporting(e) {

                console.log("e", e)
                const workbook = new ExcelJS.Workbook();

                const worksheet = workbook.addWorksheet('TransactionDetail');

                DevExpress.excelExporter.exportDataGrid({
                    component: e.component,
                    worksheet,
                    autoFilterEnabled: true,
                }).then(() => {
                    workbook.xlsx.writeBuffer().then((buffer) => {

                        saveAs(new Blob([buffer], { type: 'application/octet-stream' }), "TransactionDetail.xlsx");

                    });
                });
                e.cancel = true;
            },
            headerFilter: {
                visible: true
            },

            columns: [
                {
                    dataField: "voucherNo",
                    caption: "Voucher Type",
                },
                {
                    dataField: "voucherDate",
                    caption: "Voucher Date",
                    dataType: "date",
                    format: "dd/MMM/yyyy",
                },
                {
                    dataField: "voucherType",
                    caption: "Voucher Type",
                },
                {
                    dataField: "serviceType",
                    caption: "Service Type",
                },
                {
                    dataField: "party",
                    caption: "Party",
                },
                {
                    dataField: "account",
                    caption: "Account",
                },
                {
                    dataField: "department",
                    caption: "department",
                },
                {
                    dataField: "reference",
                    caption: "Reference",
                },
                {
                    dataField: "debit",
                    caption: "Debit",
                    format: "#,##0.##",
                    editorOptions: {
                        step: 0
                    }
                },
                {
                    dataField: "credit",
                    caption: "Credit",
                    format: "#,##0.##",
                    editorOptions: {
                        step: 0
                    }
                },
                {
                    dataField: "narration",
                    caption: "Narration",
                },


            ],



        });

    }


    function formfiled() {

    }


