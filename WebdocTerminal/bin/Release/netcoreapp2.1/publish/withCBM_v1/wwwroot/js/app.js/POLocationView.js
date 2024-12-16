


    var lpnumber;
    var loadingProgramDetail = [];
    var ponum;
    var POLocationId = 0;

    function AddpoLocation() {

        if (lpnumber) {

            if (ponum) {
                var f = document.getElementById('POLocationForm');

    if (f.checkValidity()) {

                    var values = $('#POLocationForm').serialize();

    $.post('/Export/POLocation/AddPOLocation?' + values, {poNumber: ponum, lpnumber, lpnumber }, function (data) {
  

                         if (data.error) {
        showToast("warning", "warning");
                        }

    else {
        POLocationId = data.id;
    showToast(data.message , "success");
    submitBtnShowHide(false);
    updateBtnShowHide(true);

                        }

                    });
                }

    checkValidity()
            }
    else {
        alert("Please Select PO Number")
    }
        }
    else {
        alert("Please Select LP Number")
    }
    }

    function checkValidity() {

        if (!$('#location').val()) {

        $('#locationerror').html('This is a required field');
        }
    else {
        $('#locationerror').html('');
        }
    }

    function loadingProgramNumber_changed(data) {

        ponum = "";
    $('#location').val('');
    lpnumber = data.value;
    $.get('/APICalls/GetLoadingProgramDetailByLPNumber?lpNumber=' + lpnumber, function (data) {

        loadingProgramDetail = data

          
            $("#searchBox").dxSelectBox({
        dataSource: loadingProgramDetail,
    displayExpr: "poNumber",
    acceptCustomValue: false,
    onValueChanged: function (data) {
        ponum = data.value.poNumber;
    findData()
                },

            })

               if (PermissionInputs.find(x => x.fieldName == "PONumber" && x.isChecked == false)) {

        document.getElementById('searchBox').style.display = "none";
         }

        });
    $("#btnSubmit").attr("disabled", false);

    }

    function findData() {
        $("#btnSubmit").attr("disabled", false);

    $.get('/Export/POLocation/GetPoLocationByPONumber?lpNumber=' + lpnumber + '&poNumber=' + ponum, function (data) {
 
            if (data) {
        updateBtnShowHide(true);
    submitBtnShowHide(false);

    $('#location').val(data.location);
    POLocationId = data.poLocationId;
            }
    else {
        submitBtnShowHide(true);
    updateBtnShowHide(false);

    $('#location').val('');

    POLocationId = 0;
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

    function submitBtnShowHide(show) {
        var btn = document.getElementById("btnSubmit");
    if (show)
    btn.style.display = 'inline-grid';
    else
    btn.style.display = 'none';
    }

    function updatePOLocation() {

        if (lpnumber) {
             if (ponum) {
                var f = document.getElementById('POLocationForm');

    if (f.checkValidity()) {

                    var values = $('#POLocationForm').serialize();

    $.post('/Export/POLocation/UpdatePOLocation?' + values, {poNumber: ponum, lpnumber: lpnumber, POLocationId: POLocationId }, function (data) {
        showToast("Saved ", "success");
    $("#btnSubmit").attr("disabled", true);
                         
                    });
                }

    checkValidity();

            }
    else {
        alert("Please Select PO Number")
    }

        } else {
        alert("Please Select LP Number")
    }
    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function restform() {
        var r = confirm("Are you sure you want to save now?");
    if (r == true) {
        window.location.href = window.location.href
    }

    }

    function formfiled() {


        if (PermissionInputs.find(x => x.fieldName == "LPNumber" && x.isChecked == false)) {

        document.getElementById('LPNumber').style.display = "none";
         }
        if (PermissionInputs.find(x => x.fieldName == "Location" && x.isChecked == false)) {

        document.getElementById('location').disabled = true;
         }


    }
