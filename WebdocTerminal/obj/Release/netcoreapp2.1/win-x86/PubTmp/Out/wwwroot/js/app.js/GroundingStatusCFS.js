
    function onFocusedRowChanged(e) {

        var blno = e.row.data.blNumber;

    $.get('/APICalls/GetExaminationIndex?blno=' + blno, function (data) {

            if (data.examinationMarked.blNumber != null) {
                var table = renderTable(data.examinationMarked);
    $('#examinationMark').html(table);
            }
    else {
        $('#examinationMark').html('');
            }

    if (data.examinationCompleted.blNumber != null) {
                var table = renderTable(data.examinationCompleted);
    $('#examinationComplete').html(table);
            }
    else {
        $('#examinationComplete').html('');
            }

        });
    }

    function   formatDate(date) {
            return date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
        }

    function renderTable(examination) {
 
        var table = `<table class="table table-striped">
        <thead>
            <tr>
                <th scope="col">VIR No</th>
                <th scope="col">BL No</th>
                <th scope="col">Index No</th>
                <th scope="col">Handling Code</th>
                <th scope="col">Date</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>${examination.virNumber}</td>
                <td>${examination.blNumber}</td>
                <td>${examination.indexNumber}</td>
                <td>${examination.handlingCode}</td>
                <td>${formatDate(new Date(examination.date))}</td>
            </tr>
        </tbody>
    </table>`;

    return table;
    }


    function formfiled() {

    }
