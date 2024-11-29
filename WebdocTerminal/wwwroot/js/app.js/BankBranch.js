


    $(function () {
        GetBankBranchs();
    })

    var BankBranchData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function GetBankBranchs() {
        $.get('/Import/Bank/GetBankBranch', function (data) {
            BankBranchData = data;
            console.log(BankBranchData);
            bankBranchgrid(BankBranchData);
        });
    }




    function bankBranchgrid(BankBranchData) {


        $("#gridContainer").dxDataGrid({
            dataSource: BankBranchData,
            keyExpr: "bankBranchId",
            showBorders: true,
            allowColumnResizing: true,
            columnAutoWidth: true,
            paging: {
                pageSize: 10
            },
            searchPanel: {
                visible: true,
                width: 240,
                placeholder: "Search..."
            },

            editing: {
                mode: "row",
                allowUpdating: true,
                allowAdding: true
            },
            columns: [
                {
                    dataField: "bankBranchCode",
                    validationRules: [{ type: "required" }],
                    caption: "Code"
                },
                {
                    dataField: "bankBranchName",
                    validationRules: [{ type: "required" }],
                    caption: "Branch Name"
                }
            ],

            onRowInserted: function (e) {
                console.log(e)
                console.log(e.data)
                var bankbranch = e.data;

                $.post('/Import/Bank/AddBankBranch', { bankbranch, bankbranch }, function (data) {

                    console.log(data);
                    showToast("Created", "success");

                    GetBankBranchs();

                });

            },

            onRowUpdated: function (e) {
                console.log(e);
                var bankbranch = e.data;
                $.post('/Import/Bank/UpdateBankBranch', { bankbranch, bankbranch }, function (data) {

                    console.log(data)
                    showToast("Updated", "success");
                    GetBankBranchs();

                });
            }

        });
    }



    function formfiled() {

    }
