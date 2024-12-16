


    $(function () {
        GetServiceInfos();
    })

    //var ServiceInfoData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function GetServiceInfos() {
        $.get('/Import/ServicesInformation/GetServiceInfo', function (data) {
            ServiceInfoData = data;
            console.log(ServiceInfoData);
            ServiceInfoDatagrid(ServiceInfoData);
        });
    }




    function ServiceInfoDatagrid(ServiceInfoData) {


        $("#gridContainer").dxDataGrid({
            dataSource: ServiceInfoData,
            keyExpr: "serviceInfoId",
            showBorders: true,
            allowColumnResizing: true,
            columnAutoWidth: true,
            paging: {
                pageSize: 10
            },
            searchPanel: {
                visible: true,
                width: 240,
                placeholder: "Search..."
            },

            editing: {
                mode: "row",
            },
            columns: [
                {
                    dataField: "code",
                    caption: "Code"
                },
                {
                    dataField: "name",
                    caption: "Name"
                },
                {
                    dataField: "section",
                    caption: "Section"
                },
                {
                    dataField: "type",
                    caption: "Type"
                },
                {
                    dataField: "rate",
                    caption: "Rate"
                },
                {
                    dataField: "natureOfPayment",
                    caption: "Nature Of Payment"
                }
            ],



        });
    }



    function formfiled() {

    }




    function submitServiceInfo() {
        console.log("result")

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

    var f = document.getElementById('ServicesInfoForm');

    if (f.checkValidity()) {

            var values = $('#ServicesInfoForm').serialize();

    console.log("values", values)

    $.post('/Import/ServicesInformation/AddServiceInfo?' + values,   function (result) {

        console.log("result", result)
                    if (result.error) {
        showToast(result.message, "warning");
                    }

    else {

        showToast(result.message, "success");

                    }

    window.location.href = window.location.href



                })
             
             



        }

    checkValidations();
    }

    function checkValidations() {

        if (!$('#natureOfPayment').val()) {

        $('#natureOfPaymenterror').html('This is a required field');
        }
    else {
        $('#natureOfPaymenterror').html('');
        }

    if (!$('#servicesSection').val()) {

        $('#servicesSectionerror').html('This is a required field');
        }
    else {
        $('#servicesSectionerror').html('');
        }

    if (!$('#servicesType').val()) {

        $('#servicesTypeerror').html('This is a required field');
        }
    else {
        $('#servicesTypeerror').html('');
        }

    if (!$('#servicesInfoCode').val()) {

        $('#servicesInfoCodeerror').html('This is a required field');
        }
    else {
        $('#servicesInfoCodeerror').html('');
        }

    if (!$('#servicesInfoName').val()) {

        $('#servicesInfoNameerror').html('This is a required field');
        }
    else {
        $('#servicesInfoNameerror').html('');
        }

    if (!$('#rate').val()) {

        $('#rateerror').html('This is a required field');
        }
    else {
        $('#rateerror').html('');
        }

    }


