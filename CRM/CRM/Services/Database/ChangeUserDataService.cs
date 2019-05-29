using System.Linq;

namespace CRM.Services.Database
{
    public class ChangeUserDataService
    {
        public void ChangeUserLogin(string lastUserLogin, string newUserLogin)
        {
            using (CRMContext context = new CRMContext())
            {
                context.UserModels.FirstOrDefault(x => x.Login == lastUserLogin).Login = newUserLogin;
                context.SaveChanges();
            }
        }

        public void ChangeUserPassword(string lastUserLogin, string newUserPassword)
        {
            using (CRMContext context = new CRMContext())
            {
                context.UserModels.FirstOrDefault(x => x.Login == lastUserLogin).Password = newUserPassword;
                context.SaveChanges();
            }
        }
        
        
    }
}