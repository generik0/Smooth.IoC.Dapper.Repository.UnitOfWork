using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Configuration
{
    public class ConfigurationExpert
    {
        public string GetConnectionString(string path, string name)
        {

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder
                .AddJsonFile(path, true);
            var configuration = configurationBuilder.Build();

            return configuration.GetConnectionString(name);
        }
    }
}
