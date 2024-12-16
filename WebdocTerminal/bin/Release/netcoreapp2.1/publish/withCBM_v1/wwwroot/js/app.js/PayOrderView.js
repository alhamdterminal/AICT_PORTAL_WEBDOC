

    var PreAlertPayorderDetail = [];


    $(function () {

        getPayOrderNumber();
    resttablevalues();

    });


    function resttablevalues() {
        $('#totalnoofpos').val(0);
    $('#totalpoamount').val(0);
    $('#Selectednoofpos').val(0);
    $('#Selectedpoamount').val(0);
    $('#Balancenoofpos').val(0);
    $('#Balancenoofpos').val(0);

    }

    function refresh() {
        window.location.reload();
    }


    function getPayOrderNumber() {

        $.get('/Import/PreAlertPayOrder/GenPreAlertPayOrderNumber', function (data) {

            console.log("data", data);

            $('#preAlertPayOrderNumber').val(data);

        });

    }



    function getpayorders(requestNumber) {
        console.log("requestNumber", requestNumber)
        $.get('/Import/PreAlertPayOrder/GenPaymentUpdateCode?RequestNumber=' + requestNumber, function (data) {
        console.log("data", data)

            PreAlertPayorderDetail = data;

    loadgrid(PreAlertPayorderDetail);

            //var tariffAmountTotal = 0;

            //if (data) {
        //    data.forEach(c => {
        //        tariffAmountTotal += Number(c.amount);
        //    });
        //}

        //tariffAmountTotal = tariffAmountTotal.toFixed(2);

        //$('#tariffAmountTotal').val(tariffAmountTotal);

    });
    }

    function loadgrid(PreAlertPayorderDetail) {

        var grid = $("#PreAlertPayorderDetails").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    var dataGrid = $("#PreAlertPayorderDetails").dxDataGrid({
        dataSource: PreAlertPayorderDetail,
    keyExpr: "",
    wordWrapEnabled: true,
    showBorders: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
            },

    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

            },

    editing: {

        allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click",
    mode: "cell"
            },

    columns: [
    {
        dataField: "preAlertNumber",
    caption: "Alert No"
                },
    {
        dataField: "preAlertPayOrderDate",
    caption: "Date",
    validationRules: [{type: "required" }],
    dataType: 'date',
    format: 'dd/MM/yyyy',
                },
    {
        dataField: "containerNumber",
    caption: "Container No"
                },
    {
        dataField: "size",
    caption: "Size"
                },
    "description",
    {
        dataField: "shippingAgentName",
    caption: "Account Of"
                },
    {
        dataField: "shippingLineName",
    caption: "Beneficiary"
                },
    {
        dataField: "amount",
    format: "#,##0.##",
    dataType: "number",
    caption: "Amount"
                },
    {
        dataField: "remarks",
    width: 150,
    caption: "Remarks"
                },
    {
        dataField: "isPreAlertPayOrder",
    caption: ""

                },


    ],

    summary: {
        totalItems: [


    {
        column: "amount",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },

    {
        name: 'SelectedRowCount',
    summaryType: 'custom',
    showInColumn: 'isPreAlertPayOrder',
                    }
    ],
    calculateCustomSummary: function (options) {

                    if (options.name === "SelectedRowCount") {
                        if (options.summaryProcess === "finalize") {

                            var totalnoofpos = 0;
    var totalpoamount = 0;
    var Selectednoofpos = 0;
    var Selectedpoamount = 0;
    var Balancenoofpos = 0;
    var Balancepoamount = 0;


    totalnoofpos = PreAlertPayorderDetail.length;

    if (PreAlertPayorderDetail.length) {
        PreAlertPayorderDetail.forEach(c => {

            totalpoamount += c.amount;

        });
                            }

    if (PreAlertPayorderDetail.length) {
        PreAlertPayorderDetail.forEach(c => {

            if (c.isPreAlertPayOrder == true) {
                Selectednoofpos += 1;
                Selectedpoamount += c.amount;

            }
        });
                            }

    if (PreAlertPayorderDetail.length) {
        PreAlertPayorderDetail.forEach(c => {

            if (c.isPreAlertPayOrder == false) {
                Balancenoofpos += 1;
                Balancepoamount += c.amount;

            }
        });
                            }



    $('#totalnoofpos').val(totalnoofpos);
    $('#totalpoamount').val(totalpoamount);
    $('#Selectednoofpos').val(Selectednoofpos);
    $('#Selectedpoamount').val(Selectedpoamount);
    $('#Balancenoofpos').val(Balancenoofpos);
    $('#Balancepoamount').val(Balancepoamount);
                            //PreAlertPayorderDetail.forEach(c => {
        //    if (c.isPreAlertPayOrder == true) {
        //        sumeAmount += c.amount;
        //    }

        //    totlamt += c.amount;
        //});


        //var sumeAmount = 0;
        //var totlamt = 0;
        //var balncamt = 0;

        // console.log("PreAlertPayorderDetail", PreAlertPayorderDetail)

        //PreAlertPayorderDetail.forEach(c => {
        //    if (c.isPreAlertPayOrder == true) {
        //        sumeAmount += c.amount;
        //    }

        //    totlamt += c.amount;
        //});

        //balncamt = totlamt - sumeAmount;
        //const total = PreAlertPayorderDetail.filter(c => c.isPreAlertPayOrder == true).length;

        //options.totalValue = `Total Selected: ${total} And Total Amount is : ${sumeAmount} and Balance Amount is  : ${balncamt}`;
    }
                    }
                }
            },

    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
            },
    onRowRemoved: function (e) {

    }

        }).dxDataGrid("instance");


    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');
    }



    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "preAlertNumber");
    checkColumn(e, "preAlertPayOrderDate");
    checkColumn(e, "containerNumber");
    checkColumn(e, "amount");
    checkColumn(e, "description");
    checkColumn(e, "size");
    checkColumn(e, "shippingAgentName");
    checkColumn(e, "shippingLineName");
    checkColumn(e, "remarks");


    }

    function formfiled() {

    }

    function requestNumber_changed(data) {
        console.log("data", data)

        var requestNumber = data;

    getpayorders(requestNumber)

    }


    function selectAll() {

        PreAlertPayorderDetail.forEach(c => c.isPreAlertPayOrder = true);

    loadgrid(PreAlertPayorderDetail);

    }
    function unselectAll() {

        PreAlertPayorderDetail.forEach(c => c.isPreAlertPayOrder = false);

    loadgrid(PreAlertPayorderDetail);

    }

    function submitPreAlterPayorder() {


        var chequeNumberres = $("#chequeNumber").val();
    if (!chequeNumberres) {
            //return showToast("Cheque Number not define", "error")
            return alert("Cheque Number not define");
        }

    var bvpres = $("#bvp").val();
    if (!bvpres) {
            //return showToast("BVP not define", "error")
            return alert("BVP not define");

        }

    var bankIdres = $("#bankId option:selected").val();
    if (!bankIdres) {
            //return showToast("Bank not define", "error")
            return alert("Bank not define");
        }

    var bankBranchIdres = $("#bankBranchId option:selected").val();
    if (!bankBranchIdres) {
            //return showToast("Bank Branch not define", "error")
            return alert("Bank Branch not define");
        }


    var form = document.getElementById('PayOrderForm');
    form.reportValidity();
    if (form.checkValidity()) {



            var values = $('#PayOrderForm').serialize();
            var datalist = PreAlertPayorderDetail.filter(c => c.isPreAlertPayOrder == true);

    console.log("values", values)
    console.log("datalist", datalist)

            if (datalist.length > 0) {

                var sumeAmount = 0;

                datalist.forEach(c => {

        sumeAmount += c.amount;

                });

    console.log("sumeAmount", sumeAmount);

                //if (datalist.length > 15) {
        //    //return showToast("can't save more then 15 records", "error")
        //    return alert("can't save more then 15 records");
        //}

        //if (Number(sumeAmount) > 1000000) {
        //    //return showToast("can't save more then 1000 amount", "error")
        //    return alert("can't save more then 1000 amount");
        //}
        //else {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 60000);

    $.post('/Import/PreAlertPayOrder/AddPreAlertPayOrder?' + values, {Details: datalist }, function (result) {

        console.log("result", result)
                        if (result.error) {
                            //showToast(result.message, "warning");
                            return alert(result.message);
                        }

    else {


        //showToast(result.message, "success");
        alert(result.message);

    //window.open('/Import/PreAlertPayOrder/UpdatePreAlertPayorder', "_blank");
    window.open('/Import/Reports/PayOrderReport?payorderNumber=' + result.payOrderNo + '&bankcode='+  $('#bankId').val()  , "_blank");

                        }

    window.setInterval('refresh()', 3000);


                    })
                //}


            }
    else {
        alert("Add at least 1 Detail");
            }

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





