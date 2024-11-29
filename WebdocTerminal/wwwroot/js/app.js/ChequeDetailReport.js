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
    var party = $("#partyId option:selected").val();
    var bank = $("#bank option:selected").text();
    var chequeno = $("#chequeno option:selected").text();
    if ($("#bank option:selected").val() == "") {
        bank = "";
        }

    if ($("#chequeno option:selected").val() == "") {
        chequeno = "";
        }

    loadingBar();
    $.get('/Import/Reports/ViewChequeDetailReport?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'party=' + party
    + '&' + 'bank=' + bank + '&' + 'chequeno=' + chequeno, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }


    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Bank" && x.isChecked == false)) {

        document.getElementById('bank').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Party" && x.isChecked == false)) {

        document.getElementById('partyId').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "From" && x.isChecked == false)) {

        document.getElementById('single_cal2').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "To" && x.isChecked == false)) {

        document.getElementById('single_cal3').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ChequeNo" && x.isChecked == false)) {

        document.getElementById('chequeno').disabled = true;
        }


    }