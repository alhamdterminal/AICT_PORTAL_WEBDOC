    var ExportContainerId = 0;

    $(function () {

        $("#containerno").inputmask("aaaa-999999-9");

    })

    function SaveContainer() {

        checkFiledData();

    if (result == false) {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

    var model = $('#CreateContainer').serialize();
    $.post('/Export/ExportContainer/AddExportContainer?' + model, function (data) {


                if (data.error == false) {
        alert(data.message);
                    //window.location.href = window.location.href;
                }
    else {
        alert(data.message);
                    //window.location.href = window.location.href;

                }

            });


        }
    }

    var result;


    function checkFiledData() {

        result = false;


    if (!$('#containerno').val()) {

        result = true;
    return showToast("please Add Container no", "error");
        }
    if (!$('#size').val()) {
        result = true;
    return showToast("please add size", "error");
        }

    if (!$('#status').val()) {
        result = true;
    return showToast("please select status", "error");
        }

    if (!$('#cro').val()) {
        result = true;
    return showToast("please add CRO Number", "error");
        }
    if (!$('#shippingLine').val()) {
        result = true;
    return showToast("please select Line", "error");
        }
    if (!$('#pod').val()) {
        result = true;
    return showToast("please select POD", "error");
        }

    if (!$('#ShippingAgent').val()) {
        result = true;
    return showToast("please select Agent", "error");
        }

        //if (!$('#Transporter').val()) {
        //    result = true;
        //    return showToast("please select Transporter", "error");
        //}

        if (!$('#vehicleNumber').val()) {
        result = true;
    return showToast("Please Vehicle Number", "error");
        }


    if (!$('#containerTareWeight').val()) {
        result = true;
    return showToast("Please Container Tare Weight", "error");
        }
    if (!$('#containerCondition').val()) {
        result = true;
    return showToast("Please Container Condition ", "error");
        }



    }


    function formatDateDev(date) {
        var d = new Date(date),
    month = '' + (d.getMonth() + 1),
    day = '' + d.getDate(),
    year = d.getFullYear();
    hours = d.getHours();
    minutes = d.getMinutes();
        ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    strTime = hours + ':' + minutes + ' ' + ampm;


    if (month.length < 2)
    month = '0' + month;
    if (day.length < 2)
    day = '0' + day;

    return [month, day, year].join('/') + ', ' + strTime;
    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function FindData(containNo) {
        var containerNo = containNo;

    if (containerNo.length == 13) {
        $.get('/APICalls/GetExportContainerByContainerNo?containerNo=' + containerNo, function (data) {
            if (data) {
                updateBtnShowHide(true);
                deleteBtnShowHide(true);
                submitBtnShowHide(false);

                ExportContainerId = data.exportContainerId;
                $('#containerno').val(data.containerNumber);
                $('#size').val(data.containerSize);
                $('#isocode').val(data.isoCode);
                $('#status').val(data.status);
                $('#tareWeight').val(data.tareWeight);
                //$('#shippingLine').val(data.shippingLineId);
                $('#shippingLine').val(data.shippingLineExportId).trigger('change.select2');;
                $('#pod').val(data.emptyReceivedFromYard).trigger('change.select2');;
                $('#cro').val(data.croNumber);

                $('#ShippingAgent').val(data.shippingAgentExportId).trigger('change.select2');;
                //$('#Transporter').val(data.transporterExportId).trigger('change.select2');;
                $('#vehicleNumber').val(data.vehicleNumber);
                $("#recevingDate").dxDateBox("instance").option({ value: new Date(formatDateDev(data.recevingDate)) })
                $('#containerTareWeight').val(data.containerTareWeight);
                $('#containerCondition').val(data.containerCondition);


            }
            else {

                updateBtnShowHide(false);
                deleteBtnShowHide(false);
                submitBtnShowHide(true);
                resetvaluesdata();
            }

        });
        }
    else {
        updateBtnShowHide(false);
    deleteBtnShowHide(false);
    submitBtnShowHide(true);
    resetvaluesdata();
           
        }
    }

    function resetvaluesdata() {
        $('#size').val('');
    $('#isocode').val('');
    $('#status').val('');
    $('#tareWeight').val('');
    $('#cro').val('');
    $('#pod').val('').select2().val('').trigger('change.select2');
    $('#ShippingAgent').select2().val('').trigger('change.select2');
    //$('#Transporter').select2().val('').trigger('change.select2');
    $('#shippingLine').select2().val('').trigger('change.select2');
    $('#vehicleNumber').val('');
    $("#recevingDate").dxDateBox("instance").option({value: new Date(formatDateDev(new Date())) })
    $('#containerTareWeight').val('');
    $('#containerCondition').val('');
    $('#ShippingAgentExportId').val('');
    ExportContainerId = 0;
    }

    function updateExportContainer() {


        //var f = document.getElementById('CreateContainer');

        //if (f.checkValidity()) {

        checkFiledData();

    if (result == false) {

            var model = $('#CreateContainer').serialize();
    $.post('/Export/ExportContainer/UpdateExportContainer?' + model + '&ExportContainerId=' + ExportContainerId, function (data) {


                if (data.error == false) {
        showToast(data.message, "success");

    var exportContainerId = data.exportContainerId;
    top.location.href = '/Export/ExportContainer/AssociateGD?exportContainerId=' + exportContainerId;
                }
    else {
        showToast(data.message, "warning");

                }

            });

        }

        // checkValidity();

    }


    function deleteContainer() {

        $.post('/Export/ExportContainer/DeleteExportContainer?ExportContainerId=' + ExportContainerId, function (data) {

            if (data.error == false) {
                showToast(data.message, "success");
            }
            else {
                showToast(data.message, "warning");
            }
        });
    }


    function updateBtnShowHide(show) {
        var btn = document.getElementById("btnSubmitUpdate");
    if (show)
    btn.style.display = 'inline-grid';
    else
    btn.style.display = 'none';
    }

    function deleteBtnShowHide(show) {
        var btn = document.getElementById("btnSubmitdelete");
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
    function enterContainerNumebr(val) {
        FindData(val)
    }


    function formfiled() {


    }