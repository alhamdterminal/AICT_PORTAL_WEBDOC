

    var type;

    function showFilters() {

        type = $("input[name='type']:checked").val();

    $.get('/APICalls/GetFilterIndexWise?Type=' + type, function (partial) {

        $('#filters').html(partial);

    console.log(" typetype ", type);

    loadGrid([])

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

    function containerCallback() {

        console.log('cy')

        var indexno = $("#containerIndexCYdb option:selected").val();

    console.log("igm", igm);
    console.log("indexno", indexno);

    getdata(igm, indexno);
    }


    function containerChangeCallback() {
        console.log("CFS")

        var indexno = $("#indexdb option:selected").text();

    console.log("indexno", indexno);
    getdata(igm, indexno);
    }

    function getdata(igm, indexno) {

        console.log("igm", igm);
    console.log("indexno", indexno);
    if (igm, indexno) {
        $.post('/Import/Hold/GetHoldData?igm=' + igm + '&indexno=' + indexno, function (data) {

            console.log("data", data)
            loadGrid(data);
        });
        }
    else {
        loadGrid([]);
        }

    }



    function loadGrid(dataSource) {
         
        var dataGrid = $("#holdgrid").dxDataGrid({
        dataSource: dataSource,
    keyExpr: "holdId",

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
    allowUpdating: function (e) {

                    if (e.row.data.isHold == false) {
                        return false;
                    }
    else {
                        return true;
                    }
                },
    allowDeleting: false,
    allowAdding: false
            },
    columns: [

    {
        dataField: "auctionLotNo",
    caption: "Auction Lot No",
    allowEditing: false,
                },
    {
        dataField: "holdDate",
    caption: "Hold Date",
    allowEditing: false,
    dataType: 'date',
    format: 'dd/MM/yyyy',
    allowEditing: false,
                },
    {
        dataField: "holdType",
    caption: "hold Type",
    allowEditing: false,
                },
    {
        dataField: "specialInstructions",
    caption: "special Instructions",
    allowEditing: false,
                },
    {
        dataField: "role",
    caption: "Role",
    allowEditing: false,
                },
    {
        dataField: "isHold",
    caption: "Hold Status",
    allowEditing: false,
                },
    {
        dataField: "ro",
    caption: "RO Number",
    validationRules: [{
        type: "custom",
    validationCallback: function (e, s) {
        console.log(e)
                            console.log(s)
    if (e.data.auctionLotNo != null && (e.data.ro == null || e.data.ro == "")) {
                                return false;
                            }
    else {
                                return true;
                            }


                        }
                    }],
 
                },
    {
        dataField: "removeInstruction",
    caption: "Remove Instruction",
    validationRules: [{
        type : "required",
    message : "Remove Instruction Is Required"
                    }],

                },

    ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
            },
    onRowUpdated: function (e) {
                
                var values = e.data;

    console.log("data", values);

    $.post('/Import/Hold/ReleasedHold', {values, values}, function (result) {
        console.log("result", result)
                    if (result.error) {
        showToast(result.message, "warning");

    getdata(values.virNo, values.indexNo);
                    }

    else {

        showToast(result.message, "success");
    getdata(values.virNo, values.indexNo);
                    }
                     
                });
            },
            

        }).dxDataGrid("instance");


      
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "ro");

    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    console.log("e", e)
    console.log("field", field)

    if (e.row.data.auctionLotNo == null) {

        e.editorOptions.disabled = true;

        }
        
    }


    function submitHold() {
         
        var specialInstruction = HoldForm.elements["SpecialInstructions"].value;

    if (!specialInstruction) {
        showToast("Please Enter Special Instruction", "error");

    return;
        }

    if (type == "CFS") {
            var values = $('#HoldForm').serialize();
    console.log(values)

    var indexno = $("#indexdb option:selected").text();

    console.log("igm", igm);
    console.log("indexno", indexno);

    $.post("/Import/Hold/CreateHoldIndex?values" + values + "&igm=" + igm + "&indexno=" + indexno + "&type=" + "CFS" , function (data) {

        showToast(data.message, "success");

    getdata(igm, indexno);

                })
        }

    else {
            var values = $('#HoldForm').serialize();

    var indexno = $("#containerIndexCYdb option:selected").val();


    console.log("indexno", indexno);

    $.post("/Import/Hold/CreateHoldIndex?" + values + "&igm=" + igm + "&indexno=" + indexno + "&type=" + "CY", function (data) {

        showToast(data.message, "success");

    getdata(igm, indexno);
            });
        }
    }

    function releasedHold() {

        if ($('#AuctionNo').val() == '') {
        showToast("Auction Lot input is required", "error");
    //alert('AuctionNo input is required');

    return;
        }


    if ($('#RO').val() == '') {

        showToast("RO input is required", "error");
    //alert('RO input is required');

    return;
        }


    if (type == "CFS") {
            var values = $('#HoldForm').serialize();
    console.log(values)
    var ContainwerindexNumber = $("#indexdb option:selected").text();
    var blNumber = $("#blnumberdb option:selected").text();

    $.post("/Import/Hold/ReleasedHoldCFS?" + values + "&blNumber=" + blNumber + "&indexNo=" + ContainwerindexNumber + "&igm=" + igm, function (data) {
                //showToast(data.message, "success");
                if (data.error) {
        showToast(data.message, "error");

                }
    else {
        showToast(data.message, "success");
                }
            })
        }
    else {
            var values = $('#HoldForm').serialize();
    console.log(values)

    var ContainerNumber = $('#containerCYdb').text();
    ContainerNumber = ContainerNumber.split('    ').join('');

    $.post("/Import/Hold/ReleasedHoldCY?" + values + "&igm=" + igm + "&indexNo=" + $("#containerIndexCYdb option:selected").val()
    + "&ContainerNumber=" + ContainerNumber, function (data) {

        showToast(data.message, "success");
                })
        }



    }

    function removeHold() {


        if (type == "CFS") {

            var ContainwerindexNumber = $("#indexdb option:selected").text();
    var ContainwerindexId = $("#indexdb option:selected").val();
    var blNumber = $("#blnumberdb option:selected").text();
    console.log("igm", igm);
    console.log("ContainwerindexNumber", ContainwerindexNumber);
    console.log("ContainwerindexId", ContainwerindexId);
    console.log("blNumber", blNumber);

    $.post("/Import/Hold/RemoveHoldCFS?containerIndexId=" + containerIndexId + "&blNumber=" + blNumber
    + "&indexNo=" + ContainwerindexNumber + "&igm=" + igm, function (data) {

                    if (data.error) {
        showToast(data.message, "error");

                    }
    else {
        showToast(data.message, "success");
                    }

                })
        }
    else {

            var ContainerNumber = $('#containerCYdb').text();
    ContainerNumber = ContainerNumber.split('    ').join('');
    $.post("/Import/Hold/RemoveHoldCYContainer?igm=" + igm + "&indexNo=" + $("#containerIndexCYdb option:selected").val() + "&RO=" + $('#RO').val()
    + "&ContainerNumber=" + ContainerNumber, function (data) {
        showToast(data.message, "success");
                })
        }

    }

    function formfiled() {

    }


    function releasededoHold() {
         

        if (type == "CFS") {


            
            var indexno = $("#indexdb option:selected").text();

    console.log("indexno", indexno)
    console.log("igm", igm)
    var remarks = $("#remarks").val();

    if (igm && indexno) {
        $.post("/Import/Hold/RemoveDOHold?remarks=" + remarks + "&igm=" + igm + "&indexno=" + indexno, function (data) {
            //showToast(data.message, "success");
            if (data.error) {
                showToast(data.message, "error");

            }
            else {
                showToast(data.message, "success");
            }
        })
    }
    else {
        showToast("please select index", "error");
            }


             
            
        }
    else {
        showToast("only for cfs", "error");
        }



    }


