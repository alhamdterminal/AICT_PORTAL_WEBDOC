

    var type;

    function showFilters() {

        $('#freedays').val(0);

    type = $("input[name='type']:checked").val();

    $.get('/APICalls/GetFilterIndexWise?Type=' + type, function (partial) {

        $('#filters').html(partial);

    console.log(" typetype ", type);

    $('#freedays').val(0);

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

        $('#freedays').val(0);

    var indexno = $("#containerIndexCYdb option:selected").val();


    getdata(igm, indexno)
         
    }



    function containerChangeCallback() {

        $('#freedays').val(0);

    var indexno = $("#indexdb option:selected").text();

    getdata(igm, indexno)

    }



    function getdata(igm, indexno) {

        console.log("igm", igm);

    console.log("indexno", indexno);

    $.get('/APICalls/GetStorageHolidays?igm=' + igm + '&index=' + indexno, function (data) {

        console.log("data", data);

    $('#freedays').val(data);

        })

    }






    function formfiled() {

        $('#freedays').val(0);

    }




    function UpdateDays() {


        var freedays = $('#freedays').val();

    console.log("type", type)

    if (type == "CFS") {

            var indexno = $("#indexdb option:selected").text();

    console.log("igm", igm);

    console.log("indexno", indexno);

    if (igm && indexno) {

        $.post('/Tariff/AddStorageHolidays?igm=' + igm + '&indexno=' + indexno + '&noofdays=' + freedays, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");
                getdata(igm, indexno);
            }

        });

            } else {
        showToast("please select igm index", "error");
            }


        }

    if (type == "CY") {

            var indexno = $("#containerIndexCYdb option:selected").val();

    console.log("igm", igm);

    console.log("indexno", indexno);

    if (igm && indexno) {

        $.post('/Tariff/AddStorageHolidays?igm=' + igm + '&indexno=' + indexno + '&noofdays=' + freedays, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");

            } else {
                showToast(data.message, "success");
                getdata(igm, indexno);
            }

        });

            } else {
        showToast("please select igm index", "error");
            }

        }

    }





    function refresh() {
        window.location.reload();
    }



