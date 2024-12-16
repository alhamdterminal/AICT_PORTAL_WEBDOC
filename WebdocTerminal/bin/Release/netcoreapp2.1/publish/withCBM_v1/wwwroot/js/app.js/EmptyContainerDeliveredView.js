

    var consigneeList = [];
    $(function () {


        $.get('/APICalls/GetALlConsignees', function (res) {
            if (res) {
                consigneeList = res;
                $("#searchBoxForConsigne").dxSelectBox({
                    dataSource: {
                        store: consigneeList,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ConsigneId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                })
            }
            else {
                consigneeList = []
                $("#searchBoxForConsigne").dxSelectBox({
                    dataSource: {
                        store: consigneeList,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ConsigneId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                })
            }
        });

         

    });




    function formfiled() {
        $('#emptyContainerReceivedId').on('change', function (data) {

            LoadEmptyreceveContainerData();

        });
    }


    function LoadEmptyreceveContainerData() {

        var EmptyContainerReceivedId = $('#emptyContainerReceivedId').val();
    console.log("EmptyContainerReceivedId", EmptyContainerReceivedId)
    if (EmptyContainerReceivedId) {

        $.get('/Import/GateInOut/GetEmptyReceiveContainerData?EmptyContainerReceivedId=' + EmptyContainerReceivedId, function (res) {

            console.log("res", res)

            $('#containersize').val(res.size);
            $('#type').val(res.type);
            $('#vessel').val(res.vessel);
            $('#voyage').val(res.voyage);
            $('#line').val(res.line);
            $('#consigneeName').val(res.consigneeName);
            $('#clearingAgentName').val(res.clearningAgentName);
            $('#portOfLoading').val(res.portOfLoading);
            $('#principal').val(res.principal);

            if (res.actualArrive != null) {

                $('#actualarrive').val(new Date(res.actualArrive.split("T")[0]).toISOString().substr(0, 10));

            } else {
                $('#actualarrive').val('');
            }



        });

        } else {
        resetformvalue();
        }


    }


    function resetformvalue() {
        $('#containersize').val('');
    $('#type').val('');
    $('#vessel').val('');
    $('#voyage').val('');
    $('#line').val('');
    $('#consigneeName').val('');
    $('#clearingAgentName').val('');
    $('#portOfLoading').val('');
    $('#principal').val('');
    $('#actualarrive').val('');
     

    }

    function saveinfo() {

        var EmptyContainerReceivedId = $('#emptyContainerReceivedId').val();
    console.log("EmptyContainerReceivedId", EmptyContainerReceivedId)
    if (EmptyContainerReceivedId) {

            var form = document.getElementById('EmptyContainerDelivered');

    var model = $('#EmptyContainerDelivered').serialize();


    console.log("model", model)
    console.log("form.reportValidity", form.reportValidity)

    form.reportValidity();


    if (form.checkValidity()) {

        console.log("ok", model);

    $.post('/Import/GateInOut/AddEmptyContainerDelivered?' + model, function (data) {

        console.log(" data ", data);
    if (data.error == true) {
        showToast(data.message, "warning");
    window.location.href = window.location.href;
                    }
    else {
        showToast(data.message, "success");
    window.location.href = window.location.href;
                    }
                });


            }

        }
    else {
        showToast("please select container no", "warning");
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
