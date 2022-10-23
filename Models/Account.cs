namespace TeamHunterBackend.Models
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public List<string> Roles { get; set; }
    }
}