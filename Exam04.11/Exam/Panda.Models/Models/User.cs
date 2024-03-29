﻿namespace Panda.Domain.Models
{
    using System.Collections.Generic;
    using Enums;

    public class User
    {
        public User()
        {
            this.Packages = new HashSet<Package>();
            this.Receipts = new HashSet<Receipt>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; }

        public virtual ICollection<Package> Packages { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}