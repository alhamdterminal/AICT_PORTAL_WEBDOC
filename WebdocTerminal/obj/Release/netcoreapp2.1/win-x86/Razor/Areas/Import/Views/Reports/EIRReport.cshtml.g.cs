#pragma checksum "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\Reports\EIRReport.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "e3ba18b538b2b5a83b73f5cbf2ba894724c7ff8d121689686c84f06ce5e422b4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Import_Views_Reports_EIRReport), @"mvc.1.0.view", @"/Areas/Import/Views/Reports/EIRReport.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Import/Views/Reports/EIRReport.cshtml", typeof(AspNetCore.Areas_Import_Views_Reports_EIRReport))]
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
#line 1 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using WebdocTerminal

    ;
#line 2 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using WebdocTerminal.Models

    ;
#line 4 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc

    ;
#line 5 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using DevExpress.AspNetCore

#line default
#line hidden
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"e3ba18b538b2b5a83b73f5cbf2ba894724c7ff8d121689686c84f06ce5e422b4", @"/Areas/Import/Views/Reports/EIRReport.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Import/_ViewImports.cshtml")]
    public class Areas_Import_Views_Reports_EIRReport : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/viewer.part.bundle.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/viewer.part.bundle.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/EIRReport.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(2, 61, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e3ba18b538b2b5a83b73f5cbf2ba894724c7ff8d121689686c84f06ce5e422b45283", async() => {
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
            BeginContext(63, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(65, 50, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e3ba18b538b2b5a83b73f5cbf2ba894724c7ff8d121689686c84f06ce5e422b46557", async() => {
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
            BeginContext(115, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(117, 48, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e3ba18b538b2b5a83b73f5cbf2ba894724c7ff8d121689686c84f06ce5e422b47758", async() => {
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
            BeginContext(165, 391, true);
            WriteLiteral(@"



<div class=""right_col"" role=""main"">
    <div class=""page-title"">
        <div class=""title_left"">
            <h3>
                EIR Report
            </h3>

        </div>


    </div>
    <div class=""clearfix""></div>
    <div class=""row"">
        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">
                <div class=""x_content""");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 556, "\"", 561, 0);
            EndWriteAttribute();
            BeginContext(562, 171, true);
            WriteLiteral(">\r\n\r\n\r\n\r\n                    <div class=\"col-md-12 col-sm-12 col-xs-12\">\r\n                        <div class=\"x_panel\">\r\n                            <div class=\"x_content\"");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 733, "\"", 738, 0);
            EndWriteAttribute();
            BeginContext(739, 570, true);
            WriteLiteral(@">


                                <div class=""row"">




                                    <div class=""col-md-3"">
                                        <div class=""item form-group"">
                                            <label class=""control-label col-md-3 col-sm-3 col-xs-12"" for=""name"">
                                                Vir No: <span class=""required"">*</span>
                                            </label>
                                            <div class=""col-md-9"">
                                                ");
            EndContext();
            BeginContext(1311, 510, false);
            Write(
#line 43 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\Reports\EIRReport.cshtml"
                                                  Html.DevExtreme()
                                                .Autocomplete()
                                                .ID("virnolist")
                                                .DataSource(d => d.Mvc()
                                                .Controller("APICalls")
                                                .Area("")
                                                .LoadAction("Getvirno"))
                                                .Placeholder("Type GDNumber...")

#line default
#line hidden
            );
            EndContext();
            BeginContext(1822, 656, true);
            WriteLiteral(@"
                                            </div>
                                        </div>
                                    </div>
                                    <div class=""col-md-3"">
                                        <div class=""item form-group"">
                                            <label class=""control-label col-md-3 col-sm-3 col-xs-12"" for=""name"">
                                                Container No: <span class=""required"">*</span>
                                            </label>
                                            <div class=""col-md-9"">
                                                ");
            EndContext();
            BeginContext(2480, 527, false);
            Write(
#line 60 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\Reports\EIRReport.cshtml"
                                                  Html.DevExtreme()
                                                .Autocomplete()
                                                .ID("containerlist")
                                                .DataSource(d => d.Mvc()
                                                .Controller("APICalls")
                                                .Area("")
                                                .LoadAction("Getigmocontainers"))
                                                .Placeholder("Type Container No...")

#line default
#line hidden
            );
            EndContext();
            BeginContext(3008, 1040, true);
            WriteLiteral(@"
                                            </div>
                                        </div>
                                    </div>


                                    <div class=""col-lg-6"">
                                        <button type=""button"" id=""btnSubmitReport"" class=""btn btn-dark"" style=""float:right"" onclick=""myFunction()"">Submit</button>
                                    </div>


                                </div>
                                  
                            </div>
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
    $('.select2').select2");
            WriteLiteral("();\r\n</script>\r\n");
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