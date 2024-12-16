




    var consigneeList = [];

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



        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    var cargotype = document.getElementById("cargoType").value;
    var type = document.getElementById("type").value;
    var portandterminal = document.getElementById("portandterminalID").value;
    var commodity = document.getElementById("GoodsHeadId").value;


    loadingBar();
    $.get('/Import/Reports/ViewContainerArrivalSummaryReportCSD?Cargo=' + cargotype + '&Type=' + type + '&port=' + portandterminal + '&fromdate=' + fromdate + '&todate=' + todate + '&commodity=' + commodity,
    function (data) {

        loadingBarHide();

    $('#repotdata').html(data);

            });
    }





    function formfiled() {

    }
