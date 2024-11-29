

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function UpdateExchangeRate() {
 
        var rate = $("#exchangeRate").val()
    var rate20 = $("#rateAmount20").val()
    var rate40 = $("#rateAmount40").val()
    var rate45 = $("#rateAmount45").val()

    console.log("rate",rate)
    console.log("rate20", rate20)
    console.log("rate40", rate40)
    console.log("rate45", rate45)

    if (rate <= 0 || rate20 <= 0 || rate40 <= 0 || rate45 <= 0   ) {

        showToast("plase add rate > 0", "error");

        } else {

        console.log("else")

            $.post('/Import/Setup/UpdateExchangeRate?rate=' + rate + '&rate20=' + rate20 + '&rate40=' + rate40 + '&rate45=' + rate45 , function (data) {

                if (data.error == true) {

        showToast(data.message, "error");
                } else {
        showToast(data.message, "success");
                }

            });

        }

    }






    function formfiled() {
        $.get('/Import/Setup/GetExchangeRate', function (data) {
            console.log(data);

            if (data != null) {
                console.log("data", data)

                $('#exchangeRate').val(data.exchangeRateAmount);
                $('#rateAmount20').val(data.rateAmount20);
                $('#rateAmount40').val(data.rateAmount40);
                $('#rateAmount45').val(data.rateAmount45);
            }
            else {

                $('#exchangeRate').val(0);
                $('#rateAmount20').val(0);
                $('#rateAmount40').val(0);
                $('#rateAmount45').val(0);
            }
        });
 
    }


