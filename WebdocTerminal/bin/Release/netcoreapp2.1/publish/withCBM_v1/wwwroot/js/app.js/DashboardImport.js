
    var gateInContainer = 0;
    $(function () {

        //$.get('/APICalls/UpdateAuctionContainers', function (data) {

        //});

        $.get('/APICalls/GetDashboardItem', function (data) {
            console.log(data);
            $('#upcommingContainer').html(data.upCommingContainer);

            $('#gateInContainer').html(data.gateInContainerCFS);
            $('#groundedContainer').html(data.groundedCFS);
            $('#examinationMark').html(data.examinationMarkCFS);
            $('#destuffedContainer').html(data.destuffedCFS);
            $('#destuffedIndexCFS').html(data.destuffedIndexCFS);
            $('#holdContainer').html(data.onHoldCFS);
            $('#gateOutContainer').html(data.gateOutCFS);
            $('#groundingAwaited').html(data.groundingAwaitedCFS);


            $('#upcommingContainerCY').html(data.upCommingContainerCY);
            $('#gateInContainerCY').html(data.gateInContainerCY);
            $('#groundedContainerCY').html(data.groundedCY);
            $('#examinationMarkCY').html(data.examinationMarkCY);
            //  $('#destuffedContainerCY').html(data.destuffedCY);
            $('#holdContainerCY').html(data.onHoldCY);
            $('#gateOutContainerCY').html(data.gateOutCY);
            $('#weighmentContainerCY').html(data.weighmentCY);
            $('#groundingAwaitedCY').html(data.groundingAwaitedCY);


            $('#gateinIndex').html(data.gateinIndex);
            $('#gateOutCountainerIndex').html(data.gateOutCountainerIndex);


        })

    });

    function formfiled() {

    }