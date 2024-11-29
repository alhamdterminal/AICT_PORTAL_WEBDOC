


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

        loadingBar();

    $.get('/Import/Reports/ViewTruckInDetail', function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });


    }




    function formfiled() {
        myFunction();

    }

