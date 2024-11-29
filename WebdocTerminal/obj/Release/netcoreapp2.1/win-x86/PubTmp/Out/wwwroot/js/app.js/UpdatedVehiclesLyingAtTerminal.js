


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

        console.log("test")
        console.log("test", document.getElementById("fromdate").value)
    console.log("test", document.getElementById("todate").value)
    console.log("test", document.getElementById("type").value)
    console.log("test", document.getElementById("cargotype").value)
    console.log("test", document.getElementById("shippingAgent").value)
    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;
    var type = document.getElementById("type").value;
    var cargotype = document.getElementById("cargotype").value;
    var shippingAgent = document.getElementById("shippingAgent").value;

    console.log("shippingAgent", shippingAgent);
    console.log("type", type);
    console.log("cargotype", cargotype);



    loadingBar();

    $.get('/Import/Reports/ViewUpdatedVehiclesLyingAtTerminal?fromdate=' + fromdate + '&' + 'toDate=' + todate + '&' + 'type=' + type + '&' + 'cargotype=' + cargotype + '&' + 'shippingAgent=' + shippingAgent, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    }

    function formfiled() {



    }