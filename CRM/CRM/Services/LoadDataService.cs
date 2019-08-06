using CRM.DTO;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using Business.Data;
using Business.Contexts;

namespace CRM.Services
{
    public class LoadDataService
    {
        public static bool isLoading = false;

        int index = 1;

        public List<OrderBookModel> OrderBook { get; private set; } = new List<OrderBookModel>();
        public List<TradeHistoryModel> TradeHistories { get; private set; } = new List<TradeHistoryModel>();
        public List<TradeDeltaModel> TradeDeltas { get; private set; } = new List<TradeDeltaModel>();

        public LoadDataService()
        {
            foreach (var coin in DropDownFields.Coins)
            {
                try
                {
                    Loading(coin.Value, DateTime.Now, DateTime.Now.AddDays(1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
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
            string path = $"wwwroot/json/binance-{coin}.json";
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
            try
            {
                DownloadJson(url, path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            var ticker = Ticker.FromJson(JsonFile(path));

            Debug.WriteLine($"{coin} add ticker to list");
            AddTickerToLists(OrderBook, TradeHistories, TradeDeltas, ticker, coin);

            Debug.WriteLine($"{coin} checking exists values");
            CheckExistValues(OrderBook, TradeHistories, TradeDeltas);

            Debug.WriteLine($"{coin} add list to database");
            AddListToDataBase(OrderBook, TradeHistories, TradeDeltas);

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
            try
            {
                sw.WriteLine(webClient.DownloadString("http://" + url));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            sw.Close();
        }

        private static string JsonFile(string path)
        {
            return File.ReadAllText(path);
        }

        private static void AddTickerToLists(List<OrderBookModel> orderBook, List<TradeHistoryModel> tradeHistories, List<TradeDeltaModel> tradeDeltas, Ticker ticker, string Pair)
        {
            // Проходим по каждому list'у в Ticker'e 
            // и заносим из него данные в наш лист


            foreach (var item in ticker.OrderBookAsks)
                orderBook.Add(new OrderBookModel
                {
                    CurrencyName = Pair,
                    BookType = "ask",
                    Date = item.Time.Date + item.Time.TimeOfDay,
                    Price = item.Price,
                    Volume = item.Amount,
                    MarketSituation = item.Situation.ToString()
                });
            
            foreach (var item in ticker.OrderBookBids)
                orderBook.Add(new OrderBookModel
                {
                    CurrencyName = Pair,
                    BookType = "bid",
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

        private static void CheckExistValues(List<OrderBookModel> orderBook, List<TradeHistoryModel> tradeHistories, List<TradeDeltaModel> tradeDeltas)
        {
            // Проходим по базе данных и проверяем,
            // существуют ли в ней такие элементы,
            // если да, то удаляем их из list'a
            using (BasicContext context = new BasicContext())
            {
                foreach (var DBItem in context.OrderBookModels.Where(x => x.BookType == "ask"))
                {
                    var buf = orderBook.FirstOrDefault(x =>
                       x.Date == DBItem.Date &&
                       x.Price == DBItem.Price &&
                       x.Volume == DBItem.Volume);

                    if (buf != null)
                    {
                        orderBook.Remove(buf);
                    }
                }

                
                foreach (var DBItem in context.OrderBookModels.Where(x => x.BookType == "bid"))
                {
                    var buf = orderBook.FirstOrDefault(x =>
                       x.Date == DBItem.Date &&
                       x.Price == DBItem.Price &&
                       x.Volume == DBItem.Volume);

                    if (buf != null)
                    {
                        orderBook.Remove(buf);
                    }
                }

                foreach (var DBItem in context.TradeHistoryModels)
                {
                    var buf = tradeHistories.FirstOrDefault(x =>
                       x.Date == DBItem.Date &&
                       x.Price == DBItem.Price &&
                       x.Volume == DBItem.Volume);

                    if (buf != null)
                    {
                        tradeHistories.Remove(buf);
                    }
                }

                foreach (var DBItem in context.TradeDeltaModels)
                {
                    var buf = tradeDeltas.FirstOrDefault(x =>
                        x.TimeFrom == DBItem.TimeFrom &&
                        x.TimeTo == DBItem.TimeTo &&
                        x.Delta == DBItem.Delta);

                    if (buf != null)
                    {
                        tradeDeltas.Remove(buf);
                    }
                }
            }
        }

        private static void AddListToDataBase(List<OrderBookModel> orderBook, List<TradeHistoryModel> tradeHistories, List<TradeDeltaModel> tradeDeltas)
        {
            // Добавляем данные из list'a в БД
            using (BasicContext context = new BasicContext())
            {
                context.OrderBookModels.AddRange(orderBook);
                Debug.WriteLine("items to OrderBook added");

                context.TradeHistoryModels.AddRange(tradeHistories);
                Debug.WriteLine("items to TradeHistory added");

                context.TradeDeltaModels.AddRange(tradeDeltas);
                Debug.WriteLine("items to TradeDelta added");

                context.SaveChanges();
            }
        }


        
    }
}