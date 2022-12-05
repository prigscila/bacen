using Bacen.Domain;
using Bacen.Domain.Entities;
using Bacen.Domain.Services;
using Bacen.Domain.Services.Interfaces;
using Bacen.Domain.Utils;
using Bacen.Domain.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Bacen API", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IValidator<Client>, ClientValidator>();
builder.Services.AddScoped<IValidator<Account>, AccountValidator>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.Configure<BacenDatabaseSettings>(
    builder.Configuration.GetSection("BacenDatabase")
);

builder.Services.AddHealthChecks();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers().WithOpenApi();
app.MapHealthChecks("/health");
app.Run("http://*:80");