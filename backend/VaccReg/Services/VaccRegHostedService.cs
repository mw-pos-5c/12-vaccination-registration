using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using VaccRegDb;

namespace VaccReg.Services
{
    public class VaccRegHostedService : IHostedService
    {
        private readonly ILogger<VaccRegHostedService> logger;
        private readonly IServiceScopeFactory scopeFactory;

        public VaccRegHostedService(ILogger<VaccRegHostedService> logger, IServiceScopeFactory scopeFactory)
        {
            this.logger = logger;
            this.scopeFactory = scopeFactory;
        }

        private void ImportData()
        {
            string[] args = Program.Args;
            logger.Log(LogLevel.Information, string.Join(", ", args));
            if (args.Length < 2 || !args[0].Equals("import"))
            {
                return;
            }
            
            logger.Log(LogLevel.Information, "Starting import ...");
            
            string jsonData = File.ReadAllText(args[1]);
            var data = JsonConvert.DeserializeObject<List<Registration>>(jsonData);
            using IServiceScope scope = scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<VaccRegContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            
            db.Registrations.AddRange(data);
            db.SaveChanges();
            
            logger.Log(LogLevel.Information, $"Imported {data.Count} registrations");
        }
        
        
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(ImportData, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
