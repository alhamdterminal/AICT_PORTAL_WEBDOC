#pragma checksum "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\Shared\_Filters_Container.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "32af0f48b9ce54306bfe318df510ea9fe2fb62e81e60dcea6649b91447959142"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Import_Views_Shared__Filters_Container), @"mvc.1.0.view", @"/Areas/Import/Views/Shared/_Filters_Container.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Import/Views/Shared/_Filters_Container.cshtml", typeof(AspNetCore.Areas_Import_Views_Shared__Filters_Container))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"32af0f48b9ce54306bfe318df510ea9fe2fb62e81e60dcea6649b91447959142", @"/Areas/Import/Views/Shared/_Filters_Container.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Import/_ViewImports.cshtml")]
    public class Areas_Import_Views_Shared__Filters_Container : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2286, true);
            WriteLiteral(@"
<script>

    var voyagList = [];

    var igm;

    function igm_changed(data) {
        igm = data.value;
        console.log(igm)

        $.get('/APICalls/GetVessel?IGM=' + igm, function (data) {

            if (data) {

                $('#igmyear').val(data.igmYear);
                $('#vessel').val(data.vesselName);
                console.log(data.voyages);
                voyagList = data.voyages;
                console.log(voyagList)
                // var voyageNo = data.voyages[data.voyages.length - 1].voyageNo;
                var index = voyagList.findIndex(t => t.voyageNo == data.voyages[data.voyages.length - 1].voyageNo);

                $(""#searchBox"").dxSelectBox({
                    dataSource: voyagList,
                    displayExpr: ""voyageNo"",
                    value: voyagList[index],
                    acceptCustomValue: true,
                    onValueChanged: function (data) {
                        voyageNo = data.value.voyageNo;
          ");
            WriteLiteral(@"              $.get('/APICalls/GetContainersDropdown?VoyageNo=' + voyageNo + ""&IGM="" + igm, function (containerdb) {
                            $('#containerdiv').html(containerdb);
                            //  ChangeContainerIndexDropbox();
                            containerChangeCallback(""container"");

                        });
                    },

                })

                var voyageNo = data.voyages[data.voyages.length - 1].voyageNo;
                $('#voyageno').val(voyageNo);

                //  $('#voyageno').val(voyageNo);
                //  $('#voyageno').val(voyageNo);

                $.get('/APICalls/GetContainersDropdownList?VoyageNo=' + voyageNo + ""&IGM="" + igm, function (containerdb) {


                    console.log(""containerdb"", containerdb)

                    $('#containerdiv').html(containerdb);

                    containerChangeCallback(""container"");

                });
            }

        });
    }

    $(function () {

");
            WriteLiteral("        $(\'#containerdiv\').on(\'change\', function (val) {\r\n\r\n            containerChangeCallback(\"container\");\r\n\r\n        });\r\n\r\n\r\n\r\n\r\n    });\r\n\r\n</script>\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-md-12 col-sm-12 col-xs-12\">\r\n        <div");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 2286, "\"", 2294, 0);
            EndWriteAttribute();
            BeginContext(2295, 21, true);
            WriteLiteral(">\r\n\r\n            <div");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 2316, "\"", 2324, 0);
            EndWriteAttribute();
            BeginContext(2325, 471, true);
            WriteLiteral(@">



                <div class=""row"">
                    <div class=""col-md-4 col-sm-4 col-xs-12"">
                        <div class=""item form-group"">
                            <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                IGM: <span class=""required"">*</span>
                            </label>
                            <div class=""col-md-8 col-sm-8 col-xs-12"">
                                ");
            EndContext();
            BeginContext(2798, 450, false);
            Write(
#line 92 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\Shared\_Filters_Container.cshtml"
                                  Html.DevExtreme()
                                .Autocomplete()
                                .ID("IGM")
                                .DataSource(d => d.Mvc()
                                .Controller("APICalls")
                                .Area("")
                                .LoadAction("GetIGMS"))
                                .Placeholder("Type IGM...")
                                .OnValueChanged("igm_changed")

#line default
#line hidden
            );
            EndContext();
            BeginContext(3249, 1551, true);
            WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                    <div class=""col-md-4 col-sm-4 col-xs-12"">
                        <div class=""item form-group"">
                            <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                Year:
                            </label>
                            <div class=""col-md-8 col-sm-8 col-xs-12"">
                                <input type=""text"" class=""form-control"" id=""igmyear"">
                            </div>
                        </div>
                    </div>
                    <div class=""col-md-4 col-sm-4 col-xs-12"">
                        <div class=""item form-group"">
                            <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                Vessel:
                            </label>
                            <div class=""col-md-8 col-sm-8 col-xs-12"">
             ");
            WriteLiteral(@"                   <input type=""text"" class=""form-control"" id=""vessel"">
                            </div>
                        </div>
                    </div>

                </div>

                <div class=""row"">
                    <div class=""col-md-4 col-sm-4 col-xs-12"">
                        <div class=""item form-group"">
                            <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                Voyage:
                            </label>
");
            EndContext();
            BeginContext(5008, 787, true);
            WriteLiteral(@"                            <div class=""col-md-8 col-sm-8 col-xs-12"" id=""searchBox"">


                            </div>
                        </div>
                    </div>
                    <div class=""col-md-4 col-sm-4 col-xs-12"">
                        <div class=""form-group"">
                            <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                Container # <span class=""required"">*</span>
                            </label>
                            <div class=""col-md-8 col-sm-8 col-xs-12"" id=""containerdiv"">

                            </div>
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