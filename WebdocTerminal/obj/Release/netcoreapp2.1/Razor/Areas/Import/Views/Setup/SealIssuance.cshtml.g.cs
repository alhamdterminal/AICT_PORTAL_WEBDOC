#pragma checksum "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\Setup\SealIssuance.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "dbd8b3be187b900219a9b49c3758a637d5ee3be064ca9975812b03b1e716b628"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Import_Views_Setup_SealIssuance), @"mvc.1.0.view", @"/Areas/Import/Views/Setup/SealIssuance.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Import/Views/Setup/SealIssuance.cshtml", typeof(AspNetCore.Areas_Import_Views_Setup_SealIssuance))]
namespace AspNetCore
{
    #line default
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using WebdocTerminal

    ;
#line 2 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using WebdocTerminal.Models

    ;
#line 4 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc

    ;
#line 5 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using DevExpress.AspNetCore

#line default
#line hidden
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"dbd8b3be187b900219a9b49c3758a637d5ee3be064ca9975812b03b1e716b628", @"/Areas/Import/Views/Setup/SealIssuance.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Import/_ViewImports.cshtml")]
    public class Areas_Import_Views_Setup_SealIssuance : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/SealIssuance.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 12, true);
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n\r\n");
            EndContext();
            BeginContext(12, 51, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dbd8b3be187b900219a9b49c3758a637d5ee3be064ca9975812b03b1e716b6284106", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(63, 664, true);
            WriteLiteral(@"



<div class=""right_col"" role=""main"">
    <div class=""page-title"">
        <div class=""row"">
            <div class=""col-md-11"">
                <div class=""title_left"">
                    <h3>Seal Issuance</h3>
                </div>
            </div>
        </div>
    </div>
    <div class=""clearfix""></div>

    <div class=""row"">
        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">
                <div class=""x_content"">
                    <div class=""form-group"" id=""gridContainer"">
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<!--");
            EndContext();
#line 35 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\Setup\SealIssuance.cshtml"
      

//var Seals = (IEnumerable<SelectListItem>)ViewData["SealIssuance"];


#line default
#line hidden

            BeginContext(808, 7244, true);
            WriteLiteral(@"
<script>
    var igm;

    function refresh() {
        window.location.reload();
    }

    var PaymentUpdateId = 0;
    var PaymentUpdateDetail = [];

    var SealsPurchase = [];

    var ShippingLineList = [];



    function sendgroundingcontainers(unAssignseals) {
        console.log(unAssignseals, ""unAssignseals"");

        $.post('UpdateSealIssuance', { model: unAssignseals }, function (data) {

            if (data.error == true) {

                showToast(data.message, ""error"");

                window.location.href = window.location.href;

            }
            else {

                showToast(data.message, ""success"");

                window.location.href = window.location.href;


            }

        });

    }


    function saveIssueDetail() {

        $(""#butonSubmit"").attr(""disabled"", true);

        setTimeout(function () { $(""#butonSubmit"").attr(""disabled"", false); }, 120000);

        sendgroundingcontainers(resuktContainers);
    ");
            WriteLiteral(@"}


    function getsealCode(data) {

        igm = data.value;
        console.log(igm)
    }

    function ShowDetail() {
        console.log(igm)

        var fromdate = document.getElementById(""From"").value;
        var todate = document.getElementById(""To"").value;
        var batch = igm;
        console.log(batch, ""batch"")
        GetSealsByNo(fromdate, todate, batch);
    }

    function GetSealsByNo(fromdate, todate,batch) {

        $.get('/Import/Setup/GetSealPurchasesByNo?from=' + fromdate + '&to=' + todate + '&batch=' + batch, function (result) {
            console.log(""result"", result)
            var seals = result;

            $(""#sealdetails"").dxDataGrid({
                dataSource: seals,
                keyExpr: ""sealPurchaseId"",
                showBorders: true,
                hoverStateEnabled: true,
                columnsAutoWidth: true,
                paging: {
                    pageSize: 15
                },
                scrolling: {
     ");
            WriteLiteral(@"               columnRenderingMode: ""virtual""
                },
                filterRow: {
                    visible: true,
                    applyFilter: ""auto""
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: ""Search...""
                },
                editing: {
                    mode: ""cell"",
                    allowAdding: false,
                    allowUpdating: false,
                    useIcons: true
                },
                headerFilter: {
                    visible: true
                },
                columns: [
                    {
                        dataField: ""sealPurchaseNo"",
                        caption: ""Seal Code"",
                    },
                    {
                        dataField: ""sealFrom"",
                        caption: ""Seal"",
                    },
                    {
                        dataField: """);
            WriteLiteral(@"rate"",
                        dataType: ""number"",
                    },
                    {
                        dataField: ""purchaseDate"",
                        dataType: ""date"",
                        format: ""dd/MMM/yyyy"",
                    },
                    {
                        dataField: ""status"",
                        dataType: ""boolean""
                    },
                ],
                summary: {
                    totalItems: [{
                        column: ""sealFrom"",
                        summaryType: ""count""
                    }, {
                        column: ""rate"",
                        summaryType: ""sum""
                    }]
                },

                onEditorPreparing: function (e) {
                    // hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {
                    console.log(e);
                    //var data = e.data;
                    //$.post('/Import/Set");
            WriteLiteral(@"up/UpdateSealIssuance', { data, data }, function (data) {


                    //    showToast(data.message, ""success"");
                    //});
                },


            });

        });


    }

    function checkColumn(e, field) {

        if (e.parentType === ""dataRow"" && e.dataField !== field) {
            return;
        }

        e.editorOptions.disabled = true;
    }


    function hideMenifestedColumns(e) {

        checkColumn(e, ""shippingLineName"");
        checkColumn(e, ""containerNumber"");
        checkColumn(e, ""size"");
        checkColumn(e, ""cargoType"");
        checkColumn(e, ""type"");
        checkColumn(e, ""soccoc"");
        checkColumn(e, ""totalTHCCharges"");
        checkColumn(e, ""totalCharges"");
    }

    function checkValidations() {

        //if (!$('#accNo').val()) {

        //    $('#accNoerror').html('This is a required field');
        //}
        //else {
        //    $('#accNoerror').html('');
        //}

    }

   ");
            WriteLiteral(@" function perAlertNumber_changed(data) {
        console.log(""data"", data)

        var perAlertNumber = data.selectedItem;

        getprealterDetailbyperAlertNumber(perAlertNumber)

    }



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }
</script>

<style>


    .error {
        color: red;
    }



    #error {
        visibility: hidden;
        width: 200px;
        background-color: #d25656;
        color: #fff;
        text-align: center;
        border-radius: 2px;
        padding: 16px;
        z-index: 1;
        font-size: 17px;
    }

        #error.show {
            visibility: visible;
            -webkit-animation: fadein 0.5s, fadeout 0.5s 2.5s;
            animation: fadein 0.5s, fadeout 0.5s 2.5s;
        }
</style>

<div class=""right_col"" role=""main"">
    <div class=""page-title"">");
            WriteLiteral(@"
        <div class=""row"">
            <div class=""col-md-11"">
                <div class=""title_left"">
                    <h3>Seal Issuance</h3>
                </div>
            </div>
            <div class=""col-md-1"">
            </div>
        </div>
    </div>
    <div class=""clearfix""></div>

    <div class=""row"">
        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">

                <div class=""x_content"" id=""odform"">
                    <form class=""form-horizontal form-label-left"" id=""sealPurchaseForm"">

                        <div class=""form-horizontal form-label-left row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                       Batch
                                    </label>
                                    <div cl");
            WriteLiteral("ass=\"col-md-8 col-sm-8 col-xs-12\">\r\n                                        ");
            EndContext();
            BeginContext(8054, 473, false);
            Write(
#line 311 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\Setup\SealIssuance.cshtml"
                                          Html.DevExtreme()
                                .Autocomplete()
                                .ID("SealPurchaseId")
                                .DataSource(d => d.Mvc()
                                .Controller("APICalls")
                                .Area("")
                                .LoadAction("GetSealsCode"))
                                .Placeholder("Type Batch Code...")
                                .OnValueChanged("getsealCode")

#line default
#line hidden
            );
            EndContext();
            BeginContext(8528, 5, true);
            WriteLiteral("-->\r\n");
            EndContext();
            BeginContext(8620, 3075, true);
            WriteLiteral(@"<!--</div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Seal.No From
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control col-md-7 col-xs-12"" id=""From"" name=""From"">
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                              ");
            WriteLiteral(@"          Seal.No To
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control col-md-7 col-xs-12"" id=""To"" name=""To"">
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class=""form-group"">
                            <div class=""col-md-12 col-sm-12 col-xs-12"">
                                <button type=""button"" id=""btnCancel"" class=""btn btn-primary"" onClick=""restform()"" style=""float:right;"">Cancel</button>
                                <button id=""btnSubmit"" type=""button"" onclick=""ShowDetail()"" class=""btn btn-primary"" style=""float:right;"">Submit</button>
                            </div>
                        </div>

                    </form>

                    <div class=""clearfix""></div>
                    <d");
            WriteLiteral(@"iv class=""row"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <div class=""x_panel"">
                                <div class=""x_content"">
                                    <div id=""sealdetails""></div>
                                    <div class=""ln_solid""></div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class=""form-group"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <button type=""button"" id=""btnCancel"" class=""btn btn-primary"" onClick=""restform()"" style=""float:right;"">Cancel</button>
                            <button id=""butonSubmit"" type=""button"" class=""btn btn-success"" onclick=""saveIssueDetail()"" style=""float:right;"">Submit</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>--");
            WriteLiteral(">\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
