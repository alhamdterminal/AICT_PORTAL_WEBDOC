



    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var waiverno = url.searchParams.get("waiverno");


    console.log("waiverno", waiverno);


    $.get('/Invoice/GetWaiverByNumber?waiverno=' + waiverno, function (data) {

        console.log("data", data);

    var tariffdata = data;

    console.log("tariffdata", tariffdata);


    if (tariffdata.length) {

        tariffdata.forEach(e => e.percent = 0);

    console.log("new tariffdata", tariffdata);



    var grid = $("#WaiverTariffgrid").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#WaiverTariffgrid").dxDataGrid({
        dataSource: tariffdata,
    keyExpr: "waiverId",
    showBorders: false,
    showBorders: false,
    showColumnLines: false,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",


    sorting: {
        mode: "none"
                    },
    paging: {
        enabled: false
                    },
    editing: {
        mode: "row",
    allowUpdating: true,
    selectTextOnEditStart: false,
    startEditAction: "click"
                    },
    columns: [

    {
        dataField: "description",
    caption: "Name",
    width: 200

                        },

    {
        dataField: "tariffAmount",
    caption: "CR Amount #",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                            }

                        },
    {
        dataField: "percent",
    caption: "Percent",
    width: 100,
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                            }
                        },
    {
        dataField: "discount",
    validationRules: [{type: "required" }],
    caption: "Discount",
    calculateCellValue: function (rowData) {

        console.log("rowData.", rowData.percent)

                                if (rowData.percent > 0) {


                                    var calculatedvalue = (rowData.tariffAmount / 100) * rowData.percent;


                                    if (calculatedvalue > rowData.tariffAmount) {
                                        return rowData.discount = 0;
                                    }
    else {
                                        return rowData.discount = calculatedvalue;
                                    }

                                   
                                }
    else {
                                    var calculatedvalue = rowData.discount;


    return rowData.discount = calculatedvalue;
                                }
                                
                            },

                        },
                        //{
                        //    dataField: "tariffHeadAmount",
                        //    validationRules: [{type: "required" }],
                        //    allowEditing: false,
                        //    caption: "Tariff Head Amount",
                        //    calculateCellValue: function (rowData) {
                        //        console.log("rowData.totalCharges", rowData.tariffAmount);
                        //        console.log("rowData.discount", rowData.discount);
                        //        var calculatedvalue = rowData.tariffAmount - rowData.discount;

                        //        console.log("calculatedvalue", calculatedvalue)

                        //        if (calculatedvalue < 0 || calculatedvalue == undefined || calculatedvalue == null || calculatedvalue == "") {
                        //            showToast("please reenter amount ");
                        //            return rowData.discount = 0;
                        //            return rowData.percent = 0;
                        //        }
                        //        else {
                        //            return rowData.tariffHeadAmount = calculatedvalue;
                        //        }

                                
                        //    },

                        //},



                    ]


                }).dxDataGrid("instance");



    //var storageAmount = tariffdata[0].storageAmount;
    var remarks = tariffdata[0].remarks;

    //console.log("storageAmount", storageAmount)
    console.log("remarks", remarks)

    //$("#storageAmount").val(storageAmount);
    $("#remarks").val(remarks);


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

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
                $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');
            }

             


        })

    });


    function formfiled() {

    }


    function saveWaiverInfo() { 


        var url_string = window.location.href
    var url = new URL(url_string);
    var waiverno = url.searchParams.get("waiverno");


    var tariffdata = $("#WaiverTariffgrid").dxDataGrid("option", "dataSource");

    //var storageAmount = $("#storageAmount").val();

    console.log("tariffdata", tariffdata);
    //console.log("storageAmount", storageAmount)
    console.log("waiverno", waiverno);


    var tariffAmounttotal = 0;
    var discounttariffAmounttotal = 0;
        
        tariffdata.forEach(c => {
        tariffAmounttotal += c.tariffAmount;
    discounttariffAmounttotal += c.discount;
            });


    console.log("tariffAmounttotal", tariffAmounttotal);
    console.log("discounttariffAmounttotal", discounttariffAmounttotal);



    if (tariffAmounttotal < discounttariffAmounttotal) {
        showToast("tariff amount is less then discounte amount", "warning");
        }
    else {
        $.post('/Invoice/UpdateWaiverInfo?waiverno=' + waiverno, { tariffdata: tariffdata }, function (data) {

            console.log("data", data);

            showToast(data.message, "warning");


            window.location.href = window.location.href;

        });
        }
       


    }


    function BalancetoZero() {
        console.log("BalancetoZero")


        var tariffdata = $("#WaiverTariffgrid").dxDataGrid("option", "dataSource");

    console.log("tariffdata", tariffdata);

    if (tariffdata.length) {
        tariffdata.forEach(e => e.percent = 100);

    console.log("after percent age tariffdata", tariffdata);


    $("#WaiverTariffgrid").dxDataGrid({
        dataSource: tariffdata,
    keyExpr: "waiverId",
    showBorders: false,
    showBorders: false,
    showColumnLines: false,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",


    sorting: {
        mode: "none"
                },
    paging: {
        enabled: false
                },
    editing: {
        mode: "row",
    allowUpdating: true,
    selectTextOnEditStart: false,
    startEditAction: "click"
                },
    columns: [

    {
        dataField: "description",
    caption: "Name",
    width: 200

                    },
    {
        dataField: "tariffAmount",
    caption: "CR Amount #"

                    },
    {
        dataField: "percent",
    caption: "Percent",
    width: 100
                    },
    {
        dataField: "discount",
    validationRules: [{type: "required" }],

    caption: "Discount",
    calculateCellValue: function (rowData) {

        console.log("rowData.", rowData.percent)

                            if (rowData.percent > 0) {


                                //var calculatedvalue = (rowData.tariffAmount / 100) * rowData.percent;

                                var calculatedvalue = (rowData.tariffAmount * rowData.percent) / 100;


                                if (calculatedvalue > rowData.tariffAmount) {
                                    return rowData.discount = 0;
                                }
    else {
                                    return rowData.discount = calculatedvalue;
                                }


                            }
    else {
                                var calculatedvalue = rowData.discount;


    return rowData.discount = calculatedvalue;
                            }
                        },

                    },
                    //{
                    //    dataField: "tariffHeadAmount",
                    //    validationRules: [{type: "required" }],
                    //    allowEditing: false,
                    //    caption: "Tariff Head Amount",
                    //    calculateCellValue: function (rowData) {
                    //        console.log("rowData.totalCharges", rowData.tariffAmount);
                    //        console.log("rowData.discount", rowData.discount);
                    //        var calculatedvalue = rowData.tariffAmount - rowData.discount;

                    //        console.log("calculatedvalue", calculatedvalue)

                    //        return rowData.tariffHeadAmount = calculatedvalue;
                    //    },

                    //},




                ]


            }).dxDataGrid("instance");


           // $("#storageAmount").val(0)
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


