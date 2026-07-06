using Microsoft.EntityFrameworkCore;
using ResidentialExpenseControl.Api.Data;
using ResidentialExpenseControl.Api.Repositories;
using ResidentialExpenseControl.Api.Services;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using ResidentialExpenseControl.Api.Responses;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var firstError = context.ModelState
                .Values
                .SelectMany(value => value.Errors)
                .Select(error => error.ErrorMessage)
                .FirstOrDefault();

            return new BadRequestObjectResult(new ErrorResponse
            {
                Message = firstError ?? "Dados inválidos."
            });
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<PersonRepository>();
builder.Services.AddScoped<PersonService>();

builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<TransactionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactApp");

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();