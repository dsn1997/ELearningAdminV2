namespace IIG.Core.Common.ConfigureModels
{
    public class DbConnectionStringsOptions
    {
        public const string DbConnectionStrings = "DbConnectionStrings";

        public string SqlServerConnection { get; set; }

        public string MongoDbConnection { get; set; }
        public string MongoDbDatabaseName { get; set; }
    }
}
