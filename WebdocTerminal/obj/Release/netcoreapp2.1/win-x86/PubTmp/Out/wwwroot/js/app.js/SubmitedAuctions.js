


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

    $.get('/APICalls/GetFiltersAuctionDelivery?Type=' + type, function (partial) {

        $('#filters').html(partial);
    $('#AuctionNo').val('');
        })

    }



    function containerCallback() {

         if (type == "CY")   {
        cyContainerId = $("#containerCYdb option:selected").val();

    changeContainerNumberCY();

    console.log("containerCallback")
        }

    }

    function containerChangeCallback(ornt) {
        if (ornt == "index") {
        containerIndexId = $("#indexdb option:selected").val();
    changeContainerIndexNumber();
    console.log("containerChangeCallback")

    getAuctiondetail(type, containerIndexId) 

        }
    }


    function getAuctiondetail(type, containerId) {

        console.log("type", type)
        console.log("containerId", containerId)
    $.get('/Import/Auction/GetAuctionDetail?type=' + type + '&containerId=' + containerId, function (data) {

        console.log("data", data);

    if (data) {

        console.log("if", data);
    $("#AuctionNo").val(data.auctionLotNumber)
    $("#customDoNo").val(data.customDoNo)
    $("#bidderName").val(data.bidder.bidderName)
    $("#finalBidAmount").val(data.finalBidAmount)
    $("#bankVoucherNo25").val(data.bankVoucherNo25)
    $("#bankVoucherNo75").val(data.bankVoucherNo75)



    if (data.containerIndexId) {


        $("#btnSubmit").val(data.containerIndexId)


    }
    if (data.cyContainerId) {
        $("#btnSubmit").val(data.cyContainerId);
 
                }
                 
            }
    else {
        console.log("else", data);

    $("#AuctionNo").val('')
    $("#customDoNo").val('')
    $("#bidderName").val('')
    $("#finalBidAmount").val('')
    $("#bankVoucherNo25").val('')
    $("#bankVoucherNo75").val('')
    $("#btnSubmit").val('');



            }

           
             
        });
    }

    function changeContainerIndexNumber() {
        var containerIndexId = $("#indexdb option:selected").val();
    console.log("changeContainerIndexNumber")

    }


    function changeContainerNumberCY() {
    var cyContainerId = $("#containerCYdb option:selected").val();

    console.log("changeContainerNumberCY")

     
    }


    function changeContainerIndexNumber() {
        var containerIndexId = $("#indexdb option:selected").val();

    console.log("changeContainerIndexNumber")


    }


    function routetogatePass() {

        console.log($("#btnSubmit").val())
        if ($("#btnSubmit").val()) {
            var containerId = $("#btnSubmit").val();
    console.log("containerId", containerId)
    window.open('/Import/Delivery/AuctionGatePassView?containerId=' + containerId + "&type=" + type, "_blank");
             

        }
    else {
        showToast("First Create Auction Info", "error");
        }
        
    }


    function formfiled() {

    }
