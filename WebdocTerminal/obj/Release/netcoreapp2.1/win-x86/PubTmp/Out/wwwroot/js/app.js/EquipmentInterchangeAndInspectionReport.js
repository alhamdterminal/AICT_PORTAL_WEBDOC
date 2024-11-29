

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

    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var igm = url.searchParams.get("igm");
    var ContainerNumber = url.searchParams.get("ContainerNumber");
    var Status = url.searchParams.get("Status");

    console.log("igm", igm);
    console.log("ContainerNumber", ContainerNumber);
    console.log("Status", Status);


    if (igm && ContainerNumber && Status) {
        loadingBar();
    $.get('/import/reports/ViewEquipmentInterchangeAndInspectionReport?igm=' + igm + '&ContainerNumber=' + ContainerNumber + '&Status=' + Status, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });
        }


    });





    function myFunction() {



        var containerno = $('#containerdb option:selected').text();
    var status = $('#status option:selected').text();

    console.log("igm", igm)

    console.log("Status", status)
    console.log("containerno", containerno)

    if (igm && containerno && status) {
        loadingBar();
    $.get('/Import/Reports/ViewEquipmentInterchangeAndInspectionReport?igm=' + igm + '&ContainerNumber=' + containerno + '&Status=' + status, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
            });
        }

    else {
        showToast("please select igm container and status", "error")
    }



    }
