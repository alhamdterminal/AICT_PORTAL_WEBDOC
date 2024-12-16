

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

    function showcfs() {


        element = document.getElementById('type').value;
    console.log("test:", element);

    if (element == 'CFS' || element == '') {
        document.getElementById('ctype').disabled = false;
        }
    else {
        document.getElementById('ctype').disabled = true;
    document.getElementById('ctype').innerHTML.value = 'ALL';
        }
    }

    function myFunction() {

        loadingBar();


    var fromdate = document.getElementById("fromdate").value;

    var todate = document.getElementById("todate").value;

    var type = document.getElementById("type").value;

    var ctype = document.getElementById('ctype').value;

    console.log("fromdate", fromdate);
    console.log("todate", todate);
    console.log("type", type);
    console.log("ctype", ctype);





    $.get('/Import/Reports/ViewCFSShedClearIndex?fromdate=' + fromdate + '&' + 'todate=' + todate
    + '&' + 'type=' + type + '&' + 'ctype=' + ctype, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }

    function formfiled() {

    }