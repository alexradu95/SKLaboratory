using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;
public class SettingsProvider : ISettingsProvider
{
    public SKSettings GetSettings()
    {
        return new SKSettings
        {
            appName = "SKLaboratory",
            assetsFolder = "Assets",
        };
    }
}
