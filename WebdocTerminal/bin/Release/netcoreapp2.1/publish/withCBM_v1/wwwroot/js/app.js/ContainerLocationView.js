
    $(function () {
        $.get('/APICalls/GetFilterForContainerLocation', function (partial) {

            $('#filters').html(partial);

        })

    });

    function containerChangeCallback(ornt) {

      

        var Containerno = $("#containerlist").dxAutocomplete("instance").option("value");


    var virnumber = $("#virnumberdb option:selected").val();

    console.log("virnumber", virnumber);
    console.log("Containerno", Containerno);
    console.log("containernumber", containernumber);

    if (virnumber && containernumber) {
            if (virnumber.length == 18 && containernumber.length == 11) {

        console.log("ornt", ornt);

    $.get('/APICalls/GetcontainerLocation?virno=' + virnumber + '&containerno=' + containernumber, function (res) {

        console.log("res", res);

    $("#type").val(res.type);

    $("#location").val(res.location);
                });
                 
            }
    else {

        resetvalues();

            }
        }
    else {
        resetvalues();

        }

      


    }

    function resetvalues() {

        $("#type").val('');

    $("#location").val('');

    }

    function formfiled() {

    }


    function saveContainerLocation() {

        var Containerno = $("#containerlist").dxAutocomplete("instance").option("value");

    var virnumber = $("#virnumberdb option:selected").val();
    var type = $("#type").val();

    var location = $("#location").val();

    console.log("virnumber", virnumber);
    console.log("Containerno", Containerno);
    console.log("type", type);
    console.log("location", location);

    if (!type) {
            return alert("type not define");
        }
    if (!location) {
            return alert("location not define");
        }
    if (virnumber && containernumber) {
            if (virnumber.length == 18 && Containerno.length == 11) {


        $.post('/Import/Setup/SaveImportContainerLocation?virno=' + virnumber + '&containerno=' + containernumber + '&type=' + type + '&location=' + location, function (res) {

            console.log("res", res);

            showToast(res.message, "success");
        });

            }
    else {

        showToast("please check igm and container", "error");

            }
        }
    else {
        showToast("please select igm and container", "error");

        }

    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }
