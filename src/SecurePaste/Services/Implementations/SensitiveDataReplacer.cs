using System.Text;
using System.Text.RegularExpressions;

using SecurePaste.Helpers;
using SecurePaste.Services.Interfaces;

namespace SecurePaste.Services.Implementations;

public class SensitiveDataReplacer : ISensitiveDataReplacer
{
    private readonly Dictionary<string, string> _patterns;

    public SensitiveDataReplacer()
    {
        _patterns = new Dictionary<string, string>
        {
            { "Email", @"(?i)[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}" },
            { "PhoneNumber", @"(?:\(\d{3}\)\s?|\d{3}[-.\s]?)\d{3}[-.\s]?\d{4}(?=\s|$)(?!\d)" },
            { "SSN", @"\b\d{3}-\d{2}-\d{4}\b" },
            { "CreditCard", @"(?:\d{4}[-\s]?){3}\d{4}" },
            { "Date", @"\b\d{2}/\d{2}/\d{4}\b" },
            { "IPAddress", @"\b(?:\d{1,3}\.){3}\d{1,3}\b" },
            { "URL", @"\bhttps?:\/\/[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}(/\S*)?\b" },
            { "BankAccount", @"\b\d{8,12}\b" },
            { "APIKey", @"\b[A-Za-z0-9]{32,64}\b" },
            { "PAN", @"[A-Z]{5}\d{4}[A-Z]{1}" },
            { "Aadhaar", @"\d{4}[- ]\d{4}[- ]\d{4}" }
        };
    }

    // Method to replace sensitive data in a text input
    public string ReplaceSensitiveData(string inputText)
    {
        var outputText = new StringBuilder(inputText);

        foreach (var pattern in _patterns)
        {
            var dummyData = RandomDataProvider.GenerateDummyData(pattern.Key);
            var modifiedText = Regex.Replace(outputText.ToString(), pattern.Value, dummyData);
            outputText.Clear();
            outputText.Append(modifiedText);
        }
        return outputText.ToString();
    }
}