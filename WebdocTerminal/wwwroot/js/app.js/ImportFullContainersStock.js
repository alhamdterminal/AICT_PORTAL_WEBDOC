




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
    var shippingAgent = document.getElementById("shippingAgent").value;
    var shipingLine = document.getElementById("shipingLine").value;
    var port = document.getElementById("port").value;
    var cargoType = document.getElementById("cargoType").value;

    loadingBar();

    $.get('/Import/Reports/ViewImportFullContainersStock?shippingAgent=' + shippingAgent + '&shipingLine=' + shipingLine + '&port=' + port + '&cargoType=' + cargoType + '&fromdate=' + fromdate + '&todate=' + todate, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    }




    function formfiled() {


    }

    //function InitDevExWebDocumentViewer(s, e) {

    //    console.log("ere", e)
    //    console.log("srs", s)
    //    //e.Actions[7].zoom(1.5)
    //}
