namespace TeamHunter.Interfaces;

public interface ICredentialsService
{
    public string DatabaseUsername { get; }
    public string DatabasePassword { get; }
    public string DatabaseUrl { get; }
    public string DatabaseName { get; }
    public string DatabaseConnectionString { get; }
    public string TelegramBotToken { get; }
    
}