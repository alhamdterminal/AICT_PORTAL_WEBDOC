

    var type;

    function showFilters() {

        type = $("input[name='type']:checked").val();

        //$.get('/APICalls/GetFilterIndexWise?Type=' + type, function (partial) {
        $.get('/APICalls/GetFilterIndexWiseInvoice?Type=' + type, function (partial) {

            $('#filters').html(partial);

            console.log(" typetype ", type);


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


    getdata(igm, indexno);
    }


    function containerChangeCallback() {

        console.log("CFS")

        var indexno = $("#indexdb option:selected").text();

    console.log("indexno", indexno);

    getdata(igm, indexno);

    }


    function getdata(igm, indexno) {

        console.log("igm", igm);
    console.log("indexno", indexno);
    if (igm, indexno) {
        $.post('/Import/Setup/GetShortExcessWeight?virno=' + igm + '&indexno=' + indexno + '&type=' + type, function (data) {

            console.log(" cy data ", data);

            loadGrid(data);

        });
        }

    }



    function formfiled() {


    }


    function loadGrid(dataSource) {

        var dataGrid = $("#resdataGrid").dxDataGrid({
        dataSource: dataSource,
    keyExpr: "shortExcessWeigthId",

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
    allowDeleting: false,
    allowAdding: false,
    allowUpdating: false,
            },
    columns: [


    {
        dataField: "virNumber",
    caption: "IGM",
    allowEditing: false,
                },

    {
        dataField: "indexNumber",
    caption: "Index",
    allowEditing: false,
                },
    {
        dataField: "containerType",
    caption: "Type",
    allowEditing: false,
                },

    {
        dataField: "shortWeight",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    },
    width: 120,
    caption: "Short Weight"
                },
    {
        dataField: "excesstWeight",
    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    },
    width: 120,
    caption: "Excess Weight"
                },

    {
        dataField: "remarks",
    caption: "Remarks"
                },

    ],

    summary: {
        totalItems: [
    {
        column: "shortWeight",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "excesstWeight",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    ]
                    },

    onRowUpdated: function (e) {
        console.log(e);
    var res = e.data;

    $.post('/Import/Setup/', {res: res }, function (data) {

                    if (data.error == true) {
        showToast(data.message, "error");
                    }
    else {

        showToast(data.message, "success");
    window.location.href = window.location.href;
                    }
                });

            },

        }).dxDataGrid("instance");

    }



    function SaveInfo() {

        console.log("type", type)


        var indexno = null;

    if (type == "CFS") {
        indexno = $("#indexdb option:selected").text();
        }
    if (type == "CY") {
        indexno = $("#containerIndexCYdb option:selected").text();
        }


    var ShortWeight = $("#shortWeight").val();
    var ExcesstWeight = $("#excesstWeight").val();
    var Remarks = $("#remarks").val();


    console.log("igm", igm)
    console.log("indexno", indexno)
    console.log("ShortWeight", ShortWeight)
    console.log("ExcesstWeight", ExcesstWeight)
    console.log("remarks", remarks)


    if (ShortWeight && ExcesstWeight) {
            return showToast("please add only one  short weitgh or excess weight ", "error");
        }

    if (igm && indexno && (ShortWeight || ExcesstWeight)) {

        $.post('/Import/Setup/AddShortExcessWeight?VirNumber=' + igm + '&IndexNumber=' + indexno + '&ShortWeight=' + ShortWeight + '&ExcesstWeight=' + ExcesstWeight + '&ContainerType=' + type + '&Remarks=' + Remarks, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");
                getdata(igm, indexno)
            }

        });

            } else {
        showToast("please select igm index and weight", "error");
            }

         

      
    }





    function refresh() {
        window.location.reload();
    }



