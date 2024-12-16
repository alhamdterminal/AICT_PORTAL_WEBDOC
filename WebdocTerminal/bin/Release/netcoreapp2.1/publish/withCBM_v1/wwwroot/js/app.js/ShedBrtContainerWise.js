


    var containerno = "";

    function igm_changed(data) {
        containerno = "";
    containerno = data.value;
    }


    var igmno = "";

    function iganged(data) {
        igmno = "";
    igmno = data.value;
    }


    function printreport() {

        console.log("containerno", containerno)
        console.log("igmno", igmno)
    loadingBar();

    $.get('/Import/Reports/ViewShedBrtContainerWise?containerNo=' + containerno, function (data) {
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

