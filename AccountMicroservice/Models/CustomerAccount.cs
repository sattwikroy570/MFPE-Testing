﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice
{
    public class CustomerAccount
    {
        public string CustomerId { get; set; }
        public int CurrentAccountId { get; set; }
        public int SavingsAccountId { get; set; }
    }
}
