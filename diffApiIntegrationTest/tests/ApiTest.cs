using System.Dynamic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace diffApiIntegrationTest;

[TestClass]
public class ApiTest
{
    const string host = "https://localhost:7232";

    // this test works only once per session because it posts inputs with constant IDs
    // which is considered as conflict when executed for the second time
    // you need to restart the service
    [TestMethod]
    public async Task ReturnDiffResult()
    {
        using var httpClient = new HttpClient();

        // {"input":"some value to be compared"}
        await PostInput(httpClient, "left", "eyJpbnB1dCI6InNvbWUgdmFsdWUgdG8gYmUgY29tcGFyZWQifQ==");
        // {"input":"Home Xalue 1o+be compared"}
        await PostInput(httpClient, "right", "eyJpbnB1dCI6IkhvbWUgWGFsdWUgMW8rYmUgY29tcGFyZWQifQ==");

        var response = await httpClient.GetAsync($"{host}/v1/diff/1");
        var responseBody = await response.Content.ReadAsStringAsync();

        dynamic result = 
            JsonConvert.DeserializeObject<ExpandoObject>(
                responseBody, 
                new ExpandoObjectConverter()
            );

        Assert.AreEqual("1", result.id);
        Assert.AreEqual("", result.description);
        Assert.AreEqual(4, result.differences.Count);
    }

    async Task PostInput(HttpClient httpClient, string part, string input)
    {
        var data = new StringContent(input, Encoding.UTF8, "application/custom");
        var response = await httpClient.PostAsync($"{host}/v1/diff/1/{part}", data);
        
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}