

    var type;

    function showFilters() {

        type = $("input[name='type']:checked").val();

        //$.get('/APICalls/GetFilterIndexWise?Type=' + type, function (partial) {
        $.get('/APICalls/GetFilterIndexWiseInvoice?Type=' + type, function (partial) {

            $('#filters').html(partial);

            console.log(" typetype ", type);


            $('#shippinggnet').val('');
            $('#Goods').val('');

            loadGrid([]);

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

    getigmdetail(igm, indexno);
    getdata(igm, indexno);
    }


    function containerChangeCallback() {

        console.log("CFS")

        var indexno = $("#indexdb option:selected").text();

    console.log("indexno", indexno);
    getigmdetail(igm, indexno);
    getdata(igm, indexno);

    }


    function getdata(igm, indexno) {

        console.log("igm", igm);
    console.log("indexno", indexno);
    if (igm, indexno) {
        $.post('/Import/Setup/GetBillToLineDetailIgmIndexWise?virno=' + igm + '&indexno=' + indexno + '&type=' + type, function (data) {

            console.log(" cy data ", data);

            loadGrid(data);

        });
        }

    }

    function getigmdetail(igm, indexno) {

        console.log("igm", igm);
    console.log("indexno", indexno);
    if (igm, indexno) {
        $.post('/APICalls/GetFFAndGoodsInfo?virno=' + igm + '&indexno=' + indexno + '&type=' + type, function (data) {

            console.log("GetFFAndGoodsInfo ", data);

            $('#shippinggnet').val(data.shippingAgent);
            $('#Goods').val(data.goodName);

        });
        }

    }



    function formfiled() {


    }


    function loadGrid(dataSource) {

        var dataGrid = $("#resdataGrid").dxDataGrid({
        dataSource: dataSource,
    keyExpr: "billToLineId",

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
    allowAdding: false
            },
    columns: [


    {
        dataField: "inoviceCreated",
    width: 120,
    caption: "Delivered",
    allowEditing: false,
                },
    {
        dataField: "createdDate",
    width: 120,
    caption: "Created Date",
    dataType: 'date',
    format: 'dd/MM/yyyy',
    allowEditing: false,
                },
    {
        dataField: "type",
    width: 120,
    caption: "Type",
    allowEditing: false,
                },
    {
        dataField: "tariffAmount",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    },
    width: 120,
    caption: "Tariff Amount",
    allowEditing: false,
                },
    {
        dataField: "invoiceNumber",
    dataType: "number",
    width: 120,
    caption: "Invoice No",
    allowEditing: false,
                },
    {
        dataField: "invoiceAmount",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    },
    width: 120,
    caption: "Invoice Amount",
    allowEditing: false,
                },
    {
        dataField: "remarks",
    caption: "Remarks",
    allowEditing: false,
                },

    ],

    summary: {
        totalItems: [
    {
        column: "tariffAmount",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "invoiceAmount",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    ]
                    },

    onRowRemoved: function (e) {

        console.log(e)
                console.log(e.data)

    $.post('/Invoice/DeleteBillToLineById?key=' + e.data.billToLineId, function (result) {

        console.log("result", result)
                    if (result.error) {
        showToast(result.message, "warning");
    getdata(e.data.virNo, e.data.indexNo)
                    }

    else {

        showToast(result.message, "success");
    getdata(e.data.virNo, e.data.indexNo)
                    }

                });



            }

        }).dxDataGrid("instance");

    }



    function saveManualAmount() {

        console.log("type", type)

        if (type == "CFS") {

            var indexno = $("#indexdb option:selected").text();
    var maunualAmount = $("#maunualAmount").val();
    var remarks = $("#remarks").val();


    console.log("igm", igm)
    console.log("indexno", indexno)
    console.log("maunualAmount", maunualAmount)
    console.log("remarks", remarks)


    if (igm && indexno && maunualAmount) {

        $.post('/Invoice/SaveManualAmount?igm=' + igm + '&indexno=' + indexno + '&maunualAmount=' + maunualAmount + '&type=' + type + '&remarks=' + remarks, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");
                getdata(igm, indexno)
            }

        });

            } else {
        showToast("please select igm index and amount", "error");
            }
            

        }

    if (type == "CY") {

            var indexno = $("#containerIndexCYdb option:selected").text();
    var maunualAmount = $("#maunualAmount").val();
    var remarks = $("#remarks").val();


    console.log("igm", igm)
    console.log("indexno", indexno)
    console.log("maunualAmount", maunualAmount)
    console.log("remarks", remarks)


    if (igm && indexno && maunualAmount) {

        $.post('/Invoice/SaveManualAmount?igm=' + igm + '&indexno=' + indexno + '&maunualAmount=' + maunualAmount + '&type=' + type + '&remarks=' + remarks, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");
                getdata(igm, indexno)
            }

        });

            } else {
        showToast("please select igm index and amount", "error");
            }
             
        }
         
    }



    function AssignExStorage() {



        if (type == "CFS") {

            var indexno = $("#indexdb option:selected").text();

    var remarks = $("#remarks").val();


    console.log("igm", igm)
    console.log("indexno", indexno)
    console.log("remarks", remarks)


    if (igm && indexno && maunualAmount) {

        $.post('/Invoice/AssignExStorage?igm=' + igm + '&indexno=' + indexno + '&type=' + type + '&remarks=' + remarks, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");
                getdata(igm, indexno)
            }

        });

            } else {
        showToast("please select igm index and amount", "error");
            }


        }

    if (type == "CY") {

            var indexno = $("#containerIndexCYdb option:selected").text();
    var remarks = $("#remarks").val();


    console.log("igm", igm)
    console.log("indexno", indexno)
    console.log("remarks", remarks)


    if (igm && indexno && maunualAmount) {

        $.post('/Invoice/AssignExStorage?igm=' + igm + '&indexno=' + indexno + '&type=' + type + '&remarks=' + remarks, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");
                getdata(igm, indexno)
            }

        });

            } else {
        showToast("please select igm index and amount", "error");
            }



        }


         
    }

    function AssignOnlyTariff() {


        if (type == "CFS") {

            var indexno = $("#indexdb option:selected").text();

    var remarks = $("#remarks").val();


    console.log("igm", igm)
    console.log("indexno", indexno)
    console.log("remarks", remarks)


    if (igm && indexno && maunualAmount) {

        $.post('/Invoice/AssignOnlyTariff?igm=' + igm + '&indexno=' + indexno + '&type=' + type + '&remarks=' + remarks, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");
                getdata(igm, indexno)
            }

        });

            } else {
        showToast("please select igm index and amount", "error");
            }


        }

    if (type == "CY") {

            var indexno = $("#containerIndexCYdb option:selected").text();
    var remarks = $("#remarks").val();


    console.log("igm", igm)
    console.log("indexno", indexno)
    console.log("remarks", remarks)


    if (igm && indexno && maunualAmount) {

        $.post('/Invoice/AssignOnlyTariff?igm=' + igm + '&indexno=' + indexno + '&type=' + type + '&remarks=' + remarks, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");

                getdata(igm, indexno)

            }

        });

            } else {
        showToast("please select igm index and amount", "error");
            }



        }
         
    }

    function BalancetoZero() {

        if (type == "CFS") {

            var indexno = $("#indexdb option:selected").text();

    var remarks = $("#remarks").val();


    console.log("igm", igm)
    console.log("indexno", indexno)
    console.log("remarks", remarks)


    if (igm && indexno && maunualAmount) {

        $.post('/Invoice/AssignHundredPercent?igm=' + igm + '&indexno=' + indexno + '&type=' + type + '&remarks=' + remarks, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");
                getdata(igm, indexno)
            }

        });

            } else {
        showToast("please select igm index and amount", "error");
            }


        }

    if (type == "CY") {

            var indexno = $("#containerIndexCYdb option:selected").text();
    var remarks = $("#remarks").val();


    console.log("igm", igm)
    console.log("indexno", indexno)
    console.log("remarks", remarks)


    if (igm && indexno && maunualAmount) {

        $.post('/Invoice/AssignHundredPercent?igm=' + igm + '&indexno=' + indexno + '&type=' + type + '&remarks=' + remarks, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");
                getdata(igm, indexno)
            }

        });

            } else {
        showToast("please select igm index and amount", "error");
            }



        }
    }


    function refresh() {
        window.location.reload();
    }



