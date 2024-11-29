    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField === field) {
        e.editorOptions.disabled = true;
        }
    else {
        e.editorOptions.disabled = false;
        }


    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "gdNumber");


    }
    function formfiled() {

    }