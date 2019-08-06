using Business.Contexts;
using System;
using System.Linq;

namespace CRM.Services.Database
{
    public class ChangeUserDataService
    {
        public void ChangeUserLogin(string userId, string newUserLogin)
        {
            using (BasicContext context = new BasicContext())
            {
                context.UserModels.FirstOrDefault(x => x.Id == Convert.ToInt32(userId)).Login = newUserLogin;
                context.SaveChanges();
            }
        }

        public void ChangeUserPassword(string userId, string newUserPassword)
        {
            using (BasicContext context = new BasicContext())
            {
                context.UserModels.FirstOrDefault(x => x.Id == Convert.ToInt32(userId)).Password = newUserPassword;
                context.SaveChanges();
            }
        }
        
        
    }
}