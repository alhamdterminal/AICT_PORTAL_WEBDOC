
    var OtherCharges = [];

    var AmountType = [
    {Name: "AICT" },
    {Name: "PARTY" },
    {Name: "FF / LINE" },
    {Name: "CLEARINGAGENT" },
    ];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function getgddata(gdnumber) {

        console.log("gdnumber", gdnumber);

    if (gdnumber) {
        $.get('/Export/ExportSetup/GetOtherChargeAssigningexport?gdnumber=' + gdnumber, function (data) {

            loadGrid(data);
        })

    }
    else {
        loadGrid([]);
        }
    }


    $(function () {

        $.get('/Export/ExportSetup/GetOtherCharge', function (data) {
            console.log(data)
            OtherCharges = data;
        });

    });

    function formfiled() {


    }


    function loadGrid(dataSource) {
          
        var grid = $("#otherchargeAsiging").dxDataGrid(this.gridSettings).dxDataGrid('instance');
    var dataGrid = $("#otherchargeAsiging").dxDataGrid({
        dataSource: dataSource,
    keyExpr: "otherChargeAssigningExportId",
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
 
                    var data = e.data;

    var Instance = $("#GDNumberSearch").dxAutocomplete("instance").option("value");

    if (Instance) {
        $.post('/Export/ExportSetup/AddOtherChargeAssigningExport?gdnumber=' + Instance, { data, data }, function (result) {

            console.log("result", result)
            if (result.error) {
                showToast(result.message, "warning");
            }
            else {
                showToast(result.message, "success");

                getgddata(Instance);
            }

        });
                        }
    else {

        loadGrid([])
                            showToast("Please Select index", "error");

                        }


                         
                   


                },


    onRowRemoved: function (e) {

        console.log(e)
                    console.log(e.data)
    var data = e.data.otherChargeAssigningExportId;

    $.post('/Export/ExportSetup/DeleteOtherChargeAssigned?key=' + data, function (result) {

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


    function gd_changed(data) {

        getgddata(data.value);

     }


