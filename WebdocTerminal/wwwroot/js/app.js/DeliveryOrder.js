
    var doInfo;

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function GetDeliveryOrderDisplayInfo(value) {

        console.log("value", value)

        var gdNumber = value

    $.get('/ApiCalls/GetDeliveryOrderDisplayInfo?gdnumber=' + gdNumber, function (resp) {

        doInfo = resp;

    displayInfo(doInfo);
        })

    }

    function submit() {

        checkFiledData();

    if (result == false) {

            var delivery = {
        gdNumber: $('#gdnumbers').val(),
    enteringCargoId: $('#cargocode').val(),
    remarks: $('#remarks').val()
            };

    if ($('#remarks').val()) {

        $.post('/Export/Delivery/AddDeliveryOrder', { model: delivery }, function (resp) {

            showToast(resp.message, resp.error ? "error" : "success");
            if (resp.error == false) {

                window.location.href = window.location.href;
                //var btnrout = document.getElementById("reportBtn");
                //btnrout.style.display = 'inline-grid';
            }
        });
            }
    else {
        alert("Remarks field is required");
            }
        }

    }
    var result;
    function checkFiledData() {

        result = false;


    if (!$('#gdnumbers').val()) {

        result = true;
    return showToast("please select gd number", "error");
        }

    if (!$('#cargocode').val()) {
        result = true;
    return showToast("there is no cargo code", "error");
        }

    if (!$('#remarks').val()) {
        result = true;
    return showToast("please add remarks", "error");
        }




    }


    function formatDate(date) {
        var d = new Date(date),
    month = '' + (d.getMonth() + 1),
    day = '' + d.getDate(),
    year = d.getFullYear();

    if (month.length < 2)
    month = '0' + month;
    if (day.length < 2)
    day = '0' + day;

    return [day, month, year].join('-');
    }

    function displayInfo(info) {

        console.log("info", info)

        if (info) {

        $('#status').html(info.drayOffStatus);

    $("#gd-date").html(formatDate(info.gdDate))
    $("#invoice-no").html(info.invoiceNo);
    $("#clearing-agent").html(info.clearingAgent);
    $("#challan-number").html(info.challanNumber);
    $("#shipper").html(info.shipper);
    $("#packages").html(info.noOfPackages);
    $("#commodity").html(info.commodity);
    $("#agent-representative").html(info.agentRepresentative);
    $("#representative-cnic").html(info.agentRepresentativeCNIC);
    $("#cargocode").val(info.enteringCargoId);
        }
    else {
        $('#status').html('');
    $("#gd-date").html(formatDate(''))
    $("#invoice-no").html('');
    $("#clearing-agent").html('');
    $("#challan-number").html('');
    $("#shipper").html('');
    $("#packages").html('');
    $("#commodity").html('');
    $("#agent-representative").html('');
    $("#representative-cnic").html('');
    $("#cargocode").val('');
        }

    }

    function routeTodeliveryOrderReport() {


        var gdNumber = $('#gdnumbers').val();
    top.location.href = '/Export/Reports/DeliveryOrderExport?gdNumber=' + gdNumber;
    }


    function formfiled() {



    }
