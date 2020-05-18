using System;
namespace Business.Models.DataVisioAPI
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticationResnose
    {
        public string token { get; set; }
        public DateTime expires { get; set; }
    }

    public class WalletCurrency
    {
        public string coin { get; set; }
        public double free { get; set; }
        public double used { get; set; }
        public double total { get; set; }
    }

    public class Currencies
    {
        public string USDT { get; set; }
        public string BTC { get; set; }
        public string ETH { get; set; }
        public string LTC { get; set; }
    }

    public class PlaceOrderRequest
    {
        public string type { get; set; }
        public string side { get; set; }
        public string @base { get; set; }
        public string quote { get; set; }
        public double amount { get; set; }
    }

    public class PlaceOrderResponse
    {
        public string id { get; set; }
    }

    public class ListOrderModel
    {
        public Order[] orders { get; set; }
        public Page page { get; set; }
    }

    public class Signals
    {
        public Signal[] signals { get; set; }
        public Page page { get; set; }
    }

    public class Page
    {
        public int current { get; set; }
        public int last { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
    }

    public class Signal
    {
        public string exchange { get; set; }
        public string @base { get; set; }
        public string quote { get; set; }
        public DateTime time { get; set; }
        public int value { get; set; }
        public decimal proba { get; set; }
        public decimal number { get; set; }
        public Indicator indicators { get; set; }

    }

    public class Indicator
    {

        public decimal macd { get; set; }
        public decimal sig { get; set; }
        public decimal hist { get; set; }
        public decimal rsi { get; set; }
        public decimal obv { get; set; }
        public decimal bbl { get; set; }
        public decimal bbm { get; set; }
        public decimal bbu { get; set; }
    }

    public class Candles
    {
        public decimal o { get; set; }
        public decimal h { get; set; }
        public decimal l { get; set; }
        public decimal c { get; set; }
        public decimal v { get; set; }
        public long t { get; set; }

    }

    public class Graph
    {
        public long time { get; set; }
        public DateTime Time { get; set; }
        public string time_str { get; set; }
        public decimal rsi { get; set; }
        public decimal lir { get; set; }
    }

}
