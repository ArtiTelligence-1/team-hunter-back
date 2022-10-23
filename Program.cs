using Microsoft.AspNetCore.Authentication.Cookies;

using TeamHunterBackend.DB;
using TeamHunterBackend.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/account/login";
        option.AccessDeniedPath = "/account/accessDenied";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(1);
        option.SlidingExpiration = true;
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
builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerGen(option =>
// {
//     option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//     {
//         In = ParameterLocation.Header,
//         Description = "Please enter a valid token",
//         Name = "Authorization",
//         Type = SecuritySchemeType.Http,
//         BearerFormat = "JWT",
//         Scheme = "Bearer"
//     });

//     option.AddSecurityRequirement(new OpenApiSecurityRequirement
//     {
//         {
//             new OpenApiSecurityScheme
//             {
//                 Reference = new OpenApiReference
//                 {
//                     Type=ReferenceType.SecurityScheme,
//                     Id="Bearer"
//                 }
//             },
//             new string[]{}
//         }
//     });
// });

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