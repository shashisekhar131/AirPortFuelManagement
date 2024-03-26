using AirportFuelManagementWebAPI.Business;
using AirportFuelManagementWebAPI.DAL;
using AirportFuelManagementWebAPI.DAL.Models;
using AirportFuelManagementWebAPI.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => {
    options.AddPolicy("AllowOrigin",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500", "https://127.0.0.1:5500")
                   .AllowAnyMethod()  // You might need this to allow any method, or you can specify specific methods.
                   .AllowAnyHeader()  // Allow any header, or you can specify specific headers.
                   .AllowCredentials();  // If your request includes credentials like cookies, you need to allow credentials.
        });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddScoped<IBusiness, Business>();
builder.Services.AddScoped<IDataAccess, DataAccess>();

string logFilePath = builder.Configuration.GetValue<string>("LogFilePath");

builder.Services.AddSingleton<AirportFuelManagementWebAPI.Utils.ILogger>(new Logger(logFilePath));

builder.Services.AddDbContext<AirportFuelManagementContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("AirportFuelManagementDbConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowOrigin");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
