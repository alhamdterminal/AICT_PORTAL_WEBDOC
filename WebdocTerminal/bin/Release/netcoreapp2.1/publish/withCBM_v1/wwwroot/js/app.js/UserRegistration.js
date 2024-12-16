

    $(function () {
        $('#roles').multiselect();
    
    });

    function onrolechage() {

        var b = $('#roles').val();
    console.log("b", b)
    if (b != null) {
            var filtered = b.find(function (str) { return str.includes("SHIPPING AGENTS"); })
    console.log("filtered",filtered)
    if (filtered == 'SHIPPING AGENTS') {

                var btn = document.getElementById("shippingAgnt");
    btn.style.display = 'block';

    //var btnExport = document.getElementById("shippingAgntExport");
    //btnExport.style.display = 'block';

    document.getElementById("agentId").disabled = false;
              //  document.getElementById("shippingAgentExportId").disabled = false;
        }
    else {
            var btn = document.getElementById("shippingAgnt");
    btn.style.display = 'none';

    // var btnExport = document.getElementById("shippingAgntExport");
    //btnExport.style.display = 'none';

    document.getElementById("agentId").disabled = true;
              // document.getElementById("shippingAgentExportId").disabled = true;
        }
        }
    else{
             var btn = document.getElementById("shippingAgnt");
    btn.style.display = 'none';

    // var btnExport = document.getElementById("shippingAgntExport");
    //btnExport.style.display = 'none';

    document.getElementById("agentId").disabled = true;
              // document.getElementById("shippingAgentExportId").disabled = true;
        }
        
    }


    function formfiled() {

        $('#registrationForm').submit(function (e) {

            if ($('#pass1').val() !== $('#pass2').val()) {

                $('#errorspan').html('Passwords do not match');

                e.preventDefault();
            }
        });
    }

