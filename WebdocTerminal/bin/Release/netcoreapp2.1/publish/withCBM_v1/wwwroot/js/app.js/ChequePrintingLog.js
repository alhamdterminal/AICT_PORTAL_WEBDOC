
    var CustomersData = [];

    $(function () {

        getcustomersData();
    })

    function getcustomersData() {
        $.get('/Setup/GetCustomersNameAndCode', function (data) {
            CustomersData = data;

            $("#searchpartyList").dxSelectBox({
                dataSource: {
                    store: CustomersData,
                    requireTotalCount: true,
                    pageSize: 4,
                    paginate: true,
                },
                name: 'CustomerId',
                displayExpr: "name",
                valueExpr: "customerId",
                acceptCustomValue: true,
                searchEnabled: true,
            })


        });
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
    function myFunction() {

        var fromdate = $('#single_cal2').val();
    var todate = $('#single_cal3').val();

    var customer = $("#searchpartyList").dxSelectBox('instance').option("text");

    console.log(fromdate);
    console.log(todate);
    console.log("customerid", customer);

    if (fromdate && todate) {

        $.post('/Account/Reports/ViewChequePrintingLog?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'partyname=' + customer, function (data) {

            loadingBarHide();

            $('#repotdata').html(data);
        });

        }
    else {
        alert("please select date")
    }


    }

    function formfiled() {

    }
