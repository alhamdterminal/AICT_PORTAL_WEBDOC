

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

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    //function checkDuplicationCharges(tariff) {
        //    console.log("tariff", tariff)
        //    var duplicateTariff = tariffs.find(t => ((t.rate20 == tariff.rate20 && t.rate20 != 0)
        //        || (t.rate40 == tariff.rate40 && t.rate40 != 0)
        //        || (t.rate45 == tariff.rate45 && t.rate45 != 0)
        //        || (t.cbmRate == tariff.cbmRate && t.cbmRate != 0)
        //        || (t.weightRate == tariff.weightRate && t.weightRate != 0)
        //        || (t.perIndexRate == tariff.perIndexRate && t.perIndexRate != 0)
        //        || (t.devededIndexAmount == tariff.devededIndexAmount && t.devededIndexAmount != 0))
        //        && t.tariffHead.name == tariff.tariffHead.name && t.tariffId != tariff.tariffId);

        //    if (duplicateTariff)
        //        return false;
        //    else
        //        return true;
        //}

        function hideMenifestedColumns(e) {

            checkColumn(e, "tariffHead.name");
            checkColumn(e, "tariffHead.description");

            if (PermissionInputs.find(x => x.fieldName == "ImplementFrom" && x.isChecked == false)) {

                checkColumn(e, "implementFrom");
            }

            if (PermissionInputs.find(x => x.fieldName == "Rate20" && x.isChecked == false)) {

                checkColumn(e, "rate20");
            }
            if (PermissionInputs.find(x => x.fieldName == "Rate40" && x.isChecked == false)) {

                checkColumn(e, "rate40");
            }
            if (PermissionInputs.find(x => x.fieldName == "Rate45" && x.isChecked == false)) {

                checkColumn(e, "rate45");

            }
            if (PermissionInputs.find(x => x.fieldName == "CBMRate" && x.isChecked == false)) {

                checkColumn(e, "cbmRate");
            }
            if (PermissionInputs.find(x => x.fieldName == "WeightRate" && x.isChecked == false)) {

                checkColumn(e, "weightRate");
            }
            if (PermissionInputs.find(x => x.fieldName == "PerIndexRate" && x.isChecked == false)) {
                checkColumn(e, "perIndexRate");
            }
            if (PermissionInputs.find(x => x.fieldName == "IsActive" && x.isChecked == false)) {
                checkColumn(e, "isActive");
            }
        }

    var tariffs = [];
    var tariffId;

    function loadTariffGrid() {

        $.get('/APICalls/GetTariffs', function (data) {


            console.log("data", data)


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
                    selectTextOnEditStart: true,
                    startEditAction: "click"
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

                    // aict rate
                    {
                        dataField: "aictRate20",
                        caption: "AICT Rate20",

                    },
                    {
                        dataField: "aictRate40",
                        caption: "AICT Rate 40",

                    },
                    {
                        dataField: "aictRate45",
                        caption: "AICT Rate 45",

                    },
                    {
                        dataField: "aictcbmRate",
                        caption: "AICT CBM",

                    },

                    {
                        dataField: "aictWeightRate",
                        caption: "AICT Weight",

                    },
                    {
                        dataField: "aictPerIndexRate",
                        caption: "AICT PerIndex",

                    },
                    {
                        dataField: "aictDevededIndexAmount",
                        caption: "AICT Deveded Index",

                    },
                    // ff rate

                    {
                        dataField: "ffRate20",
                        caption: "FF Rate20",

                    },
                    {
                        dataField: "ffRate40",
                        caption: "FF Rate 40",

                    },
                    {
                        dataField: "ffRate45",
                        caption: "FF Rate 45",

                    },
                    {
                        dataField: "ffcbmRate",
                        caption: "FF CBM",

                    },

                    {
                        dataField: "ffWeightRate",
                        caption: "FF Weight",

                    },
                    {
                        dataField: "ffPerIndexRate",
                        caption: "FF PerIndex",

                    },
                    {
                        dataField: "ffDevededIndexAmount",
                        caption: "FF Deveded Index",

                    },

                    //normal

                    {
                        dataField: "rate20",
                        caption: "Rate20",
                        // allowEditing: false,

                    },
                    {
                        dataField: "rate40",
                        caption: "Rate 40",
                        //allowEditing: false,

                    },
                    {
                        dataField: "rate45",
                        caption: "Rate 45",
                        // allowEditing: false,

                    },
                    {
                        dataField: "cbmRate",
                        caption: "CBM",
                        //  allowEditing: false,

                    },

                    {
                        dataField: "weightRate",
                        caption: "Weight",
                        //  allowEditing: false,

                    },
                    {
                        dataField: "perIndexRate",
                        caption: "PerIndex",
                        //  allowEditing: false,

                    },
                    {
                        dataField: "devededIndexAmount",
                        caption: "Deveded Index",
                        //   allowEditing: false,

                    },

                    {
                        caption: "Implement From",
                        dataField: "implementFrom",
                        dataType: "date"
                    },
                    {
                        caption: "Implement To",
                        dataField: "implementTo",
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
                        caption: "Doller Rate",
                        dataField: "isDollerRate",
                        dataType: "boolean"
                    },
                    {
                        dataField: "tariffType",
                        caption: "Tariff Type",
                        //   allowEditing: false,

                    },

                ],
                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onRowUpdating: function (e) {
                    var tariff = Object.assign(e.oldData, e.newData);

                    //if (checkDuplicationCharges(tariff)) {

                    $.post('/Tariff/updateAgentTariff', { tariff: tariff }, function (data) {


                        if (data.error == true) {
                            showToast(data.message, "error");
                        } else {
                            showToast(data.message, "success");

                        }
                        loadTariffGrid();
                    });
                    //}
                    //else {
                    //    showToast("Duplicate entry error! Changes will not be saved", "error");
                    //}
                }

            }).dxDataGrid("instance");

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });

    }

    function addTariff() {
         
        var tariff = $('#tariffCharegsForm').serialize();

    console.log("tariff", tariff);

    var tariffheadid = $('#tariffHead').val();

    if (tariffheadid) {


        console.log("post postpostpostpostpostpostpost")

            console.log("iscbm", $('#iscbmorRate').val(),)

    var postUrl = '/Tariff/SaveTariffInfo?' + tariff;

    $.post(postUrl, function (data) {

                    if (data.error == true) {
        showToast(data.message, "error");
                    } else {
        showToast(data.message, "success");
    loadTariffGrid();
                    }
                   
                });

        }

    else {
        showToast("please select tariff head", "error");
        }






        //    var tariffJson = {
        //        aictRate20: $('#aictRate20').val(),
        //        aictRate40: $('#aictRate40').val(),
        //        aictRate45: $('#aictRate45').val(),
        //        aictcbmRate: $('#aictcbmRate').val(),
        //        isCBMorRate: $('#iscbmorRate').val(),
        //        aictPerIndexRate: $('#aictPerIndexRate').val(),
        //        aictWeightRate: $('#aictWeightRate').val(),
        //        aictDevededIndexAmount: $('#aictDevededIndexAmount').val(),


        //        ffRate20: $('#ffRate20').val(),
        //        ffRate40: $('#ffRate40').val(),
        //        ffRate45: $('#ffRate45').val(),
        //        ffcbmRate: $('#ffcbmRate').val(),
        //        ffWeightRate: $('#ffWeightRate').val(),
        //        ffPerIndexRate: $('#ffPerIndexRate').val(),
        //        ffDevededIndexAmount: $('#ffDevededIndexAmount').val(),

        //        tariffHead: {
        //            name: $("#tariffHead option:selected").text()
        //        }
        //};





        ////if (checkDuplicationCharges(tariffJson)) {

        //    $.post(postUrl, function (data) {

        //        loadTariffGrid();
        //        showToast("Charges Added!", "success");
        //    });

        ////}
        ////else {
        ////    showToast("Duplicate entry error! Changes will not be saved", "error");
        ////}



    }



    function formfiled() {
        if (PermissionInputs.find(x => x.fieldName == "ImplementFrom" && x.isChecked == false)) {

        document.getElementById('implementFrom').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "TariffHeadId" && x.isChecked == false)) {

        document.getElementById('tariffHead').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate20" && x.isChecked == false)) {

        document.getElementById('rate20').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate40" && x.isChecked == false)) {

        document.getElementById('rate40').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Rate45" && x.isChecked == false)) {

        document.getElementById('rate45').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "CBMRate" && x.isChecked == false)) {

        document.getElementById('cbmRate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "WeightRate" && x.isChecked == false)) {

        document.getElementById('weightRate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "PerIndexRate" && x.isChecked == false)) {

        document.getElementById('perIndexRate').disabled = true;
        }
    }



    loadTariffGrid();




