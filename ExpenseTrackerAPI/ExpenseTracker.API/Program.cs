

using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.Services;
using ExpenseTracker.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Program.cs
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserAccountRepository>(_ =>
    new UserAccountRepository(connectionString));
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<ITransactionRepository>(_ => new TransactionRepository(connectionString));
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IUserInvestmentRepository>(_ => new UserInvestmentRepository(connectionString));
builder.Services.AddScoped<IUserInvestmentService, UserInvestmentService>();
builder.Services.AddScoped<IInvestmentTypeRepository>(_ => new InvestmentTypeRepository(connectionString));
builder.Services.AddScoped<IInvestmentTypeService, InvestmentTypeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.UseCors("AllowAngular");

app.MapControllers();

app.Run();