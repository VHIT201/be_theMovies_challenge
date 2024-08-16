namespace Common.Application.Settings
{
    public abstract class BaseAppSetting
    {
        public abstract BasePermissionSetting PermissionSetting { get; }
        public JwtTokenSetting JwtTokenSetting { get; set; } = new();

        public string? FolderGenerateSqlScript { get; set; }
        public ExternalServicesSetting? ExternalServicesSetting { get; set; }
        public DatabaseSetting? DatabaseSetting { get; set; } = new();
    }
}
