


    var containerno = "";

    function container_changed(data) {
        containerno = "";
    containerno = data.value;
    }


    var igmno = "";

    function igm_changed(data) {
        igmno = "";
    igmno = data.value;
    }


    function printreport() {

        console.log("containerno", containerno)
        console.log("igmno", igmno)

    if (igmno == null || igmno == undefined) {
        igmno = "";
        }

    if (containerno == null || containerno == undefined) {
        containerno = "";
        }


    loadingBar();

    $.get('/Import/Reports/ViewMaerskAfterArrival?containerNo=' + containerno, '&igmno=' + igmno, function (data) {
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
