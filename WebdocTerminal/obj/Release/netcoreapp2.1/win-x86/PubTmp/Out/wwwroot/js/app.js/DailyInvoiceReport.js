

    var virNumber;
    var virNoSouce = [];
    var clearingAgents = [];
    var type;
    var clearingAgent;
    $(function () {
        clearingAgents = [];
    virNoSoucbe = [];
    getClearingAgent();
    getvir();
    })
    function typeChange() {
        type = document.getElementById('type').value
        virNoSoucbe = [];
    clearingAgents = [];
    getvir();
    getClearingAgent();
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


    function getvir() {

        $.get('/APICalls/GetIGMS', function (data) {
            virNoSouce = data.data;
            PrintVir(virNoSouce)
        })



    }

    function getClearingAgent() {

        $.get('/APICalls/GetClearingAgentsImport', function (data) {
            clearingAgents = data;
            PrintClearingAent(clearingAgents);
        })


    }



    function PrintVir(virNoSouce) {

        $("#VIRNO").dxAutocomplete({
            dataSource: virNoSouce,
            placeholder: "Type VIR Number...",
            onValueChanged: function (data) {

                virNumber = data.value;
            }
        });
    virNoSouce = []
    }


    function PrintClearingAent(clearingAgents) {

        $("#clearingAgentSource").dxSelectBox({
            dataSource: clearingAgents,
            displayExpr: "text",
            valueExpr: "value",
            onValueChanged: function (data) {

                clearingAgent = data.value;
            }
        });

    clearingAgents = []
    }


    function myFunction() {
        if (type) {


            var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    var invoiceNo = document.getElementById("invoiceNo").value;
    var fromRang = document.getElementById("fromRang").value;
    var toRang = document.getElementById("toRang").value;

    if (virNumber) {

    }
    else {
        virNumber = ""
    }
    if (clearingAgent) {

    }
    else {
        clearingAgent = ""
    }



    loadingBar();
    $.get('/Import/Reports/ViewDailyInvoiceReport?type=' + type + '&' + 'fromdate=' + fromdate + '&' + 'todate=' + todate
    + '&' + 'virNumber=' + virNumber + '&' + 'invoiceNumber=' + invoiceNo + '&' + 'invoiceFromAmount=' + fromRang
    + '&' + 'invoiceToAmount=' + toRang + '&' + 'clearingAgent=' + clearingAgent, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
                });
        }
    else {
        alert("Please Select Type")
    }

    }




    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "ClearingAgent" && x.isChecked == false)) {

        document.getElementById('clearingAgentSource').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "InvoiceNo" && x.isChecked == false)) {

        document.getElementById('invoiceNo').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "VIR" && x.isChecked == false)) {

        document.getElementById('VIRNO').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {

        document.getElementById('single_cal2').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {

        document.getElementById('single_cal3').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Type" && x.isChecked == false)) {

        document.getElementById('type').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "FromRange" && x.isChecked == false)) {

        document.getElementById('fromRang').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ToRange" && x.isChecked == false)) {

        document.getElementById('toRang').disabled = true;
        }


    }

