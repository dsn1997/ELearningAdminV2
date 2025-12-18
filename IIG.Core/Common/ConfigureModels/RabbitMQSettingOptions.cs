namespace IIG.Core.Common.ConfigureModels
{
    public class RabbitMQSettingOptions
    {
        public const string RabbitSetting = "RabbitSetting";
        public bool Enabled { get; set; }
        public ushort PrefetchCount { get; set; } = 5;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string VHost { get; set; }
        public int? MaxChannel { get; set; }
    }
}
