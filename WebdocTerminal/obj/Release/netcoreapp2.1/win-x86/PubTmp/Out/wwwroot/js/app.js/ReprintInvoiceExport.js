
    var billNo;


    function invoice_changed(data)
    {
        billNo = data.value;

        
    }

    function print_invoice() {

        window.open('/Export/reports/ExportBillInvoice?BillNo=' + billNo, "_blank");
        
    }

    function deleteInvoice() {

        var x = confirm("Are You Sure");
    if (x == true) {

        $.post("/Export/InvoiceExport/DeleteInvoice?BillNo=" + billNo, function (data) {

            showToast(data.message, "success");

        })
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