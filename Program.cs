using Microsoft.AspNetCore.Identity;
using TeamHunterBackend.Schemas;
using TeamHunter.Interfaces;
using TeamHunter.Models.DTO;
using TeamHunter.Models;
using TeamHunter.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.Configure<SettingParams>(builder.Configuration.GetSection("SiteSettingsInit"));

builder.Services.AddTransient<ICredentialsService, CerdentialsService>();
builder.Services.AddSingleton<IDBSessionManagerService, MongoDBSessionManagerService>();
builder.Services.AddSingleton<IEventService, EventService>();
builder.Services.AddSingleton<ISettingsManagerService, SettingsManagerService>();
builder.Services.AddSingleton<ISettingsService, SettingsService>();
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>  
    {  
        options.TokenValidationParameters = new TokenValidationParameters  
        {  
            ValidateIssuer = false,  
            ValidateAudience = false,  
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            // ValidIssuer = Configuration["Jwt:Issuer"],  
            // ValidAudience = Configuration["Jwt:Issuer"],  
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TOKEN_SECRET")!))  
        };
        options!.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context => {
                // Log failed authentication here

                context.Response.OnStarting(async () => {
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(context.Exception.Message);
                });
                // Return control back to JWT Bearer middleware
                return Task.CompletedTask;
            }
        };
    }
);

// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(o =>
// {
//     o.TokenValidationParameters = new TokenValidationParameters
//     {
//         // ValidIssuer = builder.Configuration["Jwt:Issuer"],
//         // ValidAudience = builder.Configuration["Jwt:Audience"],
//         IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TOKEN_SECRET")! /* builder.Configuration["Jwt:Key"] */)),
//         ValidateIssuer = false,
//         ValidateAudience = false,
//         ValidateLifetime = false,
//         ValidateIssuerSigningKey = true
//     };
// });
// builder.Services.AddAuthorization();// Add configuration from appsettings.json

// builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//     .AddEnvironmentVariables();

// builder.Services.AddSingleton<UserService>();
// builder.Services.AddSingleton<UserPhotoService>();
// builder.Services.AddSingleton<EventService>();
// builder.Services.AddSingleton<EventTagService>();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy",
        policy =>
        {
            policy.WithOrigins("http://localhost:5086",
                                "https://localhost:7113",
                                "http://localhost:4000",
                                "http://localhost:5050",
                                "https://2867-185-17-127-253.eu.ngrok.io",
                                "https://team-hunter-front-staging.herokuapp.com")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
}

// app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();


