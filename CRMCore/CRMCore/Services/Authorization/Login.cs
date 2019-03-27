using CRMCore.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMCore.Services.Authorization
{
    public class Login // TODO: переименовать в LoginService, остальные сервисы также
    {
        public string status;

        public bool Log(string user_name, string user_password)
        {
            if (!Models.User.isAutorized) //TODO:  проверка на уровне контроллера
            {
                if (user_name != null)
                    using (CRMCoreContext db = new CRMCoreContext())
                    {
                        UserModel user = db.UserModels.FirstOrDefault(x => x.Login == user_name && x.Password == user_password);

                        if (user != null)
                        {
                            status = "Авторизация пройдена!";
                            Models.User.Id = user.Id;
                            Models.User.Login = user.Login;
                            Models.User.Password = user.Password;
                            Models.User.Name = user.Name;
                            Models.User.Surname = user.Surname;
                            Models.User.RegistrationDate = user.RegistrationDate;
                            Models.User.isAutorized = true;
                            return Models.User.isAutorized; // TODO: переделать на FormsAuthentication
                        }
                        else
                        {
                            status = "Авторизация не пройдена!";
                            return Models.User.isAutorized;
                        }

                    }

                if (Models.User.isAutorized)
                    return Models.User.isAutorized;
            }

            return Models.User.isAutorized;
        }
    }
}
