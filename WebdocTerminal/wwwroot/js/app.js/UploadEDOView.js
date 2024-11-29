


    var containers = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function listGrid(virno, containerno, indexno, shippingagnetId) {

        console.log("virno", virno)
        console.log("containerno", containerno)
    console.log("indexno", indexno)

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



    loadingBar();
    $.get('/APICalls/GetElectronicDODetail?virno=' + virno + '&containerno=' + containerno + '&indexno=' + indexno + '&shippingagnetId=' + shippingagnetId, function (data) {
        loadingBarHide();
    if (data) {

        containers = data;

    console.log("containers", containers)

    const groupByCategory = groupBy(containers);


    console.log("groupByCategory", groupByCategory)


    containerIndexesGrid(groupByCategory)
            }

    else {
        containers = []
                containerIndexesGrid(containers);

            }


        });


    }


    function groupBy(data) {
        var newarraydata = [];
    if (data) {
        data.forEach(c => {
            var result = newarraydata.find(t => t.key == c.key);

            if (result == null) {
                newarraydata.push(c);

            }

        });
        }
    return newarraydata;
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

    function formfiled() {
        loginuserinfo();
    }

    var shippingagnetId;

    function loginuserinfo() {

        $.get('/APICAlls/GetLoginUserInfo', function (user) {
            console.log("user", user);
            if (user) {
                console.log("ok")
                shippingagnetId = user;
            }



        });
    }


    function containerIndexesGrid(res) {


        var grid = $("#CargoInformation").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    containers = res;

    console.log("containers", containers)


    var grid = $("#CargoInformation").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    var dataGrid = $("#CargoInformation").dxDataGrid({
        dataSource: containers,
    keyExpr: "key",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    //dateSerializationFormat: "dd/MM/yyyy",
    paging: {
        enabled: false
            },
    editing: {
        mode: "cell",
    allowUpdating: true,
    selectTextOnEditStart: true,

            },
    export: {
        enabled: true
            },
    onExporting(e) {

                const workbook = new ExcelJS.Workbook();

    const worksheet = workbook.addWorksheet('ElectronicDeliveryOrder');

    DevExpress.excelExporter.exportDataGrid({
        component: e.component,
    worksheet,
    autoFilterEnabled: true,
                }).then(() => {
        workbook.xlsx.writeBuffer().then((buffer) => {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'ElectronicDeliveryOrders.xlsx');
        });
                });
    e.cancel = true;


            },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

            },
    scrolling: {
        columnRenderingMode: 'virtual',
            },
    columns: [
    {
        dataField: "vessel_Name",
    caption: "Vessel",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                },
    {
        dataField: "voyage",
    caption: "Voyage",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                },
    {
        dataField: "igM_No",
    caption: "IGM",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                },
    {
        dataField: "container_No",
    caption: "Container",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                }, {
        dataField: "bL_No",
    caption: "BL",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                },
    {
        dataField: "index_No",
    caption: "Index",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                },
    {
        dataField: "conT_Size",
    caption: "Size",
    allowEditing: false,
    fixed: true,
    cssClass: 'myClass'
                },
    {
        dataField: "gross_Weight",
    caption: "GrossWeight",
    allowEditing: false,
    cssClass: 'myClass'

                },
    {
        dataField: "marks_No",
    caption: "MarksNo",
    allowEditing: false,
    cssClass: 'myClass'
                },
    {
        dataField: "no_Of_Pkgs",
    caption: "No_Of_Packges",
    allowEditing: false,
    cssClass: 'myClass'
                },
    {
        dataField: "pKg_Description",
    caption: "Description",
    allowEditing: false,
    cssClass: 'myClass',
    width: " 200"

                },
    {
        dataField: "port_of_Arrival",
    caption: "Port_of_Arrival",

                },

    {
        dataField: "do_Date",
    caption: "DO_Date",
    dataType: 'date',
    format: 'dd/MM/yyyy',
    width: " 200"
                },
    {
        dataField: "valid_Date",
    caption: "Valid_Date",
    format: 'dd/MM/yyyy',
    dataType: 'date',
    width: " 200"
                },
    {
        dataField: "agent_Name",
    caption: "Agent",

                },
    {
        dataField: "doc_Tran_No",
    caption: "Doc_Tran_No",

                },
    {
        dataField: "goods_Desc",
    caption: "Goods_Desc",

                },
    {
        dataField: "ref_No",
    caption: "Ref_No",

                },

    {
        dataField: "netWeight",
    caption: "NetWeight",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    }
                },
    {
        dataField: "isEdo",
    caption: "isEdo",
    dataType: "boolean"
                },
                //{
                //    width: 100,
                //    alignment: 'center',
                //    cellTemplate: function (container, options) {
                //        $('<a />').addClass('dx-link')
                //            .text('Print')
                //            .on('dxclick', function () {

                //                console.log("options.data", options.data.electronicDeliveryOrderId)
                //                Printedo(options.data.electronicDeliveryOrderId);
                //            })
                //            .appendTo(container);
                //    }
                //}




            ],


    onRowUpdated: function (e) {


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

    function Printedo(data) {
        console.log("data", data)
        top.location.href = '/Import/Reports/EdoReportView?edoid=' + data;
    }

    var keyCodes = {
        TAB: 9,
    PAGE_UP: 33,
    PAGE_DOWN: 34,
    LEFT: 37,
    UP: 38,
    RIGHT: 39,
    DOWN: 40,

    };

    function updateinfo(data) {
        console.log("data", data)

    }

    function showcargoDetaildesc() {

        console.log("shippingagnetId", shippingagnetId)

        if (shippingagnetId) {
            var res = [];

    containerIndexesGrid(res);

    var virno = $("#virnolist").dxAutocomplete("instance").option("value")
    var containerno = $("#containerlist").dxAutocomplete("instance").option("value");
    var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value")

    console.log('virno', virno);
    console.log('containerno', containerno);
    console.log('indexno', indexno);

    listGrid(virno, containerno, indexno, shippingagnetId);

        }
    else {
            return showToast("please assign agent", "error");
        }

    }



    function SaveInfo() {
        var rowObject = $("#CargoInformation").dxDataGrid("option", "dataSource");

    console.log("rowObject", rowObject)
        var newresult = rowObject.filter(c => c.isEdo == true);
    console.log("newresult", newresult)

    if (newresult.length) {

        $.post('/Import/ElectronicDeliveryOrder/SaveEDO', { model: newresult }, function (data) {

            if (data.error == false) {
                showToast(data.message, "success")
            } else {
                showToast(data.message, "error")
            }
        });
        }

    }

