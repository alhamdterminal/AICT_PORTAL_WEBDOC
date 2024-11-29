

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    var salesTaxIdImport = 0;
    //var salesTaxIdExport = 0;
    //$(function () {




        //});
        function UpdateSalesTaxImport() {
            console.log(salesTaxIdImport)
            var salesTax = SalesTaxForm.elements["SalesTaxAmount"].value;

            $.post('/SalesTax/UpdateSalesTaxImport?salesTax=' + salesTax + '&id=' + salesTaxIdImport, function (data) {

                showToast("Sales Tax Updated", "success");

            });
        }
    //function UpdateSalesTaxExport() {
    //    console.log(salesTaxIdExport)
    //    var salesTax = document.getElementById('salesTaxAmountExport').value;
    //    console.log(salesTax)
    //    $.post('/SalesTax/UpdateSalesTaxImport?salesTax=' + salesTax + '&id=' + salesTaxIdExport, function (data) {

    //        showToast("Sales Tax Updated", "success");

    //    });
    //}




    function UpdateSalesTaxExport() {
            console.log(salesTaxIdExport)
            var salesTax = document.getElementById('salesTaxAmountExport').value;
            console.log(salesTax)
            $.post('/SalesTax/UpdateSalesTaxImport?salesTax=' + salesTax + '&id=' + salesTaxIdExport, function (data) {

                showToast("Sales Tax Updated", "success");

            });
        }

    function formfiled() {
        $.get('/SalesTax/getSalesTaxImpoer', function (data) {
            console.log(data);
            salesTaxIdImport = data.salesTaxId;
            $('#salesTaxAmountImport').val(data.salesTaxAmount);
        });



    $.get('/SalesTax/getSalesTaxExport', function (data) {
        console.log(data);
    salesTaxIdExport = data.salesTaxId;
    $('#salesTaxAmountExport').val(data.salesTaxAmount);
        });


            if (PermissionInputs.find(x => x.fieldName == "SalesTaxAmountImport" && x.isChecked == false)) {

        document.getElementById('salesTaxAmountImport').disabled = true;
    }
    //if (PermissionInputs.find(x => x.fieldName == "SalesTaxAmountExport" && x.isChecked == false)) {

        //    document.getElementById('salesTaxAmountExport').disabled = true;
        //}
    }


