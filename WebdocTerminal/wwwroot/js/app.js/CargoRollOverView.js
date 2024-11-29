


    function gdNumber_changed(data) {
        var gdnumber = data;

    $.get('/Export/CargoRollOver/GetGDDeatil?gdnumber=' + gdnumber, function (res) {


            if (res.clearingAgent) {
        $('#clearingAgent').val(res.clearingAgent);
            }
    else {
        $('#clearingAgent').val('');
            }
    if (res.shippingAgent) {
        $('#shippingAgent').val(res.shippingAgent);
            }
    else {
        $('#shippingAgent').val('');
            }
    if (res.shipper) {
        $('#shipper').val(res.shipper);
            }
    else {
        $('#shipper').val('');
            }

    if (res.noOfPackages) {
        $('#numberOfPackages').val(res.noOfPackages);
            }
    else {
        $('#numberOfPackages').val('');
            }
    if (res.packageType) {
        $('#packageType').val(res.packageType);
            }
    else {
        $('#packageType').val('');
            }

    if (res.commodity) {
        $('#commodity').val(res.commodity);
            }
    else {
        $('#commodity').val('');
            }
    if (res.vessel) {
        $('#vessel').val(res.vessel);
            }
    else {
        $('#vessel').val('');
            }
    if (res.voyage) {
        $('#voyage').val(res.voyage);
            }
    else {
        $('#voyage').val('');
            }




        });

    }



    function changed_VesselExport(arg) {



        $('#voyageExportidres')
            .empty()
            .append('<option selected="selected" value="">Select Voyage</option>');



    $.get('/APICalls/GetVoyagesFormvesselExportId?vesselExportId=' + arg, function (data) {

        VoyageExports = data;

    if (VoyageExports.length) {
        console.log("VoyageExports", VoyageExports);

    var options = VoyageExports.map(function (val, ind) {
                    return $("<option></option>").val(val.voyageExportId).html(val.voyageNumber);
                });
    console.log("options", options);

    $('#voyageExportidres').append(options);
            }
    else {
        $('#voyageExportidres')
            .empty()
            .append('<option selected="selected" value="">Select Voyage</option>');

            }

        });

        //vesselExportId = arg;

        //$.get('/APICalls/GetVoyagesFormvesselExportId?vesselExportId=' + vesselExportId, function (data) {

        //    VoyageExports = data

        //    if (VoyageExports) {

        //        $("#voyageSelectBox").dxSelectBox({
        //            dataSource: VoyageExports,
        //            displayExpr: "voyageNumber",
        //            valueExpr: 'voyageExportId',
        //            acceptCustomValue: true,
        //            onValueChanged: function (data) {

        //                voyageExportId = data.value;
        //            },
        //        })

        //        if (PermissionInputs.find(x => x.fieldName == "Voyage" && x.isChecked == false)) {

        //            document.getElementById('voyageSelectBox').style.display = "none";
        //        }
        //    }

        //});


    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function addCargoRollOver() {

        checkFiledData();

    if (result == false) {

            var x = confirm("Are you sure ");
    if (x == true) {


                var gdnumber = $('#gdnumbers').val();
    var voyageExportId = $('#voyageExportidres').val();
    var vesselExportId = $('#vesselExport').val();

    $.post('AddCargoRollOver', {gdnumber: gdnumber, voyageExportId: voyageExportId, vesselExportId: vesselExportId }, function (data) {

                    if (data.error) {
        showToast(data.message, "warning");
                    }
    else {

        showToast(data.message, "success");

    gdNumber_changed(gdnumber);

                    }

                })
            }
    else {
        window.location.reload();
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

    if (!$('#vesselExport').val()) {
        result = true;
    return showToast("please select Vessel", "error");
        }

    if (!$('#voyageExportidres').val()) {
        result = true;
    return showToast("please select Voyage Export", "error");
        }

    }


    function formfiled() {

    }
