

    var type;
    var OtherCharges = [];

    var AmountType = [
    {Name: "AICT" },
    {Name: "PARTY" },
    {Name: "FF / LINE" },
    {Name: "CLEARINGAGENT" },
    ];

    function showFilters() {

        type = $("input[name='type']:checked").val();

    $.get('/APICalls/GetFilterIndexWise?Type=' + type, function (partial) {

        $('#filters').html(partial);

    console.log(" typetype ", type);

    loadGrid([])

        })
    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function containerCallback() {
        console.log('cy')

        var indexno = $("#containerIndexCYdb option:selected").val();

    console.log("igm", igm);
    console.log("indexno", indexno);

    getcydata(igm, indexno)
    }


    function getcydata(igm, indexno) {

        console.log("igm", igm);
    console.log("indexno", indexno);
    if (igm, indexno) {
        $.post('/Import/Setup/GetOtherChargeAssigningCY?igm=' + igm + '&index=' + indexno, function (data) {


            console.log(" cy data ", data);

            loadGrid(data);
            GetStatusCYCargo(igm, indexno);
        });
        }

    }



    function containerChangeCallback() {
        console.log("CFS")

        var containerId = $("#indexdb option:selected").val();

    console.log("containerId", containerId);
    getcfsdata(containerId)

    }

    function getcfsdata(containerId) {

        console.log("containerId", containerId);

    if (containerId) {
        $.get('/Import/Setup/GetOtherChargeAssigningCFS?containerId=' + containerId, function (data) {


            console.log(" cfs data ", data);


            loadGrid(data);

            GetStatusCFSCargo(containerId);

        })

    }
    }

    function GetStatusCFSCargo(containerId) {
        $.get('/Import/Setup/GetCargoStatusCFS?containerId=' + containerId, function (data) {
            if (data.error == true) {
                alert(data.message)
            }

        })
    }

    function GetStatusCYCargo(virno  , indexno) {
        $.get('/Import/Setup/GetStatusCYCargo?virno=' + virno + '&indexno=' + indexno, function (data) {
            if (data.error == true) {
                alert(data.message)
            }

        })
    }

    $(function () {

        $.get('/Import/Setup/GetOtherCharge', function (data) {
            console.log(data)
            OtherCharges = data;
        });

    });

    function formfiled() {


    }


    function loadGrid(dataSource) {

        var grid = $("#otherchargeAsiging").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    var grid = $("#otherchargeAsiging").dxDataGrid(this.gridSettings).dxDataGrid('instance');
    var dataGrid = $("#otherchargeAsiging").dxDataGrid({
        dataSource: dataSource,
    keyExpr: "otherChargeAssigningId",

    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        pageSize: 10
                },
    editing: {
        mode: "row",
    allowDeleting: true,
    allowAdding: true
                },
    columns: [



    {
        dataField: "chargeName",
    caption: "Charge Name",
    width: 200,
    setCellValue: function (rowData, value) {
        rowData.chargeName = value;
                            var t = OtherCharges.find(d => d.chargeName == value);
    console.log(t, "t");
    if (t != null || t != undefined) {
        rowData.chargeAmount = t.chargeAmount;
                            }
                        },
    lookup: {
        dataSource: OtherCharges,
    displayExpr: "chargeName",
    valueExpr: "chargeName"
                        },
    validationRules: [{type: "required" }]

                    },

    {
        dataField: "amountType",
    validationRules: [{type: "required" }],
    caption: "Amount Type",
    lookup: {
        dataSource: AmountType,
    valueExpr: "Name",
    displayExpr: "Name"
                        }
                    },
    {
        dataField: "chargeQuantity",
    caption: "Charge Quantity",
    dataType: "number",
    format: "#,##0.##",
    validationRules: [{type: "required" }],
    editorOptions: {
        step: 0
                        }
                      

                    },

    {
        dataField: "chargeAmount",
    dataType: "number",
    format: "#,##0.##",
    width: 120,
    caption: "Charge Amount",
    allowEditing: false,
                    },

    "remarks",


    ],

    summary: {
        totalItems: [

    {
        column: "amountType",
    summaryType: "count",
    customizeText(data) {
                                return `  ${data.value.toLocaleString()}`;
                            },
                        },
    {
        column: "chargeQuantity",
    summaryType: "sum",
    customizeText(data) {
                                return `  ${data.value.toLocaleString()}`;
                            },
                        },
    {
        column: "chargeAmount",
    summaryType: "sum",
    customizeText(data) {
                                return `  ${data.value.toLocaleString()}`;
                            },
                        },

    ]
                },

    onRowInserted: function (e) {

        console.log(e)
                    console.log(e.data)
    var data = e.data;

    if (type == "CFS") {

                        var containerId = $("#indexdb option:selected").val();

    console.log("containerId", containerId);



    if (containerId) {
        $.post('/Import/Setup/AddOtherChargeAssigningCFS?ContainerId=' + containerId, { data, data }, function (result) {

            console.log("result", result)
            if (result.error) {
                showToast(result.message, "warning");
            }

            else {

                showToast(result.message, "success");

                getcfsdata(containerId);
            }

            //window.setInterval('refresh()', 3000);
        });
                        }
    else {

        loadGrid([])
                            showToast("Please Select index", "error");
                           
                        }

                     


                    }
    if (type == "CY") {
                         
                        var indexno = $("#containerIndexCYdb option:selected").val();

    console.log("igm", igm);
    console.log("indexno", indexno);

    if (igm && indexno) {
        $.post('/Import/Setup/AddOtherChargeAssigningCY?igm=' + igm + '&index=' + indexno, { data, data }, function (result) {

            console.log("result", result)
            if (result.error) {
                showToast(result.message, "warning");
            }

            else {

                showToast(result.message, "success");
                getcydata(igm, indexno)
            }

            //  window.setInterval('refresh()', 3000);
        });
                        } else {

        loadGrid([]);
    showToast("Please Select index", "error");
                             
                        }
                         




                    }
                     
                   
                     
                },


    onRowRemoved: function (e) {

        console.log(e)
                    console.log(e.data)
    var data = e.data.otherChargeAssigningId;

    $.post('/Import/Setup/DeleteOtherChargeAssigned?key=' + data, function (result) {

        console.log("result", result)
                                if (result.error) {
        showToast(result.message, "warning");
                                }

    else {

        showToast(result.message, "success");
                                     
                                }
                         
                            });
                      
                         

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


    function refresh() {
        window.location.reload();
    }



