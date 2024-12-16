

    function formfiled() {

    }

    $(function () {

        $(document).ready(function () {

            $('#roles').multiselect();



        });

    $('#passwordupdate').submit(function (e) {

            if ($('#pass1').val() !== $('#pass2').val()) {

        $('#errorspan').html('Passwords do not match');

    e.preventDefault();


            }
        });

    onrolechage();

    });


    function onrolechage() {

            var b = $('#roles').val();
    console.log("b",b)
    if (b != null) {
             var filtered = b.find(function (str) { return str.includes("SHIPPING AGENTS"); })
    console.log("filtered",filtered)
    if (filtered == 'SHIPPING AGENTS') {
            var btn = document.getElementById("shippingAgnt");
    btn.style.display = 'block';

    //   var btn = document.getElementById("shippingAgntExport");
    //btn.style.display = 'block';

    document.getElementById("shippingAgentId").disabled = false;
             //  document.getElementById("shippingAgentExportId").disabled = false;
        }
    else {
            var btn = document.getElementById("shippingAgnt");
    btn.style.display = 'none';

    // var btn = document.getElementById("shippingAgntExport");
    //btn.style.display = 'none';

    document.getElementById("shippingAgentId").disabled = true;
              // document.getElementById("shippingAgentExportId").disabled = true;
        }
         }
    else{
             var btn = document.getElementById("shippingAgnt");
    btn.style.display = 'none';

    // var btn = document.getElementById("shippingAgntExport");
    //btn.style.display = 'none';

    document.getElementById("shippingAgentId").disabled = true;
           //    document.getElementById("shippingAgentExportId").disabled = true;
        }
        
    }

