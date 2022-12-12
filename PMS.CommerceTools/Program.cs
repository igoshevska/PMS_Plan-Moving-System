using commercetools.Sdk.Api;
using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.Customers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.CommerceTools
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    var services = new ServiceCollection();
                    var configuration = new ConfigurationBuilder()
                      .AddInMemoryCollection(new List<KeyValuePair<string, string>>()
                      {
                    new KeyValuePair<string, string>("MyClient:ApiBaseAddress", "https://api.europe-west1.gcp.commercetools.com/"),
                    new KeyValuePair<string, string>("MyClient:AuthorizationBaseAddress", "https://auth.europe-west1.gcp.commercetools.com/"),
                    new KeyValuePair<string, string>("MyClient:ClientId", "Q68AyBuAY0qmV5sSWka7HKDD"),
                    new KeyValuePair<string, string>("MyClient:ClientSecret", "UN5IC1c05N1uoxfqcDawgFWUXYt2xMk0"),
                    new KeyValuePair<string, string>("MyClient:ProjectKey", "ecommerce9"),
                    new KeyValuePair<string, string>("MyClient:Scope", "manage_project:ecommerce9")
                      })
                    .Build();

                    services.UseCommercetoolsApi(configuration, "MyClient");
                    services.AddLogging();

                    var serviceProvider = services.BuildServiceProvider();
                    var projectApiRoot = serviceProvider.GetService<ProjectApiRoot>();
                    projectApiRoot.Customers();
                    // Make a call to get the Project
                    var myProject = projectApiRoot
                      .Get()
                      .ExecuteAsync()
                      .Result;
                    // Get a specific Customer by ID
                    var customerInfo = projectApiRoot
                      .Customers()
                      .WithId("60322e53-49b8-4023-9013-e6a61a3623a2")
                      .Get()
                      .ExecuteAsync()
                      .Result;

                    var customersQuery = projectApiRoot
                          .Customers()
                          .Get()
                          .ExecuteAsync()
                          .Result;

                    // Put the returned Customers in a list
                    var listOfCustomers = customersQuery.Results;

                    // Output the first Customer's email address
                    Console.WriteLine(listOfCustomers[0].Email);



                    var newCustomerDetails = new CustomerDraft()
                    {
                        Email = "dotnet-sdk@example.com",
                        Password = "password"
                    };
                    // Output the Project name

                    // Post the CustomerDraft and get the new Customer
                    var newCustomer = projectApiRoot
                      .Customers()
                      .Post(newCustomerDetails)
                      .ExecuteAsync()
                      .Result
                      // As creating a Customer returns a CustomerSignInResult, .Customer is required to get the new Customer object
                      .Customer;
                    Console.WriteLine(myProject.Name);
                });
        
    }
}
