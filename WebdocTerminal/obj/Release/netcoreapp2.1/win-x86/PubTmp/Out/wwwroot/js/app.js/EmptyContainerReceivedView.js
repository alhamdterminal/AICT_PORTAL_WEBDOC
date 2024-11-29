

    var containersList = [];
    var consigneeList = [];
    $(function () {

        $("#containerNo").inputmask("aaaa-999999-9");

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




    function getallemptyreceivedcontainers(){

        $.get('/Import/GateInOut/GetAllEmptyContainerReceived', function (res) {
            containersList = res;

            console.log("containersList", containersList);


            $("#containerDetailList").dxDataGrid({
                dataSource: containersList,
                keyExpr: "emptyContainerReceivedId",
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
                    allowUpdating: false,
                    allowAdding: false,
                    allowDeleting: true,

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
                        caption: "Size"
                    },
                    {
                        dataField: "vessel",
                        validationRules: [{ type: "required" }],
                        caption: "Vessel"
                    },
                    {
                        dataField: "voyage",
                        validationRules: [{ type: "required" }],
                        caption: "Voyage"
                    },
                    {
                        dataField: "shift",
                        validationRules: [{ type: "required" }],
                        caption: "Shift"
                    },
                    {
                        dataField: "truckNo",
                        validationRules: [{ type: "required" }],
                        caption: "Truck No"
                    },
                    {
                        dataField: "damageType",
                        validationRules: [{ type: "required" }],
                        caption: "Damage Type"
                    },

                    {
                        dataField: "pickLocation",
                        validationRules: [{ type: "required" }],
                        caption: "Pick Location"
                    },
                    {
                        dataField: "guranteeNo",
                        validationRules: [{ type: "required" }],
                        caption: "GuranteeNo"
                    },
                    {
                        dataField: "guranteeNo",
                        validationRules: [{ type: "required" }],
                        caption: "GuranteeNo"
                    },
                    {
                        dataField: "remarks",
                        validationRules: [{ type: "required" }],
                        caption: "Remarks"
                    },

                    //{
                    //    dataField: "isoCodeHeadId",
                    //    caption: "ISO Code Head",
                    //    lookup: {
                    //        dataSource: ISOCodeHeadData,
                    //        displayExpr: "isoCodeHeadDescription",
                    //        valueExpr: "isoCodeHeadId"
                    //    },
                    //    validationRules: [{ type: "required" }]

                    //},
                    {
                        dataField: "consigneId",
                        caption: "Consignee",
                        width: 250,
                        lookup: {
                            dataSource: {
                                store: consigneeList,
                                requireTotalCount: true,
                                pageSize: 4,
                                paginate: true,
                            },
                            displayExpr: "text",
                            valueExpr: "value"
                        }
                    },

                    {
                        dataField: "arrivedFrom",
                        caption: "Arrived From",
                        //allowEditing: false,
                        dataType: 'date',
                        format: 'dd/MM/yyyy'
                    },
                    {
                        dataField: "actualArrive",
                        caption: "Arrived Arrive",
                        //allowEditing: false,
                        dataType: 'date',
                        format: 'dd/MM/yyyy'
                    }

                ],



                onRowUpdated: function (e) {
                    console.log(e);
                    var model = e.data;
                    $.post('/Import/GateInOut/UpdateEmptyContainerReceived', { model, model }, function (result) {
                        console.log("result", result)
                        if (result.error) {
                            showToast(result.message, "warning");
                        }

                        else {

                            showToast(result.message, "success");
                            getallemptyreceivedcontainers();
                        }

                        window.setInterval('refresh()', 3000);
                    });
                },
                onRowRemoved: function (e) {

                    console.log("e", e)

                    $.post('/Import/GateInOut/DeleteContainerReceived?id=' + e.data.emptyContainerReceivedId, function (data) {
                        if (data.error == true) {
                            showToast(data.message, "error");

                        }
                        else {

                            showToast(data.message, "success");


                        }
                    });
                },
            });


        });

    }

    function formfiled() {

    }


    function saveinfo() {
        
         
            var form = document.getElementById('EmptyContainerReceived');

    var model = $('#EmptyContainerReceived').serialize();


    console.log("model", model)
    console.log("form.reportValidity", form.reportValidity)

    form.reportValidity();


    if (form.checkValidity()) {

        console.log("ok", model);

    $.post('/Import/GateInOut/AddEmptyContainerReceived?' + model, function (data) {

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



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }
