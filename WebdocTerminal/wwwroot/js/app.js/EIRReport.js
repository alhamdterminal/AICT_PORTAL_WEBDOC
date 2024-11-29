







    function myFunction() { 

        var igm = $("#virnolist").dxAutocomplete("instance").option("value");
    var Containerno = $("#containerlist").dxAutocomplete("instance").option("value");

    console.log("igm", igm);

    console.log("Containerno", Containerno);

    if (igm, Containerno) {
        loadingBar();

    $.get('/Import/Reports/ViewEIRReport?igm=' + igm + '&ContainerNumber=' + Containerno, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
            });

        }
    else {

        alert("please select igm and container")

    }
      
    }




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

    function formfiled() {

    }