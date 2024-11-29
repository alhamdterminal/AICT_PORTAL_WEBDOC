

    var type;
    function showFilters() {

        type = $("input[name='type']:checked").val();

    $.get('/APICalls/GetFilters?Type=' + type, function (partial) {

        $('#filters').html(partial);

        })

    $('#additionalFreeDays').val('');
    $('#waiverAmount').val('');
    $('#approvedBy').val('');
    $('#extraFreeDaysRemarks').val('');




    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function submitExtraFreeDays() {
        var additionalfreedays = $('#additionalFreeDays').val();
    var waiverAmount = $('#waiverAmount').val();

    var approvedBy = $('#approvedBy').val();
    var extraFreeDaysRemarks = $('#extraFreeDaysRemarks').val();


    if (!approvedBy ) {
            return showToast("please select Approved By", "error")
        }

    if (!extraFreeDaysRemarks) {
            return showToast("please add Remarks", "error")
        }


    if (additionalfreedays && waiverAmount) {
            return showToast("can't insert both fields at a time", "error")
        }


    if (!additionalfreedays && !waiverAmount) {
            return showToast("please fill one field", "error")
        }


    var gdno = $('#gdnumbers').val();


    if (gdno) {
        $.post('/Export/TariffExport/UpdateExtraFreeDays?gdnumber=' + gdno + '&additionalfreedays=' + additionalfreedays + '&waiverAmount=' + waiverAmount + '&approvedBy=' + approvedBy + '&extraFreeDaysRemarks=' + extraFreeDaysRemarks, function (data) {
            // showToast("Extra Free Days Created", "success");

            if (data.error == true) {
                showToast(data.message, "error")
            } else {
                showToast(data.message, "success")
            }
        });
        }


    else {
        showToast("please select GD-Number", "error")
    }

    }











    function formfiled() {


        $('#additionalFreeDays').val('');
    $('#waiverAmount').val('');
    $('#approvedBy').val('');
    $('#extraFreeDaysRemarks').val('');
    }


    var gd;

    function gdNumber_changed(data) {



        $('#additionalFreeDays').val('');
    $('#waiverAmount').val('');

    $('#approvedBy').val('');
    $('#extraFreeDaysRemarks').val('');


    gd = data;

    console.log(gd);

    cargoCallback(gd)


    }



    function cargoCallback(gd) {

        $.get('/APICalls/GetGdfreeDays?gdnumber=' + gd, function (data) {

            console.log("data", data)

            if (data) {
                $('#additionalFreeDays').val(data.additionalFreeDays);
                $('#waiverAmount').val(data.waiverAmount);
                $('#approvedBy').val(data.approvedBy);
                $('#extraFreeDaysRemarks').val(data.extraFreeDaysRemarks);

            }
            else {
                $('#additionalFreeDays').val('');
                $('#waiverAmount').val('');

                $('#approvedBy').val('');
                $('#extraFreeDaysRemarks').val('');

            }


        })

    }

