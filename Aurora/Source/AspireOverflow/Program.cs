using AspireOverflow.DataAccessLayer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using AspireOverflow.Services;
using Serilog;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

try
{
    builder.Services.AddDbContext<AspireOverflowContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    });
}
catch (SystemException Exception)
{
    Console.WriteLine(Exception.Message);

}

builder.Services.AddHttpLogging(httpLogging =>
{
    httpLogging.LoggingFields = HttpLoggingFields.All;


});

builder.Services.AddTransient<QueryService>();
builder.Services.AddTransient<UserService>();


try
{
    var logger = new LoggerConfiguration()
      .ReadFrom.Configuration(builder.Configuration)
      .Enrich.FromLogContext()
      .CreateLogger();
    builder.Logging.AddSerilog(logger);
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);

}




var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseHttpLogging();



app.MapControllers();

app.Run();
