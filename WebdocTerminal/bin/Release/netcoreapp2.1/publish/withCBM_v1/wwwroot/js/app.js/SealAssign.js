

    var batch;


    function containerCallback() {

        var ContainerId = $('#containerCYdb').val();


    GetContainerAssignSealDetail(ContainerId);

    }

    function GetContainerAssignSealDetail(containerid) {

        console.log("containerid", containerid);

    $.get('/Import/Setup/GetContainerAssignSealDetail?containerid=' + containerid, function (res) {

        console.log("res", res);

    sealListGrid(res);



        });
    }

    function sealListGrid(res) {

        $("#butonSubmit").attr("disabled", false);

    console.log("res", res)
    $("#Assignsealdetails").dxDataGrid({
        dataSource: res,
    keyExpr: "sealIssueId",
    showBorders: true,
    hoverStateEnabled: true,
    columnsAutoWidth: true,
    paging: {
        pageSize: 15
            },
    scrolling: {
        columnRenderingMode: "virtual"
            },
    filterRow: {
        visible: true,
    applyFilter: "auto"
            },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."
            },
    editing: {
        mode: "row",
    allowDeleting: true,
            },
    headerfilter: {
        visible: true
            },
    columns: [
    {
        dataField: "batchNumber",
    caption: "Batch",
                },
    {
        dataField: "sealNumber",
    caption: "Seal No",
                },
    {
        dataField: "rate",
    dataType: "number",
                }
    ],

    onRowRemoved: function (e) {
        console.log(e)
  
                var key = e.data.sealIssueId
    $.post('/Import/Setup/DeleteSealAssign?key=' + key, function (result) {

                    if (result.error) {
        showToast(result.message, "warning");
                    }

    else {

        showToast(result.message, "success");


                    }

    getcurrentSeal(e.data.batchNumber)
    GetContainerAssignSealDetail(e.data.cyContainerId)

                })

            }
        });

    }



    function assignSealstoContainer() {

        $("#butonSubmit").attr("disabled", true);

    var ContainerId = $('#containerCYdb').val();
    var currentsealno = $('#currentSealNumber').val();
    console.log("ContainerId", ContainerId);
    console.log("currentsealno ", currentsealno);
    var batch = $("#batch option:selected").val();

    console.log("batch", batch);


        if (batch && ContainerId && currentsealno > 0) {

        console.log("ok batch", batch);

    console.log("ok ContainerId", ContainerId);

    $.post('/Import/Setup/AddSealAssign?cycontainerid=' + ContainerId + '&batchnumber=' + batch, function (result) {
        console.log("result", result)
                if (result.error) {
        showToast(result.message, "warning");
                }

    else {

        showToast(result.message, "success");

                }

    getcurrentSeal(batch)
    GetContainerAssignSealDetail(ContainerId)
                //  window.setInterval('refresh()', 4000);

            });



        } else {
        showToast("please select Container and batch and seal number", "error")
    }

    }


    function refresh() {
        window.location.reload();
    }

    function ShowUnAssignSeals() {



        getcurrentSeal();
    }


    function getcurrentSeal() {

        var batch = $("#batch option:selected").val();


    $.get('/Import/Setup/GetCurrentSeal?batch=' + batch, function (result) {

        console.log("result", result)

            if (result) {
        $('#currentSealNumber').val(result.currentSealNumber);
            }
    else {
        $('#currentSealNumber').val(0);
            }
        });


    }

    function formfiled() {


    }


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

