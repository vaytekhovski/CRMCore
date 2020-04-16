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

}
