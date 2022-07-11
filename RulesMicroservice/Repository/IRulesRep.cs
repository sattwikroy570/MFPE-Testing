using RulesMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RulesMicroservice.Repository
{
    public interface IRulesRep
    {
        public ServiceCharge getServiceCharges();
        public RuleStatus evaluateMinBal(AccountCheck value);
    }
}
