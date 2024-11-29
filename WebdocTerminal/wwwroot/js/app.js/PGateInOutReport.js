

    function myFunction() {

        var fromdate, todate, cnic, name, phone = "";

    fromdate = document.getElementById("single_cal2").value;
    todate = document.getElementById("single_cal3").value;
    cnic = document.getElementById('cnic').value;
    name = document.getElementById('name').value;
    phone = document.getElementById('phone').value;

    loadingBar();
    $.get('/Import/Reports/ViewPGateInOutReport?fromdate=' + fromdate + '&todate=' + todate + '&cnic=' + cnic + '&name=' + name + '&phone=' + phone, function (data) {

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