using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Smoother.IoC.Dapper.Repository.UnitOfWork.Configuration
{
    public class ConfigurationExpert
    {
        public IConfigurationRoot Get(string path )
        {

            var configurationStrings = new List<KeyValuePair<string, string>>();
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder
                .AddInMemoryCollection(configurationStrings)
                .AddJsonFile(path, true);
            return configurationBuilder.Build();
        }
    }
}
