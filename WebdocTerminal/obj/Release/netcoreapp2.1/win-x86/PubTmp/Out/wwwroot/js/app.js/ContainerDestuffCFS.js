


    function lockdate() {

        var dtToday = new Date();

    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
    month = '0' + month.toString();
    if (day < 10)
    day = '0' + day.toString();
    var maxDate = year + '-' + month + '-' + day;
    $('#destuffdate').attr('min', maxDate);
    }


    var containrId;
    var orientation;
    var isDestuff = false;


    function refresh() {
        window.location.reload();
    }

    var InvoiceFoud = [
    {Name: "YES" },
    {Name: "NO" }
    ];

    var PackageFoud = [
    {Name: "YES" },
    {Name: "NO" }
    ];

    var packageTypes = [
    {Name: "CARTONS" },
    {Name: "DRUMS" },
    {Name: "PALLET" },
    {Name: "PACKAGES" },
    {Name: "PACKAGE" },
    {Name: "SKIDS" },
    {Name: "LOTS" },
    {Name: "BALES" },
    {Name: "ROLLS" },
    {Name: "L / VAN" },
    {Name: "PIECES" },
    {Name: "CASES" },
    {Name: "S / TRUCK" },
    {Name: "BOXES" },
    {Name: "UNITS" },
    {Name: "BAGS" },
    {Name: "BUNDELS" },
    {Name: "BLOCKS" },
    {Name: "CONTAINERS" },
    {Name: "T / CHEST" },
    {Name: "TANKS" },
    {Name: "TANK CONTAINER" },
    {Name: "TYRES" },
    {Name: "BRAKE BULK" },
    {Name: "REELS" },
    {Name: "CRATES" },
    {Name: "SLABS" },
    {Name: "FAX, TELEPHONE" },
    {Name: "SETS" },
    {Name: "COILS" },
    {Name: "BUNCH" },
    {Name: "CANS" },
    {Name: "CASE" },
    {Name: "BOX" },
    {Name: "COLLIS" },
    {Name: "COLLI" },
    {Name: "PARCELS" },
    {Name: "JERRICANS" },
    {Name: "SACKS" },
    {Name: "SHEETS" },
    {Name: "PCESES" },
    {Name: "FUTS" },
    {Name: "PJERRICANS P" },
    {Name: "BARRELS" },
    {Name: "NOS" },
    {Name: "CYLINDERS" },
    {Name: "CYLINDER" },
    {Name: "W/CASES" },
    {Name: "PICES" },
    {Name: "LIFT VANS" },
    {Name: "MODULES" },
    {Name: "GAS CYLINDER" },
    {Name: "SUITCASES" },
    {Name: "COLLIES" },
    {Name: "SAFETY LEATHER" },
    {Name: "UNDER REBATE" },
    {Name: "FOOTBALL" },
    {Name: "TRIWALL" },
    {Name: "DRUM" },
    {Name: "I B C" },
    {Name: "LVANS" },
    {Name: "PAILS" },
    {Name: "PAPERBAGS" },
    {Name: "CBPALETS" },
    {Name: "LOOS" },
    {Name: "WOODEN BOX" },
    {Name: "BULK" },
    {Name: "PAL" },
    {Name: "MOZAMBIQUE" },
    {Name: "PCS" },
    {Name: "BUNDLES" },
    {Name: "CARD BOARDS" },
    {Name: "CANES" },
    {Name: "ITEMS" },
    {Name: "BALLS" },
    {Name: "KOLLI" },
    {Name: "POT" },
    {Name: "SHAWL BATH ROB" },
    {Name: "LOOSE SCRAP" },
    {Name: "CAPS" },
    {Name: "RAMES" },
    {Name: "GOAT HAIR" },
    {Name: "TRUNKS" },
    {Name: "PLASTICKEG" },
    {Name: "POLY TANKS" },
    {Name: "JARS" },
    {Name: "OCTABIN" },
    {Name: "BOTTLES" }

    ];

    var key = 0;

    $(document).ready(function () {

        lockdate();
    setOrientation(isDestuff);

    var containers = [];

    $('#shippingAgents').css('pointer-events', 'none');



    });


    function addExcessRemark() {

        var index = containers.findIndex((obj => obj.containerIndexId == key));
    containers[index].shortExcessRemarks = $('#excessRemarkInput').val();
    }

    function checkExcess(e) {

        if (e.parentType === "dataRow" && e.dataField === "shortExcess") {

        e.editorElement.dxSelectBox('instance').option('onValueChanged', args => {
            e.setValue(args.value);
            if (args.value == "YES") {
                key = e.row.data.containerIndexId;
                $('#excessRemarkModal').modal('show');

            }
        });
        }

    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "containerNo");
    checkColumn(e, "indexNumber");
    checkColumn(e, "description");
    checkColumn(e, "manifestWeight");
    // checkColumn(e, "package");
    checkColumn(e, "packageType");
    checkColumn(e, "cosigneeName");
    checkColumn(e, "marksAndNumber");
    checkColumn(e, "manifestQuantity");
         
    }

    function containerChangeCallback(ornt) {

        orientation = ornt;

    var btn = document.getElementById("buttn");

    btn.style.display = 'inline-grid';

    populateTable();

    }

    function populateTable(dataSource) {

        if ($('#containerdb').val() || $('#indexdb').val()) {

            var containerId = orientation == "container" ? $("#containerdb option:selected").val() : $("#indexdb option:selected").val();

    $.get('/APICalls/GetShippingAgentByGateInContainer?ContainerIndexId=' + containerId, function (index) {
                if (index) {
        $('#shippingAgents').val(index.shippingAgentId);
                }
            });
        }


    if (containerId) {

        $.get('/APICalls/GetDestuffingCFSContainers?ContainerId=' + containerId + "&orientation=" + orientation, function (data) {

            console.log("data", data)

            var columnArray = [];

            if (orientation == "container") {

                columnArray = [

                    {
                        dataField: "indexNumber",
                        caption: "Index"
                    },


                    {
                        dataField: "description",
                        width: 300,
                        caption: "Description"
                    },

                    {
                        dataField: "manifestWeight",
                        caption: "ManifestWeight"
                    },
                    {
                        dataField: "foundWeight",
                        caption: "Found Weight",
                        dataType: "number",
                    },
                    {
                        dataField: "package",
                        caption: "Package"
                    },
                    {
                        dataField: "manifestQuantity",
                        caption: "ManifestQuantity"
                    },
                    {
                        dataField: "packageType",
                        caption: "Package Type",
                        lookup: {
                            dataSource: packageTypes,
                            valueExpr: "Name",
                            displayExpr: "Name"
                        }
                    },
                    {
                        dataField: "foundPackageType",
                        caption: "Found Package Type",
                        validationRules: [{ type: "required" }],
                        //width: 200,
                        lookup: {
                            dataSource: packageTypes,
                            valueExpr: "Name",
                            displayExpr: "Name"
                        }
                    },
                    {
                        dataField: "cosigneeName",
                        caption: "Consignee Name"
                    },
                    {
                        dataField: "vehicleMeasurementId",
                        width: 200,
                        caption: "Vechile Type",
                        lookup: {
                            dataSource: VehicleMeasurementList,
                            valueExpr: "vehicleMeasurementId",
                            displayExpr: "vehicleName",
                            allowClearing: true,
                        },
                        setCellValue(rowData, value) {

                            if (value) {

                                var res = VehicleMeasurementList.find(x => x.vehicleMeasurementId == value);
                                rowData.cbm = res.vehicleMeasurementCBM;
                                rowData.vehicleMeasurementId = res.vehicleMeasurementId;
                            }
                            else {
                                rowData.cbm = 0;
                                rowData.vehicleMeasurementId = null;
                            }
                        },
                    },
                    {
                        dataField: "cbm",
                        dataType: "number",
                        caption: "CBM"
                    },
                    {
                        dataField: "marksAndNumber",
                        caption: "MarksAndNumber"
                    },
                    {
                        dataField: "location",
                        caption: "Location"
                    },
                    {
                        dataField: "remarks",
                        caption: "Remarks"
                    },
                    {
                        dataField: "invoiceFoud",
                        validationRules: [{ type: "required" }],
                        caption: "Invoice Found",
                        lookup: {
                            dataSource: InvoiceFoud,
                            valueExpr: "Name",
                            displayExpr: "Name"
                        }
                    },
                    {
                        dataField: "shortExcess",
                        caption: "Short/Excess",
                        validationRules: [{ type: "required" }],
                        lookup: {
                            dataSource: PackageFoud,
                            valueExpr: "Name",
                            displayExpr: "Name"
                        }
                    },
                    //{
                    //    caption: "Hold",
                    //    dataField: "isHold",
                    //    dataType: "boolean"
                    //},
                    {
                        caption: "Invoice Amount",
                        dataField: "invoiceAmount",
                    },
                    {
                        caption: "",
                        dataField: "ischeck",
                        dataType: "boolean"
                    },
                    {
                        width: 100,
                        alignment: 'center',
                        cellTemplate: function (container, options) {
                            if (options.data.destuffStatus == "Yes") {

                                $('<a/>').addClass('dx-link')
                                    .text('Update')
                                    .on('dxclick', function () {

                                        $.post('UpdateDestuffInfo?containerIndexId=' + options.data.containerIndexId + "&foundWeight=" + options.data.foundWeight + "&package=" + options.data.package + "&foundPackageType=" + options.data.foundPackageType
                                            + "&cbm=" + options.data.cbm + "&location=" + options.data.location + "&remarks=" + options.data.remarks + "&invoiceFoud=" + options.data.invoiceFoud
                                            + "&shortExcess=" + options.data.shortExcess + "&invoiceAmount=" + options.data.invoiceAmount + "&vehicleMeasurementId=" + options.data.vehicleMeasurementId, function (data) {

                                                if (data.error == true) {

                                                    showToast(data.message, "error");

                                                }
                                                else {
                                                    showToast(data.message, "success");
                                                }

                                            });

                                    })
                                    .appendTo(container);
                            }

                        }
                    }

                ]
            }
            else {
                columnArray = [

                    {
                        dataField: "containerNo",
                        caption: "Container No"
                    },


                    {
                        dataField: "description",
                        width: 300,
                        caption: "Description"
                    },

                    {
                        dataField: "manifestWeight",
                        caption: "ManifestWeight"
                    },
                    {
                        dataField: "foundWeight",
                        dataType: "number",
                        caption: "Found Weight",

                    },
                    {
                        dataField: "package",
                        caption: "Package"
                    },
                    {
                        dataField: "manifestQuantity",
                        caption: "ManifestQuantity"
                    },
                    {
                        dataField: "packageType",
                        caption: "Package Type",
                        lookup: {
                            dataSource: packageTypes,
                            valueExpr: "Name",
                            displayExpr: "Name"
                        }
                    },
                    {
                        dataField: "foundPackageType",
                        caption: "Found Package Type",
                        validationRules: [{ type: "required" }],
                        //width: 200,
                        lookup: {
                            dataSource: packageTypes,
                            valueExpr: "Name",
                            displayExpr: "Name"
                        }
                    },
                    {
                        dataField: "cosigneeName",
                        caption: "Consignee Name"
                    },
                    {
                        dataField: "vehicleMeasurementId",
                        width: 200,
                        caption: "Vechile Type",
                        lookup: {
                            dataSource: VehicleMeasurementList,
                            valueExpr: "vehicleMeasurementId",
                            displayExpr: "vehicleName",
                            allowClearing: true,
                        },
                        setCellValue(rowData, value) {

                            if (value) {

                                var res = VehicleMeasurementList.find(x => x.vehicleMeasurementId == value);
                                rowData.cbm = res.vehicleMeasurementCBM;
                                rowData.vehicleMeasurementId = res.vehicleMeasurementId;
                            }
                            else {
                                rowData.cbm = 0;
                                rowData.vehicleMeasurementId = null;
                            }
                        },
                    },
                    {
                        dataField: "cbm",
                        dataType: "number",
                        caption: "CBM"
                    },

                    {
                        dataField: "marksAndNumber",
                        caption: "MarksAndNumber"
                    },
                    {
                        dataField: "location",
                        caption: "Location"
                    },
                    {
                        dataField: "remarks",
                        caption: "Remarks"
                    },
                    {
                        dataField: "invoiceFoud",
                        caption: "Invoice Found",
                        validationRules: [{ type: "required" }],
                        lookup: {
                            dataSource: InvoiceFoud,
                            valueExpr: "Name",
                            displayExpr: "Name"
                        }
                    },
                    {
                        dataField: "shortExcess",
                        caption: "Short/Excess",
                        validationRules: [{ type: "required" }],
                        lookup: {
                            dataSource: PackageFoud,
                            valueExpr: "Name",
                            displayExpr: "Name"
                        }
                    },
                    {
                        caption: "Invoice Amount",
                        dataField: "invoiceAmount",
                    },
                    //{
                    //    caption: "Hold",
                    //    dataField: "isHold",
                    //    dataType: "boolean"
                    //},
                    {
                        caption: "",
                        dataField: "ischeck",
                        dataType: "boolean"
                    },
                    {
                        width: 100,
                        alignment: 'center',
                        cellTemplate: function (container, options) {
                            if (options.data.destuffStatus == "Yes") {

                                $('<a/>').addClass('dx-link')
                                    .text('Update')
                                    .on('dxclick', function () {

                                        $.post('UpdateDestuffInfo?containerIndexId=' + options.data.containerIndexId + "&foundWeight=" + options.data.foundWeight + "&package=" + options.data.package + "&foundPackageType=" + options.data.foundPackageType
                                            + "&cbm=" + options.data.cbm + "&location=" + options.data.location + "&remarks=" + options.data.remarks + "&invoiceFoud=" + options.data.invoiceFoud
                                            + "&shortExcess=" + options.data.shortExcess + "&invoiceAmount=" + options.data.invoiceAmount + "&vehicleMeasurementId=" + options.data.vehicleMeasurementId, function (data) {

                                                if (data.error == true) {

                                                    showToast(data.message, "error");

                                                }
                                                else {
                                                    showToast(data.message, "success");
                                                }

                                            });

                                    })
                                    .appendTo(container);
                            }

                        }
                    }


                ]



            }

            if (dataSource)
                containers = dataSource
            else
                containers = data;


            console.log("containers", containers);

            var grid = $("#destuffContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');

            var dataGrid = $("#destuffContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "containerIndexId",
                //wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    enabled: false
                },
                editing: {
                    mode: "batch",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                columns: columnArray,
                onEditorPrepared(e) {
                    console.log("prepared")
                    checkExcess(e);
                },
                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {

                    $("#btnSubmit").attr("disabled", false);
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
                });


            });

        });

        }
    }

    function UpdateInfo( res) {
        console.log("value", res);
    }

    function submit() {

        containrId = (orientation == "container") ? $("#containerdb option:selected").val() : $("#indexdb option:selected").val();

    if (orientation == "container") {

            var containerNo = $("#containerdb option:selected").text();

    window.open("/Import/Reports/TallySheet?containerNo=" + containerNo + "&virno=" + igm + "&orientation=" + orientation)
        }

    if (orientation == "index") {

            var containerNo = $("#indexdb option:selected").text();

    window.open("/Import/Reports/TallySheet?containerNo=" + containerNo + "&virno=" + igm + "&orientation=" + orientation)
        }


       
    }

    function destuff() {


        var res = containers.filter(c => c.ischeck == true && c.destuffStatus == "Yes");
    if (res.length) {
            return alert("Can't save  igm " + res[0].virNumber + " and index index No " + res[0].indexNumber + " due to already save");
        }


    var dropDownType = $('#orientation_db').val();
    console.log("dropDownType", dropDownType)

    if (dropDownType == "index") {
        $("#btnSubmit").attr("disabled", true);

            //var Ischeckcontainers = containers.filter(c => c.ischeck == true);
            //console.log("Ischeckcontainers", Ischeckcontainers)
            //var form = document.getElementById('TellySheet');

            //var formData = getFormData($('#TellySheet'));

            //console.log("formData", formData)
            //console.log("form", form)
            //console.log("form.reportValidity", form.reportValidity)

            //form.reportValidity();

            //if (form.checkValidity() && containers) {

            //    $.post('Destuff', {containers: Ischeckcontainers, tellySheet: formData }, function (data) {




            //        if (data.error == true) {
            //            showToast(data.message, "error");
            //            //window.location.href = window.location.href;
            //            window.setInterval('refresh()', 3000);


            //        }
            //        else {
            //            showToast(data.message, "success");
            //         //   window.location.href = window.location.href;
            //            window.setInterval('refresh()', 3000); 

            //            populateTable(containers.filter(c => c.ischeck == false));
            //        }


            //    });
            //}

            var Ischeckcontainers = containers.filter(c => c.ischeck == true);
    console.log("Ischeckcontainers", Ischeckcontainers)
    var form = document.getElementById('TellySheet');

    var formData = getFormData($('#TellySheet'));
    console.log("form", form)
    console.log("form.reportValidity", form.reportValidity)
    form.reportValidity();

    if (form.checkValidity() && containers) {
        $("#btnSubmit").attr("disabled", true);
    $.post('DestuffIndexWise', {containers: Ischeckcontainers, tellySheet: formData }, function (data) {

                    if (data.error == true) {

        showToast(data.message, "error");
    //   window.location.href = window.location.href;
    window.setInterval('refresh()', 3000);


                    }
    else {
        showToast(data.message, "success");
    // window.location.href = window.location.href;
    window.setInterval('refresh()', 3000);

                        populateTable(containers.filter(c => c.ischeck == false));
                    }
    //window.location.href = window.location.href;
    window.setInterval('refresh()', 3000);



                });
            }
        }
    else {
         

            var Ischeckcontainers = containers.filter(c => c.ischeck == true);
    console.log("Ischeckcontainers", Ischeckcontainers)
    var form = document.getElementById('TellySheet');

    var formData = getFormData($('#TellySheet'));
    console.log("form", form)
    console.log("form.reportValidity", form.reportValidity)
    form.reportValidity();

    if (form.checkValidity() && containers) {
        $("#btnSubmit").attr("disabled", true);
    $.post('DestuffContainerWise', {containers: Ischeckcontainers, tellySheet: formData }, function (data) {
                     
                    if (data.error == true) {

        showToast(data.message, "error");
    //   window.location.href = window.location.href;
    window.setInterval('refresh()', 3000); 

                          
                    }
    else {
        showToast(data.message, "success");
    // window.location.href = window.location.href;
    window.setInterval('refresh()', 3000); 

                        populateTable(containers.filter(c => c.ischeck == false));
                    }
    //window.location.href = window.location.href;
    window.setInterval('refresh()', 3000); 



                });
            }
        }



    }


    function destuffsaveData() {



        var dropDownType = $('#orientation_db').val();
    console.log("dropDownType", dropDownType)

    if (dropDownType == "container") {
        showToast("On Container Wise Use Destuff & Submit", "danger");
        } else {
            var Ischeckcontainers = containers.filter(c => c.ischeck == true);

    $.post('DestuffSaved', {containers: Ischeckcontainers }, function (data) {

                if (data.error == true) {
        showToast(data.message, "error");

    window.location.href = window.location.href;
                }
    else {
        showToast(data.message, "success");
    window.location.href = window.location.href;
                }
            });
        }


    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function getFormData($form) {
        var unindexed_array = $form.serializeArray();
    var indexed_array = { };

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
        });

    return indexed_array;
    }

    var VehicleMeasurementList = [];

    function formfiled() {

        $.get('/Import/Setup/GetVehicleMeasurement', function (resData) {
            VehicleMeasurementList = resData;
        });


    }

    function emptyoutcontainer() {

        var dropDownType = $('#orientation_db').val();
    console.log("dropDownType", dropDownType)

    if (dropDownType == "container") {

            var containernumber =   $("#containerdb option:selected").text();

    console.log("igm", igm);
    console.log("containernumber", containernumber);

    if (igm && containernumber) {

        $.post('MarkEmptyOut?igm=' + igm + '&containernumber=' + containernumber, function (data) {
            if (data.error == true) {
                showToast(data.message, "error");

                window.location.href = window.location.href;
            }
            else {
                showToast(data.message, "success");
                window.location.href = window.location.href;
            }
        });
            }
    else {
        showToast("please select igm and container", "error")
    }




        } else {
            //showToast("mark container wise" , "error")


            var indexnumber = $("#indexdb  option:selected").text();

    console.log("igm", igm);
    console.log("indexnumber", indexnumber);

    if (igm && indexnumber) {

        $.post('MarkEmptyOutIndexWise?igm=' + igm + '&indexnumber=' + indexnumber, function (data) {
            if (data.error == true) {
                showToast(data.message, "error");

                window.location.href = window.location.href;
            }
            else {
                showToast(data.message, "success");
                window.location.href = window.location.href;
            }
        });
            }
    else {
        showToast("please select igm and container", "error")
    }


        }
    }



    /**/
