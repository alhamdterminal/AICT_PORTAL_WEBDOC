
    $(function () {
        $('#voucherid').val(0);
    $('#vouchercode').val('');
    GetServiceType();
    getchartofaccounts();
    getdepartments();
    getcustomers();

    });


    function showvoucherCode() {

        Grid();
    }

    function formfiled() {

    }

    var VoucherServices = [];
    var ChartOfAccounts = [];
    var Departments = [];
    var Customers = [];
    var VoucherDetail = [];
    var filterServiceType = false;
    var filtercredittype = false;
    var filterdebittype = false;


    function GetServiceType() {
        $.get('/Setup/GetVoucherServiceType', function (data) {
            VoucherServices = data;
            console.log(VoucherServices);
        });
    }

    function getchartofaccounts() {
        $.get('/Account/ChartOfAccount/GetChartOfAccountNameAndCode', function (data) {
            ChartOfAccounts = data;
        });
    }

    function getdepartments() {
        $.get('/Setup/GetDepartment', function (data) {
            Departments = data;
        });
    }

    function getcustomers() {
        $.get('/Setup/GetCustomersNameAndCode', function (data) {
            Customers = data;
        });
    }

    function Grid() {


        $("#gridContainer").dxDataGrid({
            dataSource: VoucherDetail,
            keyExpr: "voucherDetailId",
            showBorders: true,
            allowColumnResizing: true,
            columnAutoWidth: true,
            dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
            paging: {
                pageSize: 10
            },
            searchPanel: {
                visible: true,
                width: 240,
                placeholder: "Search..."
            },

            editing: {
                mode: 'row',
                allowAdding: true,
                allowUpdating: true,
                allowDeleting: true,
            },

            columns: [

                {
                    dataField: "voucherServiceTypeId",
                    caption: "Service Type",
                    setCellValue(rowData, value, e) {

                        rowData.voucherServiceTypeId = value;
                        rowData.chartOfAccountId = null;
                        if (VoucherServices.find(x => x.voucherServiceTypeId == value)) {
                            if (VoucherServices.find(x => x.voucherServiceTypeId == value).code == "CD" || VoucherServices.find(x => x.voucherServiceTypeId == value).code == "P") {
                                filterServiceType = true;
                            }
                            else {
                                filterServiceType = false;
                            }
                        }
                        else {
                            filterServiceType = false;
                        }

                    },
                    lookup: {
                        dataSource: {
                            store: VoucherServices,
                            requireTotalCount: true,
                            pageSize: 4,
                            paginate: true,
                        },
                        displayExpr: function (item) {
                            return item && item.code + "-" + item.name;
                        },
                        valueExpr: "voucherServiceTypeId"
                    },
                    validationRules: [{ type: "required" }]

                },
                {
                    dataField: "customerId",
                    caption: "Customer",
                    width: 250,
                    //validationRules: [{
                    //    type: "custom",
                    //    validationCallback: function (e, s) {
                    //        if (filterServiceType == true) {
                    //            return false;
                    //        }
                    //        else {
                    //            return true;
                    //        }
                    //    }
                    //}],
                    lookup: {
                        dataSource: {
                            store: Customers,
                            requireTotalCount: true,
                            pageSize: 4,
                            paginate: true,
                        },
                        displayExpr: "name",
                        valueExpr: "customerId",
                        allowClearing: true,
                    },
                    setCellValue(rowData, value, e) {


                        if (filterServiceType == true) {
                            var resdata = Customers.find(x => x.customerId == value);

                            if (resdata) {
                                rowData.chartOfAccountId = resdata.chartOfAccountId;
                            }
                            else {
                                rowData.chartOfAccountId = null;
                            }
                        }
                        else {
                            rowData.chartOfAccountId = null;
                        }
                        rowData.customerId = value;


                    },

                },
                {
                    dataField: "chartOfAccountId",
                    caption: "Control Account",
                    lookup: {
                        dataSource: {
                            store: ChartOfAccounts,
                            requireTotalCount: true,
                            pageSize: 4,
                            paginate: true,
                        },
                        displayExpr: "accountName",
                        valueExpr: "chartOfAccountId"
                    },
                    validationRules: [{ type: "required" }]

                },
                {
                    dataField: "departmentId",
                    caption: "Department",
                    lookup: {
                        dataSource: {
                            store: Departments,
                            requireTotalCount: true,
                            pageSize: 4,
                            paginate: true,
                        },
                        displayExpr: function (item) {
                            return item && item.departmentCode + "-" + item.departmentName;
                        },
                        valueExpr: "departmentId"
                    },
                    validationRules: [{ type: "required" }]

                },

                {
                    dataField: "debit",
                    validationRules: [{ type: "required" }],
                    caption: "Debit",
                    dataType: "number",
                    format: "#,##0.##",
                    width: 120,
                    editorOptions: {
                        step: 0
                    },

                    setCellValue(rowData, value, e) {

                        rowData.debit = value;
                        rowData.credit = 0;

                    }


                },
                {
                    dataField: "credit",
                    validationRules: [{ type: "required" }],
                    caption: "Credit",
                    dataType: "number",
                    format: "#,##0.##",
                    width: 120,
                    editorOptions: {
                        step: 0
                    },
                    setCellValue(rowData, value, e) {

                        rowData.credit = value;
                        rowData.debit = 0;

                    }
                },
                {
                    dataField: "chequeOrReference",
                    caption: "Ref/Cheq#",
                },
                {
                    dataField: "narration",
                    validationRules: [{ type: "required" }],
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
                ]
            },

            onEditorPreparing: function (e) {
                hideMenifestedColumns(e);
            },

            onRowInserting: function (e) {

                if (e.data.debit > 0 && e.data.credit > 0) {
                    showToast("debit and credit both are > 0", "error");
                    e.cancel = true;

                }

                else if (e.data.debit <= 0 && e.data.credit <= 0) {
                    showToast("debit and credit both are < 0", "error");
                    e.cancel = true;

                }

                var grid = $("#gridContainer").dxDataGrid("instance");
                grid.addRow();

            },
            onRowUpdating: function (e) {
                console.log("e", e)

                const result = $.extend(e.oldData, e.newData);
                console.log("result", result)

                if (result.debit > 0 && result.credit > 0) {
                    showToast("debit and credit both are > 0", "error");
                    e.cancel = true;

                }

                else if (result.debit <= 0 && result.credit <= 0) {
                    showToast("debit and credit both are < 0", "error");
                    e.cancel = true;
                }

            },
            //onRowInserted: function (e) {
            //    var grid = $("#gridContainer").dxDataGrid("instance");
            //    grid.addRow();

            //}

            //onRowInserted: function (e) {

            //    console.log(e.data)
            //    var voucherid = $('#voucherid').val();
            //    if (e.data.debit > 0 && e.data.credit > 0) {
            //        getVoucherDetails(voucherid);
            //        return showToast("debit and credit both are > 0", "error");
            //    }
            //    else if (e.data.debit <= 0 && e.data.credit <= 0) {
            //        getVoucherDetails(voucherid);
            //        return showToast("debit and credit both are < 0", "error");
            //    }
            //    else {
            //        AddVoucher(e.data);
            //    }
            //},

            //onRowUpdated: function (e) {
            //    var voucherid = $('#voucherid').val();
            //    console.log(e.data);
            //    if (e.data.debit > 0 && e.data.credit > 0) {
            //        getVoucherDetails(voucherid);
            //        return showToast("debit and credit both are > 0", "error")
            //    }
            //    else if (e.data.debit <= 0 && e.data.credit <= 0) {
            //        getVoucherDetails(voucherid);
            //        return showToast("debit and credit both are < 0", "error")
            //    }

            //    else {
            //        var model = e.data;
            //        $.post('/Account/Voucher/UpdateVoucherDetail', { model, model }, function (data) {
            //            if (data.error == true) {
            //                showToast(data.message, "error")
            //                getVoucherDetails(voucherid);
            //            } else {
            //                showToast(data.message, "success")
            //                getVoucherDetails(voucherid);
            //            }
            //        });
            //    }
            //},

            //onRowRemoved: function (e) {
            //    var voucherid = $('#voucherid').val();
            //    var model = e.data;
            //    $.post('/Account/Voucher/DeleteVoucherDetail', { model, model }, function (data) {
            //        if (data.error == true) {
            //            showToast(data.message, "error")
            //            getVoucherDetails(voucherid);
            //        } else {
            //            showToast(data.message, "success")
            //            getVoucherDetails(voucherid);
            //        }
            //    });
            //},

        });

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


    }

    function AddVoucher(model) {

        var voucherTypeId = $('#voucherTypeId').val();
    var single_cal2 = $('#single_cal2').val();
    var vouchercode = $('#vouchercode').val();
    var voucherid = $('#voucherid').val();

    if (voucherTypeId) {

        console.log("vouchercode", vouchercode);
    console.log("voucherid", voucherid);

    if (vouchercode && voucherid) {

        $.post('/Account/Voucher/AddVoucherDetailOnly?VoucherId=' + voucherid, { model, model }, function (data) {
            if (data.error == true) {
                showToast(data.message, "error");
                getVoucherDetails(voucherid);

            } else {

                showToast("Created", "success");

                getVoucherDetails(voucherid)

            }
        });
            }
    else {
        $.post('/Account/Voucher/AddVoucher?voucherTypeId=' + voucherTypeId + '&date=' + single_cal2, { model, model }, function (data) {
            if (data.error == true) {
                showToast(data.message, "error");
                var voucherid = $('#voucherid').val();
                //if (voucherid) {
                getVoucherDetails(voucherid);
                //     }
                //     else {
                //         VoucherDetail.splice(model, 1);
                //         Grid();
                //     }
            } else {
                showToast("Created", "success");

                $('#voucherid').val(data.message.voucherId);
                $('#vouchercode').val(data.message.voucherNo);

                var voucherid = $('#voucherid').val();
                //if (voucherid) {
                getVoucherDetails(voucherid)
                //}
                //else {
                //    getVoucherDetails(0)
                //}
            }
        });
            }


        }
    else {
        showToast("please select voucher type", "error");
        }

    }


    function AddVoucherList() {

        var voucherTypeId = $('#voucherTypeId').val();
    var single_cal2 = $('#single_cal2').val();
    var vouchredetaildata = $("#gridContainer").dxDataGrid("option", "dataSource");

    console.log("vouchredetaildata", vouchredetaildata)

    if (voucherTypeId && vouchredetaildata.length) {

        $.post('/Account/Voucher/AddVoucherList?voucherTypeId=' + voucherTypeId + '&date=' + single_cal2, { model: vouchredetaildata }, function (data) {
            if (data.error == true) {

                alert(data.message);
                //showToast(data.message, "error");                         
            } else {

                $('#voucherid').val(data.message.voucherId);
                $('#vouchercode').val(data.message.voucherNo);

                alert("Voucher No  " + data.message.voucherNo + " Created ");

                //window.location.href = window.location.href;

                //$('#voucherid').val(data.message.voucherId);
                //$('#vouchercode').val(data.message.voucherNo);
                //var voucherid = $('#voucherid').val();                         
                //getVoucherDetails(voucherid)
                //$("#btnSubmitsave").attr("disabled", false);

            }
        });
            


        }
    else {
        showToast("please select voucher type and add voucher detail", "error");
        }

    }



    function getVoucherDetails(voucherid) {

        if (voucherid) {

        $.post('/Account/Voucher/GetVoucherDetail?voucherid=' + voucherid, function (data) {
            VoucherDetail = data;
            Grid();
        });
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

    //var progressBarStatus;
    //let seconds = 10;
    //let inProgress = false;
    //let intervalId;

    function formfiled() {


        //progressBarStatus = $('#progressBarStatus').dxProgressBar({
        //    min: 0,
        //    max: 100,
        //    width: '100%',
        //    statusFormat(ratio) {
        //        return `Loading: ${ratio * 100}%`;
        //    },
        //    onComplete(e) {
        //        inProgress = false;
        //       /* progressButton.option('text', 'Restart progress');*/
        //        e.element.addClass('complete');
        //    },
        //}).dxProgressBar('instance');

    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "chartOfAccountId");

    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    console.log("filterServiceType", filterServiceType);

    if (filterServiceType == true) {

        e.editorOptions.disabled = true;

        }

    }

    function PrintDetail() {

        
        var voucherid = $('#voucherid').val();

    if (voucherid != 0 && voucherid != "") {
        window.open('/Account/Reports/SingleVoucherView?VoucherId=' + voucherid, "_blank");
        }
    else {
        showToast("no voucher found", "error");
        }

    window.location.href = window.location.href;
    }


    function loadingbarwork() {

        $('#progressBarStatus').removeClass('complete');
    if (inProgress) {
        //progressButton.option('text', 'Continue progress');
        clearInterval(intervalId);
        } else {
        //progressButton.option('text', 'Stop progress');
        setCurrentStatus();
    intervalId = setInterval(timer, 1000);
        }
    insProgress = !inProgress;
    }

    function setCurrentStatus() {
        progressBarStatus.option('value', (10 - seconds) * 10);
    $('#timer').text((`0${seconds}`).slice(-2));
    }

    function timer() {
        seconds -= 1;
    setCurrentStatus();
    if (seconds === 0) {
        clearInterval(intervalId);
    seconds = 10;
        }
    }



