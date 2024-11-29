

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


    function myFunction() {

        var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;

    console.log("fromdate", fromdate);
    console.log("todate", todate);
    var shippingAgentId = $("#shippingAgent option:selected").val();
    console.log("shippingAgentId ", shippingAgentId)

    loadingBar();

    $.get('/Import/Reports/ViewCFSReleasedContainers?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'shippingAgentId=' + shippingAgentId, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    }


    function formfiled() {


    }