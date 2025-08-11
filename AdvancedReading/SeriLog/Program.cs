using Serilog;
using Serilog.Sinks.SystemConsole; 
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Serilog.Core;

namespace ASerilog
{
    class Program
    {
        static void Main(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Configure Serilog from appsettings.json
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                Log.Information("Application starting up");
                
                // Your application logic here
                RunApplication();
                
                Log.Information("Application completed successfully");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        
        static void RunApplication()
        {
            // Example of structured logging
            var orderId = Guid.NewGuid();
            var customerId = 12345;
            var amount = 99.99m;
            
            Log.Information("Processing order {OrderId} for customer {CustomerId} with amount {Amount:C}", 
                orderId, customerId, amount);
                
            // Simulate processing
            Log.Debug("Validating order details...");
            Log.Debug("Calculating taxes...");
            Log.Debug("Updating inventory...");
            
            Log.Information("Order {OrderId} processed successfully", orderId);
        }
    }
}