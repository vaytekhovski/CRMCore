using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services.Database
{
    public static class ChangeUserDataService
    {
        public static void ChangeUserLogin(string lastUserLogin, string newUserLogin)
        {
            using (CRMContext context = new CRMContext())
            {
                context.UserModels.FirstOrDefault(x => x.Login == lastUserLogin).Login = newUserLogin;
                context.SaveChanges();
            }
        }

        public static void ChangeUserPassword(string lastUserLogin, string newUserPassword)
        {
            using (CRMContext context = new CRMContext())
            {
                context.UserModels.FirstOrDefault(x => x.Login == lastUserLogin).Password = newUserPassword;
                context.SaveChanges();
            }
        }

        public static void ChangeUserName(string lastUserLogin, string newUserName)
        {
            using (CRMContext context = new CRMContext())
            {
                context.UserModels.FirstOrDefault(x => x.Login == lastUserLogin).Name = newUserName;
                context.SaveChanges();
            }
        }

        public static void ChangeUserSurname(string lastUserLogin, string newUserSurname)
        {
            using (CRMContext context = new CRMContext())
            {
                context.UserModels.FirstOrDefault(x => x.Login == lastUserLogin).Surname = newUserSurname;
                context.SaveChanges();
            }
        }
        
    }
}