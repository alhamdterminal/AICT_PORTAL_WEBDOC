

    function loadingBar() {
        $("#large-indicator").dxLoadIndicator({
            height: 60,
            width: 60
        });
    }
    function loadingBarHide() {
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }

    function containerChangeCallback() {

    }


    function formfiled() {

    }


    function printReport() {





        var type = $("#type option:selected").val();

    if (type == "CFSIndexWise") {
            var index = $("#indexdb option:selected").text();

        }

    if (type == "CYContainers") {
            var index = $("#containerIndexCYdb option:selected").text();

        }



    console.log("igm", igm)
    console.log("index", index);

    loadingBar();

    $.get('/Import/Reports/ViewFormBAuction?igm=' + igm + '&index=' + index + '&type=' + type, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });


    }


    function showFilters() {



        type = $("#type option:selected").val();


    console.log("type", type)

    $.get('/APICalls/GetFilterForFormB?Type=' + type, function (partial) {

        $('#filters').html(partial);



        });


    }

    function containerCallback() {

    }
