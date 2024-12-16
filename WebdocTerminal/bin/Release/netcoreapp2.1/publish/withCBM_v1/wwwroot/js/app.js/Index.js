
    var enteringCargoId;
    var creditToCustomerId = 0;

    $(function () {
        $('#vesselExport').css('pointer-events', 'none');
    $('#voyageExport').css('pointer-events', 'none');
    });

    function gdNumber_changed(data) {

        var gdnumber = data.value;

    $.get('/Export/CustomerCredit/FindCreditToCustomer?gdnumber=' + gdnumber, function (res) {

        console.log("res", res)


            if (res) {
        creditToCustomerId = res.creditToCustomerId;
    enteringCargoId = res.enteringCargoId;

            if (creditToCustomerId > 0) {
        updateBtnShowHide(true);
    submitBtnShowHide(false);
            }
    else {
        updateBtnShowHide(false);
    submitBtnShowHide(true);
            }


    if (res.lpNo) {

        $('#LPNumber').dxSelectBox("instance").option('value', res.lpNo);
    $('#LPNumber').dxSelectBox("instance").option('disabled', true);
            }

    $("#vesselExport").val(res.vesselId);
    $("#voyageExport").val(res.voyageId);
    $("#invoice-amount").val(res.invoiceAmount);
    $("#authorized-by").val(res.authorizedBy);
    $("#authorized-days").val(res.authorizedDays);
    $("#invoiceNo").val(res.invoiceNo);
            }




        });
    }

    function update() {

        $.post('/Export/CustomerCredit/UpdateCreditToCustomer', { CreditToCustomerId: creditToCustomerId, AuthorizedBy: $("#authorized-by").val(), AuthorizedDays: $("#authorized-days").val(), EnteringCargoId: enteringCargoId },
            function (resp) {

                showToast("Successfully Updated!", "success");

            });
    }

    function submit() {
        var invNo = $('#invoiceNo').val();
    console.log(invNo)
    if (invNo) {
        $.post('/Export/CustomerCredit/SaveCreditToCustomer', { AuthorizedBy: $("#authorized-by").val(), AuthorizedDays: $("#authorized-days").val(), EnteringCargoId: enteringCargoId },
            function (data) {

                if (data.error) {
                    showToast("warning", "warning");
                }

                else {
                    creditToCustomerId = data.id;
                    showToast(data.message, "success");
                    updateBtnShowHide(true);
                    submitBtnShowHide(false);
                }

            });
        }
    else {
        showToast("This GD Invoice Not Create", "error");
        }

    }


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function updateBtnShowHide(show) {
        var btn = document.getElementById("btnSubmitUpdate");
    if (show)
    btn.style.display = 'inline-grid';
    else
    btn.style.display = 'none';
    }

    function submitBtnShowHide(show) {
        var btn = document.getElementById("btnSubmit");
    if (show)
    btn.style.display = 'inline-grid';
    else
    btn.style.display = 'none';
    }


    function formfiled() {


            if (PermissionInputs.find(x => x.fieldName == "GDNumber" && x.isChecked == false)) {

        document.getElementById('GDNumber').style.display = "none";
            }
            if (PermissionInputs.find(x => x.fieldName == "LPNumber" && x.isChecked == false)) {

        document.getElementById('LPNumber').style.display = "none";
            }

            if (PermissionInputs.find(x => x.fieldName == "CreditAuthorizedBy" && x.isChecked == false)) {

        document.getElementById('authorized-by').disabled = true;
            }
            if (PermissionInputs.find(x => x.fieldName == "AuthorizedDays" && x.isChecked == false)) {

        document.getElementById('authorized-days').disabled = true;
            }
             
    }