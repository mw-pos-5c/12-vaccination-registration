#region usings

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

#endregion

namespace VaccReg
{
    public class Program
    {
        public static string[] Args { get; private set; }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        public static void Main(string[] args)
        {
            Args = args;
            CreateHostBuilder(args).Build().Run();
        }
    }
}
