using System.Linq;

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
        
        
    }
}