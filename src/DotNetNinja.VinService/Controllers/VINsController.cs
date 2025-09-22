using System.Net;
using System.Text.Json;
using DotNetNinja.VinService.Models;
using DotNetNinja.VinService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetNinja.VinService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VINsController: ControllerBase
{
    private readonly INhtsaUrlBuilder _urlBuilder;

    public VINsController(INhtsaUrlBuilder urlBuilder)
    {
        _urlBuilder = urlBuilder;
    }


    [HttpGet("{vin}", Name = "GetVIN")]
    public async Task<IActionResult> Get(string vin, CancellationToken ct = default)
    {
        var http = new HttpClient();

        // Basic sanity check; vPIC accepts partial VINs but most apps want full 17 chars
        if (string.IsNullOrWhiteSpace(vin) || vin.Length is < 11 or > 17)
        {
            return BadRequest("VIN must be between 11 and 17 characters (17 recommended).");
        }
        
        // Build service url for vin
        var uri = _urlBuilder.BuildUrl(vin);

        // Fetch results
        using var resp = await http.GetAsync(uri, ct);
        if (!resp.IsSuccessStatusCode)
        {
            return StatusCode((int)resp.StatusCode, $"Upstream vPIC returned {(int)resp.StatusCode}");
        }

        // Deserialize the response
        var json = await resp.Content.ReadAsStringAsync(ct);
        var nhtsa = JsonSerializer.Deserialize<NhtsaVinResult>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true
        });

        // Check for deserialization issues or no results
        if (nhtsa == null)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to deserialize upstream vPIC response.");
        }

        if (nhtsa.Count == 0 || nhtsa.Results.Length == 0)
        {
            return NotFound();
        }

        // Return the first result
        return Ok(nhtsa.Results[0]);
    }
}