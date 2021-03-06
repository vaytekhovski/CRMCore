﻿using System;
using System.Collections.Generic;

namespace Business
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastAuthorizationDate { get; set; }
        public int? RoleId { get; set; }

        public Role Role { get; set; }
        public List<ExchangeKey> Accounts { get; set; }
        public UserModel()
        {
            Accounts = new List<ExchangeKey>();
        }


        public enum Roles
        {
            Admin = 1,
            User = 2,
            Boss = 3,
            Spectator = 4
        }

    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserModel> Users { get; set; }
        public Role()
        {
            Users = new List<UserModel>();
        }
    }

    public class ExchangeKey
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string AccountId { get; set; }
        public UserModel User { get; set; }
    }
}