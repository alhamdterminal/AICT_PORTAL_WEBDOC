

    var containerId = 0;

    var containerIndexId = 0;

    var cyContainerId = 0;

    var tariffs = [];

    var containerIndexTariffs = [];

    var newdata = null;

    var type = "CFS";


    var isDestuff = false;

    //$(function () {

        //});

        function showToast(message, icon) {

            $.toast({
                heading: message,
                showHideTransition: 'slide',
                position: 'bottom-right',
                icon: icon
            });
        }

    function containerChangeCallback(ornt) {

        if (ornt == "index") {
        containerIndexId = parseInt($('#indexdb').val());
        } else {
        containerIndexId = parseInt($('#containerdb').val());

        }

    $('#shippingAgentName').val('');


    containerCallback();
    LoadContainerIndexTariffGrid();
    }

    function showFilters() {

        type = $("input[name='type']:checked").val();
    if (type == "CFS") {
        $.get('/APICalls/GetFiltersAll?Type=' + type, function (partial) {

            $('#filters').html(partial);

            setOrientation(isDestuff)
            if (PermissionInputs.find(x => x.fieldName == "ContainerType" && x.isChecked == false)) {

                document.getElementsByClassName('container-index')[0].style.display = "none";

            } else {
                var x = document.getElementsByClassName("container-index")[0];

                x.style.display = "block"
            }





        })

    } else {
        $.get('/APICalls/GetFiltersAll?Type=' + type, function (partial) {

            $('#filters').html(partial);
            var x = document.getElementsByClassName("container-index")[0];

            x.style.display = "none"
        })
    }
    $('#shippingAgentName').val('');




    }


    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function checkDuplicationCharges(tariff) {

        var duplicateTariff = tariffs.find(t => ((t.rate20 == tariff.rate20 && t.rate20 != 0)
    || (t.rate40 == tariff.rate40 && t.rate40 != 0)
    || (t.rate45 == tariff.rate45 && t.rate45 != 0)
    || (t.cbmRate == tariff.cbmRate && t.cbmRate != 0)
    || (t.weightRate == tariff.weightRate && t.weightRate != 0)
    || (t.perIndexRate == tariff.perIndexRate && t.perIndexRate != 0)
    || (t.devededIndexAmount == tariff.devededIndexAmount && t.devededIndexAmount != 0))
    && t.tariffHead.name == tariff.tariffHead.name && t.tariffId != tariff.tariffId);

    if (duplicateTariff)
    return false;
    else
    return true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "tariffHead.name");
    checkColumn(e, "tariffHead.description");



        if (PermissionInputs.find(x => x.fieldName == "Rate20" && x.isChecked == false)) {

        checkColumn(e, "rate20");

        }
        if (PermissionInputs.find(x => x.fieldName == "Rate40" && x.isChecked == false)) {

        checkColumn(e, "rate40");

        }
        if (PermissionInputs.find(x => x.fieldName == "Rate45" && x.isChecked == false)) {

        checkColumn(e, "rate45");

        }

        if (PermissionInputs.find(x => x.fieldName == "CbmRate" && x.isChecked == false)) {

        checkColumn(e, "cbmRate");

        }
        if (PermissionInputs.find(x => x.fieldName == "WeightRate" && x.isChecked == false)) {

        checkColumn(e, "weightRate");

        }
        if (PermissionInputs.find(x => x.fieldName == "PerIndexRate" && x.isChecked == false)) {

        checkColumn(e, "perIndexRate");


        }
        if (PermissionInputs.find(x => x.fieldName == "ImplementFrom" && x.isChecked == false)) {

        checkColumn(e, "implementFrom");

        }
        if (PermissionInputs.find(x => x.fieldName == "IsActive" && x.isChecked == false)) {

        checkColumn(e, "isActive");

        }
        if (PermissionInputs.find(x => x.fieldName == "TariffId" && x.isChecked == false)) {

        checkColumn(e, "tariffId");

        }

    }

    function containerCallback() {


        if (type == "CFS") {

        //containerIndexId = parseInt($('#containerdb').val());

        $.get('/APICalls/GetShippingAgentBycontainerId?containerId=' + containerIndexId, function (data) {

            $('#shippingAgentName').val(data);
        });
        }
    else {

        $.get('/APICalls/GetShippingAgentByIgm?igm=' + igm, function (data) {

            $('#shippingAgentName').val(data);
        });
        }

    getcontainesizeAmount();

    LoadTariffGrid();

    LoadContainerIndexTariffGrid();

        

    }



    function addTariff(tariffId) {

        if (PermissionInputs.find(x => x.fieldName == "TariffId" && x.isChecked == false)) {


        showToast("Permission Denied", "error");
        } else {
                    if (!igm) {

        alert("Select IGM");
        }
    else {
            var tariff;

    if (type == "CFS") {
        tariff = { containerIndexId: containerIndexId, tariffId: tariffId, type: type };

            }

    else {
        tariff = { igm: igm, indexNo: $("#containerIndexCYdb option:selected").val(), tariffId: tariffId, type: type };
            }


    $.post('/Tariff/SaveContainerIndexTariff', tariff, function (data) {


        showToast(data.message, "warning");

                tariffs = tariffs.filter(t => t != tariffId);

    LoadTariffGrid();

    LoadContainerIndexTariffGrid();

            });
        }
        }
 

    }

    function LoadTariffGrid() {

        var url = "";

    if (type == "CFS")
    url = '/APICAlls/GetUnassignedContainerIndexTariffs?ContainerIndexId=' + containerIndexId;
    else
    url = '/APICAlls/GetUnassignedContainerIndexTariffsCY?igm=' + igm + '&indexNo=' + $("#containerIndexCYdb option:selected").val();

    $.get(url, function (data) {

        tariffs = data;

    var dataGrid = $("#tariffGrid").dxDataGrid({
        dataSource: tariffs,
    keyExpr: "tariffId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: true,
    pageSize: 10
                },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."
                },
    editing: {
        mode: "inline",
    allowUpdating: true,
    allowDeleting: true

                },
    columns: [
    {
        dataField: "tariffHead.name",
    caption: "Name",
    formItem: {
        visible: false
                        }
                    },
    {
        dataField: "tariffHead.description",
    caption: "Description",
    formItem: {
        visible: false
                        }
                    },
    "rate20",
    "rate40",
    "rate45",
    "cbmRate",
    "weightRate",
    "perIndexRate",
    "devededIndexAmount",
    {
        caption: "Implement From",
    dataField: "implementFrom",
    dataType: "date"
                    },
    {
        caption: "Is Active",
    dataField: "isActive",
    dataType: "boolean"
                    },
    {
        caption: "Is General",
    dataField: "isGeneral",
    dataType: "boolean"
                    },
    {
        caption: "Tariff Type",
    dataField: "tariffType"
                    },
    {
        dataField: "tariffId",
    caption: "Tariff",
    cellTemplate: function (container, options) {
        $("<button class='btn btn-success' onClick='addTariff(" + options.value + ")'>Add</button>")
            .appendTo(container);
                        }
                    }
    ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
                },
    onRowUpdating: function (e) {
                    var tariff = Object.assign(e.oldData, e.newData);;

    if (checkDuplicationCharges(tariff)) {

        $.post('/Tariff/updateAgentTariff', { tariff: tariff }, function (data) {

            showToast("Charges Updated!", "success");
        });
                    }
    else {
        showToast("Duplicate entry error! Changes will not be saved", "error");
                    }
                },

    onRowRemoved: function (e) {


        $.post('/Tariff/RemoveIGMTariffBYId?TariffId=' + e.data.tariffId, function (data) {

        });

                }




            }).dxDataGrid("instance");

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });
    }

    function LoadContainerIndexTariffGrid() {

        var url = "";

    if (type == "CFS")
    url = '/APICAlls/GetContainerIndexTariffs?ContainerIndexId=' + containerIndexId;
    else
    url = '/APICAlls/GetContainerIndexTariffsCY?igm=' + igm + '&indexNo=' + $("#containerIndexCYdb option:selected").val();

    $.get(url, function (data) {
        containerIndexTariffs = data;

    var dataGrid = $("#indexTariffGrid").dxDataGrid({
        dataSource: containerIndexTariffs,
    keyExpr: "tariffId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: true,
    pageSize: 10
                },
    editing: {
        allowUpdating: false,
    allowDeleting: function (e) {
                        
                        return !e.row.data.invoiceCreated;
                    },
                },
    columns: [
    "type",
    {
        dataField: "tariffHead.name",
    caption: "Name",
    formItem: {
        visible: false
                        }
                    },
    {
        dataField: "tariffHead.description",
    caption: "Description",
    formItem: {
        visible: false
                        }
                    },
    "rate20",
    "rate40",
    "rate45",
    "cbmRate",
    "weightRate",
    "perIndexRate",
    "devededIndexAmount",
    {
        caption: "Implement From",
    dataField: "implementFrom",
    dataType: "date"
                    },
    {
        dataField: "agentTariffId",
    caption: "",
    cellTemplate: function (container, options) {

        console.log(options.data.type == "Agent Tariff");

    console.log(options.data.invoiceCreated == false);

    if (options.data.type == "Agent Tariff" && options.data.invoiceCreated == false) {

        $("<button class='btn btn-danger' onClick='RemoveAgentTariff(" + options.value + ")'>Remove</button>")
            .appendTo(container);
                                }

                        }
                    }
    ], summary: {
        totalItems: [
    {
        column: "rate20",
    summaryType: "sum",
    diplayFormat: "{0}"
                        },
    {
        column: "rate40",
    summaryType: "sum",
    diplayFormat: "{0}"
                        },
    {
        column: "rate45",
    summaryType: "sum",
    diplayFormat: "{0}"
                        },
    {
        column: "cbmRate",
    summaryType: "sum"
                        },
    {
        column: "weightRate",
    summaryType: "sum"
                        },
    {
        column: "perIndexRate",
    summaryType: "sum"
                        },
    {
        column: "devededIndexAmount",
    summaryType: "sum"
                        } ]
                },
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
                },
    onRowUpdating: function (e) {
                    var tariff = Object.assign(e.oldData, e.newData);;

    if (checkDuplicationCharges(tariff)) {

        $.post('/Tariff/updateAgentTariff', { tariff: tariff }, function (data) {

            showToast("Charges Updated!", "success");

        });
                    }
    else {
        showToast("Duplicate entry error! Changes will not be saved", "error");
                    }
                },
    onRowRemoved: function (e) {

                    if (type == "CFS") {
        $.post('/Tariff/RemoveIGMTariff?TariffId=' + e.data.tariffId + '&containerIndexId=' + containerIndexId, function (data) {
            showToast(data.message, "warning");

        });
                    }
    else {

        $.post('/Tariff/RemoveIGMTariffCYContainer?TariffId=' + e.data.tariffId + '&igm=' + igm + '&IndexNo=' + $("#containerIndexCYdb option:selected").val(), function (data) {
            showToast(data.message, "warning");

        });
                    }

                }

            }).dxDataGrid("instance");


        });
    }


    function formfiled() {
          
           if (PermissionInputs.find(x => x.fieldName == "ContainerType" && x.isChecked == false)) {

        document.getElementById('filters').style.display = "none";
              

        }
    LoadTariffGrid();
    }




    function RemoveAgentTariff(value ) {

        console.log("value", value);
    if (value != null) {
        console.log("ok");
    var agentTariffId = value;

    if (type == "CFS") {
        $.post('/Tariff/SaveDisabledAgentTariff?containerIndexId=' + containerIndexId + "&agentTariffId=" + agentTariffId, function (data) {

            console.log("data", data)

            if (data.error == false) {

                showToast(data.message, "success");

                //window.location.href = window.location.href;


            }
            else {

                showToast(data.message, "error");

                window.location.href = window.location.href;

            }

        });
            }
    if (type == "CY") {
        $.post('/Tariff/SaveDisabledAgentTariffCY?igm=' + igm + '&indexNo=' + $("#containerIndexCYdb option:selected").val() + "&agentTariffId=" + agentTariffId, function (data) {

            console.log("data", data);

            if (data.error == false) {

                showToast(data.message, "success");

            }
            else {

                showToast(data.message, "error");
            }


        });
            }
        }
    else {
        showToast("Only Remove Agent Tariff", "error");

        }
      

    }



    function refershIndextariff() {
        if (type == "CFS") {
        console.log("containerIndexId", containerIndexId)
            if (containerIndexId) {

        console.log("ok")

                $.post('/Tariff/IndexRemoveDiabledTariff?containerIndexId=' + containerIndexId, function (data) {

        console.log("data", data)

                    if (data.error == false) {

        timer();

    showToast(data.message, "success");
 

                    }
    else {

        showToast(data.message, "error");
                    }

                });
            }
    else {
        showToast("Please Select Index Or Container", "error");
            }
          
        }
    if (type == "CY") {
        $.post('/Tariff/IndexRemoveDiabledTariffCY?igm=' + igm + '&indexNo=' + $("#containerIndexCYdb option:selected").val(), function (data) {

            console.log("data", data)

            if (data.error == false) {

                timer();

                showToast(data.message, "success");

            }
            else {

                showToast(data.message, "error");
            }

        });
        }
    }

    function timer() {
        setTimeout(pageRelod, 3000);

    }

    function pageRelod() {
        window.location.href = window.location.href; 
    }


    function getcontainesizeAmount() {

        if (type == "CY") {
        $.get('/APICalls/GetCYContainerSizeAmount?igm=' + igm + '&index=' + $("#containerIndexCYdb option:selected").val(), function (data) {
            console.log(data);

            if (data) {

                console.log("exchangerateamount", data)

                $('#amountSize20').val(data.amountSize20);
                $('#amountSize40').val(data.amountSize40);
                $('#amountSize45').val(data.amountSize45);
            }
            else {

                $('#amountSize20').val(0);
                $('#amountSize40').val(0);
                $('#amountSize45').val(0);
            }
        });
        }
    else {
        $('#amountSize20').val(0);
    $('#amountSize40').val(0);
    $('#amountSize45').val(0);
        }

       
    }

    function UpdateContainerSizeAmount() {

        var amountSize20 =  $('#amountSize20').val();
    var amountSize40 = $('#amountSize40').val();
    var amountSize45  = $('#amountSize45').val();

    console.log("amountSize20", amountSize20);
    console.log("amountSize40", amountSize40);
    console.log("amountSize45", amountSize45);


    if (amountSize20 <= 0 || amountSize40 <= 0 || amountSize45 <= 0) {

        showToast("plase add rate > 0", "error");

        } else {


            if (type == "CFS") {
        showToast("only for cy", "error");
            }
    else {
                var indexno = $("#containerIndexCYdb option:selected").val();

    console.log("igm", igm);

    console.log("indexno", indexno);

    if (igm && indexno) {

        $.post('/Tariff/UpdateContaineSizeAmount?igm=' + igm + '&indexno=' + indexno + '&amountSize20=' + amountSize20 + '&amountSize40=' + amountSize40 + '&amountSize45=' + amountSize45, function (data) {
            if (data.error == true) {
                showToast(data.message, "warning");
            } else {
                showToast(data.message, "success");
                window.location.href = window.location.href;
            }
        });


                } else {
        showToast("please select igm index", "error");
                }
            }
        }
    }


