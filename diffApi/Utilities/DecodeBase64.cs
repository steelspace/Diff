using System.Text;
using System.Text.Json;

public static class DecodeBase64
{
    public static Input ConvertInput(string base64)
    {
        string decodedText = Decode(base64);
        Input? input = null;

        try
        {
            input = JsonSerializer.Deserialize<Input>(decodedText);
        }

        catch (JsonException)
        {
            throw new Base64JsonFormatException();
        }

        if (input is null || input.input is null)
        {
            throw new Base64JsonFormatException();
        }

        return input;
    }

    static string Decode(string base64)
    {
        try
        {
            var decoded = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(decoded);            
        }
        catch (FormatException)
        {
            throw new Base64JsonFormatException();
        }
    }
}