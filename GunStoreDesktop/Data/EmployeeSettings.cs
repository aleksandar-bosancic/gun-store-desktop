namespace GunStoreDesktop.Data;

public class EmployeeSettings
{
    public string Language { get; set; }
    public string PrimaryColor { get; set; }
    public string AccentColor { get; set; }
    public string Theme { get; set; }

    public EmployeeSettings()
    {
        Language = "en";
        PrimaryColor = "Purple";
        AccentColor = "Teal";
        Theme = "Dark";
    }
}