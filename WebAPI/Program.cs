using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebAPI.Middlewares;
using WebAPI.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Serilog Configuration
builder.Host.UseSerilog(SerilogConfiguration.Get(builder));
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSerilogRequestLogging();
app.UseSerilog();

app.MapControllers();

app.Run();
