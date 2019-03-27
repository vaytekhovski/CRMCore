using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace CRMCore.Services.Database
{
    public static class ChangeUserData
    {
        public static void ChangeUserLogin(int UserId, string newUserLogin)
        {
            if (Models.User.isAutorized)
            {
                using (CRMCoreContext context = new CRMCoreContext())
                {
                    context.UserModels.FirstOrDefault(x => x.Id == UserId).Login = newUserLogin;
                    Models.User.Login = newUserLogin;

                    //foreach (var item in context.UserModels)
                    //{
                    //    if (item.Id == UserId)
                    //    {
                    //        item.Login = newUserLogin;
                    //        Models.User.Login = newUserLogin;
                    //        break;
                    //    }
                    //}
                    context.SaveChanges();
                }
            }
        }

        public static void ChangeUserPassword(int UserId, string newUserPassword)
        {
            if (Models.User.isAutorized)
            {
                using (CRMCoreContext context = new CRMCoreContext())
                {
                    context.UserModels.FirstOrDefault(x => x.Id == UserId).Password = newUserPassword;
                    Models.User.Password = newUserPassword;

                    //foreach (var item in context.UserModels)
                    //{
                    //    if (item.Id == UserId)
                    //    {
                    //        item.Password = newUserPassword;
                    //        Models.User.Password = newUserPassword;
                    //        break;
                    //    }
                    //}
                    context.SaveChanges();
                }
            }
        }

        public static void ChangeUserName(int UserId, string newUserName)
        {
            if (Models.User.isAutorized)
            {
                using (CRMCoreContext context = new CRMCoreContext())
                {
                    context.UserModels.FirstOrDefault(x => x.Id == UserId).Name = newUserName;
                    Models.User.Name = newUserName;

                    //foreach (var item in context.UserModels)
                    //{
                    //    if (item.Id == UserId)
                    //    {
                    //        item.Name = newUserName;
                    //        Models.User.Name = newUserName;
                    //        break;
                    //    }
                    //}
                    context.SaveChanges();
                }
            }
        }

        public static void ChangeUserSurname(int UserId, string newUserSurname)
        {
            if (Models.User.isAutorized)
            {
                using (CRMCoreContext context = new CRMCoreContext())
                {
                    context.UserModels.FirstOrDefault(x => x.Id == UserId).Surname = newUserSurname;
                    Models.User.Surname = newUserSurname;

                    //foreach (var item in context.UserModels)
                    //{
                    //    if (item.Id == UserId)
                    //    {
                    //        item.Surname = newUserSurname;
                    //        Models.User.Surname = newUserSurname;
                    //        break;
                    //    }
                    //}
                    context.SaveChanges();
                }
            }
        }
        
    }
}