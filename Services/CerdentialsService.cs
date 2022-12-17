using TeamHunter.Interfaces;

namespace TeamHunter.Services;

public class CerdentialsService : ICredentialsService
{
    public string DatabaseUsername { get => Environment.GetEnvironmentVariable("DATABASE_USERNAME")!; }
    public string DatabasePassword { get => Environment.GetEnvironmentVariable("DATABASE_PASSWORD")!; }
    public string DatabaseUrl { get => Environment.GetEnvironmentVariable("DATABASE_URL")!; }
    public string DatabaseName { get => Environment.GetEnvironmentVariable("DATABASE_NAME")!; }
    public string DatabaseConnectionString { get => $"mongodb+srv://{this.DatabaseUsername}:{this.DatabasePassword}@{this.DatabaseUrl}/{this.DatabaseName}"; }
    public string TelegramBotToken { get => Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN")!; }
}