using System.Text.RegularExpressions;

namespace DotNetNinja.VinService.Services;

public class VinValidator : IVinValidator
{
    // Transliteration table (letters → numbers)
    private static readonly Dictionary<char, int> transliteration = new()
    {
        { 'A', 1 }, { 'B', 2 }, { 'C', 3 }, { 'D', 4 }, { 'E', 5 },
        { 'F', 6 }, { 'G', 7 }, { 'H', 8 }, { 'J', 1 }, { 'K', 2 },
        { 'L', 3 }, { 'M', 4 }, { 'N', 5 }, { 'P', 7 }, { 'R', 9 },
        { 'S', 2 }, { 'T', 3 }, { 'U', 4 }, { 'V', 5 }, { 'W', 6 },
        { 'X', 7 }, { 'Y', 8 }, { 'Z', 9 },
        { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 },
        { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 }
    };

    // Weighting factors for each position
    private static readonly int[] weights = new[]
    {
        8, 7, 6, 5, 4, 3, 2, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2
    };

    public bool IsValidVin(string vin)
    {
        if (string.IsNullOrWhiteSpace(vin)) return false;

        vin = vin.Trim().ToUpper();

        // Must be exactly 17 characters, letters/numbers only, no I, O, Q
        if (!Regex.IsMatch(vin, @"^[A-HJ-NPR-Z0-9]{17}$"))
            return false;

        // Calculate check digit
        int sum = 0;
        for (int i = 0; i < vin.Length; i++)
        {
            char c = vin[i];
            int value = transliteration[c];
            sum += value * weights[i];
        }

        int remainder = sum % 11;
        string expectedCheckDigit = remainder == 10 ? "X" : remainder.ToString();

        // Actual check digit is position 9 (index 8)
        string actualCheckDigit = vin[8].ToString();

        return expectedCheckDigit == actualCheckDigit;
    }
}