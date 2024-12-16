


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

        var containerno = $('#containerdb option:selected').text();

    console.log("igm", igm)

    console.log("containerno", containerno)


    loadingBar();
    $.get('/Import/Reports/ViewDestuffContainerClientWise?virnumber=' + igm + '&containernumber=' + containerno, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });

    }



    function containerChangeCallback() {


    }

    function formfiled() {


    }