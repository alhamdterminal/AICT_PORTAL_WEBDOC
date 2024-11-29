

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
    export: {
        enabled: true
            },
    onExporting(e) {

                const workbook = new ExcelJS.Workbook();

    const worksheet = workbook.addWorksheet('PortChargesList');

    DevExpress.excelExporter.exportDataGrid({
        component: e.component,
    worksheet,
    autoFilterEnabled: true,
                }).then(() => {
        workbook.xlsx.writeBuffer().then((buffer) => {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'PortChargesList.xlsx');
        });
                });
    e.cancel = true;


            },
    editing: {
        allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click",
    mode: "cell"

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
                },
    {
        dataField: "demurrageCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Demurrage_Charges",
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "weighmentCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Weighment_Charges",
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "overWeightPenalty",
    dataType: "number",
    format: "#,##0.##",
    caption: "OverWeight_Penalty",
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "detentionChargesOrBulletSeal",
    dataType: "number",
    format: "#,##0.##",
    caption: "DetentionCharges_Or_BulletSeal",
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "thcPhcKdlpCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "THC_PHC_KDLP_Charges",
    editorOptions: {
        step: 0
                    }


                },
    {
        dataField: "loloCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Lolo_Charges",
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "qictCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Qict_Charges",
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "otherCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Other_Charges",
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "washAndCleanCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Wash_And_Clean_Charges",
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "anf",
    dataType: "number",
    format: "#,##0.##",
    caption: "ANF",
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "pallet",
    dataType: "number",
    format: "#,##0.##",
    caption: "Pallet",
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "recover",
    dataType: "number",
    format: "#,##0.##",
    caption: "Recover",
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "transportCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Transport_Charges",
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "totalCharges",
    dataType: "number",
    format: "#,##0.##",
    caption: "Total_Charges",
    allowEditing: false,
    calculateCellValue: function (rowData) {

                        var calculatedvalue = (rowData.demurrageCharges != null ? Number(rowData.demurrageCharges) : Number(0))
    +
    (rowData.weighmentCharges != null ? Number(rowData.weighmentCharges) : Number(0))
    +
    (rowData.overWeightPenalty != null ? Number(rowData.overWeightPenalty) : Number(0))
    +
    (rowData.detentionChargesOrBulletSeal != null ? Number(rowData.detentionChargesOrBulletSeal) : Number(0))
    +
    (rowData.thcPhcKdlpCharges != null ? Number(rowData.thcPhcKdlpCharges) : Number(0))
    +
    (rowData.loloCharges != null ? Number(rowData.loloCharges) : Number(0))
    +
    (rowData.qictCharges != null ? Number(rowData.qictCharges) : Number(0))
    +
    (rowData.otherCharges != null ? Number(rowData.otherCharges) : Number(0))
    +
    (rowData.washAndCleanCharges != null ? Number(rowData.washAndCleanCharges) : Number(0))
    +
    (rowData.anf != null ? Number(rowData.anf) : Number(0))
    +
    (rowData.pallet != null ? Number(rowData.pallet) : Number(0))
    +
    (rowData.recover != null ? Number(rowData.recover) : Number(0))
    +
    (rowData.transportCharges != null ? Number(rowData.transportCharges) : Number(0))
    return rowData.totalCharges = calculatedvalue;
                    },
                },


    ],

    summary: {
        totalItems: [


    {
        column: "demurrageCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "weighmentCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "overWeightPenalty",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "detentionChargesOrBulletSeal",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "thcPhcKdlpCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "loloCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "qictCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "otherCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "washAndCleanCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "anf",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "pallet",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "recover",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "transportCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "totalCharges",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    ]
            },

    onRowUpdated: function (e) {



        //var model = e.data;
        //console.log("model", model);
        //$.post('/Import/Setup/AddPortCharges', { model, model }, function (data) {

        //    if (data.error == true) {
        //        showToast(data.message, "error");


        //        var virno = $("#virnolist").dxAutocomplete("instance").option("value");

        //        var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value");

        //        var containerno = $("#containerlist").dxAutocomplete("instance").option("value");
        //        filterGrid(virno, indexno, containerno, model.type)



        //    } else {
        //        showToast(data.message, "success")

        //        var virno = $("#virnolist").dxAutocomplete("instance").option("value");

        //        var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value");

        //        var containerno = $("#containerlist").dxAutocomplete("instance").option("value");


        //        console.log('virno', virno);
        //        console.log('indexno', indexno);
        //        console.log('containerno', containerno);


        //        filterGrid(virno, indexno, containerno, model.type)
        //    }

        //});


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




    function showcargoDetaildesc() {

        var res = [];
    containerIndexesGrid(res);

    var type = $('#ContainerType').val();

    console.log("type", type)

    if (type == null) {

            //return showToast("please select type", "error");
            return alert("please select type");

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


    function SaveInfo() {

        var resultvalue = true;
    var rowObject = $("#PortchargesGrid").dxDataGrid("option", "dataSource");


    console.log("rowObject", rowObject)




    if (rowObject.length) {



        rowObject.forEach(c => {


            if ($.isNumeric(c.demurrageCharges) == false) {
                resultvalue = false;
                //return showToast("please add Demurrage Charges in numbers ", "error")
                return alert("please add Demurrage Charges in numbers");
            }
            if ($.isNumeric(c.weighmentCharges) == false) {
                resultvalue = false;
                //return showToast("please add  Weighment Charges in numbers ", "error")
                return alert("please add  Weighment Charges in numbers");
            }
            if ($.isNumeric(c.overWeightPenalty) == false) {
                resultvalue = false;
                //return showToast("please add  Over Weight Penalty in numbers ", "error")
                return alert("please add  Over Weight Penalty in numbers ");
            }
            if ($.isNumeric(c.detentionChargesOrBulletSeal) == false) {
                resultvalue = false;
                //return showToast("please add detentionChargesOrBulletSeal in numbers ", "error")
                return alert("please add detentionChargesOrBulletSeal in numbers ");
            }
            if ($.isNumeric(c.thcPhcKdlpCharges) == false) {
                resultvalue = false;
                //return showToast("please add thcPhcKdlpCharges in numbers ", "error")
                return alert("please add thcPhcKdlpCharges in numbers ");
            }
            if ($.isNumeric(c.loloCharges) == false) {
                resultvalue = false;
                //return showToast("please add loloCharges in numbers ", "error")
                return alert("please add loloCharges in numbers ");
            }
            if ($.isNumeric(c.qictCharges) == false) {
                resultvalue = false;
                //return showToast("please add qictCharges in numbers ", "error")
                return alert("please add qictCharges in numbers ");

            }
            if ($.isNumeric(c.otherCharges) == false) {
                resultvalue = false;
                //return showToast("please add otherCharges in numbers ", "error")
                return alert("please add otherCharges in numbers ");
            }
            if ($.isNumeric(c.washAndCleanCharges) == false) {
                resultvalue = false;
                //return showToast("please add washAndCleanCharges in numbers ", "error")
                return alert("please add washAndCleanCharges in numbers ");
            }
            if ($.isNumeric(c.anf) == false) {
                resultvalue = false;
                //return showToast("please add anf in numbers ", "error")
                return alert("please add anf in numbers ");
            }
            if ($.isNumeric(c.pallet) == false) {
                resultvalue = false;
                //return showToast("please add pallet in numbers ", "error")
                return alert("please add pallet in numbers ");
            }
            if ($.isNumeric(c.recover) == false) {
                resultvalue = false;
                //return showToast("please add recover in numbers ", "error")
                return alert("please add recover in numbers ");
            }
            if ($.isNumeric(c.transportCharges) == false) {
                resultvalue = false;
                //return showToast("please add transportCharges in numbers ", "error")
                return alert("please add transportCharges in numbers ");
            }


            if (c.demurrageCharges < 0) {
                resultvalue = false;
                //return showToast("please add Demurrage Charges > 0 ", "error")
                return alert("please add Demurrage Charges > 0 ");
            }
            if (c.weighmentCharges < 0) {
                resultvalue = false;
                //return showToast("please add  Weighment Charges  > 0 ", "error")
                return alert("please add  Weighment Charges  > 0 ");
            }
            if (c.overWeightPenalty < 0) {
                resultvalue = false;
                //return showToast("please add  Over Weight Penalty  > 0  ", "error")
                return alert("please add  Over Weight Penalty  > 0   ");
            }
            if (c.detentionChargesOrBulletSeal < 0) {
                resultvalue = false;
                //return showToast("please add detentionChargesOrBulletSeal  > 0 ", "error")
                return alert("please add detentionChargesOrBulletSeal  > 0    ");
            }
            if (c.thcPhcKdlpCharges < 0) {
                resultvalue = false;
                //return showToast("please add thcPhcKdlpCharges  > 0 ", "error")
                return alert("please add thcPhcKdlpCharges  > 0 ");
            }
            if (c.loloCharges < 0) {
                resultvalue = false;
                //return showToast("please add loloCharges > 0", "error")
                return alert("please add loloCharges > 0");
            }
            if (c.qictCharges < 0) {
                resultvalue = false;
                //return showToast("please add qictCharges  > 0", "error")
                return alert("please add qictCharges  > 0");
            }
            if (c.otherCharges < 0) {
                resultvalue = false;
                //return showToast("please add otherCharges  > 0 ", "error")
                return alert("please add otherCharges  > 0 ");
            }
            if (c.washAndCleanCharges < 0) {
                resultvalue = false;
                //return showToast("please add washAndCleanCharges  > 0 ", "error")
                return alert("please add washAndCleanCharges  > 0 ");
            }
            if (c.anf < 0) {
                resultvalue = false;
                //return showToast("please add anf  > 0", "error")
                return alert("please add anf  > 0");
            }
            if (c.pallet < 0) {
                resultvalue = false;
                //return showToast("please add pallet > 0", "error")
                return alert("please add pallet > 0");
            }
            if (c.recover < 0) {
                resultvalue = false;
                //return showToast("please add recover  > 0", "error")
                return alert("please add recover  > 0");
            }
            if (c.transportCharges < 0) {
                resultvalue = false;
                //return showToast("please add transportCharges > 0", "error")
                return alert("please add transportCharges > 0");
            }


        });

    if (resultvalue == true) {
        $.post('/Import/Setup/AddPortChargesContainerWise', { data: rowObject }, function (data) {

            if (data.error == true) {

                alert(data.message)


                var virno = $("#virnolist").dxAutocomplete("instance").option("value");

                var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value");

                var containerno = $("#containerlist").dxAutocomplete("instance").option("value");
                var ContainerType = $("#ContainerType").val();


                console.log('virno', virno);
                console.log('indexno', indexno);
                console.log('containerno', containerno);
                console.log('ContainerType', ContainerType);

                filterGrid(virno, containerno, indexno, ContainerType);


            } else {

                alert(data.message);

                var virno = $("#virnolist").dxAutocomplete("instance").option("value");

                var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value");

                var containerno = $("#containerlist").dxAutocomplete("instance").option("value");
                var ContainerType = $("#ContainerType").val();

                console.log('virno', virno);
                console.log('indexno', indexno);
                console.log('containerno', containerno);
                console.log('ContainerType', ContainerType);

                filterGrid(virno, containerno, indexno, ContainerType);



            }

        });

            }


        }

    else {
        //showToast("no record found", "error")
        alert("no record found");
        }
    }
