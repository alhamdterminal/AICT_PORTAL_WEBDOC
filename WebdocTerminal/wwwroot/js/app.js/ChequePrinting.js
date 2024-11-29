
    var ChequePrintingList = [];



    function finddata() {
         
        var userId = $('#userId').val();
    var fromdate = $('#single_cal2').val();
    var todate = $('#single_cal3').val();
    var search = $('#search').val();

    $.get('/Account/ChequePrinting/GetChequePrintingList?userId=' + userId + '&fromdate=' + fromdate + '&todate=' + todate + '&search=' + search, function (data) {
        console.log("data", data);
    ChequePrintingList = data;
    GridData(ChequePrintingList)
        });

    }


    function GridData(VoucherList) {


        $("#gridContainer").dxDataGrid({
            dataSource: ChequePrintingList,
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
                mode: "cell",
                allowUpdating: true,
                useIcons: true
            },
            headerFilter: {
                visible: true
            },
            columns: [
                {
                    dataField: "voucherNo",
                    caption: "Voucher No",
                    allowEditing: false,
                    validationRules: [{ type: "required" }]
                },
                {
                    dataField: "voucherDate",
                    caption: "Voucher Date",
                    dataType: "date",
                    format: "dd/MMM/yyyy",
                    allowEditing: false,
                    validationRules: [{ type: "required" }]
                },
                {
                    dataField: "bankName",
                    caption: "Bank Name",
                    allowEditing: false,
                    validationRules: [{ type: "required" }]
                },
                {
                    dataField: "partyName",
                    caption: "Party Name",
                    validationRules: [{ type: "required" }]
                },
                {
                    dataField: "chequeNo",
                    caption: "Cheque No",
                    allowEditing: false,
                    validationRules: [{ type: "required" }]
                },
                {
                    dataField: "amount",
                    caption: "Amount",
                    allowEditing: false,
                    dataType: "number",
                    format: "#,##0.##",
                    width: 120,
                    editorOptions: {
                        step: 0
                    },
                },
                {
                    dataField: "count",
                    caption: "count",
                    allowEditing: false,
                },
                {
                    width: 100,
                    alignment: 'center',
                    cellTemplate: function (container, options) {
                        $('<a/>').addClass('dx-link')
                            .text('Print')
                            .on('dxclick', function () {
                                print_cheque(options.row.data)
                            })
                            .appendTo(container);
                    }
                },
                {
                    width: 100,
                    alignment: 'center',
                    cellTemplate: function (container, options) {
                        $('<a/>').addClass('dx-link')
                            .text('route')
                            .on('dxclick', function () {
                                update_Voucher(options.row.data)
                            })
                            .appendTo(container);
                    }
                }

            ],


        });

    }

    function print_cheque(data) {

        console.log(data);

    if (!data.partyName) {
        showToast("party name are not define", "error");
        }
    else {
        $.post('/Account/ChequePrinting/AddChequePrintingLog', { data: data }, function (res) {

            console.log(res);
            if (res.error == true) {
                showToast(res.message, "error");
            } else {

                window.open('/Account/Reports/ChequePrintReport?Amount=' + data.amount + '&PartyName=' + data.partyName.replace('&', "A00A11"), "_blank");
            }
        })
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


    function update_Voucher(data) {

        window.open('/Account/Voucher/UpdateVoucherView?VoucherId=' + data.voucherId, "_blank");

    }

    function formfiled() {

    }
