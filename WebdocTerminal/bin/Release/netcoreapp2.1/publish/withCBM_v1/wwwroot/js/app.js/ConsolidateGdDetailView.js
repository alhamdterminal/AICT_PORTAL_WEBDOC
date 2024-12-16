
    $(function () {
        loadGrid();

    $("#clearingAgentCNIC").inputmask("99999-9999999-9");
    $("#driverCNIC").inputmask("99999-9999999-9");

    });




    var loadingProgramDetail = [];




    function submitBtnShowHide(show) {
        var btn = document.getElementById("btnSubmit");
    if (show)
    btn.style.display = 'inline-grid';
    else
    btn.style.display = 'none';
    }

    function formatDateDev(date) {
        var d = new Date(date),
    month = '' + (d.getMonth() + 1),
    day = '' + d.getDate(),
    year = d.getFullYear();

    return [month, day, year].join('/');
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


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function loadGrid() {

        $.get("/APICalls/GetAllExportPackages", function (packageTypes) {

            var dataGrid = $("#loadingPrograms").dxDataGrid({
                dataSource: loadingProgramDetail,
                keyExpr: "loadingProgramDetailId",
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
                    allowDeleting: true,
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
                    //{
                    //    dataField: "finalDestination",
                    //    caption: "Final Des",
                    //     validationRules: [{ type: "required" }],
                    //},
                    //{
                    //    dataField: "dishargePort",
                    //    caption: "Disharge Port",
                    //     validationRules: [{ type: "required" }],
                    //},
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

            }).dxDataGrid("instance");

        });

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

    function restform() {
        var r = confirm("Are you sure");
    if (r == true) {
        window.location.href = window.location.href
    }

    }


    function changed_GDnumber(res) {
        console.log("res", res);

        //if (res) {
        //    $.get('/APICalls/GetGDDetailGdnumber?res=' + res, function (data) {

        //        console.log("data", data)
        //        if (data) {

        //            $('#loadingTerminal').val(data.messageTo);
        //            $('#clearingAgent').val(data.clearingAgentName);
        //            $('#shipper').val(data.consignorName);
        //        }
        //        else {
        //            $('#loadingTerminal').val();
        //            $('#clearingAgent').val();
        //            $('#shipper').val();
        //        }

        //    });


        //} else {

        //    $('#loadingTerminal').val();
        //    $('#clearingAgent').val();
        //    $('#shipper').val();
        //}

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
            }
    else {
        $('#voyageExportidres')
            .empty()
            .append('<option selected="selected" value="">Select Voyage</option>');

    $('#eta').val('');
    $('#etd').val('');
    $('#cutoff').val('');
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


    function formfiled() {

    }

    function gdNumber_changed(data) {
        gdnumber = data.value;
    console.log("gdnumber", gdnumber)
    }

    function submitGdDetail() {

        checkFiledData();

    if (result == false) {
            var values = $('#LoadingPrgramForm').serialize();

    var loadingProgramDetail = $("#loadingPrograms").dxDataGrid("option", "dataSource");


    $.post('AddLoadingProgram?' + values, {loadingProgramDetail: loadingProgramDetail }, function (data) {
                if (data.error) {
        showToast(data.message, "warning");
                }
    else {
        showToast(data.message, "success");
    window.location.href = window.location.href
                }




            })
        }


    }
    var result;


    function checkFiledData() {

        result = false;


    if (!$('#gdnumberid').val()) {

        result = true;
    return showToast("please select gd number", "error");
        }

    if (!$('#LoadingProgramDate').val()) {
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
    if (!$('#InvoiceDate').val()) {
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

    if (!$('#dischargeCountry').val()) {
        result = true;
    return showToast("please select country  ", "error");
        }

    if (!$('#goodsHeadExportId').val()) {
        result = true;
    return showToast("please select Goods Head", "error");
        }
    if (!$('#cargoType').val()) {
        result = true;
    return showToast("please select Cargo Type", "error");
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

    var loadingProgramsData = $("#loadingPrograms").dxDataGrid("option", "dataSource");

    console.log("loadingProgramsData", loadingProgramsData)

    if (!loadingProgramsData.length) {
        result = true;
    return showToast("please add atleast one po detail", "error");
        }


    }




    function enterCNICNumebrForAgent(val) {
        console.log("val", val)
        FindDataAgent(val);
    }


    function FindDataAgent(val) {
        var val = val;
    console.log("val", val)
    if (val) {
        $.get('/APICalls/GetAgentNameByCNIC?res=' + val, function (data) {

            console.log("data", data)
            if (data) {
                $('#cLearingAgentReprsentative').val(data);
            }
            else {

                $('#cLearingAgentReprsentative').val('');
            }

        });
        }

    }


    function enterCNICNumebrForDriver(val) {
        console.log("val", val)
        FindDataDriver(val);
    }


    function FindDataDriver(val) {
        var val = val;
    console.log("val", val)
    if (val) {
        $.get('/APICalls/GetDriverNameByCNIC?res=' + val, function (data) {
            console.log("data", data)
            if (data) {
                $('#driverName').val(data);
            }
            else {

                $('#driverName').val('');
            }

        });
        }

    }

    function changed_DischargePort(arg) {

        console.log("arg", arg)

        getcountrybyport(arg)

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


