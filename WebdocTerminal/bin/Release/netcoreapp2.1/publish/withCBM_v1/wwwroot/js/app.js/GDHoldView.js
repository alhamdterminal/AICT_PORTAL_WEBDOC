
    var gdnumber;

    $(function () {
    })

    function gdNumber_changed(data) {
        gdnumber = data;


    $.get('/Export/GDHold/GetExportCargoDetailsByGD?gdnumber=' + gdnumber, function (data) {

            if (data) {

        $('#packageType').val(data.packageType);
    $('#numberOfPackages').val(data.noOfPackages);
    $('#clearingAgent').val(data.clearingAgent);
    $('#shipperName').val(data.shipperName);
    $('#enteringCargoId').val(data.enteringCargoId);

            }

    else {
        $('#packageType').val('');
    $('#numberOfPackages').val('');
    $('#clearingAgent').val('');
    $('#shipperName').val('');
    $('#enteringCargoId').val('');


            }

        });

    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    var result;

    function addHold() {

        checkFiledData();

    if (result == false) {

            var CargoId = $('#enteringCargoId').val();

    $.post('AddGDHold?EnteringCargoId=' + CargoId + "&remarks=" + $('#remarks').val(), function (data) {
                if (data.error == true) {
        showToast(data.message, "error");

                }
    else {
        showToast(data.message, "success");

                }

            });
        }

    }



    function releaseHold() {

        checkFiledData();

    if (result == false) {

            var CargoId = $('#enteringCargoId').val();

    $.post('RemoveHold?EnteringCargoId=' + CargoId + "&remarks=" + $('#remarks').val(), function (data) {
                if (data.error == true) {
        showToast(data.message, "error");

                }
    else {
        showToast(data.message, "success");

                }
            })
        }
    }



    function checkFiledData() {

        result = false;


    if (!$('#gdnumbers').val()) {

        result = true;
    return showToast("please select gd number", "error");
        }

    if (!$('#enteringCargoId').val()) {
        result = true;
    return showToast("there is no Cargo Code", "error");
        }

    if (!$('#remarks').val()) {
        result = true;
    return showToast("please add remarks", "error");
        }

    }




    function formfiled() {

    }
