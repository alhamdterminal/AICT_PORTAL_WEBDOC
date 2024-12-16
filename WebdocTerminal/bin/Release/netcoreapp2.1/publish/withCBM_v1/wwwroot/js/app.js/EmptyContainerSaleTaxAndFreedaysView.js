

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }




    function UpdateRate() {


        var saletax = $('#salesTax').val()
    var freedays = $('#freeDays').val()

    console.log("saletax", saletax)
    console.log("freedays", freedays)

    var ffid = $('#shippingAgentid').val();


    console.log("ffid", ffid);


    if (ffid) {
        $.post('/Import/Setup/UpdateEmptyContainerSaleTaxAndFreeday?shippingagentId=' + ffid + '&days=' + freedays + '&tax=' + saletax, function (data) {

            if (data.error == true) {

                showToast(data.message, "error");
            } else {
                showToast(data.message, "success");
            }

        });


        } else {

        console.log("else")
            showToast("plase Select line", "error");


        }

    }



    function formfiled() {

        $('#shippingAgentid').on('change', function (data) {

            LoadData();

        });
    }


    function LoadData() {

        var ffid = $('#shippingAgentid').val();


    console.log("ffid", ffid);

    if (ffid) {


        $.get('/Import/Setup/GetEmptyContainerSaleTaxAndFreeday?shippingagentId=' + ffid, function (res) {

            console.log("res", res);

            if (res) {

                $('#salesTax').val(res.freeDays);

                $('#freeDays').val(res.salesTax);
            }
            else {
                $('#salesTax').val(0);

                $('#freeDays').val(0);
            }

        });

        } else {
        $('#salesTax').val(0)
            $('#freeDays').val(0)
        }

    }

