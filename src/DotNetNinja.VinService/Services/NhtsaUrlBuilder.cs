namespace DotNetNinja.VinService.Services;

public class NhtsaUrlBuilder : INhtsaUrlBuilder
{
    private IConfiguration _config;
    private string _baseUrl => _config["NhtsaBaseUrl"] ?? "https://vpic.nhtsa.dot.gov/api/vehicles/";

    public NhtsaUrlBuilder(IConfiguration config)
    {
        _config = config;
    }

    public string BuildUrl(string vin, bool flat = false)
    {
        var endpoint = flat ? "DecodeVin" : "DecodeVinValues";
        return $"{_baseUrl}{endpoint}/{Uri.EscapeDataString(vin)}?format=json";
    }
}