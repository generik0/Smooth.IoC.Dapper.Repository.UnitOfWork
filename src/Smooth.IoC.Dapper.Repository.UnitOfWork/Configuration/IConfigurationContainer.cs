namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Configuration
{
    public interface IConfigurationContainer
    {
        string GetConnectionString(string path, string name);
    }
}