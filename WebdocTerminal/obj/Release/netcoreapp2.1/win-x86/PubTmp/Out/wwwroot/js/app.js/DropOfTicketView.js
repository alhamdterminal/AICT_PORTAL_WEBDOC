

    function restvalues() {
        $('#shippingLine').val('');
    $('#size').val('');
    $('#type').val('');
    $('#manifestedSealNumber').val('');
    }

    function containerChangeCallback( ) {

        restvalues();
    var containerNumber = $("#containerdb option:selected").text();

    console.log("igm", igm);

    console.log("containerNumber", containerNumber);

    $.get('/APICalls/GetCFSorCYContainerDetailForImportDropOfContainer?igm=' + igm + '&containerNumber=' + containerNumber, function (data) {


            if (data) {

        console.log("data", data)
                 
                $('#shippingLine').val(data.shippingLine);
    $('#size').val(data.size);
    $('#type').val(data.status);
    $('#manifestedSealNumber').val(data.manifestedSealNumber);
                 
            }
    else {
        restvalues();

            }



        })


    $.get('/APICalls/GetDropOffTicket?igm=' + igm + '&containerno=' + containerNumber, function (data) {


        console.log("data", data)


            if (data) {

        console.log("data", data)
                 

                $('#truckNo').val(data.truckNumber);

    if (data.inTime != null) {

        $('#inTime').val(new Date(data.inTime.split("T")[0]).toISOString().substr(0, 10));

                } else {
        $('#inTime').val('');
                }

    if (data.outTime != null) {

        $('#outTime').val(new Date(data.outTime.split("T")[0]).toISOString().substr(0, 10));

                } else {
        $('#outTime').val('');
                }


    updateBtnShowHide(true);
    submitBtnShowHide(false);


            }
    else {

        $('#truckNo').val('');
    $('#inTime').val('');
    $('#outTime').val('');

    updateBtnShowHide(false);
    submitBtnShowHide(true);
                  
            }



        })


    }





    function formfiled() {
        document.getElementById('ischeckOutTime').onchange = function () {
            checkstatus = this.checked;
        }

    }

    function addDropOfTicket() {

        var containerNumber = $("#containerdb option:selected").text();

    console.log("igm", igm);

    console.log("containerNumber", containerNumber);

    var f = document.getElementById('DropOfTicketForm');

    if (f.checkValidity()) {

            var values = $('#DropOfTicketForm').serialize();

    console.log("values", values);

    $.post('/Import/Setup/AddGetDropOfTicket?' + values + '&igm=' + igm + '&containerNumber=' + containerNumber  , function (result) {

        console.log("result", result)
                if (result.error) {
        showToast(result.message, "warning");
                }

    else {

        showToast(result.message, "success");

    updateBtnShowHide(false);
    submitBtnShowHide(true);

                }




    window.open('/Import/Reports/DropOfTicketImportReport?igm=' + igm + '&ContainerNumber=' + containerNumber, "_blank");
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

    if (!$('#manifestedSealNumber').val()) {

        $('#manifestedSealNumbererror').html('This is a required field');
        }
    else {
        $('#manifestedSealNumbererror').html('');
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
    var checkstatus = false;


    function updateDropOfTicket() {


        
        var containerNumber = $("#containerdb option:selected").text();

    console.log("checkstatus", checkstatus);
    console.log("igm", igm);

    console.log("containerNumber", containerNumber);

    var f = document.getElementById('DropOfTicketForm');

    if (f.checkValidity()) {

            var values = $('#DropOfTicketForm').serialize();

    console.log("values", values);

    $.post('/Import/Setup/UpdatteDropOfTicket?' + values + '&igm=' + igm + '&containerNumber=' + containerNumber + '&status=' + checkstatus, function (result) {

        console.log("result", result)
                if (result.error) {
        showToast(result.message, "warning");
                }

    else {

        showToast(result.message, "success");

                }
    window.open('/Import/Reports/DropOfTicketImportReport?igm=' + igm + '&ContainerNumber=' + containerNumber, "_blank");
    window.setInterval('refresh()', 3000);

            });
        }

    checkValidations();

    }

