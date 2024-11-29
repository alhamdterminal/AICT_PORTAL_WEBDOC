


    function sendData() {

        var doNum = $('#deliveryorderNumbr').val();

    //var doNum = SearchDOForm.elements["deliveryorder"].value;

    console.log("doNum", doNum)
    if (doNum) {
        $.post('/Invoice/UpdateDoValidity?doNum=' + doNum, function (resdata) {

            if (resdata.error == true) {
                showToast(resdata.message, "error")

            } else {
                showToast(resdata.message, "success")



                DeliveryOrderNumber = resdata.deliveryOrderNumber;
                console.log("DeliveryOrderNumber", DeliveryOrderNumber)

                if (DeliveryOrderNumber) {
                    window.open('/import/reports/DeliveryOrder?DeliveryOrderNumber=' + DeliveryOrderNumber, "_blank");
                }


            }

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


    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Donumber" && x.isChecked == false)) {
        document.getElementById('donumber').disabled = true;

        }
    }

    function chanegdonumber(data) {

        console.log("data", data);


    $.get('/APICalls/GetTotaldeliveredandbalanceqty?dono=' + data.value, function (res) {

        console.log("res", res);


    $('#totalqty').val(res.value.totalqty);
    $('#balanceqty').val(res.value.balanceqty);

        });

    }



    var igm;



    function igm_changed(data) {

        igm = data.value;

    $.get('/APICalls/GetIGMOIndexDropdown?IGM=' + igm, function (indexdb) {

        $('#indexdiv').html(indexdb);


        });


    }


    function loadGridindexwise() {


        var indexnumber = $("#indexdb option:selected").text();



    if (indexnumber) {

        $.get('/APICAlls/GetMultipleDosByIgmIndex?igm=' + igm + "&index=" + indexnumber, function (data) {

            console.log("data", data)
            Indexinvoices = data;


            var dataGrid = $("#indexinvoices").dxDataGrid({
                dataSource: Indexinvoices,
                keyExpr: "deliveryOrderId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
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
                    mode: "row",
                    allowUpdating: false,
                    selectTextOnEditStart: false,
                    startEditAction: "click"
                },
                columns: [
                    {
                        dataField: "date",
                        caption: "Invoice Date",
                        dataType: 'date',
                        format: 'dd/MM/yyyy'
                    },
                    "doNumber",
                    "invoiceNo",
                    {
                        dataField: "validTo",
                        caption: "Valid To",
                        dataType: 'date',
                        format: 'dd/MM/yyyy'
                    },
                    {
                        width: 100,
                        alignment: 'center',
                        cellTemplate: function (container, options) {
                            $('<a/>').addClass('dx-link')
                                .text('Print')
                                .on('dxclick', function () {
                                    print_do(options.row.data)
                                })
                                .appendTo(container);
                        }
                    },




                ],
                onEditorPreparing: function (e) {
                    //  hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {

                    $("#btnSubmit").attr("disabled", false);
                }

            }).dxDataGrid("instance");

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });

        }
    else {
        showToast("PLease Select Index", "error");
        }

    }



    function print_do(data) {

        console.log("data", data);


    if (data.doNumber) {
        $.post('/Invoice/UpdateDoValidity?doNum=' + data.doNumber, function (resdata) {

            if (resdata.error == true) {
                showToast(resdata.message, "error")

            } else {

                showToast(resdata.message, "success");

                DeliveryOrderNumber = resdata.deliveryOrderNumber;
                console.log("DeliveryOrderNumber", DeliveryOrderNumber)

                if (DeliveryOrderNumber) {
                    window.open('/import/reports/DeliveryOrder?DeliveryOrderNumber=' + DeliveryOrderNumber, "_blank");
                }
                //   top.location.href = '/import/Reports/DeliveryOrder?DeliveryOrderNumber=' + data.doNumber;

            }

        });

        }

    }

