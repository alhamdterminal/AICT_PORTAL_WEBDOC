

    var type;
    function showFilters() {

        type = $("input[name='type']:checked").val();

    $.get('/APICalls/GetFilters?Type=' + type, function (partial) {

        $('#filters').html(partial);
 
        })

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
        var additionalfreedays = ExtraFreeDays.elements["AdditionalFreeDays"].value;

    if (type == "CFS") {
        containerIndexId = $("#indexdb  option:selected").val();
    console.log('test', containerIndexId);

    $.post('/Tariff/UpdateExtraFreeDays?ContainerIndexId=' + containerIndexId + '&additionalfreedays=' + additionalfreedays, function (data) {
        showToast("Extra Free Days Created", "success");
            });
        }

    else {
        cyContainerId = $("#containerCYdb option:selected").val();
    console.log('CycontainerId', cyContainerId)
    $.post('/Tariff/UpdateExtraFreeDays?cyContainerId=' + cyContainerId + '&additionalfreedays=' + additionalfreedays, function (data) {
        showToast("Extra Free Days Created", "success");
            });
        }
        
    }

    function containerCallback() {

    }



    function formfiled() {
         if (PermissionInputs.find(x => x.fieldName == "ContainerType" && x.isChecked == false)) {

        document.getElementById('filters').style.display = "none";
        }if (PermissionInputs.find(x => x.fieldName == "AdditionalFreeDays" && x.isChecked == false)) {

        document.getElementById('additionalFreeDays').disabled = true;
        }
    }

