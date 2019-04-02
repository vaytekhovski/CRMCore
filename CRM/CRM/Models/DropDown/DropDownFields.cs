using CRM.Models.DropDown;
using System.Collections.Generic;

namespace CRM.Models
{
    public class DropDownFields
    {
        public static List<Field> Coins = new List<Field>();
        public static List<Field> OrderType = new List<Field>();
        public static List<Field> Situation = new List<Field>();
        public static List<Field> Nulls = new List<Field>();

        public static void InitiateFields()
        {
            InitiateCoins();
            InitiateOrderType();
            InitiateSituations();
            InitiateNulls();
        }

        private static void InitiateCoins()
        {
            Coins.Add(new Field { Value = "BTC", Name = "USDT-BTC" });
            Coins.Add(new Field { Value = "BNB", Name = "USDT-BNB" });
            Coins.Add(new Field { Value = "EOS", Name = "USDT-EOS" });
            Coins.Add(new Field { Value = "ETH", Name = "USDT-ETH" });
            Coins.Add(new Field { Value = "XRP", Name = "USDT-XRP" });
            Coins.Add(new Field { Value = "LTC", Name = "USDT-LTC" });
            Coins.Add(new Field { Value = "TRX", Name = "USDT-TRX" });

            Coins.Add(new Field { Value = "ONT", Name = "USDT-ONT" });
            Coins.Add(new Field { Value = "XLM", Name = "USDT-XLM" });
            Coins.Add(new Field { Value = "ADA", Name = "USDT-ADA" });
            Coins.Add(new Field { Value = "BCHABC", Name = "USDT-BCHABC" });
        }

        private static void InitiateOrderType()
        {
            OrderType.Add(new Field { Value = "all", Name = "Все" });
            OrderType.Add(new Field { Value = "Sell", Name = "Sell" });
            OrderType.Add(new Field { Value = "Buy", Name = "Buy" });
        }

        private static void InitiateSituations()
        {
            Situation.Add(new Field { Value = "all", Name = "Все" });
            Situation.Add(new Field { Value = "Flat", Name = "Flat" });
            Situation.Add(new Field { Value = "Trend", Name = "Trend" });
        }

        private static void InitiateNulls()
        {
            Nulls.Add(new Field { Value = "all", Name = "Показывать нулевые значения" });
            Nulls.Add(new Field { Value = "notnull", Name = "Не показывать нулевые значения" });
        }

        public static void Swap(string coin, string situation = "default", string orderType = "default", bool nulls = false)
        {
            SwapCoins(coin);
            if (orderType != "default")
                SwapOrderTypes(orderType);
            if (situation != "default")
                SwapSituations(situation);
            if(nulls)
                SwapNulls();
        }

        private static void SwapCoins(string Value)
        {
            foreach (var item in Coins)
            {
                if (item.Value == Value)
                {
                    foreach (var first in Coins)
                    {
                        string bufValue = first.Value;
                        string bufName = first.Name;

                        first.Value = item.Value;
                        first.Name = item.Name;

                        item.Value = bufValue;
                        item.Name = bufName;
                        break;
                    }
                    break;
                }
            }
        }

        private static void SwapOrderTypes(string Value)
        {
            foreach (var item in OrderType)
            {
                if (item.Value == Value)
                {
                    foreach (var first in OrderType)
                    {
                        string bufValue = first.Value;
                        string bufName = first.Name;

                        first.Value = item.Value;
                        first.Name = item.Name;

                        item.Value = bufValue;
                        item.Name = bufName;
                        break;
                    }
                    break;
                }
            }
        }

        private static void SwapSituations(string Value)
        {
            foreach (var item in Situation)
            {
                if (item.Value == Value)
                {
                    foreach (var first in Situation)
                    {
                        string bufValue = first.Value;
                        string bufName = first.Name;

                        first.Value = item.Value;
                        first.Name = item.Name;

                        item.Value = bufValue;
                        item.Name = bufName;
                        break;
                    }
                    break;
                }
            }
        }

        private static void SwapNulls()
        {

            string bufValue = Nulls[0].Value;
            string bufName = Nulls[0].Name;

            Nulls[0].Value = Nulls[1].Value;
            Nulls[0].Name = Nulls[1].Name;

            Nulls[1].Value = bufValue;
            Nulls[1].Name = bufName;

        }
    }
}