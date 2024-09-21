using NetCoreOnionArchTemplate.Application.Consts;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace NetCoreOnionArchTemplate.API.Utility
{
    public class ProjectLogger
    {
        private readonly IConfiguration _configuration;

        public ProjectLogger(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Logger CreateLogger()
        {
            return new LoggerConfiguration()
            .WriteTo.File("logs/.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.MSSqlServer(
                connectionString: _configuration.GetConnectionString("DefaultConnection"),
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "logs",
                    AutoCreateSqlTable = true
                },
                columnOptions: new ColumnOptions
                {
                    AdditionalColumns = new Collection<SqlColumn>
                    {
                        new SqlColumn { ColumnName = LoggerProperties.email, DataType = SqlDbType.NVarChar }
                    }
                })
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .CreateLogger();
        }
    }
}
