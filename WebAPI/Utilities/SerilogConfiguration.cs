namespace WebAPI.Utilities;

using Serilog;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using WebAPI.Utilities.SerilogColumnWriters;

public class SerilogConfiguration
{
    private readonly static Dictionary<string, ColumnWriterBase> ColumnOptions = new()
    {
        {"user_name", new UserColumnWriter()},
        {"level", new LevelColumnWriter()},
        {"time_stamp", new TimestampColumnWriter()},
        {"request_type",new RequestTypeColumnWriter()},
        {"request_body",new RequestBodyColumnWriter()},
        {"message", new RenderedMessageColumnWriter()},
        {"log_event", new LogEventSerializedColumnWriter()},
        {"exception", new ExceptionColumnWriter()}
    };

    public static Logger Get(WebApplicationBuilder builder)
    {
        return new LoggerConfiguration()
            .WriteTo.File("Serilog/serilog.txt")
            .WriteTo.PostgreSQL(
            builder.Configuration.GetValue<string>("PostgreSqlModules:ConnectionString"),
            builder.Configuration.GetValue<string>("PostgreSqlModules:Table"),
            columnOptions: ColumnOptions,
            needAutoCreateTable: true).Enrich.FromLogContext().CreateLogger();
    }
}