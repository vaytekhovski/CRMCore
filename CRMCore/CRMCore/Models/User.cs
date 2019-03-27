using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMCore.Models
{
    public static class User
    {
        public static bool isAutorized;
        public static int Id { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }
        public static string Name { get; set; }
        public static string Surname { get; set; }
        public static DateTime RegistrationDate { get; set; }
    }
}