


    var EmptyReceiving = [];
    var EmptyreceivingId = 0;
    var croNumber = "";
    var containerNumber = "";
    $(function () {

        //$("#containerno").inputmask("aaaa-999999-9");

        $("#croNo").change(function () {

            containerNumber = "";

            croNumber = $("#croNo option:selected").text()

            getEmptyReveingByCRO(croNumber)
            GetExportContainersBYCROnumber(croNumber)




        });

    })


    function GetExportContainersBYCROnumber(croNo) {


        $.get('/Export/EmptyReceiving/GetExportContainersBYCROnumber?croNo=' + croNo, function (data) {

            if (data) {

                $("#searchBox").dxSelectBox({
                    dataSource: data,
                    displayExpr: "value",
                    acceptCustomValue: true,
                    onValueChanged: function (data) {

                        containerNumber = data.value.value

                        getContainerDetail(containerNumber);
                        findEmptyreceiving(data.value.value)

                    },
                })

                if (PermissionInputs.find(x => x.fieldName == "ContainerNumber" && x.isChecked == false)) {

                    document.getElementById('searchBox').style.display = "none";
                }
            }

        })
    }

    function getContainerDetail(containerNumber) {

        $.get('/Export/EmptyReceiving/getContainerDetail?croNumber=' + croNumber + '&containerNumber=' + containerNumber, function (data) {

            $('#shippingLine').val(data.shippingLineId)
            $('#containerSize').val(data.containerSize)


        });
    }

    function getEmptyReveingByCRO(cro) {


        $.get('/Export/EmptyReceiving/GetContainersBYCRO?croNo=' + cro, function (data) {
            EmptyReceiving = data

            var dataGrid = $("#emptyRece").dxDataGrid({
                dataSource: EmptyReceiving,
                keyExpr: "emptyReceivingId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    enabled: false
                },

                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },

                editing: {
                    mode: "row"

                },
                columns: [
                    "croNumber",
                    "containerNumber",
                    "containerSize",
                    "containerTareWeight",
                    "containerCondition",
                    "vehicleNumber",

                ],

                onEditorPreparing: function (e) {
                },


            }).dxDataGrid("instance");
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

    function AddEmptyReceiving() {

        if (containerNumber) {
            var f = document.getElementById('EmptyReceivingForm');

    if (f.checkValidity()) {

                var model = $('#EmptyReceivingForm').serialize();
    $.post('/Export/EmptyReceiving/AddEmptyReceiving?' + model, {croNumber: croNumber, containerNumber: containerNumber }, function (data) {
        getEmptyReveingByCRO(croNumber)
                    if (data.error) {
        showToast(data.message, "warning");
                    }

    else {
        EmptyreceivingId = data.id;
    showToast(data.message, "success");
    updateBtnShowHide(true);
    submitBtnShowHide(false);

                    }
                })
            }

    checkValidity();
        }

    }

    function checkValidity() {

        if (!$('#croNo').val()) {

        $('#CRONumbererror').html('This is a required field');
        }
    else {
        $('#CRONumbererror').html('');
        }
    if (!$('#containerno').val()) {

        $('#ContainerNumbererror').html('This is a required field');
        }
    else {
        $('#ContainerNumbererror').html('');
        }
    if (!$('#containerSize').val()) {

        $('#ContainerSizeerror').html('This is a required field');
        }
    else {
        $('#ContainerSizeerror').html('');
        }
    if (!$('#containerTareWeight').val()) {

        $('#ContainerTareWeighterror').html('This is a required field');
        }
    else {
        $('#ContainerTareWeighterror').html('');
        }
    if (!$('#containerCondition').val()) {

        $('#ContainerConditionerror').html('This is a required field');
        }
    else {
        $('#ContainerConditionerror').html('');
        }
    if (!$('#shippingLine').val()) {

        $('#ShippingLineerror').html('This is a required field');
        }
    else {
        $('#ShippingLineerror').html('');
        }
    if (!$('#shippingAgent').val()) {

        $('#ShippingAgenterror').html('This is a required field');
        }
    else {
        $('#ShippingAgenterror').html('');
        }
        //if (!$('#recevingDate').val()) {

        //    $('#recevingDateerror').html('This is a required field');
        //}
        //else {
        //    $('#recevingDateerror').html('');
        //}


        //  console.log("testx",$('#recevingDate').val());

    }

    function restform() {
        var r = confirm("Are you sure you want to save now?");

    if (r == true) {
        window.location.href = window.location.href
    }

    }

    function findEmptyreceiving(contNo) {

        var containerNo = contNo;
    if (containerNo) {
        $.get('/Export/EmptyReceiving/GetEmptyReceiving?croNumber=' + croNumber + '&containerNo=' + containerNo, function (data) {
            if (data) {


                updateBtnShowHide(true);
                submitBtnShowHide(false);

                $('#croNo').val(data.containerNumber);
                //$('#containerno').val(data.containerNumber);
                $('#containerSize').val(data.containerSize);
                $('#containerTareWeight').val(data.containerTareWeight);
                $('#containerCondition').val(data.containerCondition);
                $('#VehicleNumber').val(data.vehicleNumber);
                $('#shippingLine').val(data.shippingLineId);
                $('#shippingAgent').val(data.shippingAgentExportId);
                EmptyreceivingId = data.emptyReceivingId;
                if (data.recevingDate) {
                    $("#recevingDate").dxDateBox("instance").option({ value: new Date(formatDateDev(data.recevingDate)) })
                }
                else {
                    $("#recevingDate").dxDateBox("instance").option({ value: new Date(formatDateDev(new Date())) })
                }


            }
            else {
                submitBtnShowHide(true);
                updateBtnShowHide(false);

                $('#containerTareWeight').val('');
                $('#containerCondition').val('');
                $('#VehicleNumber').val('');
                $('#shippingAgent').val('');
                EmptyreceivingId = 0;
                $("#recevingDate").dxDateBox("instance").option({ value: new Date(formatDateDev(new Date())) })

            }
        });
        }
    else {
        showToast("Please Enter Container No")
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

    function updateEmptyReceiving() {


        if (containerNumber) {
            var f = document.getElementById('EmptyReceivingForm');

    if (f.checkValidity()) {

                var model = $('#EmptyReceivingForm').serialize();
    $.post('updateEmptyReceiving?' + model, {croNumber: croNumber, containerNumber: containerNumber, EmptyreceivingId: EmptyreceivingId }, function (data) {
        getEmptyReveingByCRO(croNumber)
                    showToast("Saved", "success");
                })
            }

    checkValidity();
        }

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
    function formfiled() {


        if (PermissionInputs.find(x => x.fieldName == "CRONumber" && x.isChecked == false)) {

        document.getElementById('croNo').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ShippingLine" && x.isChecked == false)) {

        document.getElementById('shippingLine').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "ShippingAgent" && x.isChecked == false)) {

        document.getElementById('shippingAgent').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "ContainerSize" && x.isChecked == false)) {

        document.getElementById('containerSize').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "ContainerTareWeight" && x.isChecked == false)) {

        document.getElementById('containerTareWeight').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "VehicleNumber" && x.isChecked == false)) {

        document.getElementById('VehicleNumber').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "ContainerCondition" && x.isChecked == false)) {

        document.getElementById('containerCondition').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "RecevingDate" && x.isChecked == false)) {

        document.getElementById('recevingDate').disabled = true;
        }



    }
