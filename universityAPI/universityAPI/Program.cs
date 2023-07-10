// 1. Usings with EntifyFrameword (Importaciones)
// Registrar el plugin en NodeJs

using Microsoft.EntityFrameworkCore;
using universityAPI.DataAccess;
using universityAPI.Services;

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

//4. Add custom services (folder services)
builder.Services.AddScoped<IStudentService, StudentService>();

//TODO: Add the rest of services


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//5. CORS Configurations
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

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

//6. Tell app to use CORS

app.UseCors("CorsPolicy");
app.Run();
