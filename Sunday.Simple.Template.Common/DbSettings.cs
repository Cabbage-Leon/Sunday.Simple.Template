namespace Sunday.Simple.Template.Common
{
    public sealed class DbSettings
    {
        public int CommandTimeout { get; set; } = 30;
        public string ConnectionString { get; set; }
        public bool EnableSensitiveDataLogging { get; set; }
    }
}
