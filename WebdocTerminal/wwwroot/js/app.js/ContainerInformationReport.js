

    var type;
    var number = "";
    var indexnumber = "";

    function showFilters() {

        type = $("input[name='type']:checked").val();

    $.get('/APICalls/GetFilterForContainerTrackingReport?Type=' + type, function (partial) {

        $('#filters').html(partial);

        })

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

    function containerCallback() {


        if (type == "CY") {

    }


    }
    function containerChangeCallback() {
        console.log("containerChangeCallback", containerChangeCallback)

    }

    function myFunction() {

        if (type == "CFS") {

        number = $("#containerdb option:selected").text();
    //console.log("CFSindex", $("#containerIndexdiv").dxSelectBox('instance').option('value'))
    //console.log("CFSindex", $("#containerIndexdiv").dxSelectBox('instance'))
    console.log("CFSindex", $("#containerIndexdiv").dxSelectBox('instance').option('text'))
    indexnumber = $("#containerIndexdiv").dxSelectBox('instance').option('text');

        }
    if (type == "CY") {


        indexnumber = $("#containerIndexCYdb option:selected").text();
    number = $("#containerCYdb option:selected").text();

        }

    console.log("type", type)
    console.log("number", number)
    console.log("indexnumber", indexnumber)
    console.log(" $( ", $("#containerIndexCYdb option:selected").text())


    loadingBar();
    $.get('/Import/Reports/ViewContainerInformationReport?number=' + number + '&' + 'type=' + type + '&' + 'indexnumber=' + indexnumber, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });


    }

    function formfiled() {
        if (PermissionInputs.find(x => x.fieldName == "CFS" && x.isChecked == false)) {
        document.getElementById('CFS').disabled = true;

        }
        if (PermissionInputs.find(x => x.fieldName == "CY" && x.isChecked == false)) {
        document.getElementById('CY').disabled = true;

        }


    }

