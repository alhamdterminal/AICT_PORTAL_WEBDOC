


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    var shippingAgentId = 0;

    var EmptyContainerTariffList = [];

    var EmptyContainerStorageList = [];


    function loadGrid(EmptyContainerTariffList) {

        console.log("EmptyContainerTariffList", EmptyContainerTariffList)

        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    $("#gridContainer").dxDataGrid({
        dataSource: EmptyContainerTariffList,
    keyExpr: "emptyContainerTariffId",
    wordWrapEnabled: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    paging: {
        enabled: false
                },
    editing: {
        mode: "row",
    allowUpdating: true,
    allowDeleting: true,
    allowAdding: true
                },
    columns: [


    {
        dataField: "emptyContainerTariffName",
    caption: "Name",
    validationRules: [{type: "required" }],
               
                    },

    {
        dataField: "rate20",
    caption: "Rate 20",
    dataType: "number",
    validationRules: [{type: "required" }],
    format: "#,##0.##",
    editorOptions: {
        step: 0
                        }

                    },
    {
        dataField: "rate40",
    caption: "Rate 40",
    dataType: "number",
    validationRules: [{type: "required" }],
    format: "#,##0.##",
    editorOptions: {
        step: 0
                        }

                    },
    {
        dataField: "rate45",
    caption: "Rate 45",
    dataType: "number",
    validationRules: [{type: "required" }],
    format: "#,##0.##",
    editorOptions: {
        step: 0
                        }
                    },

    ],

    onRowUpdated: function (e) {
        console.log(e);
    var res = e.data;

    $.post('/Tariff/updateEmptyContainerTariff', {res: res }, function (data) {

                            if (data.error == true) {
        showToast(data.message, "error");

                            }
    else {

        showToast(data.message, "success");
    getstorageDetailbytariffId(res.shippingAgentId);
                            }
                        });

                },
    onRowRemoved: function (e) {

        console.log("e", e)

                    $.post('/Tariff/DeleteEmptyContainerTariff?emptyContainerTariffId=' + e.data.emptyContainerTariffId, function (data) {
                        if (data.error == true) {
        showToast(data.message, "error");

                        }
    else {

        showToast(data.message, "success");
    getstorageDetailbytariffId(e.data.shippingAgentId);

                        }
                    });
                },
    onRowInserted: function (e) {
        console.log("ee", e);
    addEmptycontainerTariff(e);
                }
            }).dxDataGrid("instance");



    grid.on('editorPrepared', function (e) {
            if (e.parentType !== 'dataRow') {
                return;
            }

    e.editorElement[e.editorName]('instance').on('keyDown', function (arg) {

                var gridComponent = e.component;

    var event = arg.jQueryEvent;

    if (event.keyCode === 38) {
                    if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                    }

                } else if (event.keyCode === 40) {
                    if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                    }
                }
    else if (event.keyCode === 189) {
                    if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                    }

                }
            });


        });
    }

    function addEmptycontainerTariff(e) {

            var data = e.data;

    var shippingAgentId = $('#shippingAgentId').val();
    if (shippingAgentId) {

        console.log("data", data)

            $.post('/Tariff/AddEmptycontainerTariff?shippingAgentId=' + shippingAgentId, {data: data }, function (data) {


                    if (data.error == true) {
        showToast(data.message, "error");
                    } else {
        showToast(data.message, "success");
    getstorageDetailbytariffId(shippingAgentId)
                    }


                });

            }
    else {
        showToast("please select FF", "error");
    getstorageDetailbytariffId(0)
            }
 
    }


    function formfiled() {

        $('#shippingAgentId').on('change', function (e) {

            console.log(this.value);

            getstorageDetailbytariffId(this.value);

        });


    $('#emptyContainerTariffId').on('change', function (e) {

        console.log(this.value);

    getstoragebytariffId(this.value);

        });

    }



    function getstorageDetailbytariffId(ShippingAgentId) {


        $.get('/Tariff/GetEmptyContainerTariff?ShippingAgentId=' + ShippingAgentId, function (data) {

            if (data) {
                EmptyContainerTariffList = data;
            }
            else {
                EmptyContainerTariffList = [];
            }

            loadGrid(EmptyContainerTariffList);
        })

    }


    function getstoragebytariffId(tariffid) {


        $.get('/Tariff/GetEmptyContainerStorageSlabs?tariffid=' + tariffid, function (data) {

            if (data) {
                EmptyContainerStorageList = data;
            }
            else {
                EmptyContainerStorageList = [];
            }

            loadGridStorgeSlabs(EmptyContainerStorageList);
        })

    }
    function loadGridStorgeSlabs(EmptyContainerStorageList) {

        console.log("EmptyContainerStorageList", EmptyContainerStorageList);

    var grid = $("#gridContainerStorageTariff").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    $("#gridContainerStorageTariff").dxDataGrid({
        dataSource: EmptyContainerStorageList,
    keyExpr: "emptyContainerStorageSlabId",
    wordWrapEnabled: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    paging: {
        enabled: false
            },
    editing: {
        mode: "row",
    allowUpdating: true,
    allowDeleting: true,
    allowAdding: true
            },
    columns: [

    {
        dataField: "rate20",
    caption: "Rate 20",
    dataType: "number",
    validationRules: [{type: "required" }],
    format: "#,##0.##"

                },
    {
        dataField: "rate40",
    caption: "Rate 40",
    dataType: "number",
    validationRules: [{type: "required" }],
    format: "#,##0.##"

                },
    {
        dataField: "rate45",
    caption: "Rate 45",
    dataType: "number",
    validationRules: [{type: "required" }],
    format: "#,##0.##"
                },
    {
        dataField: "description",
    caption: "Description",
    validationRules: [{type: "required" }],
            
                },
    {
        dataField: "noOfDays",
    caption: "Number Of Days",
    validationRules: [{type: "required" }],

                },

    ],

    onRowUpdated: function (e) {
        console.log(e);
    var res = e.data;

    $.post('/Tariff/updateEmptyContainerStorageSlab', {res: res }, function (data) {

                    if (data.error == true) {
        showToast(data.message, "error");
                    }
    else {

        showToast(data.message, "success");
    getstoragebytariffId(res.emptyContainerTariffId);
                    }
                });

            },
    onRowRemoved: function (e) {

        console.log("e", e)

                $.post('/Tariff/DeleteEmptyContainerStorageSlab?emptyContainerStorageSlabId=' + e.data.emptyContainerStorageSlabId, function (data) {
                    if (data.error == true) {
        showToast(data.message, "error");

                    }
    else {

        showToast(data.message, "success");
    getstorageDetailbytariffId(e.data.emptyContainerTariffId);

                    }
                });
            },
    onRowInserted: function (e) {
        console.log("ee", e);
    addEmptycontainerSlab(e);
            }
        }).dxDataGrid("instance");



    grid.on('editorPrepared', function (e) {
            if (e.parentType !== 'dataRow') {
                return;
            }

    e.editorElement[e.editorName]('instance').on('keyDown', function (arg) {

                var gridComponent = e.component;

    var event = arg.jQueryEvent;

    if (event.keyCode === 38) {
                    if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                    }

                } else if (event.keyCode === 40) {
                    if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                    }

                }
            });


        });

    }


    function addEmptycontainerSlab(e) {

        var data = e.data;

    var tariffid = $('#emptyContainerTariffId').val();
    if (tariffid) {

        console.log("data", data)

            $.post('/Tariff/AddEmptycontainerStorageSlab?tariffid=' + tariffid, {data: data }, function (data) {


                if (data.error == true) {
        showToast(data.message, "error");
                } else {
        showToast(data.message, "success");
    getstoragebytariffId(tariffid)
                } 
            });

        }
    else {
        showToast("please select Tariff Head", "error");
    getstoragebytariffId(0)
        }

    }

    /**/
