namespace TeamHunterBackend.DB
{
    public class DBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string[] CollectionName { get; set; } = null!;
    }
}