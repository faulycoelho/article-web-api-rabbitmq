using Api.Controllers;
using static Infra.InversionOfControl.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHostedService<PaymentWorker>();
builder.Services.AddHealthChecks();

var app = builder.Build();
app.ConfigureHealthCheck();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
