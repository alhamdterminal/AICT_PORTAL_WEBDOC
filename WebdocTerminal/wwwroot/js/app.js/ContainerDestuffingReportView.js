


    var containerno = "";

    function igm_changed(data) {
        containerno = "";
    containerno = data.value;
    }




    function myFunction() {

        console.log("containerno", containerno)

        var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;

    var ShippingAgent = $("#agentId option:selected").val();

    loadingBar();

    $.get('/Import/Reports/ViewContainerDestuffingReport?line=' + ShippingAgent + '&fromdate=' + fromdate + '&todate=' + todate + '&containerno=' + containerno, function (data) {
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

