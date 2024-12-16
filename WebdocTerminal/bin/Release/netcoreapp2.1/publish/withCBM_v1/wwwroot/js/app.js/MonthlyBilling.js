
    var igm = "";
    var containerNo = "";
    var indexNo = "";

    var subBill = "";
    function handleClick(cb) {
        if (cb.checked == true) {
        subBill = '1'

    }
    else {
        subBill = ""
    }

    }
    function igm_changed(data) {

        console.log(document.getElementById('type').value)
        igm = data.value;


    if (document.getElementById('type').value == "CY") {
        $.get('/APICalls/GetCYContainersByIGMNumber?IGM=' + igm, function (data) {

            var containerNumbers = data;
            $("#containerBox").dxSelectBox({
                dataSource: containerNumbers,
                displayExpr: "value",
                acceptCustomValue: true,
                onValueChanged: function (data) {
                    containerNo = data.value.value


                    $.get('/APICalls/GetCYIndexByContainerNo?containerNo=' + containerNo, function (indexdb) {

                        var indexes = indexdb;
                        $("#indexBox").dxSelectBox({
                            dataSource: indexes,
                            displayExpr: "value",
                            acceptCustomValue: true,
                            onValueChanged: function (data) {
                                indexNo = data.value.value



                            },

                        })

                    });
                },

            })
        })
    }

    if (document.getElementById('type').value == "CFS") {
        $.get('/APICalls/GetCFSContainersByIGMNumber?IGM=' + igm, function (data) {

            var containerNumbers = data;
            $("#containerBox").dxSelectBox({
                dataSource: containerNumbers,
                displayExpr: "value",
                acceptCustomValue: true,
                onValueChanged: function (data) {
                    containerNo = data.value.value


                    $.get('/APICalls/GetCFSIndexByContainerNo?containerNo=' + containerNo, function (indexdb) {

                        var indexes = indexdb;
                        $("#indexBox").dxSelectBox({
                            dataSource: indexes,
                            displayExpr: "value",
                            acceptCustomValue: true,
                            onValueChanged: function (data) {
                                indexNo = data.value.value



                            },

                        })

                    });
                },

            })
        })
    }
    }

    function myFunction() {
        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    var type = document.getElementById('type').value
    var party = document.getElementById("shippingAgent").value;
    var invoiceNumber = document.getElementById("invoiceNumber").value;
    var fromRange = document.getElementById("fromRange").value;
    var toRange = document.getElementById("toRange").value;
    var cbm = document.getElementById("cbm").value;


    loadingBar();

    $.get('/Import/Reports/ViewMonthlyBilling?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'type=' + type + '&' + 'party=' + party
    + '&' + 'invoiceNumber=' + invoiceNumber + '&' + 'fromRange=' + fromRange + '&' + 'toRange=' + toRange + '&' + 'igm=' + igm
    + '&' + 'indexNo=' + indexNo + '&' + 'cbm=' + cbm + '&' + 'containerNo=' + containerNo + '&' + 'subBill=' + subBill
    , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }

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


    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "IGM" && x.isChecked == false)) {

        document.getElementById('IGM').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "InvoiceNo" && x.isChecked == false)) {

        document.getElementById('invoiceNumber').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "CBM" && x.isChecked == false)) {

        document.getElementById('cbm').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "SubBill" && x.isChecked == false)) {

        document.getElementById('isnegative').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Agent" && x.isChecked == false)) {

        document.getElementById('shippingAgent').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "From" && x.isChecked == false)) {

        document.getElementById('fromRange').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "To" && x.isChecked == false)) {

        document.getElementById('toRange').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Type" && x.isChecked == false)) {

        document.getElementById('type').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {

        document.getElementById('single_cal2').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {

        document.getElementById('single_cal3').disabled = true;
        }

    }