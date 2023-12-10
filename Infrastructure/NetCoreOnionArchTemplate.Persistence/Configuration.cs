using Microsoft.Extensions.Configuration;

namespace NetCoreOnionArchTemplate.Persistence
{
    static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                //Microsoft.Extensions.Configuration alttaki manager için yüklenmesi gerekiyor.
                ConfigurationManager configurationManager = new();
                //appsettings.json'ın path'ini burada ayarlamamız gerekiyor.
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/NetCoreOnionArchTemplate.API"));
                //Microsoft.Extensions.Configuration.Json alttaki json_reader için yüklenmesi gerekiyor
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("DefaultConnection");
            }
        }
    }
}
