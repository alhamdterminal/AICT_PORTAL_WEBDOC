

    function showcargoDetaildesc() {

        var virno = $("#virnolist").dxAutocomplete("instance").option("value")

    var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value")

    var fromdate = document.getElementById("single_cal2").value;


    if (virno && indexno) {
        $.get('/APICalls/GetCollectionBreakup?virno=' + virno + '&indexno=' + indexno + '&BillDate=' + fromdate, function (data) {

            console.log("data", data);

        });
        }
    else {
        alert("please  select IGM and Index")
    }

        
    }
