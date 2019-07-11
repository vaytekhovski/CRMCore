using System;

namespace THManager
{
    public partial class Orders
    {
        public Orders()
        {

        }


        public Orders(int _Id, string _AccountId, string _Base, string _Side, DateTime _TimeEnded, decimal _ClosedAmount, decimal _Rate)
        {
            Id = _Id;
            AccountId = _AccountId;
            Base = _Base;
            Side = _Side;
            TimeEnded = _TimeEnded;
            ClosedAmount = _ClosedAmount;
            Rate = _Rate;
        }



        public int Id { get; set; }
        public string AccountId { get; set; }
        public string Exchange { get; set; }
        public string Base { get; set; }
        public string Quote { get; set; }
        public string OrderId { get; set; }
        public string Side { get; set; }
        public DateTime TimeStarted { get; set; }
        public DateTime TimeEnded { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal ClosedAmount { get; set; }
        public decimal Rate { get; set; }
        public byte Completed { get; set; }
    }
}
