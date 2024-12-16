
    function formfiled() {

    }


    $(function () {

        $("#igmno").inputmask("aaaa-9999-99999999");

    });




    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function UploadInfo() {



        console.log("igmno", $('#igmno').val())

        var igmnumber = $('#igmno').val().replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '_').length;

    console.log("igmnumber", igmnumber)


    if (igmnumber == 16) {

        //var DatFiles = [{ FILE_NAME: "1" }, { FILE_NAME: "1" }, { FILE_NAME: "1" }, { FILE_NAME: "1" }];


        //$.post('/Import/IGMInfo/ProcessDatFiles', { DatFiles: DatFiles }, function (res) {
        $.post('/Import/IGMInfo/ProcessDatFiles', function (res) {

            console.log("res", res);

            if (res.results.length) {

                $.post('/Import/IGMInfo/UpdateIGMO?igm=' + $('#igmno').val(), { model: res.results }, function (res) {

                    console.log("res", res);

                    if (res.Results.length) {



                    }
                });


            }
        });


        } else {
        showToast("please type correct igm", "error")
    }
        

    }


