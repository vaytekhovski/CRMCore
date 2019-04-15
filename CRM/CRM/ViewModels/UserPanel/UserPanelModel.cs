using System;

namespace CRM.ViewModels
{
    public class UserPanelModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
