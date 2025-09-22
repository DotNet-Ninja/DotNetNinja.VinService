namespace DotNetNinja.VinService.Services;

public interface INhtsaUrlBuilder
{
    string BuildUrl(string vin, bool flat = false);
}