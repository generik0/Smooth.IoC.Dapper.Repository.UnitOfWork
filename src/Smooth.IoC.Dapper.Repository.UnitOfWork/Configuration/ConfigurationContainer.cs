using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Configuration
{
    public class ConfigurationContainer : IConfigurationContainer
    {
        private readonly IDictionary<string, IConfigurationRoot> _configurations = new ConcurrentDictionary<string, IConfigurationRoot>();

        public string GetConnectionString(string path, string name)
        {
            IConfigurationRoot configuration = null;
            if (_configurations.TryGetValue(path, out configuration))
            {
                return configuration.GetConnectionString(name);
            }
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder
                .AddJsonFile(path, true);
            configuration = configurationBuilder.Build();
            _configurations.Add(path, configuration);
            return configuration.GetConnectionString(name);
        }
    }
}
