using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models
{
    public class CustomerAccountDetails
    {
        public string CustomerId { get; set; }
        public int CurrentAccountId { get; set; }
        public int SavingsAccountId { get; set; }
    }
}
