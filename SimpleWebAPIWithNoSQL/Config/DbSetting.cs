namespace SimpleWebAPIWithNoSQL.Config
{
    public class DbSetting
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
        public required string CollectionName { get; set; }
    }
}