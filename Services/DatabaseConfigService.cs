using TeamHunter.Interfaces;

namespace TeamHunter.Services;

public class DatabaseConfigService : IDatabaseConfigService
{
    public string Username { get => Environment.GetEnvironmentVariable("DATABASE_USERNAME")!; }
    public string Password { get => Environment.GetEnvironmentVariable("DATABASE_PASSWORD")!; }
    public string Url { get => Environment.GetEnvironmentVariable("DATABASE_URL")!; }
    public string DatabaseName { get => Environment.GetEnvironmentVariable("DATABASE_NAME")!; }
    public string ConnectionString { get => $"mongodb+srv://{this.Username}:{this.Password}@{this.Url}/{this.DatabaseName}"; }
}