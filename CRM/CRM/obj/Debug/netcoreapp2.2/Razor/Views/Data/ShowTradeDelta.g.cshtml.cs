#pragma checksum "C:\Users\arsen\source\repos\CRM\CRM\Views\Data\ShowTradeDelta.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9af393b02144f35587fe61569b248fb55df1199f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Data_ShowTradeDelta), @"mvc.1.0.view", @"/Views/Data/ShowTradeDelta.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Data/ShowTradeDelta.cshtml", typeof(AspNetCore.Views_Data_ShowTradeDelta))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\arsen\source\repos\CRM\CRM\Views\_ViewImports.cshtml"
using CRM;

#line default
#line hidden
#line 2 "C:\Users\arsen\source\repos\CRM\CRM\Views\_ViewImports.cshtml"
using CRM.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9af393b02144f35587fe61569b248fb55df1199f", @"/Views/Data/ShowTradeDelta.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03934a8a7d47f2a43fed5950159610447dac5492", @"/Views/_ViewImports.cshtml")]
    public class Views_Data_ShowTradeDelta : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/jquery-latest.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/jquery.tablesorter.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\arsen\source\repos\CRM\CRM\Views\Data\ShowTradeDelta.cshtml"
  
    Layout = "~/Views/Shared/Data/TradeDeltaLayout.cshtml";

#line default
#line hidden
            BeginContext(68, 27, true);
            WriteLiteral("<!DOCTYPE html>\r\n\r\n<html>\r\n");
            EndContext();
            BeginContext(95, 262, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9af393b02144f35587fe61569b248fb55df1199f4749", async() => {
                BeginContext(101, 102, true);
                WriteLiteral("\r\n    <meta name=\"viewport\" content=\"width=device-width\" />\r\n    <title>ShowTradeDelta</title>\r\n\r\n    ");
                EndContext();
                BeginContext(203, 69, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9af393b02144f35587fe61569b248fb55df1199f5242", async() => {
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
                BeginContext(272, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(278, 70, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9af393b02144f35587fe61569b248fb55df1199f6582", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(348, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(357, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(359, 1511, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9af393b02144f35587fe61569b248fb55df1199f8719", async() => {
                BeginContext(365, 108, true);
                WriteLiteral("\r\n    <div>\r\n        <div style=\"text-align:center\">\r\n            <span>\r\n                Суммарная дельта: ");
                EndContext();
                BeginContext(474, 17, false);
#line 18 "C:\Users\arsen\source\repos\CRM\CRM\Views\Data\ShowTradeDelta.cshtml"
                             Write(ViewBag.summDelta);

#line default
#line hidden
                EndContext();
                BeginContext(491, 440, true);
                WriteLiteral(@"
            </span>
        </div>
        <table class=""table table-bordered table-striped"" style="" margin: auto; width:60%"" id=""myTable"">
            <thead>
                <tr>
                    <td><p>Валюта:</p></td>
                    <td><p>Время от:</p></td>
                    <td><p>Время до:</p></td>
                    <td><p>Дельта:</p></td>

                </tr>
            </thead>
            <tbody>
");
                EndContext();
#line 32 "C:\Users\arsen\source\repos\CRM\CRM\Views\Data\ShowTradeDelta.cshtml"
                 foreach (var value in ViewBag.show)
                {

#line default
#line hidden
                BeginContext(1004, 57, true);
                WriteLiteral("                    <tr>\r\n                        <td><p>");
                EndContext();
                BeginContext(1062, 18, false);
#line 35 "C:\Users\arsen\source\repos\CRM\CRM\Views\Data\ShowTradeDelta.cshtml"
                          Write(value.CurrencyName);

#line default
#line hidden
                EndContext();
                BeginContext(1080, 42, true);
                WriteLiteral("</p></td>\r\n                        <td><p>");
                EndContext();
                BeginContext(1123, 23, false);
#line 36 "C:\Users\arsen\source\repos\CRM\CRM\Views\Data\ShowTradeDelta.cshtml"
                          Write(value.TimeFrom.DateTime);

#line default
#line hidden
                EndContext();
                BeginContext(1146, 42, true);
                WriteLiteral("</p></td>\r\n                        <td><p>");
                EndContext();
                BeginContext(1189, 21, false);
#line 37 "C:\Users\arsen\source\repos\CRM\CRM\Views\Data\ShowTradeDelta.cshtml"
                          Write(value.TimeTo.DateTime);

#line default
#line hidden
                EndContext();
                BeginContext(1210, 42, true);
                WriteLiteral("</p></td>\r\n                        <td><p>");
                EndContext();
                BeginContext(1253, 11, false);
#line 38 "C:\Users\arsen\source\repos\CRM\CRM\Views\Data\ShowTradeDelta.cshtml"
                          Write(value.Delta);

#line default
#line hidden
                EndContext();
                BeginContext(1264, 38, true);
                WriteLiteral("</p></td>\r\n                    </tr>\r\n");
                EndContext();
#line 40 "C:\Users\arsen\source\repos\CRM\CRM\Views\Data\ShowTradeDelta.cshtml"
                }

#line default
#line hidden
                BeginContext(1321, 542, true);
                WriteLiteral(@"            </tbody>
        </table>
        <script type=""text/javascript"">

            $('#myTable').tablesorter(
                {
                    dateFormat: 'mmddYYYY',
                    headers: {
                        0: { sorter: false },
                        1: { sorter: ""shortDate"", dateFormat: ""ddmmyyyy"" },
                        2: { sorter: ""shortDate"", dateFormat: ""ddmmyyyy"" },
                        3: { sorter: true }
                    }
                });

        </script>
    </div>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1870, 11, true);
            WriteLiteral("\r\n</html>\r\n");
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
