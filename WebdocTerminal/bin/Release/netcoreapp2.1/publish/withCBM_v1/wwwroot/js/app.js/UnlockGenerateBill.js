




    function formfiled() {



    }

    $(function () {

        getcontainerdetail();






    })

    function getcontainerdetail() {
        $.get('/APICalls/Getcontainerdetail', function (data) {

            containers = data;


            console.log("containers", containers)

            var dataGrid = $("#unlockContainers").dxDataGrid({
                dataSource: containers,
                keyExpr: "key",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    enabled: false
                },
                selection: {
                    mode: 'multiple',
                },
                filterRow: {
                    visible: true,
                    applyFilter: "auto"
                },
                scrolling: {
                    columnRenderingMode: "virtual"
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },
                onInitialized(e) {
                    dataGrid = e.component;

                },
                editing: {

                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click",
                    mode: "cell"
                },
                columns: [

                    {
                        dataField: "virNo",
                        caption: "Vir No",
                        allowEditing: false,
                    },
                    {
                        dataField: "containerNo",
                        caption: "Container No",
                        allowEditing: false,
                    },
                    {
                        dataField: "status",
                        caption: "Status",
                        allowEditing: false,
                    },
                    {
                        dataField: "containerSize",
                        caption: "Container Size",
                        allowEditing: false,
                    },
                    {
                        dataField: "cargoType",
                        caption: "Cargo Type",
                        allowEditing: false,
                    },
                    {
                        dataField: "goodName",
                        caption: "Good",
                        allowEditing: false,
                        validationRules: [{ type: "required" }],

                    },
                    {
                        dataField: "shippingAgent",
                        caption: "Shipping Agent",
                        allowEditing: false,
                        validationRules: [{ type: "required" }],

                    },
                    //{
                    //    dataField: "shippingLine",
                    //    caption: "Shipping Line",
                    //    allowEditing: false,
                    //},
                    {
                        dataField: "portName",
                        caption: "Port",
                        allowEditing: false,
                    },
                    {
                        dataField: "gateInDate",
                        dataType: 'date',
                        format: 'dd/MM/yyyy',
                        caption: "Arrival Date",
                        allowEditing: false,
                    },
                    {
                        dataField: "updationDate",
                        dataType: 'date',
                        format: 'dd/MM/yyyy',
                        caption: "Updation Date",
                        allowEditing: false,
                    },
                    {
                        dataField: "countofIndexes",
                        caption: "Count",
                        allowEditing: false,
                    },
                    {
                        dataField: "remarks",
                        caption: "Remarks",
                    },
                    {
                        width: 100,
                        alignment: 'center',
                        cellTemplate: function (container, options) {

                            $('<a/>').addClass('dx-link')
                                .text('Update')
                                .on('dxclick', function () {

                                    $.post('UpdateUnlocakRemakrs?virno=' + options.data.virNo + "&containerno=" + options.data.containerNo + "&remarks=" + options.row.data.remarks, function (data) {

                                        if (data.error == true) {
                                            showToast(data.message, "error");
                                        }
                                        else {
                                            showToast(data.message, "success");
                                        }

                                    });

                                })
                                .appendTo(container);


                        }
                    }
                    //{
                    //    caption: "",
                    //    dataField: "invoiceLocked",
                    //    dataType: "boolean"

                    //}


                ],



            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');
            function calculateStatistics() {

                console.log(dataGrid)
                console.log(dataGrid.getSelectedRowsData())

                var listofcontainers = dataGrid.getSelectedRowsData();

                var newlistofcontainers = listofcontainers.filter(x => x.shippingAgent == "" || x.goodName == "");

                console.log(newlistofcontainers);

                if (newlistofcontainers.length) {
                    return showToast("please un select " + newlistofcontainers[0].containerNo + " due to FF or Goods are not avaible", "error");
                }
                else {
                    console.log("ok");
                    $.post('SaveUnlockGenerateBill', { listofcontainers: listofcontainers }, function (data) {

                        if (data.error) {
                            showToast(data.message, "error");

                        }
                        else {
                            showToast(data.message, "success");

                            window.location.href = window.location.href;
                        }
                    });

                }


            }

            $('#calculateButton').dxButton({
                text: 'save',
                type: 'default',
                onClick: calculateStatistics,
            });


        });

    }




    function unlockBillContainers() {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);



    console.log("asd", $("#unlockContainers"))
    console.log("asd", $("#unlockContainers").dxDataGrid("option", "dataSource").getSelectedRowsData())


        $("#unlockContainers").dxDataGrid("option", "dataSource").getSelectedRowsData().then((rowData) => {

        console.log("rowData", rowData)
    })

        //console.log("unlock in", containers);


        //var listofcontainers = containers.filter(c => c.invoiceLocked == false);


        //console.log("ready to ", listofcontainers)

        //$.post('SaveUnlockGenerateBill', {listofcontainers: listofcontainers }, function (data) {

        //    if (data.error) {
        //        showToast(data.message, "error");

        //    }
        //    else {
        //        showToast(data.message, "success");

        //    }
        //    window.location.href = window.location.href;

        //});
    }


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

