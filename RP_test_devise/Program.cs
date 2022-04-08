using Microsoft.EntityFrameworkCore;
using RP_test_devise.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Set up the connection to the DB via the ConnectionString "MyDBDevises", wich is stored in appsetings.json
builder.Services.AddDbContext<TestContext>(opt =>opt.UseSqlServer(builder.Configuration.GetConnectionString("MyDBDevises")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
