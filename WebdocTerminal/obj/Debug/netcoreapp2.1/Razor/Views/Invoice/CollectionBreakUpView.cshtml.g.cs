#pragma checksum "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Views\Invoice\CollectionBreakUpView.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "d702516a9aff56c50e98061b643f699dd927c2e1f9ab89eb6f0b4dba065292f5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Invoice_CollectionBreakUpView), @"mvc.1.0.view", @"/Views/Invoice/CollectionBreakUpView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Invoice/CollectionBreakUpView.cshtml", typeof(AspNetCore.Views_Invoice_CollectionBreakUpView))]
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
#line 1 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Views\_ViewImports.cshtml"
using WebdocTerminal

    ;
#line 2 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Views\_ViewImports.cshtml"
using WebdocTerminal.Models

    ;
#line 4 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc

    ;
#line 5 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Views\_ViewImports.cshtml"
using DevExpress.AspNetCore

#line default
#line hidden
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d702516a9aff56c50e98061b643f699dd927c2e1f9ab89eb6f0b4dba065292f5", @"/Views/Invoice/CollectionBreakUpView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Views/_ViewImports.cshtml")]
    public class Views_Invoice_CollectionBreakUpView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/CollectionBreakUpView.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal form-label-left"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("CollectionBreakUpForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 1050, true);
            WriteLiteral(@"<style>

    #CargoInformation {
        max-height: 750px;
    }

    .my-class {
        background-color: #4b5f71;
        color: white;
    }

    .newbox {
        text-align: center;
        padding: 3px 0px 3px 0px;
        font-size: 14px;
        font-weight: 600;
        margin: -3px -10px 10px -10px;
        color: white;
        background: #2a3f54;
        border-bottom: 1px solid #d1d1d1;
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

    .dx-header-row > td > .dx-datagrid-text-content {
        white-space: normal;
        vertical-align: top;
        text-align: ce");
            WriteLiteral("nter;\r\n    }\r\n</style>\r\n\r\n");
            EndContext();
            BeginContext(1050, 60, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d702516a9aff56c50e98061b643f699dd927c2e1f9ab89eb6f0b4dba065292f56277", async() => {
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
            BeginContext(1110, 1049, true);
            WriteLiteral(@"



<div class=""right_col"" role=""main"">
    <div class=""page-title"">
        <div class=""row"">
            <div class=""col-md-11"">
                <div class=""title_left"">
                    <h3>Collection BreakUP</h3>
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

                <div class=""x_content"">


                    <div class=""row"">

                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""item form-group"">
                                <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                    Vir No: <span class=""required"">*</span>
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
           ");
            WriteLiteral("                         ");
            EndContext();
            BeginContext(2161, 510, false);
            Write(
#line 80 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Views\Invoice\CollectionBreakUpView.cshtml"
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
            BeginContext(2672, 572, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>

                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""item form-group"">
                                <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                    Index No: <span class=""required"">*</span>
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    ");
            EndContext();
            BeginContext(3246, 522, false);
            Write(
#line 98 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Views\Invoice\CollectionBreakUpView.cshtml"
                                      Html.DevExtreme()
                                                .Autocomplete()
                                                .ID("containerIndexlist")
                                                .DataSource(d => d.Mvc()
                                                .Controller("APICalls")
                                                .Area("")
                                                .LoadAction("Getigmoindexes"))
                                                .Placeholder("Type Index...")

#line default
#line hidden
            );
            EndContext();
            BeginContext(3769, 1902, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>

                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""control-group"">
                                <div class=""controls"">
                                    <label class=""control-label col-md-3 col-sm-3 col-xs-12"" for=""first-name"">
                                        From:
                                    </label>
                                    <div class=""col-md-9 col-xs-12"">
                                        <input type=""text"" class=""form-control has-feedback-left"" id=""single_cal2"" name=""fromdate"" aria-describedby=""inputSuccess2Status2"">
                                        <span class=""fa fa-calendar-o form-control-feedback left"" aria-hidden=""true""></span>
                                        <span id=""inputSuccess2Status2"" class=""sr-only"">(success)</span>
                                    </div>
        ");
            WriteLiteral(@"                        </div>
                            </div>
                        </div>
                    </div>

                    <div class=""row"">

                        <div class=""col-md-8 col-sm-8 col-xs-12"">
                        </div>

                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""form-group"">
                                <button type=""button"" class=""btn btn-dark"" onclick=""showcargoDetaildesc()"" style=""float:right""> View Detail</button>

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

                <div class=""x_content""");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 5671, "\"", 5676, 0);
            EndWriteAttribute();
            BeginContext(5677, 25, true);
            WriteLiteral(">\r\n\r\n                    ");
            EndContext();
            BeginContext(5702, 6455, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d702516a9aff56c50e98061b643f699dd927c2e1f9ab89eb6f0b4dba065292f513252", async() => {
                BeginContext(5775, 6375, true);
                WriteLiteral(@"

                        <div class=""form-horizontal form-label-left row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        M3 :
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <div class=""col-sm-4""> <h5 id=""pono"">  </h5> </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class=""form-horizontal form-label-left row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-s");
                WriteLiteral(@"m-4 col-xs-12"" for=""name"">
                                        PLID Number
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" id=""plidNumber"" class=""form-control"" name=""PLIDNumber"">
                                        <span class=""error"" id=""PLIDNumbererror""></span>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Warehouse Location
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" id=""wa");
                WriteLiteral(@"rehouseLocation"" class=""form-control"" name=""WarehouseLocation"">
                                        <span class=""error"" id=""WarehouseLocationerror""></span>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        CBM
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""number"" step=""any"" id=""cbm"" class=""form-control"" name=""CBM"" required>
                                        <span class=""error"" id=""CBMerror""></span>
                                    </div>
                                </div>
                            </div>



                WriteLiteral(@"


                        </div>

                        <div class=""form-horizontal form-label-left row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Weight
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""number"" step=""any"" id=""weight"" class=""form-control"" name=""Weight"" required>
                                        <span class=""error"" id=""Weighterror""></span>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                              ");
                WriteLiteral(@"      <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Location
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" id=""location"" class=""form-control"" name=""Location"" required>
                                        <span class=""error"" id=""Locationerror""></span>
                                    </div>
                                </div>
                            </div>


                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Color Code
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                 ");
                WriteLiteral(@"                       <input type=""text"" id=""colorcode"" class=""form-control"" name=""ColorCode"">
                                        <span class=""error"" id=""ColorCodeerror""></span>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class=""form-horizontal form-label-left row"">

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Package Received
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""number"" id=""packageReceived"" class=""form-control"" name=""PackageReceived"" required>
                                        <s");
                WriteLiteral("pan class=\"error\" id=\"PackageReceivederror\"></span>\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n\r\n\r\n\r\n                        </div>\r\n                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(12157, 86, true);
            WriteLiteral("\r\n\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n</div>\r\n");
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