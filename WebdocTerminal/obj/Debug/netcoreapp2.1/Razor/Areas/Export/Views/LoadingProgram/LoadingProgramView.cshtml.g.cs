#pragma checksum "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\LoadingProgram\LoadingProgramView.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9a43fa61a198cee59005096410337e75b312b6cba399b826b856b5c228c04e66"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Export_Views_LoadingProgram_LoadingProgramView), @"mvc.1.0.view", @"/Areas/Export/Views/LoadingProgram/LoadingProgramView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Export/Views/LoadingProgram/LoadingProgramView.cshtml", typeof(AspNetCore.Areas_Export_Views_LoadingProgram_LoadingProgramView))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"9a43fa61a198cee59005096410337e75b312b6cba399b826b856b5c228c04e66", @"/Areas/Export/Views/LoadingProgram/LoadingProgramView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Export/Views/_ViewImports.cshtml")]
    public class Areas_Export_Views_LoadingProgram_LoadingProgramView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/LoadingProgramView.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal form-label-left"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("LoadingPrgramForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\LoadingProgram\LoadingProgramView.cshtml"
  

    var VesselExports = (IEnumerable<SelectListItem>)ViewData["VesselExports"];

    var Shipper = (IEnumerable<SelectListItem>)ViewData["Shipper"];

    var ShippingLine = (IEnumerable<SelectListItem>)ViewData["ShippingLine"];

    var ClearingAgent = (IEnumerable<SelectListItem>)ViewData["ClearingAgent"];

    var ShippingAgent = (IEnumerable<SelectListItem>)ViewData["ShippingAgent"];

    var PortAndTerminal = (IEnumerable<SelectListItem>)ViewData["PortAndTerminal"];

    var Destinations = (IEnumerable<SelectListItem>)ViewData["Destinations"];

#line default
#line hidden

            BeginContext(576, 527, true);
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
            BeginContext(1103, 57, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9a43fa61a198cee59005096410337e75b312b6cba399b826b856b5c228c04e666483", async() => {
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
            BeginContext(1160, 573, true);
            WriteLiteral(@"

<div class=""right_col"" role=""main"">
    <div class=""page-title"">
        <div class=""row"">
            <div class=""col-md-11"">
                <div class=""title_left"">
                    <h3>Loading Program</h3>
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
                    ");
            EndContext();
            BeginContext(1733, 13813, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9a43fa61a198cee59005096410337e75b312b6cba399b826b856b5c228c04e668287", async() => {
                BeginContext(1802, 1253, true);
                WriteLiteral(@"



                        <div class=""form-horizontal form-label-left row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Loading Program Number
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control col-md-7 col-xs-12"" id=""lpnumber"" name=""LoadingProgramNumber"" readonly>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">");
                WriteLiteral("\r\n                                        Loading Program Date\r\n                                    </label>\r\n                                    <div class=\"col-md-8 col-sm-8 col-xs-12\">\r\n                                        ");
                EndContext();
                BeginContext(3057, 389, false);
                Write(
#line 89 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\LoadingProgram\LoadingProgramView.cshtml"
                                          Html.DevExtreme().DateBox()
                                       .Type(DateBoxType.Date)
                                       .DisplayFormat("dd / MM / YYYY")
                                       .Value(DateTime.Now)
                                       .Name("LoadingProgramDate")
                                       .ID("lpdate")
                                        

#line default
#line hidden
                );
                EndContext();
                BeginContext(3447, 2343, true);
                WriteLiteral(@"
                                        <span class=""error"" id=""lpdateerror""></span>

                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Reference Number
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control col-md-7 col-xs-12"" id=""rfNumber"" name=""ReferenceNumber"" required>
                                        <span class=""error"" id=""ReferenceNumbererror""></span>

                                    </div>
                                </div>
                            </div>

                        ");
                WriteLiteral(@"</div>

                        <div class=""form-horizontal form-label-left row"">

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Invoice Number
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control col-md-7 col-xs-12"" id=""invnumber"" name=""InvoiceNumber"">
                                        <span class=""error"" id=""InvoiceNumbererror""></span>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    ");
                WriteLiteral(@"<label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Invoice Date
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        ");
                EndContext();
                BeginContext(5792, 424, false);
                Write(
#line 135 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\LoadingProgram\LoadingProgramView.cshtml"
                                          Html.DevExtreme().DateBox()
                                               .Type(DateBoxType.Date)
                                                .DisplayFormat("dd / MM / YYYY")
                                               .Value(DateTime.Now)
                                               .Name("InvoiceDate")
                                               .ID("invDate")
                                        

#line default
#line hidden
                );
                EndContext();
                BeginContext(6217, 666, true);
                WriteLiteral(@"
                                        <span class=""error"" id=""invDateerror""></span>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Shipper
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        ");
                EndContext();
                BeginContext(6884, 131, false);
                Write(
#line 153 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\LoadingProgram\LoadingProgramView.cshtml"
                                         Html.DropDownList("ShipperId", Shipper, "Select Shipper", new { @class = "form-control", @id = "shipper", @required = "required" })

#line default
#line hidden
                );
                EndContext();
                BeginContext(7015, 776, true);
                WriteLiteral(@"
                                        <span class=""error"" id=""Shippererror""></span>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class=""form-horizontal form-label-left row"">

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Vessel
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        ");
                EndContext();
                BeginContext(7792, 202, false);
                Write(
#line 169 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\LoadingProgram\LoadingProgramView.cshtml"
                                         Html.DropDownList("VesselExportId", VesselExports, "Select Vessel Export", new { @class = "form-control", @id = "vesselExports", @required = "required", @onchange = "changed_VesselExport(this.value)" })

#line default
#line hidden
                );
                EndContext();
                BeginContext(7994, 674, true);
                WriteLiteral(@"
                                        <span class=""error"" id=""VesselExporterror""></span>

                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Voyage
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <div");
                EndContext();
                BeginWriteAttribute("class", " class=\"", 8668, "\"", 8676, 0);
                EndWriteAttribute();
                BeginContext(8677, 754, true);
                WriteLiteral(@" id=""voyageSelectBox"">


                                        </div>
                                        <span class=""error"" id=""VoyageExporterror""></span>
                                    </div>
                                </div>
                            </div>


                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Clearing Agent
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        ");
                EndContext();
                BeginContext(9432, 168, false);
                Write(
#line 197 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\LoadingProgram\LoadingProgramView.cshtml"
                                         Html.DropDownList("ClearingAgentExportId", ClearingAgent, "Select Clearing Agent", new { @class = "form-control", @id = "clearingAgentExport", @required = "required" })

#line default
#line hidden
                );
                EndContext();
                BeginContext(9600, 2894, true);
                WriteLiteral(@"
                                        <span class=""error"" id=""ClearingAgenterror""></span>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class=""form-horizontal form-label-left row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Vessel ETA
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control col-md-7 col-xs-12"" id=""eta"" readonly>
                                    </div>
                                </div>

                            </div>
                            <div class=");
                WriteLiteral(@"""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Vessel ETD
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control col-md-7 col-xs-12"" id=""etd"" readonly>
                                    </div>
                                </div>

                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Cargo Cut-Off
                                    </label>
                                    <div class=""col-md-8 col-s");
                WriteLiteral(@"m-8 col-xs-12"">
                                        <input type=""text"" class=""form-control col-md-7 col-xs-12"" id=""cutoff"" readonly>

                                    </div>
                                </div>

                            </div>




                        </div>



                        <div class=""form-horizontal form-label-left row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Shipping Agent
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        ");
                EndContext();
                BeginContext(12495, 162, false);
                Write(
#line 255 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\LoadingProgram\LoadingProgramView.cshtml"
                                         Html.DropDownList("ShippingAgentExportId", ShippingAgent, "Select Shipping Agent", new { @class = "form-control", @id = "shippingAgent", @required = "required" })

#line default
#line hidden
                );
                EndContext();
                BeginContext(12657, 679, true);
                WriteLiteral(@"
                                        <span class=""error"" id=""ShippingAgenterror""></span>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Loading Terminal
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        ");
                EndContext();
                BeginContext(13337, 163, false);
                Write(
#line 266 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\LoadingProgram\LoadingProgramView.cshtml"
                                         Html.DropDownList("PortAndTerminalId", PortAndTerminal, "Select Loading Terminal", new { @class = "form-control", @id = "loadingTeminal", @required = "required" })

#line default
#line hidden
                );
                EndContext();
                BeginContext(13500, 678, true);
                WriteLiteral(@"
                                        <span class=""error"" id=""PortAndTerminalerror""></span>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Destination
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        ");
                EndContext();
                BeginContext(14179, 146, false);
                Write(
#line 278 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\LoadingProgram\LoadingProgramView.cshtml"
                                         Html.DropDownList("Destination", Destinations, "Select Destination", new { @class = "form-control", @id = "destination", @required = "required" })

#line default
#line hidden
                );
                EndContext();
                BeginContext(14325, 783, true);
                WriteLiteral(@"
                                        <span class=""error"" id=""Destinationerror""></span>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class=""form-horizontal form-label-left row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Shipping Line
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        ");
                EndContext();
                BeginContext(15109, 152, false);
                Write(
#line 292 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Export\Views\LoadingProgram\LoadingProgramView.cshtml"
                                         Html.DropDownList("ShippingLineId", ShippingLine, "Select Shipping Line", new { @class = "form-control", @id = "shippingLine", @required = "required" })

#line default
#line hidden
                );
                EndContext();
                BeginContext(15261, 278, true);
                WriteLiteral(@"
                                        <span class=""error"" id=""ShippingLineerror""></span>
                                    </div>
                                </div>
                            </div>




                        </div>


                    ");
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
            BeginContext(15546, 1233, true);
            WriteLiteral(@"

                    <div class=""clearfix""></div>
                    <div class=""row"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <div class=""x_panel"">
                                <div class=""x_content"">
                                    <div id=""loadingPrograms""></div>
                                    <div class=""ln_solid""></div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class=""form-group"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <button type=""button"" id=""btnCancel"" class=""btn btn-primary"" onClick=""restform()"" style=""float:left;"">Cancel</button>
                            <button id=""btnSubmit"" type=""button"" onclick=""submitLoadingProgram()"" class=""btn btn-success"">Submit</button>
                            <a id=""btnSubmitUpdate"" class=""btn btn-success"" onc");
            WriteLiteral("lick=\"updateLoadingProgram()\" style=\"display:none\">Update</a>\r\n\r\n                        </div>\r\n\r\n\r\n                    </div>\r\n\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
