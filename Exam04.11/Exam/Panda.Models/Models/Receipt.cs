﻿namespace Panda.Domain.Models
{
    using System;

    public class Receipt
    {
        public int Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public int RecipientId { get; set; }
        public User Recipient { get; set; }

        public int PackageId { get; set; }
        public Package Package { get; set; }
    }
}