

    function submitOrder() {
        var values = $('#orderForm').serialize();

    $.ajax({
        url: "/Import/Delivery/AddOrder",
    type: "post",
    data: values,
    success: function (response) {
        // you will get response from your php page (what you echo or print)                 

    },
    error: function (jqXHR, textStatus, errorThrown) {
        console.log(textStatus, errorThrown);
            }


        });
    }

