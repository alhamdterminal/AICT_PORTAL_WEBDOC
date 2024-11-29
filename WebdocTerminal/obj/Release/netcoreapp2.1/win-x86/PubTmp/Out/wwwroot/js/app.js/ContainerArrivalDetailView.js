




    function callChangefunc() {

        var containerno = $("#containerlist").dxAutocomplete("instance").option("value");

    console.log("containerno", containerno);

    loadingBar();




    $.get('/Import/Reports/ViewContainerArrivalDetailView?ContainerNumber=' + containerno, function (data) {
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