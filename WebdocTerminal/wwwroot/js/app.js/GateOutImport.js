

    $(function () {

        $('#vessel').on('change', function (val) {


            var name = $("#vessel").val();

            $.get('/Import/GateInOut/GetVessel?Name=' + name, function (data) {

                $('#igmyear').val(data.igmYear);

                var voyageNo = data.voyages[data.voyages.length - 1].voyageNo;

                $('#voyageno').val(voyageNo);

                $.get('/Import/Delivery/GetContainersDropdown?VoyageNo=' + voyageNo + "&IGM=" + data.igm, function (containerdb) {

                    $('#containerdiv').html(containerdb);

                });
            });

        });



    });



    function submitOrder() {
        var values = $('#getOutForm').serialize();

    $.ajax({
        url: "/Import/GateInOut/AddGetOut",
    type: "post",
    data: values,
    success: function (response) {
        // you will get response from your php page (what you echo or print)

    },
    error: function (jqXHR, textStatus, errorThrown) {
    }


        });
    }

