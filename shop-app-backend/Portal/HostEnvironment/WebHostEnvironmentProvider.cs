using AppAbstract.HostEnvironmentProvider;

namespace ShopPortal.HostEnvironment;

internal class WebHostEnvironmentProvider(IWebHostEnvironment hostEnvironment) : IWebHostEnvironmentProvider
{
    public string GetWebRootPath() => hostEnvironment.WebRootPath;
}