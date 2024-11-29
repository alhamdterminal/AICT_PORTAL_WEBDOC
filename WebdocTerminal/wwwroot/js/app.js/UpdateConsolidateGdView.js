
    var loadingProgramDetail = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    $(function () {
        loadGrid();
    });

    function loadGrid() {

        $.get("/APICalls/GetAllExportPackages", function (packageTypes) {

            var dataGrid = $("#loadingPrograms").dxDataGrid({
                dataSource: loadingProgramDetail,
                keyExpr: "loadingProgramDetailId",
                wordWrapEnabled: true,
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
                    mode: "row",
                    allowUpdating: true,
                    allowDeleting: false,
                    allowAdding: true
                },
                columns: [
                    {
                        dataField: "poNumber",
                        caption: "PO Number",
                        validationRules: [{ type: "required" }],
                    },

                    {
                        dataField: "style",
                        caption: "Style",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "totalPackages",
                        caption: "Total Packages",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "packageType",
                        caption: "Package Type",
                        validationRules: [{ type: "required" }],
                        width: 200,
                        lookup: {
                            dataSource: packageTypes,
                            displayExpr: "packageType",
                            valueExpr: "packageType"
                        }
                    },
                    {
                        dataField: "totalPieces",
                        caption: "Total Pieces",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "insDate",
                        caption: "INS Date",
                        width: 200,
                        dataType: "date",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "docsDate",
                        caption: "Docs Date",
                        width: 200,
                        dataType: "date",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "plidNumber",
                        caption: "PLID Number",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "warehouseLocation",
                        caption: "Location",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "cbm",
                        caption: "CBM",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "weight",
                        caption: "Weight",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "location",
                        caption: "BRT Location",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "colorCode",
                        caption: "Color Code",
                        validationRules: [{ type: "required" }],
                    },

                    "remarks",

                ],

                onRowInserted: function (e) {

                    var loadingProgramId = $('#loadingprogramid').val();

                    if (loadingProgramId) {
                        console.log(e.data)
                        var res = e.data;
                        $.post('/Export/ConsolidateGDDetail/AddLPDetail?loadingProgramId=' + loadingProgramId, { res, res }, function (data) {

                            if (data.error) {
                                showToast(data.message, "warning");
                            }
                            else {
                                showToast(data.message, "success");

                            }

                        });
                    }

                    else {
                        showToast("please find GD detail", "error");
                    }

                },

                onRowUpdated: function (e) {
                    console.log(e);
                    var res = e.data;
                    $.post('/Export/ConsolidateGDDetail/UpdateConsolidateDetailinfo', { res, res }, function (data) {
                        if (data.error) {
                            showToast(data.message, "warning");
                        }
                        else {
                            showToast(data.message, "success");

                        }
                    });
                },

            }).dxDataGrid("instance");

        });

    }


    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var GDNumber = url.searchParams.get("GDNumber");

    console.log("GDNumber", GDNumber);



    $('#gdnumber').val(GDNumber);

    changed_GDnumber(GDNumber);

    getinfo(GDNumber);
    });


    function changed_GDnumber(res) {
        console.log("res", res);

    if (res) {
        $.get('/APICalls/GetGDDetailGdnumber?res=' + res, function (data) {

            console.log("data", data)
            if (data) {

                $('#loadingTerminal').val(data.messageTo);
                $('#clearingAgent').val(data.clearingAgentName);
                $('#shipper').val(data.consignorName);
            }
            else {
                $('#loadingTerminal').val();
                $('#clearingAgent').val();
                $('#shipper').val();
            }

        });


        } else {

        $('#loadingTerminal').val();
    $('#clearingAgent').val();
    $('#shipper').val();
        }

    }


    function getinfo(res) {
        console.log("res", res);

    if (res) {
        $.get('/APICalls/GetGDDetailingo?res=' + res, function (data) {

            console.log("data", data)
            if (data) {

                $('#numberofPackages').val(data.noOfPackages);
                if (data.performed != null) {

                    $('#gateindate').val(new Date(data.performed.split("T")[0]).toISOString().substr(0, 10));

                } else {

                    $('#gateindate').val('');
                }

                if (data.submitedDate != null) {

                    $('#associationDate').val(new Date(data.submitedDate.split("T")[0]).toISOString().substr(0, 10));

                } else {

                    $('#associationDate').val('');
                }

                if (data.gateOutDate != null) {

                    $('#gateOutDate').val(new Date(data.gateOutDate.split("T")[0]).toISOString().substr(0, 10));

                } else {

                    $('#gateOutDate').val('');
                }



            }
            else {
                $('#numberofPackages').val('');
                $('#gateindate').val('');
                $('#associationDate').val('');
                $('#gateOutDate').val('');

            }

        });


        } else {

        $('#numberofPackages').val('');
    $('#gateindate').val('');
    $('#associationDate').val('');
    $('#gateOutDate').val('');
        }

    }

    var voyageid = 0;
    function fingddetail() {

        var GDNumber = $('#gdnumber').val();

    $.get('/APICalls/GetConsolidateGDDetailByGDnumber?gdnumber=' + GDNumber, function (res) {

        console.log("res", res)
            if (res) {

        $('#loadingprogramid').val(res.loadingProgramId);
    if (res.loadingProgramDetails.length) {

        loadingProgramDetail = res.loadingProgramDetails;
    loadGrid();
                }
    else {
        loadingProgramDetail = [];
    loadGrid();
                }


    if (res.loadingProgramDate != null) {
        $('#lpdate').val(new Date(res.loadingProgramDate.split("T")[0]).toISOString().substr(0, 10));
                } else {
        $('#lpdate').val('');
                }
    if (res.invoiceDate != null) {
        $('#invDate').val(new Date(res.invoiceDate.split("T")[0]).toISOString().substr(0, 10));
                } else {
        $('#invDate').val('');
                }
    if (res.recevingStartTime != null) {
        $('#recevingStartTime').val(new Date(res.recevingStartTime.split("T")[0]).toISOString().substr(0, 10));
                } else {
        $('#recevingStartTime').val('');
                }
    if (res.recevingEndTime != null) {
        $('#recevingEndTime').val(new Date(res.recevingEndTime.split("T")[0]).toISOString().substr(0, 10));
                } else {
        $('#recevingEndTime').val('');
                }

    $('#rfNumber').val(res.referenceNumber);
    $('#invnumber').val(res.invoiceNumber);

    $('#shippingLine').val(res.shippingLineExportId).trigger('change.select2');

    $('#shipperSeal').val(res.shipperSeal);

    $('#vesselExports').val(res.vesselExportId).trigger('change.select2');

    //  $('#voyageExportidres').val(res.voyageExportId);
    voyageid = res.voyageExportId;

    $('#shippingAgent').val(res.shippingAgentExportId).trigger('change.select2');
    //$('#commodity').val(res.commodityId).trigger('change.select2');
    $('#commodityName').val(res.commodityName);

    //$('#dischargeCountry').val(res.dischargeCountry).trigger('change.select2');

    $('#destination').val(res.destination).trigger('change.select2');



                //if (res.destination) {
        //    getcountrybyport(res.destination)
        //}
        //else {
        //    $('#dischargeCountry').val('');
        //}
        $('#finalDestination').val(res.finalDestination).trigger('change.select2');

    // $('#finalDestination').val(res.finalDestination);

    $('#clearingAgentCNIC').val(res.clearingAgentCNIC);

    $('#cLearingAgentReprsentative').val(res.cLearingAgentReprsentative);

    $('#driverCNIC').val(res.driverCNIC);
    $('#driverName').val(res.driverName);
    $('#cargoRecevingCondition').val(res.cargoRecevingCondition);

    $('#goodsHeadExportId').val(res.goodsHeadExportId).trigger('change.select2');
    $('#cargoType').val(res.cargoType);
    $('#clearingAgentExportId').val(res.clearingAgentExportId).trigger('change.select2');
    $('#exportConsigneeId').val(res.exportConsigneeId).trigger('change.select2');
    $('#portAndTerminalExportId').val(res.portAndTerminalExportId).trigger('change.select2');


            }
    else {
        resetformvalues();
            }
        });
    }

    function getcountrybyport(portname) {

        console.log("portname", portname);

    if (portname) {

        $.get('/APICalls/GetCountryByPort?portname=' + portname, function (res) {

            console.log("res for port", res);

            if (res) {
                $('#dischargeCountry').val(res.countryName);
            }
            else {
                $('#dischargeCountry').val('');
            }

        });
        }
    else {
        $('#dischargeCountry').val('');
        }

    }

    function resetformvalues() {
        voyageid = 0;
    $('#lpdate').val('');
    $('#invDate').val('');
    $('#recevingStartTime').val('');
    $('#recevingStartTime').val('');
    $('#rfNumber').val('');
    $('#invnumber').val('');

    $('#shippingLine').select2().val('').trigger('change.select2');

    $('#shipperSeal').val('');

    $('#vesselExports').select2().val('').trigger('change.select2');

    $('#voyageExportidres').val('');

    $('#shippingAgent').select2().val('').trigger('change.select2');

    //$('#commodity').select2().val('').trigger('change.select2');

    $('#commodityName').val('');


    $('#dischargeCountry').val('');

    $('#destination').select2().val('').trigger('change.select2');
    $('#finalDestination').select2().val('').trigger('change.select2');

    //$('#finalDestination').val('');
    $('#clearingAgentCNIC').val('');
    $('#cLearingAgentReprsentative').val('');
    $('#driverCNIC').val();
    $('#driverName').val();
    $('#cargoRecevingCondition').val();

    $('#goodsHeadExportId').select2().val('').trigger('change.select2');
    $('#cargoType').val('');

    $('#portAndTerminalExportId').select2().val('').trigger('change.select2');
    $('#clearingAgentExportId').select2().val('').trigger('change.select2');
    $('#exportConsigneeId').select2().val('').trigger('change.select2');

    loadingProgramDetail = [];
    loadGrid();
    }


    function changed_VesselExport(arg) {


        $('#voyageExportidres')
            .empty()
            .append('<option selected="selected" value="">Select Voyage</option>');

    $('#eta').val('');
    $('#etd').val('');
    $('#cutoff').val('');

    $.get('/APICalls/GetVoyagesFormvesselExportId?vesselExportId=' + arg, function (data) {

        VoyageExports = data;

    if (VoyageExports.length) {
        console.log("VoyageExports", VoyageExports);

    var options = VoyageExports.map(function (val, ind) {
                    return $("<option></option>").val(val.voyageExportId).html(val.voyageNumber);
                });
    console.log("options", options);

    $('#voyageExportidres').append(options);

                if (voyageid > 0) {
        $('#voyageExportidres').val(voyageid);
    changed_VoyageExport(voyageid);
                }

            }
    else {
        $('#voyageExportidres')
            .empty()
            .append('<option selected="selected" value="">Select Voyage</option>');

    $('#eta').val('');
    $('#etd').val('');
    $('#cutoff').val('');

    voyageid = 0;
            }

        });


    }

    function changed_VoyageExport(arg) {
        console.log("arg", arg)

        callChangefunc(arg)
    if (arg) {
        callChangefunc(arg)
    } else {
        $('#eta').val('');
    $('#etd').val('');
    $('#cutoff').val('');
        }
    }



    function changed_DischargePort(arg) {

        console.log("arg", arg)

        getcountrybyport(arg)




    }


    function callChangefunc(val) {

        var VoyageExportId = val;
    $.get("/Export/LoadingProgram/GetVoyageExportById?VoyageExportId=" + VoyageExportId, function (data) {
            if (data) {
        $('#eta').val(formatDate(data.eta));
    $('#etd').val(formatDate(data.etd));
    $('#cutoff').val(formatDate(data.cutOff));
            }
    else {
        $('#eta').val('');
    $('#etd').val('');
    $('#cutoff').val('');
            }

        })

    }

    function formatDate(date) {
        var d = new Date(date),
    month = '' + (d.getMonth() + 1),
    day = '' + d.getDate(),
    year = d.getFullYear();
    hour = d.getHours();
    Minutes = d.getMinutes(),
    Seconds = d.getSeconds()

    if (month.length < 2)
    month = '0' + month;
    if (day.length < 2)
    day = '0' + day;

    return [year, month, day].join('-');
    }

    function formfiled() {


    }



    var result;


    function checkFiledData() {

        result = false;


    if (!$('#gdnumber').val()) {

        result = true;
    return showToast("please select gd number", "error");
        }

    if (!$('#lpdate').val()) {
        result = true;
    return showToast("please select Loading Program Date", "error");
        }

    if (!$('#rfNumber').val()) {
        result = true;
    return showToast("please add Reference Number", "error");
        }

    if (!$('#invnumber').val()) {
        result = true;
    return showToast("please add Invoice Number", "error");
        }
    if (!$('#invDate').val()) {
        result = true;
    return showToast("please add Invoice Date", "error");
        }
    if (!$('#shippingLine').val()) {
        result = true;
    return showToast("please select Shipping Line", "error");
        }
    if (!$('#vesselExports').val()) {
        result = true;
    return showToast("please select Vessel", "error");
        }

    if (!$('#voyageExportidres').val()) {
        result = true;
    return showToast("please select Voyage Export", "error");
        }

    if (!$('#shipperSeal').val()) {
        result = true;
    return showToast("please add Shipper Seal", "error");
        }

    if (!$('#shippingAgent').val()) {
        result = true;
    return showToast("please select Shipping Agent", "error");
        }


    if (!$('#commodityName').val()) {
        result = true;
    return showToast("please add Commodity", "error");
        }


    if (!$('#destination').val()) {
        result = true;
    return showToast("please select Destination", "error");
        }

    if (!$('#cargoRecevingCondition').val()) {
        result = true;
    return showToast("please add C R Condition", "error");
        }

    if (!$('#finalDestination').val()) {
        result = true;
    return showToast("please add Final Destination", "error");
        }

    if (!$('#cLearingAgentReprsentative').val()) {
        result = true;
    return showToast("please add Agent Representative", "error");
        }

    if (!$('#clearingAgentCNIC').val()) {
        result = true;
    return showToast("please add Agent CNIC", "error");
        }

    if (!$('#driverName').val()) {
        result = true;
    return showToast("please add Driver Name", "error");
        }
    if (!$('#driverCNIC').val()) {
        result = true;
    return showToast("please add Driver CNIC", "error");
        }
    if (!$('#recevingStartTime').val()) {
        result = true;
    return showToast("please add Driver CNIC", "error");
        }

    if (!$('#recevingEndTime').val()) {
        result = true;
    return showToast("please add Driver CNIC", "error");
        }

    if (!$('#goodsHeadExportId').val()) {
        result = true;
    return showToast("please select Goods Head", "error");
        }

    if (!$('#cargoType').val()) {
        result = true;
    return showToast("please select cargo type", "error");
        }

    if (!$('#clearingAgentExportId').val()) {
        result = true;
    return showToast("please select clearingagent", "error");
        }

    if (!$('#portAndTerminalExportId').val()) {
        result = true;
    return showToast("please select loading terminal", "error");
        }

    if (!$('#exportConsigneeId').val()) {
        result = true;
    return showToast("please select consignee", "error");
        }

    }


    function submitGdDetail() {

        var loadingProgramId = $('#loadingprogramid').val();
    if (loadingProgramId) {
        checkFiledData();

    if (result == false) {

                var values = $('#LoadingPrgramForm').serialize();

    $.post('/Export/ConsolidateGDDetail/UpdateConsolidateinfo?' + values + '&loadingProgramId=' + loadingProgramId, function (data) {
                    if (data.error) {
        showToast(data.message, "warning");
                    }
    else {
        showToast(data.message, "success");
                    }
                })
            }
        }
    else {
        showToast("Please Find Detail and lp Code", "error");
        }




    }
