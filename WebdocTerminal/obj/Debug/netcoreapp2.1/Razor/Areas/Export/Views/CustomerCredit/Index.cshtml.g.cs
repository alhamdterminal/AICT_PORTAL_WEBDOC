#pragma checksum "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\CustomerCredit\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "049050614774abd914908cd8f9a8ec784966dfada14809e81c18dea51a7d993d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Export_Views_CustomerCredit_Index), @"mvc.1.0.view", @"/Areas/Export/Views/CustomerCredit/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Export/Views/CustomerCredit/Index.cshtml", typeof(AspNetCore.Areas_Export_Views_CustomerCredit_Index))]
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
#line 1 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\_ViewImports.cshtml"
using WebdocTerminal

    ;
#line 2 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\_ViewImports.cshtml"
using WebdocTerminal.Models

    ;
#line 4 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc

    ;
#line 5 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\_ViewImports.cshtml"
using DevExpress.AspNetCore

#line default
#line hidden
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"049050614774abd914908cd8f9a8ec784966dfada14809e81c18dea51a7d993d", @"/Areas/Export/Views/CustomerCredit/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Export/Views/_ViewImports.cshtml")]
    public class Areas_Export_Views_CustomerCredit_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/Index.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\CustomerCredit\Index.cshtml"
  
    var VesselExport = (IEnumerable<SelectListItem>)ViewData["VesselExports"];
    var VoyageExport = (IEnumerable<SelectListItem>)ViewData["VoyageExports"];


#line default
#line hidden

            BeginContext(169, 525, true);
            WriteLiteral(@"
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

");
            EndContext();
            BeginContext(694, 44, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "049050614774abd914908cd8f9a8ec784966dfada14809e81c18dea51a7d993d4949", async() => {
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
            BeginContext(738, 541, true);
            WriteLiteral(@"

<div class=""right_col"" role=""main"">
    <div class=""page-title"">
        <div class=""row"">
            <div class=""col-md-11"">
                <div class=""title_left"">
                    <h3>Credit To Customer</h3>
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

                <div class=""x_content""");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 1279, "\"", 1284, 0);
            EndWriteAttribute();
            BeginContext(1285, 505, true);
            WriteLiteral(@">

                    <div class=""form-horizontal form-label-left row"">
                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""item form-group"">
                                <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                    GD-Number:
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    ");
            EndContext();
            BeginContext(1792, 550, false);
            Write(
#line 64 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\CustomerCredit\Index.cshtml"
                                      Html.DevExtreme()
                                          .Autocomplete()
                                          .ID("GDNumber")
                                          .DataSource(d => d.Mvc()
                                          .Controller("APICalls")
                                          .Area("")
                                          .LoadAction("GetGDNumbers"))
                                          .Placeholder("Type GDNumber...")
                                          .OnValueChanged("gdNumber_changed")

#line default
#line hidden
            );
            EndContext();
            BeginContext(2343, 541, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""item form-group"">
                                <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                    LP Number:
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">

                                    ");
            EndContext();
            BeginContext(2886, 725, false);
            Write(
#line 83 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\CustomerCredit\Index.cshtml"
                                      Html.DevExtreme()
                                        .SelectBox()
                                        .ID("LPNumber")
                                        .ValueExpr("value")
                                        .DisplayExpr("text")
                                        .AcceptCustomValue(true)
                                        .SearchEnabled(true)
                                        .DataSource(d => d.Mvc()
                                        .Controller("APICalls")
                                        .Area("")
                                        .LoadAction("GetLoadingProgramNumbers"))
                                        .Placeholder("Type Loading Program ...")

#line default
#line hidden
            );
            EndContext();
            BeginContext(3612, 1249, true);
            WriteLiteral(@"

                                </div>
                            </div>
                        </div>

                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""form-group"">
                                <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                    Invoice No
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    <input type=""number"" id=""invoiceNo"" class=""form-control"" readonly />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class=""form-horizontal form-label-left row"">
                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""form-group"">
                                <label class=""control-label col-md-4 col-sm-4 co");
            WriteLiteral("l-xs-12\" for=\"first-name\">\r\n                                    Vessel\r\n                                </label>\r\n                                <div class=\"col-md-8 col-sm-8 col-xs-12\">\r\n                                    ");
            EndContext();
            BeginContext(4862, 145, false);
            Write(
#line 119 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\CustomerCredit\Index.cshtml"
                                     Html.DropDownList("VesselExportId", VesselExport, "Select Vessel", new { @class = "form-control", @id = "vesselExport", @readonly = "readonly" })

#line default
#line hidden
            );
            EndContext();
            BeginContext(5007, 538, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>

                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""form-group"">
                                <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                    Voyage
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    ");
            EndContext();
            BeginContext(5546, 145, false);
            Write(
#line 130 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\CustomerCredit\Index.cshtml"
                                     Html.DropDownList("VoyageExportId", VoyageExport, "Select Voyage", new { @class = "form-control", @id = "voyageExport", @readonly = "readonly" })

#line default
#line hidden
            );
            EndContext();
            BeginContext(5691, 2922, true);
            WriteLiteral(@"
                                    <span class=""error"" id=""VoyageExporterror""></span>
                                </div>
                            </div>
                        </div>

                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""form-group"">
                                <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                    Invoice Amount
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    <input type=""number"" id=""invoice-amount"" class=""form-control"" readonly />
                                </div>
                            </div>
                        </div>


                    </div>

                    <div class=""form-horizontal form-label-left row"">
                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div cl");
            WriteLiteral(@"ass=""item form-group"">
                                <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                    Credit Authorized By
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    <input type=""text"" class=""form-control"" id=""authorized-by"" name=""AuthorizedBy"">
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""item form-group"">
                                <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                    Authorized Days
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    <input type=""number"" class=""form-control"" id=""authorized-days"" n");
            WriteLiteral(@"ame=""AuthorizedDays"">
                                </div>
                            </div>
                        </div>


                    </div>

                    <div class=""form-group"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <button type=""button"" id=""btnCancel"" class=""btn btn-primary"" onclick=""restform()"" style=""float:right;"">Cancel</button>
                            <button id=""btnSubmit"" type=""button"" style=""float:right;"" onclick=""submit()"" class=""btn btn-success"">Submit</button>
                            <a id=""btnSubmitUpdate"" class=""btn btn-success"" onclick=""update()"" style=""display:none ; float: right;"">Update</a>

                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>





</div>

");
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
