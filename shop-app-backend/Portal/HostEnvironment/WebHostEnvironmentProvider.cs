using AppAbstract.HostEnvironmentProvider;

namespace Portal.HostEnvironment;

internal class WebHostEnvironmentProvider(IWebHostEnvironment hostEnvironment) : IWebHostEnvironmentProvider
{
    public string GetWebRootPath() => hostEnvironment.WebRootPath;
}