using Bacen.Domain;
using Bacen.Domain.Entities;
using Bacen.Domain.Services;
using Bacen.Domain.Services.Interfaces;
using Bacen.Domain.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IValidator<Client>, ClientValidator>();
builder.Services.Configure<BacenDatabaseSettings>(
    builder.Configuration.GetSection("BacenDatabase")
);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers().WithOpenApi();
app.Run();