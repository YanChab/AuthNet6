namespace AuthNet6.Server.Services
{
    public interface IMongoDbSettings
    {
        public string? CollectionName { get; set; }
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
    }
}