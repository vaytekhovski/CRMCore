using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services.Database
{
    public static class ChangeUserDataService
    {
        public static void ChangeUserLogin(int UserId, string newUserLogin)
        {
            using (CRMContext context = new CRMContext())
            {
                context.UserModels.FirstOrDefault(x => x.Id == UserId).Login = newUserLogin;
                context.SaveChanges();
            }
        }

        public static void ChangeUserPassword(int UserId, string newUserPassword)
        {
            using (CRMContext context = new CRMContext())
            {
                context.UserModels.FirstOrDefault(x => x.Id == UserId).Password = newUserPassword;
                context.SaveChanges();
            }
        }

        public static void ChangeUserName(int UserId, string newUserName)
        {
            using (CRMContext context = new CRMContext())
            {
                context.UserModels.FirstOrDefault(x => x.Id == UserId).Name = newUserName;
                context.SaveChanges();
            }
        }

        public static void ChangeUserSurname(int UserId, string newUserSurname)
        {
            using (CRMContext context = new CRMContext())
            {
                context.UserModels.FirstOrDefault(x => x.Id == UserId).Surname = newUserSurname;
                context.SaveChanges();
            }
        }
        
    }
}