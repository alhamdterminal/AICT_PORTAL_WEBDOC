

    var resdata = [];

    function Findgddetail() {
        var res = $('#gdno').val();

    if (res) {
        window.open('/Export/ConsolidateGDDetail/UpdateConsolidateGdView?GDNumber=' + res, "_blank");
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

    function formfiled() {


    }
