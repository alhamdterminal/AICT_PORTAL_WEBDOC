

    var keyCodes = {
        TAB: 9,
    PAGE_UP: 33,
    PAGE_DOWN: 34,
    LEFT: 37,
    UP: 38,
    RIGHT: 39,
    DOWN: 40,

    };

    var containers = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function filterGrid(virno, containerno, indexno, type) {

        console.log("virno", virno)
        console.log("containerno", containerno)
    console.log("indexno", indexno)
    console.log("type", type)

    if (virno == undefined) {
        virno = "";
        }
    if (containerno == undefined) {
        containerno = "";
        }
    if (indexno == undefined) {
        indexno = "";
        }

    console.log("after virno", virno)
    console.log("after containerno", containerno)
    console.log("after indexno", indexno)
    console.log("type", type)




    $.get('/APICalls/GetPortChargesDetailContainerWise?virno=' + virno + '&containerno=' + containerno + '&indexno=' + indexno + '&type=' + type, function (data) {

            if (data) {

        containers = data;

    containerIndexesGrid(containers)
            }

    else {
        containers = []
                containerIndexesGrid(containers);

            }


        });


    }







    function formfiled() {

    }


    function loadingBar() {

        console.log("load")
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'block';

    $("#large-indicator").dxLoadIndicator({
        height: 60,
    width: 60
        });
    }
    function loadingBarHide() {
        console.log("loaded")
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }



    function containerIndexesGrid(res) {

        containers = res;

    console.log("containers", containers)

    var grid = $("#PortchargesGrid").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    var dataGrid = $("#PortchargesGrid").dxDataGrid({
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

    editing: {
        allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click",
    mode: "row"

            },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

            },
    columns: [

    {
        dataField: "shippingAgent",
    caption: "Line/FF",
    allowEditing: false,
    fixed: true,
                }, {
        dataField: "type",
    caption: "Type",
    allowEditing: false,
    fixed: true,
                },

    {
        dataField: "virNumber",
    caption: "IGM",
    allowEditing: false,
    fixed: true,
                },
    {
        dataField: "containerNumber",
    caption: "Container_No",
    allowEditing: false,
    fixed: true,
                } ,
    {
        dataField: "demurrageCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Demurrage_Charges"
                },
    {
        dataField: "weighmentCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Weighment_Charges"
                },
    {
        dataField: "overWeightPenalty",
    dataType: "number",
    format: "#,##0.##",
    caption: "OverWeight_Penalty"
                },
    {
        dataField: "detentionChargesOrBulletSeal",
    dataType: "number",
    format: "#,##0.##",
    caption: "DetentionCharges_Or_BulletSeal"
                },
    {
        dataField: "thcPhcKdlpCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "THC_PHC_KDLP_Charges"
                },
    {
        dataField: "loloCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Lolo_Charges"
                },
    {
        dataField: "qictCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Qict_Charges"
                },
    {
        dataField: "otherCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Other_Charges"
                },
    {
        dataField: "washAndCleanCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Wash_And_Clean_Charges"
                },
    {
        dataField: "anf",
    dataType: "number",
    format: "#,##0.##",
    caption: "ANF"
                },
    {
        dataField: "pallet",
    dataType: "number",
    format: "#,##0.##",
    caption: "Pallet"
                },
    {
        dataField: "recover",
    dataType: "number",
    format: "#,##0.##",
    caption: "Recover"
                },
    {
        dataField: "transportCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Transport_Charges"
                },
    {
        dataField: "totalCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Total_Charges",
    allowEditing: false,
                },


    ],

    summary: {
        totalItems: [

    {
        column: "demurrageCharges",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    {
        column: "weighmentCharges",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    {
        column: "overWeightPenalty",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    {
        column: "detentionChargesOrBulletSeal",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    {
        column: "thcPhcKdlpCharges",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    {
        column: "loloCharges",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    {
        column: "qictCharges",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    {
        column: "otherCharges",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    {
        column: "washAndCleanCharges",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    {
        column: "anf",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    {
        column: "pallet",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    {
        column: "recover",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    {
        column: "transportCharges",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    {
        column: "totalCharges",
    summaryType: "sum",
    displayFormat: '{0}',
                    },
    ]
            },

    onRowUpdated: function (e) {



                var model = e.data;
    console.log("model", model);
    $.post('/Tariff/AddPortChargesContainerWise', {model, model}, function (data) {

                    if (data.error == true) {
        showToast(data.message, "error");

    var virno = $("#virnolist").dxAutocomplete("instance").option("value");

    var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value");

    var containerno = $("#containerlist").dxAutocomplete("instance").option("value");
    filterGrid(virno, indexno, containerno, model.type)



                    } else {
        showToast(data.message, "success")

                        var virno = $("#virnolist").dxAutocomplete("instance").option("value");

    var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value");

    var containerno = $("#containerlist").dxAutocomplete("instance").option("value");


    console.log('virno', virno);
    console.log('indexno', indexno);
    console.log('containerno', containerno);


    filterGrid(virno, indexno, containerno, model.type)
                    }

                });
            },
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


    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }


    function showcargoDetaildesc() {

        var res = [];
    containerIndexesGrid(res);

    var type = $('#ContainerType').val();

    console.log("type", type)

    if (type == null) {

            return showToast("please select type", "error");

        } else {
        console.log("type ok ", type)
            var virno = $("#virnolist").dxAutocomplete("instance").option("value");

    var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value");

    var containerno = $("#containerlist").dxAutocomplete("instance").option("value");


    console.log('virno', virno);
    console.log('indexno', indexno);
    console.log('containerno', containerno);
    filterGrid(virno, containerno, indexno, type);
        }



        //console.log('agent', agent);

        //

    }

