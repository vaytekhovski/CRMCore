#pragma checksum "D:\REPOSITORY\CRM\CRM\CRM\Views\Data\ShowOrderBookBids.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "21816820c36b3977a7de732f00a94185dd31318a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Data_ShowOrderBookBids), @"mvc.1.0.view", @"/Views/Data/ShowOrderBookBids.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Data/ShowOrderBookBids.cshtml", typeof(AspNetCore.Views_Data_ShowOrderBookBids))]
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
#line 1 "D:\REPOSITORY\CRM\CRM\CRM\Views\_ViewImports.cshtml"
using CRM;

#line default
#line hidden
#line 2 "D:\REPOSITORY\CRM\CRM\CRM\Views\_ViewImports.cshtml"
using CRM.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"21816820c36b3977a7de732f00a94185dd31318a", @"/Views/Data/ShowOrderBookBids.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03934a8a7d47f2a43fed5950159610447dac5492", @"/Views/_ViewImports.cshtml")]
    public class Views_Data_ShowOrderBookBids : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
            BeginContext(0, 6, true);
            WriteLiteral("\r\n\r\n\r\n");
            EndContext();
#line 4 "D:\REPOSITORY\CRM\CRM\CRM\Views\Data\ShowOrderBookBids.cshtml"
  
    Layout = "~/Views/Shared/Data/OrderBookLayout.cshtml";

#line default
#line hidden
            BeginContext(73, 31, true);
            WriteLiteral("\r\n\r\n<!DOCTYPE html>\r\n\r\n<html>\r\n");
            EndContext();
            BeginContext(104, 267, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "21816820c36b3977a7de732f00a94185dd31318a4850", async() => {
                BeginContext(110, 105, true);
                WriteLiteral("\r\n    <meta name=\"viewport\" content=\"width=device-width\" />\r\n    <title>ShowOrderBookBids</title>\r\n\r\n    ");
                EndContext();
                BeginContext(215, 69, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "21816820c36b3977a7de732f00a94185dd31318a5346", async() => {
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
                BeginContext(284, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(290, 70, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "21816820c36b3977a7de732f00a94185dd31318a6686", async() => {
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
                BeginContext(360, 4, true);
                WriteLiteral("\r\n\r\n");
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
            BeginContext(371, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(373, 1652, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "21816820c36b3977a7de732f00a94185dd31318a8827", async() => {
                BeginContext(379, 109, true);
                WriteLiteral("\r\n    <div>\r\n\r\n        <div style=\"text-align:center\">\r\n            <span>\r\n                Суммарный объем: ");
                EndContext();
                BeginContext(489, 18, false);
#line 25 "D:\REPOSITORY\CRM\CRM\CRM\Views\Data\ShowOrderBookBids.cshtml"
                            Write(ViewBag.summVolume);

#line default
#line hidden
                EndContext();
                BeginContext(507, 478, true);
                WriteLiteral(@"
            </span>
        </div>

        <table class=""table table-bordered table-striped"" style="" margin: auto; width:60%"" id=""myTable"">
            <thead>
                <tr>
                    <td><p>Валюта:</p></td>
                    <td><p>Дата:</p></td>
                    <td><p>Цена:</p></td>
                    <td><p>Объем:</p></td>
                    <td><p>Ситуация:</p></td>
                </tr>
            </thead>
            <tbody>
");
                EndContext();
#line 40 "D:\REPOSITORY\CRM\CRM\CRM\Views\Data\ShowOrderBookBids.cshtml"
                 foreach (var value in ViewBag.show)
                {

#line default
#line hidden
                BeginContext(1058, 54, true);
                WriteLiteral("                    <tr>\r\n                        <td>");
                EndContext();
                BeginContext(1113, 18, false);
#line 43 "D:\REPOSITORY\CRM\CRM\CRM\Views\Data\ShowOrderBookBids.cshtml"
                       Write(value.CurrencyName);

#line default
#line hidden
                EndContext();
                BeginContext(1131, 35, true);
                WriteLiteral("</td>\r\n                        <td>");
                EndContext();
                BeginContext(1167, 44, false);
#line 44 "D:\REPOSITORY\CRM\CRM\CRM\Views\Data\ShowOrderBookBids.cshtml"
                       Write(value.Date.ToString("dd MMMM yyyy hh:mm:ss"));

#line default
#line hidden
                EndContext();
                BeginContext(1211, 35, true);
                WriteLiteral("</td>\r\n                        <td>");
                EndContext();
                BeginContext(1247, 11, false);
#line 45 "D:\REPOSITORY\CRM\CRM\CRM\Views\Data\ShowOrderBookBids.cshtml"
                       Write(value.Price);

#line default
#line hidden
                EndContext();
                BeginContext(1258, 35, true);
                WriteLiteral("</td>\r\n                        <td>");
                EndContext();
                BeginContext(1294, 12, false);
#line 46 "D:\REPOSITORY\CRM\CRM\CRM\Views\Data\ShowOrderBookBids.cshtml"
                       Write(value.Volume);

#line default
#line hidden
                EndContext();
                BeginContext(1306, 35, true);
                WriteLiteral("</td>\r\n                        <td>");
                EndContext();
                BeginContext(1342, 21, false);
#line 47 "D:\REPOSITORY\CRM\CRM\CRM\Views\Data\ShowOrderBookBids.cshtml"
                       Write(value.MarketSituation);

#line default
#line hidden
                EndContext();
                BeginContext(1363, 34, true);
                WriteLiteral("</td>\r\n                    </tr>\r\n");
                EndContext();
#line 49 "D:\REPOSITORY\CRM\CRM\CRM\Views\Data\ShowOrderBookBids.cshtml"
                }

#line default
#line hidden
                BeginContext(1416, 602, true);
                WriteLiteral(@"            </tbody>
        </table>
        <script type=""text/javascript"">

            $('#myTable').tablesorter(
                {
                    dateFormat: 'mmddYYYY',
                    headers: {
                        0: { sorter: true },
                        1: { sorter: ""shortDate"", dateFormat: ""ddmmyyyy"" },
                        2: { sorter: true },
                        3: { sorter: true },
                        4: { sorter: true },
                        5: { sorter: true }
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
            BeginContext(2025, 11, true);
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
