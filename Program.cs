using Microsoft.EntityFrameworkCore;
using GlobalBankApi.Data; // Nome do seu namespace de dados

var builder = WebApplication.CreateBuilder(args);

// banco de dados
builder.Services.AddDbContext<AppDB>(options =>
    options.UseInMemoryDatabase("GlobalBankDb"));

builder.Services.AddControllers();

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();