using Business.Models.DataVisioAPI;
using CRM.ViewModels.Balances;
using System;
using System.Collections.Generic;

namespace CRM.ViewModels
{
    public class UserPanelModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ShowAccount AccountData { get; set; }

        public BalancesModel Balances { get; set; }
        public List<ShowAccount> Accounts { get; set; }
    }
}
