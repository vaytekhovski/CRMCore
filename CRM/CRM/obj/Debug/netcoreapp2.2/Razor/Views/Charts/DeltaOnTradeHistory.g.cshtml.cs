#pragma checksum "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f44e0455cfb250d912b2b55e101ea93bfeb26352"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Charts_DeltaOnTradeHistory), @"mvc.1.0.view", @"/Views/Charts/DeltaOnTradeHistory.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Charts/DeltaOnTradeHistory.cshtml", typeof(AspNetCore.Views_Charts_DeltaOnTradeHistory))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f44e0455cfb250d912b2b55e101ea93bfeb26352", @"/Views/Charts/DeltaOnTradeHistory.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a3b16a7ccb85590fca9010f5f3fac5f1b6b0aa4c", @"/Views/_ViewImports.cshtml")]
    public class Views_Charts_DeltaOnTradeHistory : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
  
    Layout = "~/Views/Shared/Charts/ChartsLayout.cshtml";

#line default
#line hidden
            BeginContext(63, 25, true);
            WriteLiteral("\n<!DOCTYPE html>\n\n<html>\n");
            EndContext();
            BeginContext(88, 578, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f44e0455cfb250d912b2b55e101ea93bfeb263523568", async() => {
                BeginContext(94, 565, true);
                WriteLiteral(@"
    <meta name=""viewport"" content=""width=device-width"" />
    <title>DeltaOnTradeHistory</title>

    <script src=""https://cdn.plot.ly/plotly-latest.min.js""></script>

    <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"">
    <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js""></script>
    <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js""></script>
");
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
            BeginContext(666, 1, true);
            WriteLiteral("\n");
            EndContext();
            BeginContext(667, 2658, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f44e0455cfb250d912b2b55e101ea93bfeb263525307", async() => {
                BeginContext(673, 453, true);
                WriteLiteral(@"
    <div>
        <div>
            <div>
                <div id=""Chart"">

                </div>

            </div>
            <script type=""text/javascript"">

            CHART = document.getElementById('Chart');

                var datesDelta = [];
                var deltaValues = [];

                var datesTHBuy = [];
                var THBuyValues = [];

                var datesTHSell = [];
                var THSellValues = [];




");
                EndContext();
#line 44 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
             foreach (var item in ViewBag.datesDelta)
            {
                

#line default
#line hidden
                BeginContext(1216, 46, true);
                WriteLiteral("\n                    datesDelta.push(new Date(");
                EndContext();
                BeginContext(1263, 4, false);
#line 47 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
                                        Write(item);

#line default
#line hidden
                EndContext();
                BeginContext(1267, 20, true);
                WriteLiteral("));\n                ");
                EndContext();
#line 48 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
                       

            }

#line default
#line hidden
                BeginContext(1310, 1, true);
                WriteLiteral("\n");
                EndContext();
#line 52 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
             foreach (var item in ViewBag.deltaValues)
            {
                

#line default
#line hidden
                BeginContext(1402, 49, true);
                WriteLiteral("\n                    deltaValues.push(parseFloat(");
                EndContext();
                BeginContext(1452, 4, false);
#line 55 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
                                           Write(item);

#line default
#line hidden
                EndContext();
                BeginContext(1456, 20, true);
                WriteLiteral("));\n                ");
                EndContext();
#line 56 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
                       

            }

#line default
#line hidden
                BeginContext(1499, 1, true);
                WriteLiteral("\n");
                EndContext();
#line 60 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
             foreach (var item in ViewBag.datesTHBuy)
            {
                

#line default
#line hidden
                BeginContext(1590, 46, true);
                WriteLiteral("\n                    datesTHBuy.push(new Date(");
                EndContext();
                BeginContext(1637, 4, false);
#line 63 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
                                        Write(item);

#line default
#line hidden
                EndContext();
                BeginContext(1641, 20, true);
                WriteLiteral("));\n                ");
                EndContext();
#line 64 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
                       

            }

#line default
#line hidden
                BeginContext(1684, 1, true);
                WriteLiteral("\n");
                EndContext();
#line 68 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
             foreach (var item in ViewBag.THBuyValues)
            {
                

#line default
#line hidden
                BeginContext(1776, 49, true);
                WriteLiteral("\n                    THBuyValues.push(parseFloat(");
                EndContext();
                BeginContext(1826, 4, false);
#line 71 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
                                           Write(item);

#line default
#line hidden
                EndContext();
                BeginContext(1830, 20, true);
                WriteLiteral("));\n                ");
                EndContext();
#line 72 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
                       

            }

#line default
#line hidden
                BeginContext(1873, 1, true);
                WriteLiteral("\n");
                EndContext();
#line 76 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
             foreach (var item in ViewBag.datesTHSell)
            {
                

#line default
#line hidden
                BeginContext(1965, 47, true);
                WriteLiteral("\n                    datesTHSell.push(new Date(");
                EndContext();
                BeginContext(2013, 4, false);
#line 79 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
                                         Write(item);

#line default
#line hidden
                EndContext();
                BeginContext(2017, 20, true);
                WriteLiteral("));\n                ");
                EndContext();
#line 80 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
                       

            }

#line default
#line hidden
                BeginContext(2060, 1, true);
                WriteLiteral("\n");
                EndContext();
#line 84 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
             foreach (var item in ViewBag.THSellValues)
            {
                

#line default
#line hidden
                BeginContext(2153, 50, true);
                WriteLiteral("\n                    THSellValues.push(parseFloat(");
                EndContext();
                BeginContext(2204, 4, false);
#line 87 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
                                            Write(item);

#line default
#line hidden
                EndContext();
                BeginContext(2208, 20, true);
                WriteLiteral("));\n                ");
                EndContext();
#line 88 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Charts/DeltaOnTradeHistory.cshtml"
                       

            }

#line default
#line hidden
                BeginContext(2251, 1067, true);
                WriteLiteral(@"

            var Delta = {
                x: datesDelta,
                y: deltaValues,
                mode: 'markers',
                marker: {
                    color: 'rgb(51, 183, 86)',
                    size: 4
                },
                name: 'Delta'
            };

            var TradeHistoryBuy = {
                x: datesTHBuy,
                y: THBuyValues,
                mode: 'lines',
                line: {
                    color: 'rgb(128, 0, 128)',
                    width: 2
                },
                name: 'Trade History BUY'
                };

            var TradeHistorySell = {
                x: datesTHSell,
                y: THSellValues,
                mode: 'lines',
                line: {
                    color: 'rgb(241, 79, 42)',
                    width: 2
                },
                name: 'Trade History SELL'
            };

            var data = [Delta, TradeHistoryBuy, TradeHistorySell];

            Plotly.plot(CHART, data);

     ");
                WriteLiteral("       </script>\n        </div>\n    </div>\n");
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
            BeginContext(3325, 9, true);
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
