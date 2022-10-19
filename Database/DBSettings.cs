namespace TeamHunterBackend.DB
{
    public class DBSettings
    {
        private string _connectionString = "";
        public string ConnectionString { 
            get => 
                _connectionString.Replace("<password>", ConnectionPassword); 
            set{
            _connectionString = value;
            }
        }
        public string DatabaseName { get; set; } = null!;
        public string[] CollectionName { get; set; } = null!;
        public string ConnectionPassword { get => Environment.GetEnvironmentVariable("MONGODB_PASSWORD")!; }
    }
}