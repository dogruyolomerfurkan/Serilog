using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace WebAPI.Utilities.SerilogColumnWriters;

public class UserColumnWriter : ColumnWriterBase
{
    public UserColumnWriter(int? columnLength = 100) : base(NpgsqlDbType.Varchar, columnLength) { }

    public override object? GetValue(LogEvent logEvent, IFormatProvider? formatProvider = null)
    {
        KeyValuePair<string, LogEventPropertyValue> username = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name");
        return username.Value?.ToString();
    }
}