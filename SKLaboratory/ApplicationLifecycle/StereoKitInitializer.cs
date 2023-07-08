using StereoKit;
namespace SKLaboratory.Initialization;

public class StereoKitInitializer
{
    public void Initialize()
    {
        var settings = new SKSettings
        {
            appName = "SKLaboratory",
            assetsFolder = "Assets",
        };
        SK.Initialize(settings);
    }
}
