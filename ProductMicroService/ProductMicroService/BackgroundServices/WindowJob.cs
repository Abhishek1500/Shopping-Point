using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductMicroService.Data;
using ProductMicroService.Models;

namespace ProductMicroService.BackgroundServices
{
    public class WindowJob : BackgroundService
    {

        ILogger<BackgroundService> _logger;
        IServiceScopeFactory _serviceScopeFactory;

        public WindowJob(ILogger<BackgroundService> logger,IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(3600000, stoppingToken);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                    var products= context.Products.AsQueryable();
                    foreach(Product p in products)
                    {
                        p.Quantity = p.Quantity + 2;
                    }
                    await context.SaveChangesAsync();
                     _logger.LogInformation("runnning");
                    
                }
            }

        }
    }
}
