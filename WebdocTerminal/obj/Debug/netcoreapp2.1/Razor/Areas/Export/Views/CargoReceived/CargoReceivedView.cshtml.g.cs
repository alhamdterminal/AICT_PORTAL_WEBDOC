#pragma checksum "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\CargoReceived\CargoReceivedView.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "a856042933fcc680741076091edc2eaa6ee6ec2e2d9eb833f06ee9aad615f606"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Export_Views_CargoReceived_CargoReceivedView), @"mvc.1.0.view", @"/Areas/Export/Views/CargoReceived/CargoReceivedView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Export/Views/CargoReceived/CargoReceivedView.cshtml", typeof(AspNetCore.Areas_Export_Views_CargoReceived_CargoReceivedView))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"a856042933fcc680741076091edc2eaa6ee6ec2e2d9eb833f06ee9aad615f606", @"/Areas/Export/Views/CargoReceived/CargoReceivedView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Export/Views/_ViewImports.cshtml")]
    public class Areas_Export_Views_CargoReceived_CargoReceivedView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/CargoReceivedView.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "F", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "P", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal form-label-left"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("CargoReceivedForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 1273, true);
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

    input[type=""date""] {
        color: #fff;
        position: relative;
    }

        input[type=""date""]::-webkit-datetime-(t-year-field {
            position: absolute !important;
            border-left: 1px solid #8c8c8c;
            padding: 2px;
            color: #000;
            left: 56px;
        } input[type=""date""]::-webkit-datetime-edit-month-field {
            position: absolute !important;
            border-left: 1px solid #8c8c8c;
            padding: 2px;
           ");
            WriteLiteral(" color: #000;\r\n            left: 26px;\r\n        } input[type=\"date\"]::-webkit-datetime-edit-day-field {\r\n            position: absolute !important;\r\n            color: #000;\r\n            padding: 2px;\r\n            left: 4px;\r\n        }\r\n</style>\r\n\r\n");
            EndContext();
            BeginContext(1273, 56, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a856042933fcc680741076091edc2eaa6ee6ec2e2d9eb833f06ee9aad615f6067396", async() => {
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
            BeginContext(1329, 604, true);
            WriteLiteral(@"

<div class=""right_col"" role=""main"">
    <div class=""page-title"">
        <div class=""row"">
            <div class=""col-md-11"">
                <div class=""title_left"">
                    <h3>Cargo Received</h3>
                </div>
            </div>
            <div class=""col-md-1"">
                <div class=""title_right"">
                </div>
            </div>
        </div>
    </div>
    <div class=""clearfix""></div>

    <div class=""row"">
        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">

                <div class=""x_content""");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 1933, "\"", 1938, 0);
            EndWriteAttribute();
            BeginContext(1939, 23, true);
            WriteLiteral(">\r\n                    ");
            EndContext();
            BeginContext(1962, 13925, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a856042933fcc680741076091edc2eaa6ee6ec2e2d9eb833f06ee9aad615f6069462", async() => {
                BeginContext(2031, 551, true);
                WriteLiteral(@"

                        <div class=""form-horizontal form-label-left row"">
                            <div class=""col-md-4"">

                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        LP Number: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        ");
                EndContext();
                BeginContext(2584, 903, false);
                Write(
#line 87 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\CargoReceived\CargoReceivedView.cshtml"
                                          Html.DevExtreme()
                                              .SelectBox()
                                              .ID("loadingProgramDetail")
                                              .ValueExpr("value")
                                              .DisplayExpr("text")
                                              .AcceptCustomValue(true)
                                              .SearchEnabled(true)
                                              .DataSource(d => d.Mvc()
                                                .Controller("APICalls")
                                                .Area("")
                                                .LoadAction("GetUnGateInLoadingProgramNumbers"))
                                                .Placeholder("Type Loading Program ...")
                                                .OnValueChanged("lpnumber_change")

#line default
#line hidden
                );
                EndContext();
                BeginContext(3488, 592, true);
                WriteLiteral(@"
                                    </div>
                                </div>


                            </div>

                            <div class=""col-md-4"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Truck Number
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"" id=""truckNumberBox"">
                                        <div");
                EndContext();
                BeginWriteAttribute("class", " class=\"", 4080, "\"", 4088, 0);
                EndWriteAttribute();
                BeginContext(4089, 4854, true);
                WriteLiteral(@" id=""TruckNumberBox"">
                                        </div>
                                        <span class=""error"" id=""TruckNumbererror""></span>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4"">
                            </div>


                        </div>

                        <div class=""form-horizontal form-label-left row"">
                            <div class=""col-md-4"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        GD Number
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""gdNumber"" readonly>
                                    <");
                WriteLiteral(@"/div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Vehicle Received Date
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""date"" class=""form-control"" id=""vehicleReceivedDate"" readonly>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                    ");
                WriteLiteral(@"    Clearing Agent
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""clearingAgent"" readonly>
                                        <span class=""error"" id=""ClearingAgenterror""></span>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class=""ln_solid""></div>
                        <div class=""form-horizontal form-label-left row"">

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Agent Representative
                                    </label>
                                    <div ");
                WriteLiteral(@"class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" id=""cLearingAgentReprsentative"" class=""form-control"" name=""CLearingAgentReprsentative"" required>
                                        <span class=""error"" id=""CLearingAgentReprsentativeerror""></span>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Agent CNIC
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" id=""clearingAgentCNIC"" class=""form-control"" name=""ClearingAgentCNIC"" required>
                                        <span c");
                WriteLiteral(@"lass=""error"" id=""ClearingAgentCNICerror""></span>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Shipment Status (F/P)
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <select class=""form-control"" id=""packageType"" name=""PackageType"" required>
                                            ");
                EndContext();
                BeginContext(8943, 41, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a856042933fcc680741076091edc2eaa6ee6ec2e2d9eb833f06ee9aad615f60617752", async() => {
                    BeginContext(8969, 6, true);
                    WriteLiteral("Select");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("disabled", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(8984, 46, true);
                WriteLiteral("\r\n                                            ");
                EndContext();
                BeginContext(9030, 28, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a856042933fcc680741076091edc2eaa6ee6ec2e2d9eb833f06ee9aad615f60619723", async() => {
                    BeginContext(9048, 1, true);
                    WriteLiteral("F");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(9058, 46, true);
                WriteLiteral("\r\n                                            ");
                EndContext();
                BeginContext(9104, 28, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a856042933fcc680741076091edc2eaa6ee6ec2e2d9eb833f06ee9aad615f60621252", async() => {
                    BeginContext(9122, 1, true);
                    WriteLiteral("P");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(9132, 4015, true);
                WriteLiteral(@"
                                        </select>
                                        <span class=""error"" id=""PackageTypeerror""></span>
                                    </div>
                                </div>
                            </div>

                        </div>


                        <div class=""form-horizontal form-label-left row"">

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Driver Name
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" id=""driverName"" class=""form-control"" name=""DriverName"" required>
                                        <span class=""error"" id=""DriverNameerror""></span>
            ");
                WriteLiteral(@"                        </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Driver CNIC
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" id=""driverCNIC"" class=""form-control"" name=""DriverCNIC"" required>
                                        <span class=""error"" id=""DriverCNICerror""></span>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4"">
                                <div class=""item form-group"">
                                    <label class");
                WriteLiteral(@"=""col-md-4"" for=""name"">
                                        Weight Declared By Driver
                                    </label>
                                    <div class=""col-md-8"">
                                        <input type=""number"" step=""any"" id=""weightDeclaredByDriver"" class=""form-control"" name=""WeightDeclaredByDriver"" required>
                                        <span class=""error"" id=""WeightDeclaredByDrivererror""></span>
                                    </div>
                                </div>
                            </div>


                        </div>

                        <div class=""form-horizontal form-label-left row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        No Of Packages Received
                        ");
                WriteLiteral(@"            </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""number"" id=""numberOfPackagesReceived"" class=""form-control"" name=""NumberOfPackagesReceived"" required>
                                        <span class=""error"" id=""NumberOfPackagesReceivederror""></span>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Receving Start
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        ");
                EndContext();
                BeginContext(13149, 436, false);
                Write(
#line 262 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\CargoReceived\CargoReceivedView.cshtml"
                                          Html.DevExtreme().DateBox()
                                           .Type(DateBoxType.DateTime)
                                           .DisplayFormat("dd / MM / YYYY, hh:mm:ss")
                                           .Value(DateTime.Now)
                                           .Name("RecevingStartTime")
                                            .ID("recevingStartTime")

                                        

#line default
#line hidden
                );
                EndContext();
                BeginContext(13586, 678, true);
                WriteLiteral(@"
                                        <span class=""error"" id=""RecevingStartTimeerror""></span>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Receving End
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        ");
                EndContext();
                BeginContext(14266, 497, false);
                Write(
#line 280 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\CargoReceived\CargoReceivedView.cshtml"
                                          Html.DevExtreme().DateBox()
                                                        .Type(DateBoxType.DateTime)
                                                         .DisplayFormat("dd / MM / YYYY, hh:mm:ss")
                                                        .Value(DateTime.Now)
                                                        .Name("RecevingEndTime")
                                                        .ID("recevingEndTime")

                                        

#line default
#line hidden
                );
                EndContext();
                BeginContext(14764, 1116, true);
                WriteLiteral(@"
                                        <span class=""error"" id=""RecevingEndTimeerror""></span>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class=""form-horizontal form-label-left row"">

                            <div class=""col-md-4"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4"" for=""name"">
                                        C R Condition
                                    </label>
                                    <div class=""col-md-8"">
                                        <input type=""text"" id=""cargoRecevingCondition"" class=""form-control"" name=""CargoRecevingCondition"" required>
                                        <span class=""error"" id=""CargoRecevingConditionerror""></span>
                                    </div>
                                </div>");
                WriteLiteral("\r\n                            </div>\r\n\r\n                        </div>\r\n                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(15887, 1366, true);
            WriteLiteral(@"
                    <div class=""form-group"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <button id=""btnSubmit"" type=""button"" style=""float:right;"" onclick=""AddCargoReceived()"" class=""btn btn-success"">Submit</button>
                            <a id=""btnSubmitUpdate"" class=""btn btn-success"" onclick=""updateCargoReceived()"" style=""display:none ; float: right;"">Update</a>
                            <a id=""btnEDI"" class=""btn btn-warning"" onclick=""gateInEDI()"" style=""float: right;"">Gate-in & Create EDI</a>
                            <button type=""button"" id=""btnCancel"" class=""btn btn-primary"" onclick=""restform()"" style=""float:right;"">Cancel</button>
                            <a id=""btn2"" class=""btn btn-info"" onclick=""routetoCIRrepost()"" style=""display:none; float: right;"">Print</a>

                        </div>
                    </div>
                </div>
            </div>
        </div> 
    </div>
    <div class=""clearfix""></div>
  ");
            WriteLiteral(@"  <div class=""row"">
        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">
                <div class=""x_content"">
                    <div id=""loadingProgramDetal""></div>
                    <div class=""ln_solid""></div>
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