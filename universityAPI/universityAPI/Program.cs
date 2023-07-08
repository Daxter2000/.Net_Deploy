// 1. Usings with EntifyFrameword (Importaciones)
// Registrar el plugin en NodeJs

using Microsoft.EntityFrameworkCore;
using universityAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);

//2. Connection with DB
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

//3. Add context. The context store our db connection and we need to instantiate that service
builder.Services.AddDbContext<UniversityDbContext>(options => options.UseSqlServer(connectionString));
//TODO 
// Connection with SQL server express
//Para 


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
