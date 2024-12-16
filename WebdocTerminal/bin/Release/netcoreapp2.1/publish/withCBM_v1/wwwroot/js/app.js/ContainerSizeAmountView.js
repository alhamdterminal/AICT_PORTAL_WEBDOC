

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function UpdateContainerSizeAmount() {

        var rate20 = $("#amountSize20").val()
    var rate40 = $("#amountSize40").val()
    var rate45 = $("#amountSize45").val()

    console.log("rate20", rate20)
    console.log("rate40", rate40)
    console.log("rate45", rate45)

    if (rate20 <= 0 || rate40 <= 0 || rate45 <= 0 ) {

        showToast("plase add rate > 0", "error");

        } else {

            var form = document.getElementById('ContainerSizeAmountForm');

    var model = $('#ContainerSizeAmountForm').serialize();


    console.log("model", model)



    $.post('/Import/Setup/UpdateContainerSizeAmount?' + model, function (data) {

                if (data.error == true) {

        showToast(data.message, "error");
    window.location.href = window.location.href;

                } else {
        showToast(data.message, "success");

    window.location.href = window.location.href;
                }

            });
             

           

        }

    }






    function formfiled() {
        $.get('/Import/Setup/GetContainerSizeAmount', function (data) {
            console.log(data);

            if (data != null) {

                console.log("exchangerateamount", data)

                $('#containerSizeAmountId').val(data.containerSizeAmountId);
                $('#amountSize20').val(data.amountSize20);
                $('#amountSize40').val(data.amountSize40);
                $('#amountSize45').val(data.amountSize45);
            }
            else {

                $('#containerSizeAmountId').val(0);
                $('#amountSize20').val(0);
                $('#amountSize40').val(0);
                $('#amountSize45').val(0);
            }
        });

    }


