

    var GatePassWeights = [];


    $(function () {
        showFilters();
    console.log("test")
    })

    function showFilters() {


        $.get('/APICalls/GetFilterIndexWiseForGatePassWeighment', function (partial) {

            $('#filters').html(partial);


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



    function containerChangeCallback() {
        console.log("CFS")

        var indexno = $("#indexdb option:selected").text();

    console.log("indexno", indexno);
    getcfsdata(indexno)

    }

    function getcfsdata(indexno) {

        console.log("indexno", indexno);

    if (igm && indexno) {
        $.get('/Import/Setup/GetGatePassWeightments?igm=' + igm + '&index=' + indexno, function (data) {


            console.log(" cfs data ", data);

            if (data) {
                GatePassWeights = data;
            } else {
                GatePassWeights = [];
            }



            console.log("  GatePassWeights data ", GatePassWeights);

            loadGrid()

        })

    }
    }


    function formfiled() {


    }


    function loadGrid( ) {


        console.log("GatePassWeights", GatePassWeights);

    var grid = $("#gatePassWeightment").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    var dataGrid = $("#gatePassWeightment").dxDataGrid({
        dataSource: GatePassWeights,
    keyExpr: "gatePassWeightmentId",
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
        dataField: "truckNumber",
    caption: "Truck No" ,
    validationRules: [{type: "required" }],


                    },
    {
        dataField: "grossWeight",
    caption: "GrossWeight",
    dataType: "number",
    validationRules: [{type: "required" }],
    editorOptions: {
        step: 0
                        }

                    },
    {
        dataField: "manifestedWeight",
    caption: "ManifestedWeight",
    dataType: "number",
    validationRules: [{type: "required" }],
    editorOptions: {
        step: 0
                        }

                    }

    ],
    onRowInserted: function (e) {

        console.log(e)
                    console.log(e.data)
    var data = e.data;


    var indexno = $("#indexdb option:selected").text();

    console.log("igm", igm);
    console.log("indexno", indexno);
    console.log("data", data);

    if (igm && indexno) {

        console.log("ok")

                        $.post('/Import/Setup/AddGatePassWeighment?igm=' + igm + '&indexno=' + indexno, {data, data}, function (result) {

        console.log("result", result)
                                if (result.error) {
        showToast(result.message, "warning");
                                }

    else {

        showToast(result.message, "success");

    getcfsdata(indexno);
                                }

    window.setInterval('refresh()', 3000);
                            });
                        }
    else {
        showToast("Please Select index", "error");
                        }





                     


                },


    onRowRemoved: function (e) {

        console.log(e)
                    console.log(e.data)
    var data = e.data.gatePassWeightmentId;

    $.post('/Import/Setup/DeleteGatePassWeighment?key=' + data, function (result) {

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

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }


    function refresh() {
        window.location.reload();
    }



