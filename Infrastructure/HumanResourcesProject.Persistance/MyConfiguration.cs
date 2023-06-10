using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance
{
    public static class MyConfiguration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/HumanResourcesProject.WebApp"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("Cloud");
            }
        }
    }
}
