

    var gdnumberList = [];
    var containerList = [];


    $(function () {

        GetGdslist();
    Getcontainernolist();

    });




    function GetGdslist() {
        $.get('/APICalls/GetPendingGds', function (res) {
            if (res) {
                $("#searchBoxForGdnumber").dxSelectBox({
                    dataSource: {
                        store: res,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'GDNumber',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                    onValueChanged: function (e) {
                        getgdDetail(e.value)
                    }
                })


                $("#ContainerWisesearchBoxForGdnumber").dxSelectBox({
                    dataSource: {
                        store: res,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'GDNumber',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                    onValueChanged: function (e) {
                        getgdDetailcontainerwise(e.value)
                    }
                })


            }
            else {
                $("#searchBoxForGdnumber").dxSelectBox({
                    dataSource: {
                        store: [],
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'GDNumber',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                });

                $("#ContainerWisesearchBoxForGdnumber").dxSelectBox({
                    dataSource: {
                        store: [],
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'GDNumber',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                });


            }
        });
    }

    function Getcontainernolist() {
        $.get('/APICalls/GetPendingcontainers', function (res) {
            if (res) {
                $("#searchBoxForcontainernumber").dxSelectBox({
                    dataSource: {
                        store: res,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ContainerNumber',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,


                })

                $("#ContainerWisesearchBoxForcontainernumber	").dxSelectBox({
                    dataSource: {
                        store: res,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ContainerNumber',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                    onValueChanged: function (e) {
                        findatacontainerwise(e.value)
                    }

                })
            }
            else {
                $("#searchBoxForcontainernumber").dxSelectBox({
                    dataSource: {
                        store: [],
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ContainerNumber',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,

                })

                $("#ContainerWisesearchBoxForcontainernumber	").dxSelectBox({
                    dataSource: {
                        store: [],
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ContainerNumber',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,

                })
            }
        });
    }

    //start work for gdwise detail

    function getgdDetail(value) {

        console.log("value", value);

    if (value) {
        $.get('/APICalls/GetgGdDetail?Gdnumber=' + value, function (resData) {

            if (resData) {
                $('#numberOfPackages').val(resData.noOfPackages);
            }
            else {
                resetgdvalues()
            }

        });
        }
    else {
        resetgdvalues();
        }
    findata();
    }

    function resetgdvalues() {
        $('#numberOfPackages').val('');

    }

    function findata() {

        var gdnumber = $("#searchBoxForGdnumber").dxSelectBox('instance').option("value");

    console.log("gdnumber", gdnumber);

    $.get('/APICalls/GetExportContainerItemsByGdnumber?Gdnumber=' + gdnumber, function (data) {

        GridData(data);

        });
    }

    function GridData(res) {
        var dataGrid = $("#ExportContainerItem").dxDataGrid({
        dataSource: res,
    keyExpr: "exportContainerItemId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
            },

    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

            },

    editing: {

        allowDeleting: true,
    selectTextOnEditStart: true,
    startEditAction: "click",
    mode: "row"
            },

    columns: [
    {
        dataField: "gdNumber",
    caption: "GD Number",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "containerNumber",
    caption: "Container No",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "numberOfPackages",
    caption: "No Of Packages",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "allowLoading",
    caption: "Allow Loading",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "destination",
    caption: "Destination",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    ],

    onRowRemoved: function (e) {

                var key = e.data.exportContainerItemId;
    $.post('/Export/ExportContainer/DeleteGdBindingDetail?key=' + key, function (resout) {
                    if (resout.error) {
        showToast(resout.message, "warning");
    getgdDetail(e.data.gdNumber);
                    }
    else {
        showToast(resout.message, "success");
    getgdDetail(e.data.gdNumber);
                    }
                });
            }

        }).dxDataGrid("instance");


    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }

    function addDetail() {

        var gdnumber = $("#searchBoxForGdnumber").dxSelectBox('instance').option("value");
    var exportcontainerid = $("#searchBoxForcontainernumber").dxSelectBox('instance').option("value");
    var containernumber = $("#searchBoxForcontainernumber").dxSelectBox('instance').option("text");
    var noOfPackages = $("#numberOfPackages").val();

    if (!gdnumber) {
            return showToast("please select gdnumber", "error")
        }
    if (!exportcontainerid) {
            return showToast("please select containerno", "error")
        }
    if (!containernumber) {
            return showToast("please select containerno", "error")
        }
    if (!noOfPackages) {
            return showToast("please add no of packages", "error")
        }
    let model = {
        gdNumber: gdnumber,
    containerNumber: containernumber,
    exportContainerId: exportcontainerid,
    numberOfPackages: noOfPackages
        }

    $.post('/Export/ExportContainer/AddGdBindingDetail', {model: model }, function (resout) {

            if (resout.error) {
        showToast(resout.message, "warning");
    getgdDetail(gdnumber);
            }
    else {
        showToast(resout.message, "success");
    getgdDetail(gdnumber);
            }
        });


    }

    function AddExportContainerItems() {

        var model = $("#ExportContainerItem").dxDataGrid("instance").option("dataSource");

    if (model.length) {

            if (confirm('Are you sure want to complete binding')) {

        $.post('/Export/ExportContainer/AddGdBindingDetailList', { model: model }, function (resout) {

            if (resout.error) {
                alert(resout.message);
                window.location.href = window.location.href;
            }
            else {
                alert(resout.message);
                window.location.href = window.location.href;
            }

        });

            }
    else {
        window.location.href = window.location.href;
            }

        }
    else {
        showToast("no data found to save", "danger");
        }
        //const grid = $('#ExportContainerItem').dxDataGrid("instance");
        //console.log("grid",grid)
        //const filterExpr = grid.getCombinedFilter(true);
        //console.log("filterExpr", filterExpr)
        //const dataSource = grid.getDataSource();
        //console.log("dataSource", dataSource)
        //const loadOptions = dataSource.loadOptions();
        //console.log("loadOptions", loadOptions)
        //dataSource
        //    .store()
        //    .load({filter: filterExpr, sort: loadOptions.sort, group: loadOptions.group })
        //    .then((result) => {
        //        // your code...
        //        console.log("result", result)
        //        //$("#gridContainer2").dxDataGrid("instance").option("dataSource", result)
        //    });

    }


    //end work for gdwise detail

    //start work for container wise detail
    function getgdDetailcontainerwise(value) {

        console.log("value", value);

    if (value) {
        $.get('/APICalls/GetgGdDetail?Gdnumber=' + value, function (resData) {

            if (resData) {
                $('#containerwisenumberOfPackages').val(resData.noOfPackages);
            }
            else {
                $('#containerwisenumberOfPackages').val('');
            }

        });
        }
    else {
        $('#containerwisenumberOfPackages').val('');
        } 
    }

    function findatacontainerwise(value) {

        var exportcontainerid = $("#ContainerWisesearchBoxForcontainernumber").dxSelectBox('instance').option("value");

    console.log("exportcontainerid", exportcontainerid);

    $.get('/APICalls/GetExportContainerItemsByexportcontainerid?exportcontainerid=' + exportcontainerid, function (data) {

        GridDataContainerwise(data);

        });

    }

    function GridDataContainerwise(res) {
        var dataGrid = $("#ExportContainerItemcontainerwise").dxDataGrid({
        dataSource: res,
    keyExpr: "exportContainerItemId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
            },

    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

            },

    editing: {

        allowDeleting: true,
    selectTextOnEditStart: true,
    startEditAction: "click",
    mode: "row"
            },

    columns: [
    {
        dataField: "gdNumber",
    caption: "GD Number",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "containerNumber",
    caption: "Container No",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "numberOfPackages",
    caption: "No Of Packages",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "allowLoading",
    caption: "Allow Loading",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "destination",
    caption: "Destination",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    ],

    onRowRemoved: function (e) {

                var key = e.data.exportContainerItemId;
    $.post('/Export/ExportContainer/DeleteGdBindingDetail?key=' + key, function (resout) {
                    if (resout.error) {
        showToast(resout.message, "warning");
    getgdDetailcontainerwise(e.data.gdNumber);
                    }
    else {
        showToast(resout.message, "success");
    getgdDetailcontainerwise(e.data.gdNumber);
                    }
                });
            }

        }).dxDataGrid("instance");


    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }

    function addDetailcontainerwise() {

        var gdnumber = $("#ContainerWisesearchBoxForGdnumber").dxSelectBox('instance').option("value");
    var exportcontainerid = $("#ContainerWisesearchBoxForcontainernumber").dxSelectBox('instance').option("value");
    var containernumber = $("#ContainerWisesearchBoxForcontainernumber").dxSelectBox('instance').option("text");
    var noOfPackages = $("#containerwisenumberOfPackages").val();

    if (!gdnumber) {
            return showToast("please select gdnumber", "error")
        }
    if (!exportcontainerid) {
            return showToast("please select containerno", "error")
        }
    if (!containernumber) {
            return showToast("please select containerno", "error")
        }
    if (!noOfPackages) {
            return showToast("please add no of packages", "error")
        }
    let model = {
        gdNumber: gdnumber,
    containerNumber: containernumber,
    exportContainerId: exportcontainerid,
    numberOfPackages: noOfPackages
        }

    $.post('/Export/ExportContainer/AddGdBindingDetail', {model: model }, function (resout) {

            if (resout.error) {
        showToast(resout.message, "warning");
    getgdDetailcontainerwise(gdnumber);
    findatacontainerwise(exportcontainerid);
            }
    else {
        showToast(resout.message, "success");
    getgdDetailcontainerwise(gdnumber);
    findatacontainerwise(exportcontainerid);
            }
        });


    }

    function AddExportContainerItemscontainerwise() {

        var model = $("#ExportContainerItemcontainerwise").dxDataGrid("instance").option("dataSource");

    if (model.length) {

            if (confirm('Are you sure want to complete binding')) {

        $.post('/Export/ExportContainer/AddGdBindingDetailListContainerwise', { models: model }, function (resout) {

            if (resout.error) {
                alert(resout.message);
                window.location.href = window.location.href;
            }
            else {
                alert(resout.message);
                window.location.href = window.location.href;
            }

        });

            }
    else {
        window.location.href = window.location.href;
            }

        }
    else {
        showToast("no data found to save", "danger");
        }
        //const grid = $('#ExportContainerItem').dxDataGrid("instance");
        //console.log("grid",grid)
        //const filterExpr = grid.getCombinedFilter(true);
        //console.log("filterExpr", filterExpr)
        //const dataSource = grid.getDataSource();
        //console.log("dataSource", dataSource)
        //const loadOptions = dataSource.loadOptions();
        //console.log("loadOptions", loadOptions)
        //dataSource
        //    .store()
        //    .load({filter: filterExpr, sort: loadOptions.sort, group: loadOptions.group })
        //    .then((result) => {
        //        // your code...
        //        console.log("result", result)
        //        //$("#gridContainer2").dxDataGrid("instance").option("dataSource", result)
        //    });

    }

    //end work for container wise detail



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function formfiled() {

    }

    function renderContent(values) {

        console.log(values);

    if (values == 'GDBinding') {
        $("#searchBoxForGdnumber").dxSelectBox('instance').option("value", "");
    $("#searchBoxForGdnumber").dxSelectBox('instance').option("text", "");

    $("#searchBoxForcontainernumber").dxSelectBox('instance').option("value", "");
    $("#searchBoxForcontainernumber").dxSelectBox('instance').option("text", "");

    $("#numberOfPackages").val('');

    GridData([]);
        }

    if (values == 'ContainerBinding') {
        $("#ContainerWisesearchBoxForcontainernumber").dxSelectBox('instance').option("value", "");
    $("#ContainerWisesearchBoxForcontainernumber").dxSelectBox('instance').option("text", "");

    $("#ContainerWisesearchBoxForGdnumber").dxSelectBox('instance').option("value", "");
    $("#ContainerWisesearchBoxForGdnumber").dxSelectBox('instance').option("text", "");

    $("#containerwisenumberOfPackages").val('');

    GridDataContainerwise([]);
        } 
        
    }


    function showFilters() {
       var type = $("#typeassociation option:selected").val();

    console.log("type", type);

    if (type == 'Multiple') {
        document.getElementById("singleInfo").style.display = "none";
    document.getElementById("MultipleInfo").style.display = "block";
    GetAlongsideGDs();
        }
    if (type == 'Single') {
        document.getElementById("singleInfo").style.display = "block";
    document.getElementById("MultipleInfo").style.display = "none";
        }


    $("#ContainerWisesearchBoxForcontainernumber").dxSelectBox('instance').option("value", "");
    $("#ContainerWisesearchBoxForcontainernumber").dxSelectBox('instance').option("text", "");

    $("#ContainerWisesearchBoxForGdnumber").dxSelectBox('instance').option("value", "");
    $("#ContainerWisesearchBoxForGdnumber").dxSelectBox('instance').option("text", "");

    $("#containerwisenumberOfPackages").val('');

    GridDataContainerwise([]);
    alongsidedatagrid([])

    }


    function GetAlongsideGDs() {

        var type = $("#typeassociation option:selected").val();

    console.log("type", type);

    if (type == 'Multiple') {


        $.get('/APICalls/AlongsideGds', function (data) {

            alongsidedatagrid(data);

        });
        }
    else {
        alongsidedatagrid([]);
        }
       
    }


    function alongsidedatagrid(res) {

        var dataGrid = $("#alongsidedatacontainerwise").dxDataGrid({
        dataSource: res,
    keyExpr: "gdNumber",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
            },

    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

            },

    editing: {

        allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click",
    mode: "cell"
            },

    columns: [
    {
        dataField: "gdNumber",
    caption: "GD Number",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "numberOfPackages",
    caption: "No Of Packages",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "allowLoading",
    caption: "Allow Loading",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "destination",
    caption: "Destination",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "shipperName",
    caption: "Shipper Name",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        caption: "",
    value:false,
    dataField: "ischecked",
    dataType: "boolean"
                }

    ],

    

        }).dxDataGrid("instance");


    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }


    function addalongsidedataDetail() {


        var exportcontainerid = $("#ContainerWisesearchBoxForcontainernumber").dxSelectBox('instance').option("value");
    if (!exportcontainerid) {
            return showToast("please select containerno", "error")
        }
    var res = $("#alongsidedatacontainerwise").dxDataGrid("instance").option("dataSource");

    console.log("res", res);

        var model = res.filter(c => c.ischecked == true);

    console.log("model", model);

    if (model.length) {

        $.post('/Export/ExportContainer/AddMultipleGdBindingDetail?exportcontainerid=' + exportcontainerid, { model: model }, function (resout) {

            if (resout.error) {
                showToast(resout.message, "warning");
                findatacontainerwise(exportcontainerid);
                GetAlongsideGDs();
            }
            else {
                showToast(resout.message, "success");
                findatacontainerwise(exportcontainerid);
                GetAlongsideGDs();
            }
        });


        }
    else {
        showToast("please select  GD's For Binding", "error")
    }


    }


