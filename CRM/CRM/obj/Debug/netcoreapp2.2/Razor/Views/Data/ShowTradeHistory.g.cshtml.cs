#pragma checksum "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Data/ShowTradeHistory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2c7a32b54953594a5012bc8a2336d9f4aa9d9e80"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Data_ShowTradeHistory), @"mvc.1.0.view", @"/Views/Data/ShowTradeHistory.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Data/ShowTradeHistory.cshtml", typeof(AspNetCore.Views_Data_ShowTradeHistory))]
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
#line 1 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/_ViewImports.cshtml"
using CRM;

#line default
#line hidden
#line 2 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/_ViewImports.cshtml"
using CRM.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2c7a32b54953594a5012bc8a2336d9f4aa9d9e80", @"/Views/Data/ShowTradeHistory.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a3b16a7ccb85590fca9010f5f3fac5f1b6b0aa4c", @"/Views/_ViewImports.cshtml")]
    public class Views_Data_ShowTradeHistory : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
            BeginContext(0, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 2 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Data/ShowTradeHistory.cshtml"
  
    Layout = "~/Views/Shared/Data/TradeHistoryLayout.cshtml";

#line default
#line hidden
            BeginContext(68, 26, true);
            WriteLiteral("\n\n<!DOCTYPE html>\n\n<html>\n");
            EndContext();
            BeginContext(94, 258, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2c7a32b54953594a5012bc8a2336d9f4aa9d9e804859", async() => {
                BeginContext(100, 100, true);
                WriteLiteral("\n    <meta name=\"viewport\" content=\"width=device-width\" />\n    <title>ShowTradeHistory</title>\n\n    ");
                EndContext();
                BeginContext(200, 69, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2c7a32b54953594a5012bc8a2336d9f4aa9d9e805341", async() => {
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
                BeginContext(269, 5, true);
                WriteLiteral("\n    ");
                EndContext();
                BeginContext(274, 70, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2c7a32b54953594a5012bc8a2336d9f4aa9d9e806660", async() => {
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
                BeginContext(344, 1, true);
                WriteLiteral("\n");
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
            BeginContext(352, 1, true);
            WriteLiteral("\n");
            EndContext();
            BeginContext(353, 1943, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2c7a32b54953594a5012bc8a2336d9f4aa9d9e808759", async() => {
                BeginContext(359, 104, true);
                WriteLiteral("\n    <div>\n\n        <div style=\"text-align:center\">\n            <span>\n                Суммарный объем: ");
                EndContext();
                BeginContext(464, 18, false);
#line 22 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Data/ShowTradeHistory.cshtml"
                            Write(ViewBag.summVolume);

#line default
#line hidden
                EndContext();
                BeginContext(482, 561, true);
                WriteLiteral(@"
            </span>
        </div>

        <table class=""table table-bordered table-striped"" style="" margin: auto; width:60%"" id=""myTable"">
            <thead>
                <tr>
                    <td><p>Валюта:</p></td>
                    <td><p>Дата:</p></td>
                    <td><p>Тип сделки:</p></td>
                    <td><p>Время ордера:</p></td>
                    <td><p>Цена:</p></td>
                    <td><p>Объем:</p></td>
                    <td><p>Ситуация:</p></td>
                </tr>
            </thead>
            <tbody>
");
                EndContext();
#line 39 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Data/ShowTradeHistory.cshtml"
                 foreach (var value in ViewBag.show)
                {

#line default
#line hidden
                BeginContext(1114, 56, true);
                WriteLiteral("                    <tr>\n                        <td><p>");
                EndContext();
                BeginContext(1171, 18, false);
#line 42 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Data/ShowTradeHistory.cshtml"
                          Write(value.CurrencyName);

#line default
#line hidden
                EndContext();
                BeginContext(1189, 38, true);
                WriteLiteral("</p></td>\n                        <td>");
                EndContext();
                BeginContext(1228, 44, false);
#line 43 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Data/ShowTradeHistory.cshtml"
                       Write(value.Date.ToString("dd MMMM yyyy hh:mm:ss"));

#line default
#line hidden
                EndContext();
                BeginContext(1272, 37, true);
                WriteLiteral("</td>\n                        <td><p>");
                EndContext();
                BeginContext(1310, 10, false);
#line 44 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Data/ShowTradeHistory.cshtml"
                          Write(value.Side);

#line default
#line hidden
                EndContext();
                BeginContext(1320, 41, true);
                WriteLiteral("</p></td>\n                        <td><p>");
                EndContext();
                BeginContext(1362, 25, false);
#line 45 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Data/ShowTradeHistory.cshtml"
                          Write(value.OrderTime.TimeOfDay);

#line default
#line hidden
                EndContext();
                BeginContext(1387, 41, true);
                WriteLiteral("</p></td>\n                        <td><p>");
                EndContext();
                BeginContext(1429, 11, false);
#line 46 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Data/ShowTradeHistory.cshtml"
                          Write(value.Price);

#line default
#line hidden
                EndContext();
                BeginContext(1440, 41, true);
                WriteLiteral("</p></td>\n                        <td><p>");
                EndContext();
                BeginContext(1482, 12, false);
#line 47 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Data/ShowTradeHistory.cshtml"
                          Write(value.Volume);

#line default
#line hidden
                EndContext();
                BeginContext(1494, 41, true);
                WriteLiteral("</p></td>\n                        <td><p>");
                EndContext();
                BeginContext(1536, 21, false);
#line 48 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Data/ShowTradeHistory.cshtml"
                          Write(value.MarketSituation);

#line default
#line hidden
                EndContext();
                BeginContext(1557, 36, true);
                WriteLiteral("</p></td>\n                    </tr>\n");
                EndContext();
#line 50 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Data/ShowTradeHistory.cshtml"
                }

#line default
#line hidden
                BeginContext(1611, 678, true);
                WriteLiteral(@"            </tbody>
        </table>
        <script type=""text/javascript"">

            $('#myTable').tablesorter(
                {
                    dateFormat: 'mmddYYYY',
                    headers: {
                        0: { sorter: false },
                        1: { sorter: ""shortDate"", dateFormat: ""ddmmyyyy"" },
                        2: { sorter: false },
                        3: { sorter: false },
                        4: { sorter: false },
                        5: { sorter: true },
                        6: { sorter: true },
                        7: { sorter: false }
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
            BeginContext(2296, 9, true);
            WriteLiteral("\n</html>\n");
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
