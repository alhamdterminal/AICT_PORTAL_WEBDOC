
    window.addEventListener('load', function () {

        //Options for AdvancedSearchViewJQuery
        let options = {
        //Load model on start
        loadModelOnStart: true,
    //Load query on start
    loadQueryOnStart: false,

    enableExport: true,

    endpoint: "/api/easyquery",

    //Handlers
    handlers: {
        //Error handler
        onError: (error) => {
        console.log("error", error)
        // console.error(error.type + " error:\n" + error.text);
    },
            },
    //Widgets options
    widgets: {
        //EntityiesPanel options
        entitiesPanel: {
        showCheckboxes: true
                },
    //CloumnsPanel options
    columnsPanel: {
        allowAggrColumns: true,
    allowCustomExpressions: true,
    attrElementFormat: "{entity} {attr}",
    titleElementFormat: "{attr}",
    showColumnCaptions: true,
    adjustEntitiesMenuHeight: false,
    customExpressionText: 2,
    showPoweredBy: false,
    menuOptions: {
        showSearchBoxAfter: 30,
    activateOnMouseOver: true
                    }
                },
    //Querypanel options
    queryPanel: {
        showPoweredBy: false,
    alwaysShowButtonsInPredicates: false,
    allowParameterization: true,
    allowInJoinConditions: true,
    autoEditNewCondition: true,
    buttons: {
        condition: ["menu"],
    predicate: ["addCondition", "addPredicate", "enable", "delete"]
                    },
    menuOptions: {
        showSearchBoxAfter: 20,
    activateOnMouseOver: true
                    }
                },
            },
    result: {
        //Show EasyChart
        showChart: true
            }
        };

    let view = new easyquery.ui.AdvancedSearchView();
    this.console.log("v",view)
    view.init(options);
    document['AdvancedSearchView'] = view;
    });

    $(function () {
        document.getElementsByClassName('eqv-export-buttons')[0].style.display = "none";
      
    });
    function formfiled() {
        //  document.getElementById('QueryPanel').getElementsByTagName("a")[0].style.display = "none";
        //  console.log("das" , document.getElementById('QueryPanel').getElementsByTagName("a")[0])
    }

    function ExportGrid() {

        console.log(document.getElementsByClassName('eqv-grid-panel'));
    var htmltable= document.getElementsByClassName('eqv-grid-panel')[0];
    var html = htmltable.outerHTML;
    console.log(html)
    window.open('data:application/vnd.ms-excel,' + encodeURIComponent(html));
        // downloadFile('output.csv', 'outputDiv.innerHTML/csv;charset=UTF-8,' + encodeURIComponent(html));


    }