using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TeamHunterBackend.DB;
using TeamHunterBackend.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(option =>
//     {
//         option.LoginPath = "/account/login";
//         option.AccessDeniedPath = "/account/accessDenied";
//         option.ExpireTimeSpan = TimeSpan.FromMinutes(1);
//         option.SlidingExpiration = true;
//     });

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option => 
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
    option.Events = new JwtBearerEvents{
        OnAuthenticationFailed = context => {
            if(context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddScoped<IAccountService, AccountService>();
// Add services to the container.

builder.Services.Configure<DBSettings>(builder.Configuration.GetSection("TeamHunterDBSettings"));

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<UserPhotoService>();
builder.Services.AddSingleton<EventService>();
builder.Services.AddSingleton<ChatService>();
builder.Services.AddSingleton<EventMessageService>();
builder.Services.AddSingleton<EventTagService>();
builder.Services.AddSingleton<IGenerateIDService, GenerateIDService>();
builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
builder.Services.AddSingleton<IUserCredentialService, UserCredentialService>();
builder.Services.AddSingleton<IUserTokenService, UserTokenService>();

// builder.Services.AddSingleton<IUserService, UserService>();
// builder.Services.AddSingleton<IUserPhotoService, UserPhotoService>();
// builder.Services.AddSingleton<IEventService, EventService>();
// builder.Services.AddSingleton<IChatService, ChatService>();
// builder.Services.AddSingleton<IEventMessageService, EventMessageService>();
// builder.Services.AddSingleton<IEventTagService, EventTagService>();
// builder.Services.AddSingleton<IGenerateID, IDGenerator>();



builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy",
        policy =>
        {
            policy.WithOrigins("http://localhost:5086",
                                "https://localhost:7113")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

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

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();