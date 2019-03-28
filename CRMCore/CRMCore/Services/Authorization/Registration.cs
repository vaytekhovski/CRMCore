﻿using CRMCore.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMCore.Services.Authorization
{
    public class RegistrationService
    {
        public string status;
        public bool Reg(string user_name, string user_password, string user_confirm_password)
        {
            bool registration = false;
            bool isUserExist = false;


            if (user_name != null)
            {
                using (CRMCoreContext db = new CRMCoreContext())
                {
                    UserModel user = db.UserModels.FirstOrDefault(x => x.Login == user_name);
                    if (user != null)
                    {
                        status = "Логин занят!";
                        registration = false;
                        isUserExist = true;
                        return registration;
                    }
                }
            }

            if (user_password != null && user_confirm_password != null && !isUserExist)
            {
                if (user_password != user_confirm_password)
                {
                    status = "Пароли не совпадают!";
                    registration = false;
                    return registration;
                }
                else
                {
                    status = "";
                    registration = true;
                }
            }

            if (registration)
            {
                using (CRMCoreContext db = new CRMCoreContext())
                {
                    db.UserModels.Add(new UserModel
                    {
                        Login = user_name,
                        Password = user_password,
                        RegistrationDate = DateTime.Now,
                        Name = " ",
                        Surname = " "
                    });
                    db.SaveChanges();
                }
                status = "Регистрация завершена!";
                
            }

            return registration;
        }
        
    }
}
