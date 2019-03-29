using CRM.DTO;
using CRM.Models;
using CRM.Models.Database;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

namespace CRM.Services
{
    public class LoadDataService
    {
        private List<OrderBookAsksModel> orderBookAsks = new List<OrderBookAsksModel>();
        private List<OrderBookBidsModel> orderBookBids = new List<OrderBookBidsModel>();
        private List<TradeHistoryModel> tradeHistories = new List<TradeHistoryModel>();
        private List<TradeDeltaModel> tradeDeltas = new List<TradeDeltaModel>();

        public static bool isLoading = false;

        int index = 1;

        public LoadDataService()
        {
            foreach (var coin in DropDownFields.Coins)
            {
                Loading(coin.Value, DateTime.Now, DateTime.Now.AddDays(1));
            }
        }

        public LoadDataService(DateTime startDate, DateTime dateTime)
        {
            StartLoading(startDate, dateTime);
        }

        public void StartLoading(DateTime startDate, DateTime dateTime)
        {
            foreach (var coin in DropDownFields.Coins)
            {
                Loading(coin.Value, startDate, dateTime);
            }
        }


        public void Loading(string coin, DateTime startDate, DateTime endDate)
        {
            
            isLoading = true;
            Debug.WriteLine($"[{index}/{DropDownFields.Coins.Count}] Download {coin} started from {startDate} to {endDate}");

            // Тут определяются даты, для url запроса на получение данных
            int sDay = startDate.Day;
            int sMonth = startDate.Month;
            int sYear = startDate.Year;

            int eDay = endDate.Day;
            int eMonth = endDate.Month;
            int eYear = endDate.Year;

            // Путь к файлу, в который будет записан Json
            string path = $"wwwroot/binance-{coin}.json";
            // Url запрос на сервер, который вернет Json
            string url = $"159.65.126.124:5000/export/binance/{coin}/usdt?from={sYear}/{sMonth}/{sDay}&to={eYear}/{eMonth}/{eDay}&format=json";

            // Загружаем данные с сервера в json файл
            // После чего создаем объект Ticker,
            // в котором хранятся спаршенные данные Json
            // После чего, переносим данные из Ticker'a в list'ы, 
            // преобразованные в формат для БД
            // Проверяем, существуют ли уже такие данные в БД,
            // и только после этого добавляем их в саму БД

            Debug.WriteLine($"{coin} download json");
            DownloadJson(url, path);
            var ticker = Ticker.FromJson(JsonFile(path));

            Debug.WriteLine($"{coin} add ticker to list");
            AddTickerToLists(orderBookAsks, orderBookBids, tradeHistories, tradeDeltas, ticker, coin);

            Debug.WriteLine($"{coin} checking exists values");
            CheckExistValues(orderBookAsks, orderBookBids, tradeHistories, tradeDeltas);

            Debug.WriteLine($"{coin} add list to database");
            AddListToDataBase(orderBookAsks, orderBookBids, tradeHistories, tradeDeltas);

            Debug.WriteLine($"[{index}/{DropDownFields.Coins.Count}] Download {coin} ended");
            isLoading = false;
            index++;
        }

        private static void DownloadJson(string url, string fullnamelocation)
        {
            WebClient webClient = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            StreamWriter sw = new StreamWriter(fullnamelocation, false, Encoding.UTF8);
            sw.WriteLine(webClient.DownloadString("http://" + url));
            sw.Close();
        }

        private static string JsonFile(string path)
        {
            return File.ReadAllText(path);
        }

        private static void AddTickerToLists(List<OrderBookAsksModel> orderBookAsks, List<OrderBookBidsModel> orderBookBids, List<TradeHistoryModel> tradeHistories, List<TradeDeltaModel> tradeDeltas, Ticker ticker, string Pair)
        {
            // Проходим по каждому list'у в Ticker'e 
            // и заносим из него данные в наш лист


            foreach (var item in ticker.OrderBookAsks)
                orderBookAsks.Add(new OrderBookAsksModel
                {
                    CurrencyName = Pair,
                    Date = item.Time.Date + item.Time.TimeOfDay,
                    Price = item.Price,
                    Volume = item.Amount,
                    MarketSituation = item.Situation.ToString()
                });
            
            foreach (var item in ticker.OrderBookBids)
                orderBookBids.Add(new OrderBookBidsModel
                {
                    CurrencyName = Pair,
                    Date = item.Time.Date + item.Time.TimeOfDay,
                    Price = item.Price,
                    Volume = item.Amount,
                    MarketSituation = item.Situation.ToString()
                });

            foreach (var item in ticker.TradeHistory)
                tradeHistories.Add(new TradeHistoryModel
                {
                    CurrencyName = Pair,
                    Date = item.Time.Date + item.Time.TimeOfDay,
                    Side = item.Side.ToString(),
                    OrderTime = item.OrderTime,
                    Price = item.Price,
                    Volume = item.Amount,
                    MarketSituation = item.Situation.ToString()
                });

            foreach (var item in ticker.TradeDelta)
                tradeDeltas.Add(new TradeDeltaModel
                {
                    CurrencyName = Pair,
                    TimeFrom = item.TimeFrom,
                    TimeTo = item.TimeTo,
                    Delta = item.Value
                });
        }

        private static void CheckExistValues(List<OrderBookAsksModel> orderBookAsks, List<OrderBookBidsModel> orderBookBids, List<TradeHistoryModel> tradeHistories, List<TradeDeltaModel> tradeDeltas)
        {
            // Проходим по базе данных и проверяем,
            // существуют ли в ней такие элементы,
            // если да, то удаляем их из list'a
            using (CRMContext context = new CRMContext())
            {
                foreach (var DBItem in context.OrderBookAsksModels)
                {
                    var buf = orderBookAsks.FirstOrDefault(x =>
                       x.Date == DBItem.Date &&
                       x.Price == DBItem.Price &&
                       x.Volume == DBItem.Volume
                   );
                    if (buf != null)
                        orderBookAsks.Remove(buf);
                }

                
                foreach (var DBItem in context.OrderBookBidsModels)
                {
                    var buf = orderBookBids.FirstOrDefault(x =>
                       x.Date == DBItem.Date &&
                       x.Price == DBItem.Price &&
                       x.Volume == DBItem.Volume
                   );
                    if (buf != null)
                        orderBookBids.Remove(buf);
                }

                foreach (var DBItem in context.TradeHistoryModels)
                {
                    var buf = tradeHistories.FirstOrDefault(x =>
                       x.Date == DBItem.Date &&
                       x.Price == DBItem.Price &&
                       x.Volume == DBItem.Volume
                   );
                    if (buf != null)
                        tradeHistories.Remove(buf);
                }

                foreach (var DBItem in context.TradeDeltaModels)
                {
                    var buf = tradeDeltas.FirstOrDefault(x =>
                        x.TimeFrom == DBItem.TimeFrom &&
                        x.TimeTo == DBItem.TimeTo &&
                        x.Delta == DBItem.Delta
                    );
                    if (buf != null)
                        tradeDeltas.Remove(buf);
                }
            }
        }

        private static void AddListToDataBase(List<OrderBookAsksModel> orderBookAsks, List<OrderBookBidsModel> orderBookBids, List<TradeHistoryModel> tradeHistories, List<TradeDeltaModel> tradeDeltas)
        {
            // Добавляем данные из list'a в БД
            using (CRMContext context = new CRMContext())
            {
                context.OrderBookAsksModels.AddRange(orderBookAsks);
                Debug.WriteLine("items to OrderBookAsks added");

                context.OrderBookBidsModels.AddRange(orderBookBids);
                Debug.WriteLine("items to OrderBookBids added");

                context.TradeHistoryModels.AddRange(tradeHistories);
                Debug.WriteLine("items to TradeHistory added");

                context.TradeDeltaModels.AddRange(tradeDeltas);
                Debug.WriteLine("items to TradeDelta added");

                context.SaveChanges();
            }
        }


        
    }
}