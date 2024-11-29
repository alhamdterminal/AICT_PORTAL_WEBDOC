

    var BankList = [];
    var BankBrachList = [];


    $(function () {

        GetBanks();
    GetBankBranches(); 
    });



    function GetBanks() {
        $.get('/Import/Bank/GetBank', function (data) {
            BankList = data;
            console.log("BankList", BankList);
        });
    }

    function GetBankBranches() {
        $.get('/Import/Bank/GetBankBranch', function (data) {
            BankBrachList = data;
            console.log("BankBrachList", BankBrachList);
        });
    }



    function GetPreAlterPayOrderByDate(fromdate, todate) {


        $.get('/Import/PreAlertPayOrder/GetPreAlertPayorderByDate?fromDate=' + fromdate + '&todate=' + todate, function (result) {
            console.log("result", result)
            var PreAlterPayOders = result;

            var grid = $("#prealertpayorerdetails").dxDataGrid(this.gridSettings).dxDataGrid('instance');

            $("#prealertpayorerdetails").dxDataGrid({
                dataSource: PreAlterPayOders,
                keyExpr: "preAlertPayOrderId",
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
                    allowDeleting: true,
                    useIcons: true
                },
                headerFilter: {
                    visible: true
                },
                columns: [
                    {
                        dataField: "preAlertPayOrderNumber",
                        caption: "Pay Order No",
                    },
                    {
                        dataField: "requestNumber",
                        caption: "Request No",
                    },

                    {
                        dataField: "payOrderCreatedDate",
                        caption: "Created Date",
                        dataType: "date",
                        format: "dd/MMM/yyyy",
                    }, {
                        width: 100,
                        alignment: 'center',
                        cellTemplate: function (container, options) {
                            $('<a/>').addClass('dx-link')
                                .text('Print')
                                .on('dxclick', function () {
                                    PrintPayorder(options.data);
                                })
                                .appendTo(container);
                        }
                    }
                ],
                masterDetail: {
                    enabled: true,
                    template: function (container, options) {
                        //$.get('/Consultant/GetConsultants', function (data) {
                        //    consultants = data;
                        //    console.log(options);
                        //    $.get('/Services/GetServices', function (data) {
                        //        tests = data;

                        $("<div>")
                            .dxDataGrid({
                                columnAutoWidth: true,
                                showBorders: true,
                                filterRow: {
                                    visible: true,
                                    applyFilter: "auto"
                                },
                                headerFilter: {
                                    visible: true
                                },
                                editing: {
                                    mode: "row",
                                    allowUpdating: true,
                                    useIcons: true
                                },
                                columns: [
                                    {
                                        dataField: "key",
                                        caption: "Desc",
                                        allowEditing: false,

                                    },
                                    {
                                        dataField: "shippingAgentName",
                                        caption: "Account Of",
                                        allowEditing: false,

                                    },
                                    {
                                        dataField: "shippingLineName",
                                        caption: "Beneficiary",
                                        allowEditing: false,

                                    },
                                    {
                                        dataField: "amount",
                                        caption: "Amount",
                                        format: "#,##0.##",
                                        allowEditing: false,

                                    },
                                    {
                                        dataField: "chequeNumber",
                                        caption: "Cheque No",
                                        caption: "Cheque Number",
                                    },
                                    {
                                        dataField: "bvp",
                                        caption: "bvp",
                                    },
                                    {
                                        dataField: "bankId",
                                        caption: "Bank",
                                        validationRules: [{ type: "required" }],
                                        lookup: {
                                            dataSource: BankList,
                                            valueExpr: "bankId",
                                            displayExpr: "bankName"
                                        }
                                    },
                                    {
                                        dataField: "bankBranchId",
                                        caption: "Branch",
                                        validationRules: [{ type: "required" }],
                                        lookup: {
                                            dataSource: BankBrachList,
                                            valueExpr: "bankBranchId",
                                            displayExpr: "bankBranchName"
                                        }
                                    },
                                    {
                                        dataField: "remarks",
                                        caption: "Remarks",
                                    },



                                ],
                                onRowUpdated: function (e) {
                                    console.log(e);
                                    var data = e.data;
                                    $.post('/Import/PreAlertPayOrder/UpdatePreAlertPayOrderDetail', { data, data }, function (data) {


                                        showToast(data.message, "success");
                                    });
                                },
                                dataSource: new DevExpress.data.DataSource({
                                    store: new DevExpress.data.ArrayStore({
                                        data: options.data.preAlertPayOrderDetails
                                    }),
                                })
                            }).appendTo(container);
                        //    });
                        //});
                    }
                },



                //onEditingStart: function (e) {
                //    //console.log(e);
                //    var data = e.data ;
                //    console.log("data",data)
                //},
                onEditorPreparing: function (e) {
                    // hideMenifestedColumns(e);
                },
                onRowRemoved: function (e) {
                    console.log(e)
                    var key = e.data.preAlertPayOrderId;

                    console.log("key", key);

                    $.post('/Import/PreAlertPayOrder/DeletePreAlertPayOrder?key=' + key, function (result) {

                        if (result.error) {
                            showToast(result.message, "warning");
                        }

                        else {

                            showToast(result.message, "success");

                        }

                        window.setInterval('refresh()', 3000);

                    })

                }

            });





        });


    }

    function PrintPayorder(data) {
        console.log("data", data)
        var preAlertPayOrderNumber = data.preAlertPayOrderNumber;
    var bank = data.bankId;
    //top.location.href = '/Import/Reports/PayOrderReport?payorderNumber=' + preAlertPayOrderNumber;

    window.open('/Import/Reports/PayOrderReport?payorderNumber=' + preAlertPayOrderNumber + '&bankcode=' + bank, "_blank");

    }
    function refresh() {
        window.location.reload();
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

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {



        checkColumn(e, "preAlertPayOrderNumber");

    checkColumn(e, "requestNumber");
    checkColumn(e, "payOrderCreatedDate");
         
    }

    function ShowDetail() {


        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;

    GetPreAlterPayOrderByDate(fromdate , todate);
    }

