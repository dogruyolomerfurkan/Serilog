using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace WebAPI.Utilities.SerilogColumnWriters;

public class RequestBodyColumnWriter : ColumnWriterBase
{
    public RequestBodyColumnWriter(int? columnLength = 5000) : base(NpgsqlDbType.Varchar, columnLength)
    {
    }

    public override object? GetValue(LogEvent logEvent, IFormatProvider? formatProvider = null)
    {
        KeyValuePair<string, LogEventPropertyValue> requestBody = logEvent.Properties.FirstOrDefault(p => p.Key == "request_body");
        return requestBody.Value?.ToString();
    }
}
