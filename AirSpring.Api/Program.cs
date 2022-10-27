using Microsoft.OpenApi.Models;
using AirSpring.DAL;
using AirSpring.DAL.Repository;
using AirSpring.DAL.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Defining Documentation
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AirSpring API", Version = "v1" });
});
builder.Services.AddTransient<IUser, UserRepository>();
builder.Services.AddTransient<IEmployee, EmployeeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    // CORS
    options.WithOrigins("https://localhost:7144");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
