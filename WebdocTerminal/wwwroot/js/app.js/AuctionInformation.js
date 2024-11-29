

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
        }

    var type;

    function showFilters() {

        type = $("input[name='type']:checked").val();

    $.get('/APICalls/GetFiltersAuction?Type=' + type, function (partial) {

        $('#filters').html(partial);
    $('#AuctionNo').val('');
            })

        }



    function containerCallback() {

            if (type == "CY") {
        cyContainerId = $("#containerCYdb option:selected").val();

    changeContainerNumberCY();
            }

        }

    function containerChangeCallback(ornt) {
            if (ornt == "index") {
        containerIndexId = $("#indexdb option:selected").val();
    changeContainerIndexNumber();
            }
        }

    function changeContainerIndexNumber() {
            var containerIndexId = $("#indexdb option:selected").val();

        }


    function changeContainerNumberCY() {
            var cyContainerId = $("#containerCYdb option:selected").val();

            if (PermissionInputs.find(x => x.fieldName == "CYContainerId" && x.isChecked == false)) {

        document.getElementById('containerCYdb').disabled = true;

            } else {

        getAuctionlotNumbercy(cyContainerId);

    AuctionInfo("CY", cyContainerId);
            }

        }



    function changeContainerIndexNumber() {
            var containerIndexId = $("#indexdb option:selected").val();

            if (PermissionInputs.find(x => x.fieldName == "ContainerIndexId" && x.isChecked == false)) {

        document.getElementById('containerIndexdb').disabled = true;

            } else {

        getAuctionlotNumbercfs(containerIndexId)

                AuctionInfo("CFS", containerIndexId)
            }

        }


    function getAuctionlotNumbercfs(containerIndexId) {

        $.get('/APICalls/AuctionNumberByContainerIndexNumber?containerIndexId=' + containerIndexId, function (data) {

            console.log("cfsdata", data)

            $('#AuctionNo').val(data.auctionLotNo);



        });
        }


    function getAuctionlotNumbercy(cyContainerId) {
        $.get('/APICalls/AuctionNumberByCYContainerId?CYContainerId=' + cyContainerId, function (data) {

            console.log("cydata", data)

            $('#AuctionNo').val(data.auctionLotNo);
        });
        }


    function AuctionInfo(type, containerId) {


        console.log("type", type);

    console.log("containerId", containerId);

    $.get('/APICalls/GetAuctionInfo?type=' + type + '&containerId=' + containerId, function (data) {

        console.log("data data", data);

    if (data) {
        $('#AuctionNo').val(data.auctionLotNumber)
                    $('#customDoNo').val(data.customDoNo)
    $('#bidder').val(data.bidderId)
    $('#finalBidAmount').val(data.finalBidAmount)
    $('#bankVoucherNo25').val(data.bankVoucherNo25)
    $('#bankVoucherNo75').val(data.bankVoucherNo75)
    $('#single_cal1').val(new Date(data.auctionDate).toISOString().substr(0, 10));
    $('#single_cal2').val(new Date(data.customDoDate).toISOString().substr(0, 10));


    updateBtnShowHide(true);
    submitBtnShowHide(false);

                }
    else {

                    var dt = new Date();


    $('#AuctionNo').val('')
    $('#customDoNo').val('')
    $('#bidder').val('')
    $('#finalBidAmount').val('')
    $('#bankVoucherNo25').val('')
    $('#bankVoucherNo75').val('')
    $('#single_cal1').val(new Date(dt).toISOString().substr(0, 10));
    $('#single_cal2').val(new Date(dt).toISOString().substr(0, 10));

    updateBtnShowHide(false);
    submitBtnShowHide(true);
                }
            });
        }

    function submitAuction() {
            var values = $('#AuctionForm').serialize();


    if (type == "CFS") {

                var f = document.getElementById('AuctionForm');

    if (f.checkValidity()) {


        $.post("/Import/Auction/CreateAuction?values" + values + "&containerId=" + containerIndexId + "&type=" + type, function (data) {

            if (data.error == false) {

                showToast(data.message, "success");


                //  routetogatePass(containerIndexId);

                window.location.href = window.location.href;
                updateBtnShowHide(true);
                submitBtnShowHide(false);

                //  window.location.href = "/import/auction/SubmitedAuctions";

            }
            else {
                console.log("data");

                updateBtnShowHide(false);
                submitBtnShowHide(true);

                showToast(data.message, "error");
            }
        })

    }


    checkValidity();

            }
    else {

                var f = document.getElementById('AuctionForm');

    if (f.checkValidity()) {
        $.post("/Import/Auction/CreateAuction?values" + values + "&containerId=" + cyContainerId + "&type=" + type, function (data) {


            if (data.error == false) {

                showToast(data.message, "success");
                window.location.href = window.location.href;
                updateBtnShowHide(true);
                submitBtnShowHide(false);

                // window.location.href = "/import/auction/SubmitedAuctions";
                //  routetogatePass(cyContainerId);
            }
            else {
                console.log("data");

                updateBtnShowHide(false);
                submitBtnShowHide(true);

                showToast(data.message, "error");
            }
        })

    }
    checkValidity();


            }


        }


    function updateauctionInfo() {
            var values = $('#AuctionForm').serialize();


    if (type == "CFS") {

                var f = document.getElementById('AuctionForm');

    if (f.checkValidity()) {


        $.post("/Import/Auction/UpdateAuction?values" + values + "&containerId=" + containerIndexId + "&type=" + type, function (data) {

            if (data.error == false) {

                showToast(data.message, "success");


                window.location.href = window.location.href;
                //  window.location.href = "/import/auction/SubmitedAuctions";

            }
            else {
                console.log("data");

                showToast(data.message, "error");
            }
        })

    }


    checkValidity();

            }
    else {

                var f = document.getElementById('AuctionForm');

    if (f.checkValidity()) {
        $.post("/Import/Auction/CreateAuction?values" + values + "&containerId=" + cyContainerId + "&type=" + type, function (data) {


            if (data.error == false) {

                showToast(data.message, "success");

                window.location.href = window.location.href;
                // window.location.href = "/import/auction/SubmitedAuctions";
                //  routetogatePass(cyContainerId);
            }
            else {
                console.log("data");


                showToast(data.message, "error");
            }
        })

    }
    checkValidity();


            }
        }


    function formfiled() {

            if (PermissionInputs.find(x => x.fieldName == "ContainerType" && x.isChecked == false)) {

        document.getElementById('filters').style.display = "none";

            }
            if (PermissionInputs.find(x => x.fieldName == "AuctionNo" && x.isChecked == false)) {

        document.getElementById('AuctionNo').disabled = true;

            }
            if (PermissionInputs.find(x => x.fieldName == "CustomDoNo" && x.isChecked == false)) {

        document.getElementById('customDoNo').disabled = true;

            }
           
            if (PermissionInputs.find(x => x.fieldName == "FinalBidAmount" && x.isChecked == false)) {

        document.getElementById('finalBidAmount').disabled = true;

            }
            if (PermissionInputs.find(x => x.fieldName == "FinalBidAmount" && x.isChecked == false)) {

        document.getElementById('finalBidAmount').disabled = true;

            }
            if (PermissionInputs.find(x => x.fieldName == "BankVoucherNo25" && x.isChecked == false)) {

        document.getElementById('bankVoucherNo25').disabled = true;

            }
            if (PermissionInputs.find(x => x.fieldName == "BankVoucherNo75" && x.isChecked == false)) {

        document.getElementById('bankVoucherNo75').disabled = true;

            }
            if (PermissionInputs.find(x => x.fieldName == "AuctionDate" && x.isChecked == false)) {

        document.getElementById('single_cal1').disabled = true;

            }
            if (PermissionInputs.find(x => x.fieldName == "CustomDoDate" && x.isChecked == false)) {

        document.getElementById('single_cal2').disabled = true;

            }

        }


    function findTotal() {

            var data25 = parseInt(document.getElementById("bankVoucherNo25").value != "" ? document.getElementById("bankVoucherNo25").value : 0);
    var data75 = parseInt(document.getElementById("bankVoucherNo75").value != "" ? document.getElementById("bankVoucherNo75").value : 0);


    var tot = 0;

    console.log("tot", tot);


    console.log("data25", data25);
    console.log("data75", data75);

    tot = data25 + data75 ;

    console.log("tot", tot);


    document.getElementById('finalBidAmount').value = tot;
        }




    function checkValidity() {

            if (!$('#AuctionNo').val()) {

        $('#AuctionNoerror').html('This is a required field');
            }
    else {
        $('#AuctionNoerror').html('');
            }
    if (!$('#customDoNo').val()) {

        $('#customDoNoerror').html('This is a required field');
            }
    else {
        $('#customDoNoerror').html('');
            }
    if (!$('#bidder').val()) {

        $('#bidderNameerror').html('This is a required field');
            }
    else {
        $('#bidderNameerror').html('');
            }
    if (!$('#bankVoucherNo25').val()) {

        $('#bankVoucherNo25error').html('This is a required field min fill with 0');
            }
    else {
        $('#bankVoucherNo25error').html('');
            }
    if (!$('#bankVoucherNo75').val()) {

        $('#bankVoucherNo75error').html('This is a required field min fill with 0');
            }
    else {
        $('#bankVoucherNo75error').html('');
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

