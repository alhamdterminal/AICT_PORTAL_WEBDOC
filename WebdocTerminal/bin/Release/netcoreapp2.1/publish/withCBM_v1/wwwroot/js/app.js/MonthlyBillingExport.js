
    var containerNo = "";
    var GDNo = "";
    var ws = ""

    $(function () {
        $.get('/APICalls/GetExportContainers', function (data) {
            var containers = data;
            containerNo = "";
            GDNo = "";
            virNo = "";
            $("#containerBox").dxSelectBox({
                dataSource: containers,
                displayExpr: "text",
                searchEnabled: true,
                onValueChanged: function (data) {
                    if (data.value) {

                        containerNo = data.value.text

                        getContainerGDs(data.value.value);
                    }

                },

            }).dxSelectBox("instance");
        })
    })

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

    function getContainerGDs(containerID) {
        $.get('/APICalls/GetExportContainerItemsByContainerId?containerId=' + containerID, function (data) {
            var GDs = data;
            GDNo = "";
            virNo = "";
            $("#GDBox").dxSelectBox({
                dataSource: GDs,
                displayExpr: "text",
                searchEnabled: true,
                onValueChanged: function (data) {
                    if (data.value) {

                        GDNo = data.value.text
                        getVir(data.value.value);
                    }

                },

            }).dxSelectBox("instance");
            if (PermissionInputs.find(x => x.fieldName == "Index" && x.isChecked == false)) {

                document.getElementById('GDBox').style.display = "none";
            }
        })
    }

    function getVir(voyageID) {
        $.get('/APICalls/GetVIRbyVoyageId?voyageId=' + voyageID, function (data) {
            var voyages = data;
            virNo = "";
            $("#VIRBox").dxSelectBox({
                dataSource: voyages,
                displayExpr: "text",
                searchEnabled: true,
                onValueChanged: function (data) {
                    if (data.value) {

                        virNo = data.value.text

                    }

                },

            }).dxSelectBox("instance");

            if (PermissionInputs.find(x => x.fieldName == "VIR" && x.isChecked == false)) {

                document.getElementById('VIRBox').style.display = "none";
            }

        })
    }


    var subBill = "";
    function handleClick(cb) {
        if (cb.checked == true) {
        subBill = '1'

    }
    else {
        subBill = ""
    }
    }

    function myFunction() {

        loadingBar();

    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;
    var party = document.getElementById("shippingAgent").value;
    var invoiceNumber = document.getElementById("invoiceNumber").value;
    var fromRange = document.getElementById("fromRange").value;
    var toRange = document.getElementById("toRange").value;
    var cbm = document.getElementById("cbm").value;

    $.get('/Export/Reports/ViewMonthlyBillingExport?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'party=' + party
    + '&' + 'invoiceNumber=' + invoiceNumber + '&' + 'invoiceFromAmount=' + fromRange + '&' + 'invoiceToAmount=' + toRange + '&' + 'igm=' + virNo
    + '&' + 'indexNo=' + GDNo + '&' + 'cbm=' + cbm + '&' + 'containerNo=' + containerNo + '&' + 'subBill=' + subBill
    , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }

    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Container" && x.isChecked == false)) {

        document.getElementById('containerBox').style.display = "none";
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
        if (PermissionInputs.find(x => x.fieldName == "FromRange" && x.isChecked == false)) {

        document.getElementById('fromRange').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ToRange" && x.isChecked == false)) {

        document.getElementById('toRange').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {

        document.getElementById('fromdate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {

        document.getElementById('todate').disabled = true;
        }

    }


