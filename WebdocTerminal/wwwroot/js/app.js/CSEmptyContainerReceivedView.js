

    var containersList = [];
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


    ;
    setTimeout(function () {getallemptyreceivedcontainers(); }, 5000);

    });




    function getallemptyreceivedcontainers() {

        $.get('/Import/GateInOut/GetAllCSEmptyContainerReceived', function (res) {
            containersList = res;

            console.log("containersList", containersList);


            $("#containerDetailList").dxDataGrid({
                dataSource: containersList,
                keyExpr: "csEmptyContainerReceiveId",
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                paging: {
                    pageSize: 10
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."
                },

                editing: {
                    mode: "row",
                    allowUpdating: true,
                    allowAdding: false,
                    allowDeleting: false,

                },
                columns: [
                    {
                        dataField: "containerNo",
                        validationRules: [{ type: "required" }],
                        caption: "Container No"
                    },
                    {
                        dataField: "size",
                        validationRules: [{ type: "required" }],
                        caption: "Size",

                        lookup: {
                            dataSource: [{ containerSize: 20 }, { containerSize: 40 }, { containerSize: 45 }],
                            displayExpr: "containerSize",
                            valueExpr: "containerSize"
                        }
                    },
                    {
                        dataField: "shift",
                        validationRules: [{ type: "required" }],
                        caption: "Shift"
                    },
                    {
                        dataField: "tireWeight",
                        validationRules: [{ type: "required" }],
                        caption: "Tire Weight"
                    },
                    {
                        dataField: "trukNo",
                        validationRules: [{ type: "required" }],
                        caption: "Truck No"
                    },
                    {
                        dataField: "damageType",
                        validationRules: [{ type: "required" }],
                        caption: "Damage Type"
                    },


                    {
                        dataField: "otherRemarks",
                        validationRules: [{ type: "required" }],
                        caption: "Other Remarks"
                    },
                    {
                        dataField: "receivedDate",
                        caption: "Received",
                        dataType: 'date',
                        format: 'dd/MM/yyyy',
                        allowEditing: false,
                    }

                ],

                onRowUpdated: function (e) {

                    console.log(e);
                    var model = e.data;

                    $.post('/Import/GateInOut/UpdateCSEmptyContainerReceived', { model, model }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error")
                        } else {
                            showToast(data.message, "success")
                        }
                        window.location.href = window.location.href;
                    })

                },


            });


        });

    }

    function formfiled() {

    }


    function saveinfo() {

        

        if ($('#containerNo').val().length == 11) {
            var form = document.getElementById('CSEmptyContainerReceived');

    var model = $('#CSEmptyContainerReceived').serialize();


    console.log("model", model)
    console.log("form.reportValidity", form.reportValidity)

    form.reportValidity();


    if (form.checkValidity()) {

        console.log("ok", model);

    $.post('/Import/GateInOut/AddCSEmptyContainerReceived?'  + model , function (data) {

        console.log(" data ", data);
    if (data.error == true) {
        showToast(data.message, "warning");
    getallemptyreceivedcontainers();
                    }
    else {
        showToast(data.message, "success");
    getallemptyreceivedcontainers();
                    }
                });


            }

        }
    else {
        alert("please enter 11 character of container no");
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
