namespace SecurePaste.Helpers;

public class RandomDataProvider
{
    // Method to generate random dummy data for each type
    public static string GenerateDummyData(string patternKey)
    {
        var random = new Random();

        return patternKey switch
        {
            "Email" => $"{GenerateRandomString(5)}@{GenerateRandomString(5)}.com",
            "PhoneNumber" => $"({random.Next(100, 999)}) {random.Next(100, 999)}-{random.Next(1000, 9999)}",
            "SSN" => $"{random.Next(100, 999)}-{random.Next(10, 99)}-{random.Next(1000, 9999)}",
            "CreditCard" => $"{random.Next(1000, 9999)}-{random.Next(1000, 9999)}-{random.Next(1000, 9999)}-{random.Next(1000, 9999)}",
            "Date" => $"{random.Next(1, 13):D2}/{random.Next(1, 32):D2}/{random.Next(1900, 2023)}",
            "IPAddress" => $"{random.Next(1, 255)}.{random.Next(1, 255)}.{random.Next(1, 255)}.{random.Next(1, 255)}",
            "URL" => $"https://www.{GenerateRandomString(8)}.com",
            "BankAccount" => $"{random.Next(10000000, 999999999)}",
            "APIKey" => GenerateRandomString(32),
            "PAN" => $"{new string(Enumerable.Range(0, 5).Select(_ => (char)('A' + random.Next(0, 26))).ToArray())}{random.Next(1000, 9999)}{(char)('A' + random.Next(0, 26))}",
            "Aadhaar" => $"{random.Next(1000, 9999)}-{random.Next(1000, 9999)}-{random.Next(1000, 9999)}",
            _ => "REPLACEMENT"
        };
    }
    // Helper method to generate a random string of a specified length
    public static string GenerateRandomString(int length)
    {
        var random = new Random();
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = chars[random.Next(chars.Length)];
        }
        return new string(result);
    }
}