namespace SecurePaste.Services.Interfaces;

public interface ISensitiveDataReplacer
{
    string ReplaceSensitiveData(string inputText);
}