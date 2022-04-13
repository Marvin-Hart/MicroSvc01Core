using Microsoft.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroSvc01Core.Context;
using MicroSvc01Core.Utils;

namespace MicroSvc01Core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SystemInfoController : ControllerBase
    {
        private readonly DogContext _context;
        private readonly ILogger<SystemInfoController> _logger;
        private readonly IWebHostEnvironment _env;

        public SystemInfoController(DogContext context, ILogger<SystemInfoController> logger, IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
        }
        
        [HttpGet]
        public async Task<Dictionary<string, string>> Status()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            data["Timestamp"] = DateTime.Now.ToString("MM/dd/yyyy @ h:mm:ss tt");
            data["ASPNETCore_Environment"] = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            data["Environment"] = _env.EnvironmentName;
            data["MachineName"] = Environment.MachineName;
            data["IP Address"] = Network.GetNetworkIPV4();

            data["OSVersion"] = Environment.OSVersion.ToString();
            data["Is64BitOperatingSystem"] = Environment.Is64BitOperatingSystem.ToString();
            data["Is64BitProcess"] = Environment.Is64BitProcess.ToString();
            data["ProcessorCount"] = Environment.ProcessorCount.ToString();

            long uptimeInSeconds = Stopwatch.GetTimestamp() / Stopwatch.Frequency;
            data["MachineUpTime"] = TimeSpan.FromSeconds(uptimeInSeconds).ToString();

            data["ApplicationName"] = _env.ApplicationName;
            data["UserDomainName"] = Environment.UserDomainName;
            data["UserName"] = Environment.UserName;

            data["DatabaseServer"] = new SqlConnectionStringBuilder(_context.Database.GetConnectionString()).DataSource;
            data["DatabaseName"] = new SqlConnectionStringBuilder(_context.Database.GetConnectionString()).InitialCatalog;
            data["DatabaseConnection"] = _context.Database.CanConnect().ToString();

            return await Task.FromResult(data);
        }
    }
}
