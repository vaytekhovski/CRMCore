using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Master
{
    public partial class Orders
    {
        public int Id { get; set; }
        //[Column("SagaId", TypeName = "char(36)")]
        //public string SagaId { get; set; }
        //[Column("AccountId", TypeName = "char(36)")]
        public string AccountId { get; set; }
        public string Exchange { get; set; }
        public string Base { get; set; }
        public string Quote { get; set; }
        //[Column("OrderId", TypeName = "char(36)")]
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
