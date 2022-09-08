using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace WebAPI.Utilities.SerilogColumnWriters;

public class RequestTypeColumnWriter : ColumnWriterBase
{
    public RequestTypeColumnWriter(int? columnLength = 100) : base(NpgsqlDbType.Varchar, columnLength) { }

    public override object? GetValue(LogEvent logEvent, IFormatProvider? formatProvider = null)
    {
        KeyValuePair<string, LogEventPropertyValue> requestType = logEvent.Properties.FirstOrDefault(p => p.Key == "request_type");
        return requestType.Value?.ToString();
    }
}