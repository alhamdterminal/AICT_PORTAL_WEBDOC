#pragma checksum "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\Reports\TerminalReceiptSec.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1454bfb9181f91814f2b90c2a61983da5359aad9082c75812aae3c7ff6d281af"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Export_Views_Reports_TerminalReceiptSec), @"mvc.1.0.view", @"/Areas/Export/Views/Reports/TerminalReceiptSec.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Export/Views/Reports/TerminalReceiptSec.cshtml", typeof(AspNetCore.Areas_Export_Views_Reports_TerminalReceiptSec))]
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
#line 1 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\_ViewImports.cshtml"
using WebdocTerminal

    ;
#line 2 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\_ViewImports.cshtml"
using WebdocTerminal.Models

    ;
#line 4 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc

    ;
#line 5 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\_ViewImports.cshtml"
using DevExpress.AspNetCore

#line default
#line hidden
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1454bfb9181f91814f2b90c2a61983da5359aad9082c75812aae3c7ff6d281af", @"/Areas/Export/Views/Reports/TerminalReceiptSec.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Export/Views/_ViewImports.cshtml")]
    public class Areas_Export_Views_Reports_TerminalReceiptSec : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/viewer.part.bundle.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/viewer.part.bundle.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/TerminalReceiptSec.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(0, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 3 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\Reports\TerminalReceiptSec.cshtml"
  

    var EGMNumber = (IEnumerable<SelectListItem>)ViewData["EGMNumber"];

    var ShippingAgent = (IEnumerable<SelectListItem>)ViewData["ShippingAgent"];


#line default
#line hidden

            BeginContext(171, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(175, 61, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "1454bfb9181f91814f2b90c2a61983da5359aad9082c75812aae3c7ff6d281af5828", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(236, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(238, 50, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1454bfb9181f91814f2b90c2a61983da5359aad9082c75812aae3c7ff6d281af7104", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(288, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(292, 57, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1454bfb9181f91814f2b90c2a61983da5359aad9082c75812aae3c7ff6d281af8309", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(349, 487, true);
            WriteLiteral(@"

<div class=""right_col"" role=""main"">
    <div class=""page-title x_panel"">
        <div class=""title_left"">
            <h3>
                Terminal Rececipt
            </h3>
        </div>
        <div class=""title_right"">

        </div>
    </div>
    <div class=""clearfix""></div>
    <div class=""clearfix""></div>

    <div class=""row"">
        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">

                <div class=""x_content""");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 836, "\"", 841, 0);
            EndWriteAttribute();
            BeginContext(842, 76, true);
            WriteLiteral(">\r\n                    <div class=\"form-horizontal form-label-left row\">\r\n\r\n");
            EndContext();
#line 38 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\Reports\TerminalReceiptSec.cshtml"
                           Html.RenderPartial("_VesselVoyageSelection"); 

#line default
#line hidden

            BeginContext(994, 428, true);
            WriteLiteral(@"


                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""item form-group"">
                                <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                    EGM
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    ");
            EndContext();
            BeginContext(1423, 115, false);
            Write(
#line 48 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\Reports\TerminalReceiptSec.cshtml"
                                     Html.DropDownList("EGMNumber", EGMNumber, "Select EGMNumber", new { @class = "form-control select2", @id = "egm" })

#line default
#line hidden
            );
            EndContext();
            BeginContext(1538, 647, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>



                    </div>
                    <div class=""form-horizontal form-label-left row"">
                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""item form-group"">
                                <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                    ShippingAgent
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    ");
            EndContext();
            BeginContext(2186, 137, false);
            Write(
#line 63 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\Reports\TerminalReceiptSec.cshtml"
                                     Html.DropDownList("ShippingAgent", ShippingAgent, "Select ShippingAgent", new { @class = "form-control select2", @id = "shippingAgent" })

#line default
#line hidden
            );
            EndContext();
            BeginContext(2323, 572, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""item form-group"">
                                <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                    Container : <span class=""required"">*</span>
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    ");
            EndContext();
            BeginContext(2897, 742, false);
            Write(
#line 73 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\Reports\TerminalReceiptSec.cshtml"
                                      Html.DevExtreme()
                                                                .Autocomplete()
                                                                .ID("ContainerNumber")
                                                                .DataSource(d => d.Mvc()
                                                                .Controller("APICalls")
                                                                .Area("")
                                                                .LoadAction("GetExpotrContainers"))
                                                                .Placeholder("Type Container...")
                                                                .OnValueChanged("container_changed")

#line default
#line hidden
            );
            EndContext();
            BeginContext(3640, 572, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""item form-group"">
                                <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                    GD Number : <span class=""required"">*</span>
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    ");
            EndContext();
            BeginContext(4214, 663, false);
            Write(
#line 91 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\Reports\TerminalReceiptSec.cshtml"
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
            BeginContext(4878, 914, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>





                    </div>

                    <div class=""form-horizontal form-label-left row"">
                        <div class=""col-md-12  col-xs-12"">
                            <button type=""button"" style=""float:right;"" onclick=""printReport()"" class=""btn btn-primary"">Print</button>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class=""row"">

        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">
                <div id=""large-indicator""></div>
                <div class=""x_content"" id=""repotdata"">

                </div>
            </div>
        </div>

    </div>

</div>



<script>
    $('.select2').select2();
</script>
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