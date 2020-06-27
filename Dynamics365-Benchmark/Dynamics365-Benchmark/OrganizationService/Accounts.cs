using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics365_Benchmark.OrganizationService
{
    public class Accounts
    {
        public void AddAccounts(IOrganizationService organizationService, int target)
        {
            for (int i = 0; i < target; i++)
            {
                var entity = new Entity("account");
                entity["name"] = $"Test account {i}";
                organizationService.Create(entity);
            }
        }
    }
}
