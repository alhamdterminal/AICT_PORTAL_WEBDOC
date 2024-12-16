

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function UpdateAmount() {

        //var amount = SalesTaxForm.elements["amount"].value;
        var amount = $('#amount').val();

    console.log("amount", amount);

    $.post('/Import/Setup/UpdateGeneralSealAmount?amount=' + amount, function (result) {

        console.log("result", result)
            if (result.error) {
        showToast(result.message, "warning");
            }

    else {

        showToast(result.message, "success");
    window.setInterval('refresh()', 3000);


            }

        });
    }

    function refresh() {
        window.location.reload();
    }


    function formfiled() {
        $.get('/Import/Setup/GetGenerealSealAmountView', function (data) {
            console.log(data);
            if (data) {

                $('#amount').val(data.amount);
            } else {
                $('#amount').val(0);
            }
        });
       
    }



