using Newtonsoft.Json;
using RulesMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RulesMicroservice.Repository
{
    public class RulesRep : IRulesRep
    {
        Uri baseAddress = new Uri("http://52.159.81.113/api");   //Port No.
        HttpClient client;
        public RulesRep()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;

        }
        public ServiceCharge getServiceCharges()
        {
            ServiceCharge ob = new ServiceCharge();
            ob.ServiceChargeBalance = 100F;
            return ob;
        }
        public RuleStatus evaluateMinBal(AccountCheck value)
        {
            AccountMsg ob = new AccountMsg();
            RuleStatus ob1 = new RuleStatus();
            string token = TokenInfo.StringToken;
            client.DefaultRequestHeaders.Add("Authorization", token);
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Account/getAccount/" + value.AccountId).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ob = JsonConvert.DeserializeObject<AccountMsg>(data);
                ob.AccBal = ob.AccBal - value.Amount;
                if (ob.AccType == "Savings Account" && ob.AccBal < 500)
                    ob1.Warning = true;
                else
                    ob1.Warning = false;
            }
            return ob1;
        }
    }
}
