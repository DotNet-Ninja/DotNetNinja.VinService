namespace DotNetNinja.VinService.Models;

public class NhtsaVinResult
{
    public int Count { get; set; }
    public string Message { get; set; } = string.Empty;
    public string SearchCriteria { get; set; } = string.Empty;
    public Vehicle[] Results { get; set; } = [];
}


