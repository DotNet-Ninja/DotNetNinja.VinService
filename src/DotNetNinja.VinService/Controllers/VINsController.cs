using System.Net;
using System.Text.Json;
using DotNetNinja.VinService.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace DotNetNinja.VinService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VINsController: ControllerBase
{
    [HttpGet("{vin}", Name = "GetVIN")]
    public async Task<IActionResult> Get(string vin, CancellationToken ct = default)
    {
        var http = new HttpClient();

        // Basic sanity check; vPIC accepts partial VINs but most apps want full 17 chars
        if (string.IsNullOrWhiteSpace(vin) || vin.Length is < 11 or > 17)
            return BadRequest(ApiResponse.Error(HttpStatusCode.BadRequest, "VIN must be between 11 and 17 characters (17 recommended)."));
        
        // Example: https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVinValues/1HGCM82633A004352?format=json
        var uri = $"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVinValues/{Uri.EscapeDataString(vin)}?format=json";

        using var resp = await http.GetAsync(uri, ct);
        if (!resp.IsSuccessStatusCode)
            return StatusCode((int)resp.StatusCode,
                ApiResponse.Error(resp.StatusCode, $"Upstream vPIC returned {(int)resp.StatusCode}"));

        // Pass the JSON straight through
        var json = await resp.Content.ReadAsStringAsync(ct);
        var nhtsa = JsonSerializer.Deserialize<NhtsaVinResult>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true
        });
        if (nhtsa == null)
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.Error(HttpStatusCode.InternalServerError, "Failed to deserialize upstream vPIC response."));
        if (nhtsa.Count == 0 || nhtsa.Results.Length == 0)
        {
            return NotFound();
        }
            return Ok(ApiResponse<Vehicle>.Success(HttpStatusCode.OK, nhtsa.Results[0]));
    }
}