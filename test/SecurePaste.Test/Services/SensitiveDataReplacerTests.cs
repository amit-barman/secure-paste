using Xunit;
using System.Text.RegularExpressions;
using SecurePaste.Services.Implementations;

namespace SecurePaste.Test.Services;

public class SensitiveDataReplacerTests
{
    private readonly SensitiveDataReplacer _dataReplacer;
    public SensitiveDataReplacerTests()
    {
        _dataReplacer = new SensitiveDataReplacer();
    }
    [Fact]
    public void ReplaceSensitiveData_ShouldReplacePatternsWithDummyData()
    {
        string inputText = @"Contact John Doe at 555-123-4567 or email johndoe@example.com for support. His SSN 
        is 123-45-6789, his bank account number is 1234567890123456, and for online purchases, use credit card 
        number 4111-1111-1111-1111 and his DOB is 20/02/1995. You can access the API at https://api.example.com/ 
        with the API key 12345abcde67890fghijklmnopqrstuv for integration. His server’s IP address is 192.168.1.1, 
        which can be used to check the connection. Additionally, John’s PAN is ABCDE1234F and Aadhaar number 
        is 1234 5678 9123. All of this data is for demonstration purposes and should not be used in real-world 
        scenarios. Always be cautious with sensitive information and use secure connections when handling real 
        data.";

        var result = _dataReplacer.ReplaceSensitiveData(inputText);

        // Ensure the result contains valid dummy data
        Assert.Matches(@"(?:\d{4}[-\s]?){3}\d{4}", result); // credit card
        Assert.Matches(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}", result); // email
        Assert.Matches(@"(?:\(\d{3}\)\s?|\d{3}[-.\s]?)\d{3}[-.\s]?\d{4}(?=\s|$)(?!\d)", result); // phone number
        Assert.Matches(@"\b\d{3}-\d{2}-\d{4}\b", result); // SSN
        Assert.Matches(@"\b\d{2}/\d{2}/\d{4}\b", result); // Date
        Assert.Matches(@"\b(?:\d{1,3}\.){3}\d{1,3}\b", result); // IP Address
        Assert.Matches(@"\bhttps?:\/\/[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}(/\S*)?\b", result); // URL
        Assert.Matches(@"\b\d{4}-\d{4}-\d{4}-\d{4}\b", result); // Bank Account
        Assert.Matches(@"\b[A-Za-z0-9]{32,64}\b", result); // API Key
        Assert.Matches(@"[A-Z]{5}\d{4}[A-Z]{1}", result); // PAN
        Assert.Matches(@"\d{4}[- ]\d{4}[- ]\d{4}", result); // Aadhaar

        // Check that the original sensitive data has been removed
        Assert.DoesNotContain("4111-1111-1111-1111", result); // credit card
        Assert.DoesNotContain("johndoe@example.com", result);  // email
        Assert.DoesNotContain("555-123-4567", result);  // Phone number
        Assert.DoesNotContain("123-45-6789", result);  // SSN
        Assert.DoesNotContain("20/02/1995", result);  // Date
        Assert.DoesNotContain("1234567890123456", result);  // Bank Account
        Assert.DoesNotContain("https://api.example.com/", result);  // URL
        Assert.DoesNotContain("192.168.1.1", result);  // IP address
        Assert.DoesNotContain("ABCDE1234F", result);  // PAN
        Assert.DoesNotContain("1234 5678 9123", result);  // Aadhaar
    }

    [Fact]
    public void ReplaceSensitiveData_ShouldReturnOriginalTextWhenNoPatternsMatch()
    {
        string inputText = "No sensitive data here";

        var result = _dataReplacer.ReplaceSensitiveData(inputText);

        Assert.Equal(inputText, result);
    }

    [Fact]
    public void ReplaceSensitiveData_ShouldHandleEmptyInputText()
    {
        string inputText = "";

        var result = _dataReplacer.ReplaceSensitiveData(inputText);

        Assert.Equal(inputText, result);
    }
}
