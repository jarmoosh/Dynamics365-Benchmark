using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Configuration;
using System.Threading.Tasks;

namespace Dynamics365_Benchmark.Rest
{
    public static class TokenHelper
    {
        public static async Task<string> GetAccessToken()
        {
            var clientId = ConfigurationManager.AppSettings["clientId"];
            var secret = ConfigurationManager.AppSettings["clientSecret"];
            var organizationUrl = ConfigurationManager.AppSettings["organizationUrl"];
            var aadInstanceUrl = ConfigurationManager.AppSettings["aadInstanceUrl"];
            var tenantId = ConfigurationManager.AppSettings["tenantId"];

            var clientcred = new ClientCredential(clientId, secret);
            var authenticationContext = new AuthenticationContext($"{aadInstanceUrl}/{tenantId}");
            var authenticationResult = await authenticationContext.AcquireTokenAsync(organizationUrl, clientcred);
            return authenticationResult.AccessToken;
        }
    }
}
