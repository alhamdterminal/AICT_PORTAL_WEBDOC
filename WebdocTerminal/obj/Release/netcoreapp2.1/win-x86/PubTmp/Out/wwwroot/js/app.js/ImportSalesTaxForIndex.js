

    var type;

    function showFilters() {

        type = $("input[name='type']:checked").val();

    $.get('/APICalls/GetFiltersForHold?Type=' + type, function (partial) {

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

    function containerCallback() {
        if (type == "CFS") {
        //containerIndexId = $("#indexdb option:selected").val();
        changeContainerIndexNumber();
        }

    if (type == "CY") {

        changeContainerNumberCY();
        }

    }

    function changeContainerIndexNumber() {
        var containerIndexId = $("#indexdb option:selected").val();

    $.get('/APICalls/GetCFSSalesTaxIndex?containerIndexId=' + containerIndexId, function (data) {

        console.log("IndexValue", data);
    if (data) {
        console.log(data.salesTaxAmount)
                    $('#salesTaxAmount').val(data.salesTaxAmount);
    updateBtnShowHide(true);
    submitBtnShowHide(false);
                }
    else {
        $('#salesTaxAmount').val('');
    updateBtnShowHide(false);
    submitBtnShowHide(true);
                }

            });
      

    }


    function changeContainerNumberCY() {
        var cycontainerid = $("#containerCYdb option:selected").val();

    $.get('/APICalls/GetCYSalesTaxIndex?cycontainerid=' + cycontainerid, function (data) {

        console.log("IndexValue", data);
    if (data) {

        $('#salesTaxAmount').val(data.salesTaxAmount);
    updateBtnShowHide(true);
    submitBtnShowHide(false);
            }
    else {
        $('#salesTaxAmount').val('');
    updateBtnShowHide(false);
    submitBtnShowHide(true);
            }

        });

    }



    function updateBtnShowHide(show) {
        var btn = document.getElementById("btnSubmitUpdate");
    if (show)
    btn.style.display = 'inline-grid';
    else
    btn.style.display = 'none';
    }

    function submitBtnShowHide(show) {
        var btn = document.getElementById("btnSubmit");
    if (show)
    btn.style.display = 'inline-grid';
    else
    btn.style.display = 'none';
    }

    function AddIndexSalesTax() {

        console.log("type", type)

        if (type == "CFS") {

        console.log("CFS", type)

            var containerIndexId = $("#indexdb option:selected").val();

    if (containerIndexId) {
                var salesTaxAmount = $('#salesTaxAmount').val();


    $.post('/SalesTax/SaleTaxIndexForCFS?ContainerIndexId=' + containerIndexId + '&salesTaxAmount=' + salesTaxAmount, function (data) {

        console.log(data);


    showToast(data.message, "success");

    window.location.href = window.location.href;


                });
            }
    else {
        showToast("Index Not Found", "error");
    window.location.href = window.location.href;

            }

       


        }

    if (type == "CY") {

        console.log("CY", type)

            var cycontainerid = $("#containerCYdb option:selected").val();

    if (cycontainerid) {
                var salesTaxAmount = $('#salesTaxAmount').val();


    $.post('/SalesTax/SaleTaxIndexForCY?cycontainerid=' + cycontainerid + '&salesTaxAmount=' + salesTaxAmount, function (data) {

        console.log(data);


    showToast(data.message, "success");

    window.location.href = window.location.href;


                });
            }
    else {
        showToast("Container Not Found", "error");
    window.location.href = window.location.href;

            }
           

        }



    }

    function updateIndexSalesTax() {
        console.log("type", type)
        console.log("type", type)

    if (type == "CFS") {

        console.log("CFS", type)

            var salesTaxAmount = $('#salesTaxAmount').val();

    var containerIndexId = $("#indexdb option:selected").val();

    if (containerIndexId) {

        $.post('/SalesTax/UpdateSaleTaxIndexForCFS?ContainerIndexId=' + containerIndexId + '&salesTaxAmount=' + salesTaxAmount, function (data) {

            console.log(data);
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
        showToast("Index Not Found", "error");
    window.location.href = window.location.href;

            }



        }

    if (type == "CY") {

        console.log("CY", type)



            var cycontainerid = $("#containerCYdb option:selected").val();
    if (cycontainerid) {
                var salesTaxAmount = $('#salesTaxAmount').val();


    $.post('/SalesTax/UpdateSaleTaxIndexForCY?CYContainerId=' + cycontainerid + '&salesTaxAmount=' + salesTaxAmount, function (data) {

        console.log(data);

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
        showToast("Container Not Found", "error");
    window.location.href = window.location.href;

            }
         
        }


    }








    function containerChangeCallback(ornt) {
        if (ornt == "index") {
        containerIndexId = $("#indexdb option:selected").val();
    changeContainerIndexNumber();
        }
    }


    function formfiled() {

    }
