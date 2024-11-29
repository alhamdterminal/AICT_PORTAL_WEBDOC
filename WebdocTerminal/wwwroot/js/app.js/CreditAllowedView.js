



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function formfiled() {

    }

    var igm;



    function igm_changed(data) {

        igm = data.value;

    resetformvalues();

    $.get('/APICalls/GetIGMOIndexDropdown?IGM=' + igm, function (indexdb) {

        $('#indexdiv').html(indexdb);
             
        });

    }


    function resetformvalues() {

        $('#ff').val('')
        $('#consignee').val('')
    $('#clearingagent').val('')
    $('#cbm').val('')
    $('#weight').val('')
    $('#rate').val('')
    $('#storage').val('')
    $('#totalcharges').val('')
    $('#creditAllowedRs').val('')
    $('#creditDays').val('')
    $('#remarks').val('')

    $('#invoiceNo').val('')
    $('#createdDate').val('')

    }

    function showdetail() {

        resetformvalues();
    var indexnumber = $("#indexdb option:selected").text();

    console.log("igm", igm)
    console.log("index no ", indexnumber)

    if (igm && indexnumber) {

        $.get('/APICAlls/GetCreditAllowInfo?igm=' + igm + "&index=" + indexnumber, function (data) {

            console.log("data", data);

            if (data) {
                $('#ff').val(data.ff)
                $('#consignee').val(data.consignee)
                $('#clearingagent').val(data.clearingAgent)
                $('#cbm').val(data.cbm)
                $('#weight').val(data.weight)
                $('#rate').val(data.ffAictShare)
                $('#storage').val(data.storage)
                $('#totalcharges').val(data.totalCharges)
                $('#otherCharges').val(data.otherCharges)

                $('#creditAllowedRs').val(data.creditAllowedRs)
                $('#creditDays').val(data.creditDays)
                $('#remarks').val(data.remarks)

                $('#invoiceNo').val(data.invoiceNo)

                if (data.createdDate != null) {

                    $('#createdDate').val(new Date(data.createdDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#createdDate').val('');
                }


            } else {
                resetformvalues();
            }

        });


        } else {
        showToast("please select igm and index", "error")
    }

    }


    function SaveDetail() {
       
        var indexnumber = $("#indexdb option:selected").text();

    var creditAlloRs = $("#creditAllowedRs").val();
    var creditdys = $("#creditDays").val();
    var remark = $("#remarks").val();

    console.log("creditAlloRs", creditAlloRs);
    console.log("creditdys", creditdys);
    console.log("remark", remark);

    if (!creditAlloRs || Number(creditAlloRs) === 0) {
            return showToast("please add amount", "error")
        }
    if (!creditdys || Number(creditdys) === 0) {
            return showToast("please add days", "error")
        }
    if (!remark) {
            return showToast("please add remark", "error")
        }

    console.log("igm", igm);

    console.log("index no ", indexnumber);


    if (igm && indexnumber) {


            var form = document.getElementById('CreditAllowedForm');

    var model = $('#CreditAllowedForm').serialize();


    console.log("model", model)
    console.log("form.reportValidity", form.reportValidity)

    form.reportValidity();


    if (form.checkValidity()) {


        console.log("ok");


    let data = {

        virNumber: igm,
    indexNumber: indexnumber,
    creditAllowedRs: creditAlloRs,
    creditDays: creditdys,
    remarks: remark,


                };

    console.log("dara" ,  data)

    $.post('/Import/Setup/AddCreditAllowed' , {data: data} , function (data) {

                    if (data.error == true) {
        showToast(data.message, "error");

                    }
    else {
        showToast(data.message, "success");
                    }
                    //showdetail();
                })

            }


        } else {
        showToast("please select igm and index", "error")
    }



    }

