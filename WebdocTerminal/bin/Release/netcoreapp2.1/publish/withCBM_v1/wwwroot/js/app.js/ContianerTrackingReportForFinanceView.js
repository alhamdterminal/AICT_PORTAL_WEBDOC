
    function myFunction() {
        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    var ShippingAgent = $("#agentId option:selected").val();
    var type = document.getElementById('type').value;
    var Containerno = $("#containerlist").dxAutocomplete("instance").option("value");

    if (Containerno == "" || Containerno == "null" || Containerno == undefined || Containerno == "undefined" || Containerno == null) {
        Containerno = "";
        }

    loadingBar();
    $.get('/Import/Reports/ViewContianerTrackingReportForFinance?fromDate=' + fromdate + '&todate=' + todate + '&shippingagnet=' + ShippingAgent + '&type=' + type + '&Containerno=' + Containerno, function (data) {

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


    }