using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

public class CustomFormatter : TextInputFormatter
{
    public CustomFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/custom"));

        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    public async override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        var httpContext = context.HttpContext;

        using var reader = new StreamReader(httpContext.Request.Body, encoding);
        string body = await reader.ReadToEndAsync();

        try
        {
            var input = DecodeBase64.ConvertInput(body);
            return await InputFormatterResult.SuccessAsync(input);
        }
        catch
        {
            return await InputFormatterResult.FailureAsync();
        }
    }
}