

    $(function () {
        getParkingTicketNumber();

    })

    function getParkingTicketNumber() {

        $.get('/Import/Setup/GenParkingTicketCode', function (data) {

            console.log(data);
            $('#parkingTicketNumber').val(data);

        });
    }




    function formfiled() {

    }

    function addParkingTicket() {

         
        var f = document.getElementById('ParkingTicketForm');

    if (f.checkValidity()) {

            var values = $('#ParkingTicketForm').serialize();

    console.log("values", values);

    var tokenNo = $('#parkingTicketNumber').val();
    console.log("tokenNo", tokenNo);

    $.post('/Import/Setup/AddParkingTicket?' + values , function (result) {

        console.log("result", result)
                if (result.error) {
        showToast(result.message, "warning");
                }

    else {

        showToast(result.message, "success");

                }
    window.open('/Import/Reports/ParkingTicketReport?tokenNo=' + tokenNo , "_blank");
    window.setInterval('refresh()', 3000);

            });
        }

    checkValidations();

    }

    function checkValidations() {

        if (!$('#truckNo').val()) {

        $('#truckNoerror').html('This is a required field');
        }
    else {
        $('#truckNoerror').html('');
        }

    if (!$('#truckingCompany').val()) {

        $('#truckingCompanyerror').html('This is a required field');
        }
    else {
        $('#truckingCompanyerror').html('');
        }


    if (!$('#driverName').val()) {

        $('#driverNameerror').html('This is a required field');
        }
    else {
        $('#driverNameerror').html('');
        }


    if (!$('#driverPhoneNo').val()) {

        $('#driverPhoneNoerror').html('This is a required field');
        }
    else {
        $('#driverPhoneNoerror').html('');
        }

    }


    function refresh() {
        window.location.reload();
    }


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }
