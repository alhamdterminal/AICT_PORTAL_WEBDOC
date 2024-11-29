
    function searching() {
        var doNumber = SearchGatePassForm.elements["donumber"].value;

    if (doNumber) {

        top.location.href = '/import/Delivery/OrderDetail?doNumber=' + doNumber;
        }
    }


    function formfiled () {
        if (PermissionInputs.find(x => x.fieldName == "Donumber" && x.isChecked == false)) {

        document.getElementById('donumber').disabled = true;
     
            
        }
    }
