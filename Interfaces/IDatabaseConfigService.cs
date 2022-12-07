namespace TeamHunter.Interfaces;

public interface IDatabaseConfigService
{
    public string Username { get; }
    public string Password { get; }
    public string Url { get; }
    public string DatabaseName { get; }
    public string ConnectionString { get; }
}