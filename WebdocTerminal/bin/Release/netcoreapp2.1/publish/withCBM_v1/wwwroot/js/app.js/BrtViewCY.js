

    $(function () {
        $.get('/APICalls/GetFiltersBRTCY', function (partial) {

            $('#filters').html(partial);

        })

    });






    function containerCallback() {



        var CyContainerId = $("#containerCYdb option:selected").val();

    $.get('/APICalls/GetCyContainerxBRTLocationBYCyContainerId?CyContainerId=' + CyContainerId, function (location) {

        console.log("location", location);
    if (location) {
        //    $('#brtlocationId').val();
        $('#brtlocationId').val(location.brtLocationId).trigger('change.select2');
            }
    else {
        $('#brtlocationId').select2().val('').trigger('change.select2');
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

    function saveContainerLocation() {



        var CyContainerId = $("#containerCYdb option:selected").val();

    //var location = ExtraFreeDays.elements["location"].value;

    var locationid = $("#brtlocationId").val();
    var location = $("#brtlocationId  option:selected").text();


    console.log("CyContainerId", CyContainerId)
    console.log("locationid", locationid)
    console.log("location", location)

    if (CyContainerId && locationid && location) {

        $.post('/Import/BRT/UpdateCyContainerLocation?CyContainerId=' + CyContainerId + '&locationid=' + locationid + '&location=' + location, function (data) {

            if (data.error) {
                showToast(data.message, "warning");
            }

            else {

                showToast(data.message, "success");

            }
            //showToast(data.message, "success");

        });
        }
    else {
        showToast("must select container and location", "error");
        }

       


    }





    function formfiled() {
        if (PermissionInputs.find(x => x.fieldName == "ContainerType" && x.isChecked == false)) {

        document.getElementById('filters').style.display = "none";

        }
        if (PermissionInputs.find(x => x.fieldName == "Location" && x.isChecked == false)) {

        document.getElementById('location').disabled = true;

        }

    }
