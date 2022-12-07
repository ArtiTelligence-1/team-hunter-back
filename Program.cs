using Microsoft.AspNetCore.Identity;
using TeamHunterBackend.DB;
using TeamHunterBackend.Schemas;
using TeamHunterBackend.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.Configure<DBSettings>(builder.Configuration.GetSection("TeamHunterDBSettings"));

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<UserPhotoService>();
builder.Services.AddSingleton<MessageService>();
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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();


