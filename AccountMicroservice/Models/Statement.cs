using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Models
{
    public class Statement
    {
        public DateTime Date { get; set; }
        public string Narration { get; set; }
        public double Withdrawal { get; set; }
        public double Deposit { get; set; }
        public double ClosingBalance { get; set; }
    }
}
