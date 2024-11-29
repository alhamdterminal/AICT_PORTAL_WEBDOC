

    var osvmlist = [];
    var bondedlist = [];


    $(function () {

        getosvms();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getosvms() {
        $.get('/Export/OSVM/GetPenddingosvms', function (data) {
            osvmlist = data;
            loadgrid(osvmlist);
        });
    }

    function loadgrid(osvmlist) {

        $.get('/APICalls/GetAllBoundedTranspoter', function (data) {
            bondedlist = data;

            $("#gridContainer").dxDataGrid({
                dataSource: osvmlist,
                keyExpr: "osvmId",
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
                    allowDeleting: false,
                    allowAdding: false
                },
                columns: [
                    {
                        dataField: "gdNumber",
                        validationRules: [{ type: "required" }],
                        caption: "GD Number",
                        allowEditing: false,
                    },
                    {
                        dataField: "containerNo",
                        validationRules: [{ type: "required" }],
                        caption: "Container No",
                        allowEditing: false,
                    },
                    {
                        dataField: "pccssSealNumber",
                        validationRules: [{ type: "required" }],
                        caption: "Pccss SealNumber"
                    },
                    {
                        dataField: "vehicleNo",
                        validationRules: [{ type: "required" }],
                        caption: "Vehicle No"

                    },
                    {
                        dataField: "bondedCarrierName",
                        caption: "Bonded Carrier",
                        lookup: {
                            dataSource: bondedlist,
                            displayExpr: "boundedCarrierName",
                            valueExpr: "boundedCarrierName"
                        },
                        validationRules: [{ type: "required" }]

                    },

                ],


                onRowUpdated: function (e) {
                    console.log(e.data)
                    var model = e.data;
                    $.post('/Export/OSVM/Updateosvm', { model, model }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error")
                            getosvms();
                        } else {
                            showToast(data.message, "success")
                            getosvms();


                        }
                    });
                },

            });

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });
    }



    function formfiled() {
        //getcities();

    }
