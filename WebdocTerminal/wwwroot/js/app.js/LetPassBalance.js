

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

    document.onload = $(function () {
        //var url_string = window.location.href
        //var url = new URL(url_string);

        loadingBar();

    $.get('/Import/Reports/ViewLetPassBalance', function (data) {

        loadingBarHide();

    $('#repotdata').html(data);

        });


        //$("#termsPaneliFrame").attr("src", "/Import/Reports/Print");
        //var frameElement = document.getElementById("FrameToPrint");
        //    if (frameElement.contentDocument.contentType !== "text/html")
        //        frameElement.contentWindow.print();


        //$.get('/Import/Reports/Print', function (data) {
        //    $("#FrameToPrint").append(data);
        //    //$('#FrameToPrint').html(data);
        //    var frameElement = document.getElementById("FrameToPrint");
        //    /*frameElement.addEventListener("load", function (e) {* /
        //        if (frameElement.contentDocument.contentType !== "text/html")
        //            frameElement.contentWindow.print();
        //    //});
        //}); 

       


    //         @*window.open('@Url.Action("Print", "Reports")', "PrintingFrame");
    //var frameElement = document.getElementById("FrameToPrint");
    //frameElement.addEventListener("load", function (e) {
    //            if (frameElement.contentDocument.contentType !== "text/html")
    //frameElement.contentWindow.print();
    //        });*@

    });

    function formfiled() {

    }