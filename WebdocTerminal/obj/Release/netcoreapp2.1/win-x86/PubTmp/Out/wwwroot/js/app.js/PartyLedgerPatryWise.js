
    function callChangefunc() {
        var partyId = $("#partyId option:selected").val();
    loadingBar();
    $.get('/Import/Reports/ViewPartyLedgerPatryWise?partyId=' + partyId, function (data) {
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

        if (PermissionInputs.find(x => x.fieldName == "Party" && x.isChecked == false)) {

        document.getElementById('partyId').disabled = true;
        }

    }