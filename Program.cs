using System;
using System.Threading.Tasks;
using Microsoft.Graph;
using Azure.Identity;
using Microsoft.Graph.Models;

class Program
{
    private static readonly string _clientId = "CLIENT_ID";
    private static readonly string _tenantId = "TENANT_ID";

    static async Task Main(string[] args)
    {
        var credential = new InteractiveBrowserCredential(new InteractiveBrowserCredentialOptions
        {
            TenantId = _tenantId,
            ClientId = _clientId,
            RedirectUri = new Uri("http://localhost")
        });
        var accessToken = credential.GetToken(new Azure.Core.TokenRequestContext());
        var graphClient = new GraphServiceClient(credential, new[] { "User.Read" });

        try
        {
            User? user = await graphClient.Me.GetAsync();

            if (user != null)
            {
                Console.WriteLine($"Name: {user.DisplayName}");
                Console.WriteLine($"Email: {user.Mail ?? user.UserPrincipalName}");
            }
            else
            {
                Console.WriteLine("Failed to retrieve user details.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
