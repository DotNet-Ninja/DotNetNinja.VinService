
namespace DotNetNinja.VinService.Models;

public class Vehicle: IModel
{
    public string BodyClass { get; set; } = string.Empty;
    public string CurbWeightLB { get; set; } = string.Empty;
    public string DisplacementCC { get; set; } = string.Empty;
    public string DisplacementCI { get; set; } = string.Empty;
    public string DisplacementL { get; set; } = string.Empty;
    public string Doors { get; set; } = string.Empty;
    public string DriveType { get; set; } = string.Empty;
    public string DriverAssist { get; set; } = string.Empty;
    public string EngineConfiguration { get; set; } = string.Empty;
    public string EngineCycles { get; set; } = string.Empty;
    public string EngineCylinders { get; set; } = string.Empty;
    public string EngineHP { get; set; } = string.Empty;
    public string EngineKW { get; set; } = string.Empty;
    public string EngineManufacturer { get; set; } = string.Empty;
    public string EngineModel { get; set; } = string.Empty;
    public string FuelInjectionType { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string MakeID { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string ManufacturerId { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string ModelID { get; set; } = string.Empty;
    public string ModelYear { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public string Series2 { get; set; } = string.Empty;
    public string SuggestedVIN { get; set; } = string.Empty;
    public string TopSpeedMPH { get; set; } = string.Empty;
    public string TransmissionSpeeds { get; set; } = string.Empty;
    public string TransmissionStyle { get; set; } = string.Empty;
    public string Trim { get; set; } = string.Empty;
    public string Trim2 { get; set; } = string.Empty;
    public string Turbo { get; set; } = string.Empty;
    public string VIN { get; set; } = string.Empty;
    public string Wheels { get; set; } = string.Empty;
}