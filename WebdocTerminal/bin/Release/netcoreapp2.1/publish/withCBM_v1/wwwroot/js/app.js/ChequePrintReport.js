

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
    console.log("url.searchParams", url.searchParams)
    var Amount = url.searchParams.get("Amount");
    var PartyName = url.searchParams.get("PartyName");

    console.log("PartyName", PartyName)
    loadingBar();

    $.get('/Account/Reports/ViewChequePrintReport?amount=' + Amount + '&partyname=' + PartyName, function (data) {

        loadingBarHide();
    $('#repotdata').html(data);

        });
    });

    function formfiled() {

    }

