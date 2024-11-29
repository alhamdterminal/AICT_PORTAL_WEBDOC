

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function UpdateAuctionAmount() {
        var form = document.getElementById('AuctionForm');

    var model = $('#AuctionForm').serialize();


    console.log("model", model)
    console.log("form.reportValidity", form.reportValidity)

    form.reportValidity();


    if (form.checkValidity()) {

        console.log("ok")

            $.post('/Import/Auction/UpdateAuctionAmount?' + model, function (data) {
                if (data.error == true) {
        showToast(data.message, "error")

    } else {
        showToast(data.message, "success")

    }

            });
        }


    }





    function formfiled() {

        getauctionInfo();
    }


    function getauctionInfo() {
        $.get('/Import/Auction/GetAuctionAmount', function (data) {
            console.log(data);
            if (data) {

                $('#rate20').val(data.rate20)
                $('#rate40').val(data.rate40)
                $('#rate45').val(data.rate45)
                $('#cbm').val(data.cbm)
                $('#weight').val(data.weight)
                $('#hanndlingWeight').val(data.hanndlingWeight)


            } else {

                $('#rate20').val(0)
                $('#rate40').val(0)
                $('#rate45').val(0)
                $('#cbm').val(0)
                $('#weight').val(0)
                $('#hanndlingWeight').val(0)


            }
        });

    }

