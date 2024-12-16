


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    var tariffId = 0;

    var StorageSlabs = [];


    function containerCallback() {

    }


    //$(function () {



        //});

        function loadStorageSlabGrid(StorageSlabs) {

            console.log("StorageSlabs", StorageSlabs)



            $("#gridContainer").dxDataGrid({
                dataSource: StorageSlabs,
                keyExpr: "storageSlabId",
                wordWrapEnabled: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                paging: {
                    enabled: false
                },
                editing: {
                    mode: "row",
                    allowUpdating: true,
                    allowDeleting: true,
                    allowAdding: true
                },
                columns: [


                    {
                        dataField: "aictRate",
                        caption: "AICT Rate",
                        dataType: "number",

                    },

                    {
                        dataField: "ffRate",
                        caption: "FF Rate",
                        dataType: "number",

                    },


                    {
                        dataField: "rate",
                        caption: "Rate",
                        allowEditing: false,


                    },


                    //////////////////20 40 45//////////////////


                    // AICT


                    {
                        dataField: "aictRate20",
                        caption: "AICT Rate20",
                        dataType: "number",

                    },
                    {
                        dataField: "aictRate40",
                        caption: "AICT Rate40",
                        dataType: "number",

                    },
                    {
                        dataField: "aictRate45",
                        caption: "AICT Rate45",
                        dataType: "number",

                    },

                    // ff

                    {
                        dataField: "ffRate20",
                        caption: "FF Rate20",
                        dataType: "number",

                    },
                    {
                        dataField: "ffRate40",
                        caption: "FF Rate40",
                        dataType: "number",

                    },
                    {
                        dataField: "ffRate45",
                        caption: "FF Rate45",
                        dataType: "number",

                    },

                    // normal

                    {
                        dataField: "rate20",
                        caption: "Rate20",
                        dataType: "number",
                        allowEditing: false,

                    },
                    {
                        dataField: "rate40",
                        caption: "Rate40",
                        dataType: "number",
                        allowEditing: false,

                    },
                    {
                        dataField: "rate45",
                        caption: "Rate45",
                        dataType: "number",
                        allowEditing: false,

                    },


                    {
                        dataField: "description",
                        caption: "Description",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "noOfDays",
                        caption: "Number Of Days",
                        validationRules: [{ type: "required" }],
                    },


                ],

                onRowUpdated: function (e) {
                    console.log(e);
                    var res = e.data;

                    $.post('/Tariff/updateStorageSlab', { res: res }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error");

                        }
                        else {

                            showToast(data.message, "success");
                            getstorageDetailbytariffId(res.tariffId);
                        }
                    });

                },
                onRowRemoved: function (e) {

                    console.log("e", e)

                    $.post('/Tariff/DeleteupdateStorageSlab?StorageSlabId=' + e.data.storageSlabId, function (data) {
                        if (data.error == true) {
                            showToast(data.message, "error");

                        }
                        else {

                            showToast(data.message, "success");
                            getstorageDetailbytariffId(e.data.tariffId);

                        }
                    });
                },
                onRowInserted: function (e) {
                    console.log("ee", e);
                    addStorageSlab(e);
                }
            });

        }

    function getFreeDays() {

        if (tariffId == 0) {

        alert("Please select tariff")
    }
    else {

        $.get('/Tariff/GetExtraFreeDays?tariffId=' + tariffId, function (val) {

            $('#freedays').val(val)
            if (val > 0) {
                document.getElementById("freedays").readOnly = true;
            }
            else {
                document.getElementById("freedays").readOnly = false;
            }

        });
        }

    }

    function saveFreeDays() {

        if (tariffId == 0) {

        alert("Please select tariff")
    }
    else {

        $.post('/Tariff/SaveExtraFreeDays', { freedays: $('#freedays').val(), tariffId: tariffId }, function (data) {

            if ($('#freedays').val() > 0) {
                document.getElementById("freedays").readOnly = true;
            }
            else {
                document.getElementById("freedays").readOnly = false;
            }
            showToast("Extra free days saved!", "success");

        });
        }
    }

    function addStorageSlab(e) {
 
            var data = e.data;

    var tariffresid = $('#tariff').val();
    if (tariffresid) {

        console.log("data", data)
                $.post('/Tariff/AddStorageSlab?tariffid=' + tariffresid, {slab: data }, function (data) {


                    if (data.error == true) {
        showToast(data.message, "error");
                    } else {
        showToast(data.message, "success");
    getstorageDetailbytariffId(tariffId)
                    }


                });

            }
    else {
        showToast("please select tariff", "error");
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


        //        rate:  data.rate,
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



        //if (data.error == true) {
        //    showToast(data.message, "error");
        //} else {
        //    showToast(data.message, "success");
        //    loadTariffGrid();
        //}
        //var storageSlab = { rate: , rate20: data.rate20, rate40: data.rate40, rate45: data.rate45, description: data.description, noOfDays: data.noOfDays, tariffId: tariffId };
        //$.post('/Tariff/AddStorageSlab', { slab: storageSlab }, function (data) {

        //    showToast("Storage Tariff", "success");

        //});

    }


    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Tariff" && x.isChecked == false)) {

        document.getElementById('tariff').disabled = true;

        }
        if (PermissionInputs.find(x => x.fieldName == "FreeDays" && x.isChecked == false)) {

        document.getElementById('freedays').disabled = true;

        }




    $('#tariff').on('change', function (e) {

        tariffId = this.value;

    //getFreeDays();
    getstorageDetailbytariffId(tariffId)
           
        });
    }



    function getstorageDetailbytariffId(tariffId) {


        $.get('/APICalls/GetStorageSlabsByTariff?TariffId=' + tariffId, function (data) {

            if (data) {
                StorageSlabs = data;
            }
            else {
                StorageSlabs = [];
            }

            loadStorageSlabGrid(StorageSlabs);
        })

    }


    /**/
