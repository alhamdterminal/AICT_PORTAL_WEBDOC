

    var type;

    $(function () {

        $('#blnumberdiv').on('change', function (val) {

            console.log("change bl")
            // containerChangeCallback("index");
            var blNumbr = $("#blnumberdb option:selected").text();

            var indexnumber = $("#indexdb option:selected").text();


            changeBLNumberConsigneenameCFS(indexnumber, blNumbr, igm)
        });




    });


    function showFilters() {

        type = $("input[name='type']:checked").val();
    console.log("typetypetypetypetype", type);

    if (type == "CFS") {
        document.getElementById('blnodropDown').style.display = "block";
        }
    else {
        document.getElementById('blnodropDown').style.display = "none";
        }


    document.getElementById("DelivryorderForm").reset();


    $.get('/APICalls/GetFiltersForDeliveryOrder?Type=' + type, function (partial) {

        $('#filters').html(partial);

        }) 

    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function containerChangeCallback(ornt) {
        console.log("ContainerIndex")
        indexnumber = $("#indexdb").text();

        if (PermissionInputs.find(x => x.fieldName == "ContainerIndexId" && x.isChecked == false)) {

        document.getElementById('indexdb').disabled = true;;

        }
    else {

        getIndexInvoicedata();

    getBlNumber(igm, $("#indexdb option:selected").text());
        }

    }

    function getIndexInvoicedata() {

        console.log("$('#indexdb').val()", $('#indexdb').val())
        $.get('/APICalls/GetContainerInvoice?ContainerIndexId=' + $('#indexdb').val(), function (invoice) {
        console.log(invoice)
            if (invoice) {
        $('#agent').val(invoice.clearingAgentId);
    $('#repname').val(invoice.clearingAgentRep);
    $('#phonenumber').val(invoice.phoneNumber);
    $('#cnic').val(invoice.cnic);
            }
        });


    $('#btnSubmit').attr("disabled", false);
    }


    function getBlNumber(igm, Index) {
        console.log("Index", Index);
    console.log("igm", igm);
    $.get('/APICalls/GetBLNumberByIGMAndIndex?indexnumber=' + Index + "&IGM=" + igm, function (respBLnumbers) {


        console.log("respBLnumbers", respBLnumbers)

            $('#blnumberdiv').html(respBLnumbers);

    var blNumbr = $("#blnumberdb option:selected").text();
    if (blNumbr) {
        changeBLNumberConsigneenameCFS(Index, blNumbr, igm)
    }
            
        });
    }


    function containerCallback() {
        console.log("Container")
        
     
        indexnumber = $("#containerIndexCYdb option:selected").text();
    containernumber = $("#containerCYdb option:selected").text();
        if (PermissionInputs.find(x => x.fieldName == "CYContainerId" && x.isChecked == false)) {

        document.getElementById('containerCYdb').disabled = true;;

        } else {
        $.get('/APICalls/GetCYContainerInvoice?CYContainerId=' + $("#containerCYdb option:selected").val(), function (invoice) {

            if (invoice) {
                $('#agent').val(invoice.clearingAgentId);
                $('#repname').val(invoice.clearingAgentRep);
                $('#phonenumber').val(invoice.phoneNumber);
                $('#cnic').val(invoice.cnic);
            }

        });
    changeBLNumberConsigneenameCY(indexnumber, containernumber , igm);

    $('#btnSubmit').attr("disabled", false);
        }

        


        
    }

    function changeBLNumberConsigneenameCY(indexnumber, containernumber, igm) {

        var blNumberData = { };
    console.log("igm", igm);
    $.get('/APICalls/changeBLNumberConsigneenameCY?indexnumber=' + indexnumber + '&containernumber=' + containernumber+ '&igm=' + igm, function (data) {
            if (data) {
        console.log(data)
                blNumberData = data;
    $('#blnumber').val(data.blNumber);
    $('#consigneename').val(data.consigneeName);
    $('#consigneentn').val(data.consigneeNTN);
    $('#doNumber').val(data.doNumber);
    $('#gdNumber').val(data.gdNumber);
    $('#dataStatus').val(data.status);
    if (data.documentCode != null) {
        getdocumentCodeList(data.documentCode);
                }

    if (data.documentCode == null) {
                    var request = "000"
    getdocumentCodeList();
                }

            }
    else {
        $('#blnumber').val('');
    $('#consigneename').val('');
    $('#consigneentn').val('');
    $('#doNumber').val('');
    $('#gdNumber').val('');
    $('#dataStatus').val('');
            }

            


        });
    }

    function changeBLNumberConsigneenameCFS(Index, blNumbr, igm) {

        var blNumberData = { };
    console.log("igm", igm);
    $.get('/APICalls/changeBLNumberConsigneenameCFS?Index=' + Index + '&blNumbr=' + blNumbr + '&igm=' + igm, function (data) {
            if (data) {
        console.log(data)
                blNumberData = data;
    $('#blnumber').val(data.blNumber);
    $('#consigneename').val(data.consigneeName);
    $('#consigneentn').val(data.consigneeNTN);
    $('#doNumber').val(data.doNumber);
    $('#gdNumber').val(data.gdNumber);
    $('#dataStatus').val(data.status);

    if (data.documentCode != null) {
        getdocumentCodeList(data.documentCode);
                }

    if (data.documentCode == null) {
                    var request = "000"
    getdocumentCodeList();
                }

            }
    else {
        $('#blnumber').val('');
    $('#consigneename').val('');
    $('#consigneentn').val('');
    $('#doNumber').val('');
    $('#gdNumber').val('');
    $('#dataStatus').val('');
            }


        });
    }



    var documentListGrid = [];

    function getdocumentCodeList(res) {
        console.log("resresres", res)
        $.get('/APICalls/getDocumentCodeList?res=' + res, function (data) {
        console.log(data);
    documentListGrid = data;
    var dataGrid = $("#documentListGrid").dxDataGrid({
        dataSource: documentListGrid,
    keyExpr: "documentCodeId",
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

        allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click",
    mode: "cell"
                },
    columns: [
    "code",
    "documentName",
    "remarks",
    {
        caption: "Document Received",
    dataField: "isChecked",
    dataType: "boolean"

                    }


    ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
                },
    onRowRemoved: function (e) {

    }

            }).dxDataGrid("instance");



        });
    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }


    function hideMenifestedColumns(e) {

        checkColumn(e, "code");
    checkColumn(e, "documentName");
    }

    var returnOdId;
    var returnDONumber;

    var odFormvalue;
    var blnum;
    var containr;
    var containrindex;
    var vessl;

    function submitOrder() {
        blnum = '';
    containr = '';
    containrindex = '';
    vessl = '';
    var values = $('#DelivryorderForm').serialize();
    blnum = $('#blnumber').val();
    containr = $("#containerdb option:selected").text();
    containrindex = $("#containerIndexdiv option:selected").text();
    vessl = $("#vessel option:selected").text();

    console.log("documentsLisrs", documentListGrid);

    if (documentListGrid) {
            var docList = documentListGrid.find(x => x.isChecked === false);

    console.log("docList docList", docList)
        }


    if (docList) {
        console.log("docList", docList)
            showToast("Please Select This" + docList.code, "error");

        } else {
        console.log("myreasdasdasdasdasdsult ", documentListGrid)
           
            //showToast("Saved", "success");
            $.post("/Import/Delivery/CreateDeliveryOrder?" + values, {documentListGrid: documentListGrid}, function (res) {

            if (res.error == true) {

        showToast(res.message, "error");

    return false;
            }
    else {

        showToast("DO Generated!: #" + res.doNumber, "success");

    if (type == "CFS") {
        window.open('/import/Reports/DeliveryOrder?deliveryOrderNumber=' + res.doNumber, "_blank");
                }
    else {

        window.open('/import/Reports/DeliveryOrderCY?deliveryOrderNumber=' + res.doNumber, "_blank");
                }


            }

    returnDONumber = res.doNumber;

    afterSubmitForm();

        });

        }
        

    }

    function afterSubmitForm() {
        if (type == "CFS") {

            var btn = document.getElementById("gtbtn");
    btn.style.display = 'inline-grid';
    var btnrout = document.getElementById("reportBtn");
    btnrout.style.display = 'inline-grid';


        }
    else {

            var btn = document.getElementById("gtbtnCYcontainer");
    btn.style.display = 'inline-grid';
    var btnrout = document.getElementById("reportBtnCYcontainer");
    btnrout.style.display = 'inline-grid';


        }
    $("select").prop('disabled', true);
    $("input").prop('disabled', true);
    $('#btnSubmit').attr("disabled", true);
    $('#btnSubmit').attr("disabled", true);
    $('#DelivryorderForm').trigger("reset");
    }

    function routeTodeliveryOrderReport() {

        //  top.location.href = '/import/Reports/DeliveryOrder?deliveryOrderNumber=' + returnDONumber;


        window.open('/import/Reports/DeliveryOrder?deliveryOrderNumber=' + returnDONumber, "_blank");
    }

    function routeTodeliveryOrderReportCY() {

        //top.location.href = '/import/Reports/DeliveryOrderCY?deliveryOrderNumber=' + returnDONumber;

        window.open('/import/Reports/DeliveryOrderCY?deliveryOrderNumber=' + returnDONumber, "_blank");
    }

    function routetoGatepass() {

        var dono = returnDONumber;
    window.open('/import/Delivery/OrderDetail?doNumber=' + dono, "_blank");

    window.location.href = window.location.href;

       // top.location.href = '/import/Delivery/OrderDetail?doNumber=' + dono;
    }

    function routetoGatepassCYcontainer() {

        var dono = returnDONumber;
    window.open('/import/Delivery/OrderDetail?doNumber=' + dono, "_blank");

    window.location.href = window.location.href;

     //   top.location.href = '/import/Delivery/OrderDetail?doNumber=' + dono;
    }


    function formfiled() {

         if (PermissionInputs.find(x => x.fieldName == "ContainerType" && x.isChecked == false)) {

        document.getElementById('filters').style.display = "none";
     
        } if (PermissionInputs.find(x => x.fieldName == "Date" && x.isChecked == false)) {

        document.getElementById('single_cal1').disabled = true;
     
        }
        if (PermissionInputs.find(x => x.fieldName == "ClearingAgentId" && x.isChecked == false)) {

        document.getElementById('agent').disabled = true;
     
        }
        if (PermissionInputs.find(x => x.fieldName == "GDNumber" && x.isChecked == false)) {

        document.getElementById('gdNumber').disabled = true;
     
        }
        if (PermissionInputs.find(x => x.fieldName == "Consingee" && x.isChecked == false)) {

        document.getElementById('consigneename').disabled = true;
     
        }
        if (PermissionInputs.find(x => x.fieldName == "ConsigneeNTN" && x.isChecked == false)) {

        document.getElementById('consigneentn').disabled = true;
     
        }
        if (PermissionInputs.find(x => x.fieldName == "SealNo" && x.isChecked == false)) {

        document.getElementById('sealNo').disabled = true;
     
        }
        if (PermissionInputs.find(x => x.fieldName == "CNIC" && x.isChecked == false)) {

        document.getElementById('cnic').disabled = true;
     
        }
        if (PermissionInputs.find(x => x.fieldName == "HoldRelease" && x.isChecked == false)) {

        document.getElementById('holdRelease').disabled = true;
     
        }
        if (PermissionInputs.find(x => x.fieldName == "DetainConfiscate" && x.isChecked == false)) {

        document.getElementById('detainConfiscate').disabled = true;
     
        }
        if (PermissionInputs.find(x => x.fieldName == "NumberConfiscated" && x.isChecked == false)) {

        document.getElementById('numberConfiscated').disabled = true;
     
        }
        if (PermissionInputs.find(x => x.fieldName == "NoOfPackages" && x.isChecked == false)) {

        document.getElementById('noOfPackages').disabled = true;
     
        }
        if (PermissionInputs.find(x => x.fieldName == "ClearingAgentRep" && x.isChecked == false)) {

        document.getElementById('repname').disabled = true;
     
        }
        if (PermissionInputs.find(x => x.fieldName == "PhoneNumber" && x.isChecked == false)) {

        document.getElementById('phonenumber').disabled = true;
     
         }

    }

    /**/