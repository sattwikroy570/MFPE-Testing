using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RulesMicroservice.Models
{
    public class AccountMsg
    {
        public int AccId { get; set; }
        public string AccType { get; set; }
        public double AccBal { get; set; }
    }
}
