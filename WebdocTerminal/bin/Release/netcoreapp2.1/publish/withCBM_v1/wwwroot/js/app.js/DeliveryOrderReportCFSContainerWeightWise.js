

    var virNumber;
    var virNoSouce = [];
    var type = "CFS";
    var IGM = "";
    var indexNo = "";
    var Vessel = "";
    var Voyage = "";
    var Consignee = "";
    var typePerameter = "";
    var isDestuff = false;

    //$(function () {

        //    $.get('/APICalls/GetFiltersAll?Type=' + type, function (partial) {
        //        console.log("partial",partial)


        //     $('#filters').html(partial);

        //    })
        //  //    setOrientation(isDestuff);


        //});

        //function containerCallback() { }
        //function containerChangeCallback() { }

        function myFunction() {

            if (type) {

                if (igm) {

                    IGM = igm
                    console.log("igm", IGM)
                }
                else {
                    console.log("igm", IGM)
                }




                indexNo = $("#indexdb option:selected").text()


                var fromdate = $('#DateStartTime').val();
                var todate = $('#EndStartTime').val();

                var shippingAgent = document.getElementById("shippingAgent").value;




                loadingBar();
                $.get('/Import/Reports/ViewDeliveryOrderReportCFSContainerWeightWise?igm=' + IGM + '&' + 'indexNo=' + indexNo + '&' + 'fromdate=' + fromdate
                    + '&' + 'todate=' + todate + '&' + 'shippingAgent=' + shippingAgent, function (data) {

                        loadingBarHide();

                        $('#repotdata').html(data);
                    });


            }

        }

    function loadingBar() {
        $("#large-indicator").dxLoadIndicator({
            height: 60,
            width: 60
        });
    }
    function loadingBarHide() {
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }


    function formfiled() {
        if (PermissionInputs.find(x => x.fieldName == "ContainerType" && x.isChecked == false)) {
        document.getElementById('containerType').style.display = "none";

        } if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {
        document.getElementById('dateStartTime').style.pointerEvents = "none";

        }
        if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {
        document.getElementById('endStartTime').style.pointerEvents = "none";

        }
        if (PermissionInputs.find(x => x.fieldName == "ShippingAgent" && x.isChecked == false)) {
        document.getElementById('shippingAgent').disabled = true;

        }

    }

