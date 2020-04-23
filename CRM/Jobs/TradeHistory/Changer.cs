using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using Business.Contexts;

namespace Jobs
{
    class Changer
    {
        public Changer()
        {

        }

        private AccountTradeHistory lastEl;


        public List<AccountTradeHistory> ChangeOrdersBeforeCalculate(List<Order> orders, bool regularCalculating = true)
        {
            lastEl = regularCalculating ? Helper.FindLastSellDayAgo() : new AccountTradeHistory { Id = 0, Time = new DateTime(1970, 1, 1, 1, 1, 1) };


            orders = ChangeAmounts(orders);
            
            return AddToTradeHistories(orders.Where(x => x.closed > lastEl.Time).ToList()).OrderByDescending(x => x.Time).ToList();
        }

        private List<Order> ChangeAmounts(List<Order> orders)
        {
            using(BasicContext db = new BasicContext())
            {
                foreach (var item in db.WrongOrders.ToList())
                {
                    try
                    {
                        var order = orders.FirstOrDefault(x => x.id == item.OrderId.ToString());
                        if(order != null)
                            order.amount = Convert.ToDouble(item.Amount);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                }
            }

            
            return orders;
        }


        private List<AccountTradeHistory> AddToTradeHistories(ICollection<Order> orders)
        {
            List<AccountTradeHistory> AccountTradeHistories = new List<AccountTradeHistory>();

            int counter = lastEl.Id;

            foreach (var item in orders.OrderBy(x => x.closed))
            {
                if (item.amount == 0) continue;

                AccountTradeHistories.Add(new AccountTradeHistory
                {
                    Id = ++counter,
                    Account = item.account_id,
                    Time = item.closed.AddHours(3),
                    Side = item.side,
                    Pair = item.@base,
                    Price = Convert.ToDecimal(item.price),
                    Quantity = Convert.ToDecimal(item.amount),
                    DollarQuantity = Convert.ToDecimal(item.price * item.amount),
                    LowerBand = 0M
                });
            }

            return AccountTradeHistories;
        }
    }
}
