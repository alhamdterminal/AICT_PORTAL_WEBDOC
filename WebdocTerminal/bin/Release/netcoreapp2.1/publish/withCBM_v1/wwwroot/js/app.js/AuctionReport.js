


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
    var type2 = document.getElementById("type2").value;
    var type = document.getElementById("type").value;

    var shippingAgent = document.getElementById("shippingAgent").value;

    console.log("shippingAgent", shippingAgent);
    console.log("type", type);
    console.log("type2", type2);



    loadingBar();

    $.get('/Import/Reports/ViewAuctionReport?fromdate=' + fromdate + '&' + 'toDate=' + todate + '&' + 'type2=' + type2 + '&' + 'type=' + type
    + '&' + 'shippingAgent=' + shippingAgent, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }

    function formfiled() {
        if (PermissionInputs.find(x => x.fieldName == "Type" && x.isChecked == false)) {
        document.getElementById('type2').disabled = true;

        } if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {
        document.getElementById('single_cal2').disabled = true;

        }
        if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {
        document.getElementById('single_cal3').disabled = true;

        }
        if (PermissionInputs.find(x => x.fieldName == "Location" && x.isChecked == false)) {
        document.getElementById('type').disabled = true;

        }

    }