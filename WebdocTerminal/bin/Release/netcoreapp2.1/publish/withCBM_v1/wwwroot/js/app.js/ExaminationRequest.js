


    var type;
    var examinationCharges = [];
    var containerlist = [];


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function containerChangeCallback(ornt) {
        console.log("cfs")
        console.log("type", type)

    console.log($('#indexdb').val());
    console.log("virno " , igm);



    var cid = $('#indexdb').val();
    if (cid) {

        getcfsdata();
    getcontainerlist("CFS" , igm, $('#indexdb option:selected ').text());
    GetExaminationRequestdata(type, cid, "", "");
        }
    else {

        getcfsdata();
    GetExaminationRequestdata(type, 0, "", "")
    getcontainerlist("CFS","", "");

        }

    }

    function getcfsdata() {

        loadGrid([]); 
    }


    function containerCallback() {

        console.log("cy")

        console.log("type", type)


    var indexno = $('#containerIndexCYdb').val();
    console.log("indexno", indexno)
    console.log("igm", igm)
    if (igm, indexno) {

        getcydata(igm, indexno);
    getcontainerlist("CY", igm, indexno);
    GetExaminationRequestdata(type, 0, igm, indexno);
        }
    else {
        getcydata(igm, indexno)
            getcontainerlist("CY", "", "");
    GetExaminationRequestdata(type, 0, "", "")

        }

    }


    function getcontainerlist(type,igm , index) {

        var urlforcontainers = "";

    if (type == "CFS") {
        urlforcontainers = '/APICalls/GetALLCFSContainers?virno=' + igm + '&indexno=' + $('#indexdb option:selected ').text();

    $.get(urlforcontainers, function (res) {
                if (res) {
        containerlist = res;
    loadContainerGrid(containerlist)
                }
    else {
        loadContainerGrid([])
    }
            });
        }
    else if (type == "CY") {
        urlforcontainers = '/APICalls/GetALLCYContainers?virno=' + igm + '&indexno=' + index;
    $.get(urlforcontainers, function (res) {
                if (res) {
        containerlist = res;
    loadContainerGrid(containerlist)
                }
    else {
        loadContainerGrid([])
    }
            });
        }
    else {
        loadContainerGrid([])

    }

     
    }


    function loadContainerGrid(resdta){
        $("#searchBoxForContainerList").dxSelectBox({
            dataSource: {
                store: resdta,
                requireTotalCount: true,
                pageSize: 4,
                paginate: true,
            },
            name: 'ContainerNo',
            displayExpr: "text",
            valueExpr: "value",
            acceptCustomValue: true,
            searchEnabled: true,
        })
    }

    function getcydata(igm, indexno) {

        console.log("igm", igm);
    console.log("indexno", indexno);
    if (igm, indexno) {
        $.post('/Import/Setup/GetExaminationChargeAssignCY?igm=' + igm + '&index=' + indexno, function (data) {


            console.log(" cy data ", data);

            loadGrid(data)

        });
        }
    else {

        loadGrid([]);

        }

    }



    $(function () {

        $("#cnic").inputmask("99999-9999999-9");

    //limitWeightCBMValues("Weight");
    //limitWeightCBMValues("CBM");

    $.get('/Import/Setup/GetExaminationCharges', function (data) {
        console.log(data)
            examinationCharges = data;
        });
    });


    function restvalues() {
        $('#principal').val('');
    $('#line').val('');
    $('#consigneId').val('');
    $('#consignee').val('');
    $('#consigneNTN').val('');
    $('#stRegistrationNo').val('');
    $('#commodity').val('');
    $('#blnumber').val('');
    $('#igmDetilCBM').val('');
    $('#destuffCBM').val('');
    $('#examinationRequestCBM').val('');
    $('#lineDoNumber').val('');
    $('#agents').select2().val('').trigger('change.select2');
    $('#cnic').val('');
    $('#repname').val('');
    $('#phonenumber').val('');
    $('#becashNo').val('');
    $('#ppresentationDate').val('');
    $('#customRegNo').val('');
    $('#ccustomRegDate').val(new Date().toISOString().substr(0, 10));
    $('#ccustomOutDate').val('');
    $('#examinationDate').val('');
    $('#groundingDate').val('');
    $('#email').val('');
    $('#deliveryStatus').val('');

    document.getElementById("isTPCargo").checked = false;
    document.getElementById("isEPZCargo").checked = false;
    loadContainerGrid([]);

    }

    function enterCNICNumebr() {


        var val = $('#cnic').val();
    var clearingagentid =  $('#agents').val();

    console.log("val", val)
    console.log("clearingagentid", clearingagentid)

    if (val) {
        $.get('/APICalls/GetExaminationRequestByClearingAgnetAndCNIC?val=' + val + '&clearingagentid=' + clearingagentid, function (data) {
            if (data) {

                $('#phonenumber').val(data.phoneNumber);
                $('#repname').val(data.clearingAgentRep);
            }
            else {

                $('#phonenumber').val('');
                $('#repname').val('');
            }

        });
        }
         
    }


    function showFilters() {

        restvalues();

    type = $("input[name='type']:checked").val();


    $.get('/APICalls/GetFilterIndexWise?Type=' + type, function (partial) {

        $('#filters').html(partial);

    console.log(" typetype ", type);

    loadGrid([]); 
        })

    if (type == "CFS") {
        document.getElementById('ContainerType').style.display = "block"
    } else {
        document.getElementById('ContainerType').style.display = "none"
    }



    }




    function loadGrid(dataSource) {

        var grid = $("#examinationchargeAsiging").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    var dataGrid = $("#examinationchargeAsiging").dxDataGrid({
        dataSource: dataSource,
    keyExpr: "examinationChargeAssignId",

    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        pageSize: 10
            },
    editing: {
        mode: "row",
    allowDeleting: true,
    allowAdding: true
            },
    columns: [



    {
        dataField: "chargeName",
    caption: "Charge Name",
    width: 200,
    setCellValue: function (rowData, value) {
        rowData.chargeName = value;
                        var t = examinationCharges.find(d => d.examinationChargeName == value);
    console.log(t, "t");
    if (t != null || t != undefined) {
        rowData.chargeAmount20 = t.examinationChargeAmount20;
    rowData.chargeAmount40 = t.examinationChargeAmount40;
    rowData.chargeAmount45 = t.examinationChargeAmount45;
    rowData.examinationChargeId = t.examinationChargeId;

                        }
                    },
    lookup: {
        dataSource: examinationCharges,
    displayExpr: "examinationChargeName",
    valueExpr: "examinationChargeName"
                    },
    validationRules: [{type: "required" }]

                },
    {
        dataField: "chargeName",
    caption: "Name",
    allowEditing: false,

                },

    {
        dataField: "chargeQuantity",
    caption: "Charge Quantity 20",
    dataType: "number",
    validationRules: [{type: "required" }],
    editorOptions: {
        step: 0
                    }

                },

    {
        dataField: "chargeQuantity40",
    caption: "Charge Quantity  40",
    dataType: "number",
    validationRules: [{type: "required" }],
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "chargeQuantity45",
    caption: "Charge Quantity  45",
    dataType: "number",
    validationRules: [{type: "required" }],
    editorOptions: {
        step: 0
                    }

                },
    {
        dataField: "chargeAmount20",
    dataType: "number",
    format: "#,##0.##",
    width: 120,
    caption: "Amount 20",
    allowEditing: false,
                },
    {
        dataField: "chargeAmount40",
    dataType: "number",
    format: "#,##0.##",
    width: 120,
    caption: "Amount 40",
    allowEditing: false,
                },
    {
        dataField: "chargeAmount45",
    dataType: "number",
    format: "#,##0.##",
    width: 120,
    caption: "Amount 45",
    allowEditing: false,
                },


    {
        dataField: "examinationChargeId",
    width: 120,
    caption: "Examination Code",
    allowEditing: false,
                },


    "remarks",


    ],

    summary: {
        totalItems: [

    {
        column: "chargeName",
    summaryType: "count",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "chargeQuantity",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "chargeQuantity40",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "chargeQuantity45",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "chargeAmount20",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "chargeAmount40",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },
    {
        column: "chargeAmount45",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },
                    },

    ]
            },
    onRowInserted: function (e) {

        console.log(e)
                console.log(e.data)
    var data = e.data;

    if (type == "CFS") {

        showToast("olny for cy", "error");


    var indexno = $("#containerIndexCYdb option:selected").val();

    getcfsdata();
                }
    if (type == "CY") {

                    var indexno = $("#containerIndexCYdb option:selected").val();

    console.log("igm", igm);
    console.log("indexno", indexno);

    if (data) {
                        var examinationChargeId =  data.examinationChargeId;
    if (igm, indexno) {
        $.post('/Import/Setup/AddExaminationChargeAssign?type=' + type + '&igm=' + igm + '&index=' + indexno + '&examinationChargeId=' + examinationChargeId, { data, data }, function (result) {

            console.log("result", result)
            if (result.error) {
                showToast(result.message, "warning");
                getcydata(igm, indexno)
            }
            else {
                showToast(result.message, "success");
                getcydata(igm, indexno)
            }
        });
                        }
                    }

                   

                }

            },

    onRowRemoved: function (e) {

        console.log(e)
                console.log(e.data)
    var data = e.data.examinationChargeAssignId;

    $.post('/Import/Setup/DeleteexaminationChargeAssigned?key=' + data, function (result) {

        console.log("result", result)
                    if (result.error) {
        showToast(result.message, "warning");
                    }

    else {

        showToast(result.message, "success");

                    }

                });



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

    else if (event.keyCode === 189) {
                    if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                    }

                }
            });


        });
    }


    function GetExaminationRequestdata(type, cid, igm, index) {


        console.log("type", type)
        console.log("cid", cid)
    console.log("igm", igm)
    console.log("index", index)

    var url = "";

    if (type == "CFS") {
        url = '/Invoice/GetExaminationRequestdata?containerId=' + cid;
        }
    else {
        url = '/Invoice/GetExaminationRequestdatacy?igm=' + igm + '&indexno=' + index;

        }

    $.get(url, function (data) {


        console.log("data", data)
            if (data) {

        $('#commodity').val(data.commodity);
    $('#consigneId').val(data.consigneId);
    $('#consignee').val(data.consignee);
    $('#consigneNTN').val(data.consigneNTN);
    $('#stRegistrationNo').val(data.stRegistrationNo);
    $('#blnumber').val(data.blNumber);
    $('#line').val(data.line);
    $('#principal').val(data.principal);
    $('#igmDetilCBM').val(data.igmDetilCBM);
    $('#destuffCBM').val(data.destuffCBM);

    $('#examinationRequestCBM').val(data.examinationRequestCBM);
    $('#lineDoNumber').val(data.lineDoNumber);
    // $('#agents').val(data.clearingAgentId);
    $('#agents').val(data.clearingAgentId).trigger('change.select2');

    $('#cnic').val(data.cnic);
    $('#repname').val(data.clearingAgentRep);
    $('#phonenumber').val(data.phoneNumber);
    $('#becashNo').val(data.beCashNo);
    $('#email').val(data.email);


    if (data.presentationDate != null) {
                    if (data.presentationDate == "0001-01-01T00:00:00") {
        $('#ppresentationDate').val('');
                    }
    else {
        $('#ppresentationDate').val(new Date(data.presentationDate.split("T")[0]).toISOString().substr(0, 10));
                    }
                    
                } else {
        $('#ppresentationDate').val('');
                }


    $('#customRegNo').val(data.customRegNo);

    if (data.customOutChargeDate != null) {

        $('#ccustomOutDate').val(new Date(data.customOutChargeDate.split("T")[0]).toISOString().substr(0, 10));
                } else {
        $('#ccustomOutDate').val(new Date().toISOString().substr(0, 10));
                }

    if (data.customRegDate != null) {


                    if (data.customRegDate == "0001-01-01T00:00:00") {
        $('#ccustomRegDate').val('');
                    }
    else {
        $('#ccustomRegDate').val(new Date(data.customRegDate.split("T")[0]).toISOString().substr(0, 10));
                    }

                 } else {
        $('#ccustomRegDate').val('');
                }


    if (data.examinationDate != null) {

        $('#examinationDate').val(new Date(data.examinationDate.split("T")[0]).toISOString().substr(0, 10));
                } else {
        $('#examinationDate').val('');
                }

    if (data.groundingDate != null) {

        $('#groundingDate').val(new Date(data.groundingDate.split("T")[0]).toISOString().substr(0, 10));
                } else {
        $('#groundingDate').val('');
                }


    if (data.isTPCargo == true) {
        document.getElementById("isTPCargo").checked = true;
    console.log("etst")

                }
    else {
        document.getElementById("isTPCargo").checked = false;
    console.log("test")
                }
    if (data.isEPZCargo == true) {
        document.getElementById("isEPZCargo").checked = true;
    console.log("etst")

                }
    else {
        document.getElementById("isEPZCargo").checked = false;
    console.log("test")
                }
    $('#deliveryStatus').val(data.deliveryStatus);



                 

                
                
            } else {

        $('#principal').val('');
    $('#line').val('');
    $('#consigneId').val('');
    $('#consignee').val('');
    $('#consigneNTN').val('');
    $('#stRegistrationNo').val('');
    $('#blnumber').val('');
    $('#commodity').val('');

    $('#igmDetilCBM').val('');
    $('#destuffCBM').val('');


    $('#examinationRequestCBM').val('');
    $('#lineDoNumber').val('');
    //$('#agents').val('');
    $('#agents').select2().val('').trigger('change.select2');
    $('#cnic').val('');
    $('#repname').val('');
    $('#phonenumber').val('');
    $('#becashNo').val('');
    $('#email').val('');
    $('#ppresentationDate').val('');
    $('#customRegNo').val('');
    $('#ccustomRegDate').val(new Date().toISOString().substr(0, 10));
    $('#ccustomOutDate').val('');
    $('#examinationDate').val('');
    $('#groundingDate').val('');

    document.getElementById("isTPCargo").checked = false;
    document.getElementById("isEPZCargo").checked = false;
    $('#deliveryStatus').val('');

            }

        });

         

    }





    function formfiled() {

        $('#agents').on('change', function (e) {
            enterCNICNumebr();

        });


    }


    function timer() {
        setTimeout(pageRelod, 3000);

    }

    function pageRelod() {
        window.location.href = window.location.href;
    }


    function submitExaminationRequest() {
         
        if (type) {
            var form = document.getElementById('ExaminationRequestForm');

    var model = $('#ExaminationRequestForm').serialize();


    console.log("model", model)
    console.log("form.reportValidity", form.reportValidity)

    form.reportValidity();

    if (form.checkValidity()) {


                if (type == "CFS") {

                    var containerid = $('#indexdb').val();

    var igmDetilCBM = $('#igmDetilCBM').val();
    var destuffCBM = $('#destuffCBM').val();

    console.log("containerid", containerid)

    if (containerid) {


        console.log("ok cfs");

    $.post('/Invoice/SaveExaminationRequest?' + model + '&type=' + type + "&containerid=" + containerid + "&igmDetilCBM=" + igmDetilCBM + "&destuffCBM=" + destuffCBM, function (data) {

                            if (data.error == true) {

        showToast(data.message, "warning");

                            } else {
        showToast(data.message, "success");
                                 

                            }
                        });


                    } else {
        showToast("please select ContainerIndex", "error")

    }



                }
    if (type == "CY") {


                    var indexno = $('#containerIndexCYdb').val();

    console.log("containerid", containerid)
    if (!igm) {

        alert("Select IGM");
                    }

    else {
                        if (indexno) {


        console.log("ok cy");



    $.post('/Invoice/SaveExaminationRequestCY?' + model + '&igm=' + igm + '&indexno=' + indexno, function (data) {

                                if (data.error == true) {

        showToast(data.message, "warning");

                                } else {
        showToast(data.message, "success");

                                }
                            });


                        } else {
        showToast("please select ContainerIndex", "error")

    }
                    }



                }


            }

        }
    else {

        showToast("please select type", "error")
    }





    }



    function isNumberKey(txt, evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 46) {
            //Check if the text already contains the . character
            if (txt.value.indexOf('.') === -1) {
                return true;
            } else {
                return false;
            }
        } else {
            if (charCode > 31 &&
    (charCode < 48 || charCode > 57))
    return false;
        }
    return true;
    }

    function updatentnandstn() {
        var consigneId = $('#consigneId').val();
    var consigneNTN = $('#consigneNTN').val();
    var stRegistrationNo = $('#stRegistrationNo').val();


    if (!consigneId) {
            return showToast("no consignee code found","error")
        }

    if (!consigneNTN) {
            return showToast("please add consignee ntn", "error")
        }
    if (!stRegistrationNo) {
            return showToast("please add St registration no", "error")
        }

    if (consigneId && consigneNTN && stRegistrationNo) {
        $.post('/Invoice/UpdateConsigneeInfo?consigneId=' + consigneId + '&consigneNTN=' + consigneNTN + '&stRegistrationNo=' + stRegistrationNo, function (data) {

            if (data.error == true) {

                showToast(data.message, "warning");

            } else {
                showToast(data.message, "success");

            }
        });
        }
     


    }
