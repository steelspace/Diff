namespace diffApiTest;

[TestClass]
public class Decodebase64Test
{
    [TestMethod]
    public void ReturnsDecodedModel()
    {
        var input = DecodeBase64.ConvertInput("eyJpbnB1dCI6InNvbWUgdmFsdWUgdG8gYmUgY29tcGFyZWQifQ==");

        Assert.IsNotNull(input);
        Assert.AreEqual("some value to be compared", input.input);
    }  

    [TestMethod]
    [ExpectedException(typeof(Base64JsonFormatException))]
    public void ThrowExceptionWhenBase64IsMalformed()
    {
        var input = DecodeBase64.ConvertInput("eyJpXXX-+#$dWUgdG8gYmUgY29tcGFyZWQifQ==");

        Assert.IsNotNull(input);
        Assert.AreEqual("some value to be compared", input.input);
    }

    [TestMethod]
    [ExpectedException(typeof(Base64JsonFormatException))]
    public void ThrowExceptionWhenBase64DoesntContainJson()
    {
        DecodeBase64.ConvertInput("cmFuZG9tIHRleHQ=");
    }

    [TestMethod]
    [ExpectedException(typeof(Base64JsonFormatException))]
    public void ThrowExceptionWhenBase64JsonFormatIsNotCorrect()
    {
        DecodeBase64.ConvertInput("eyJ1bmtub3duIjoxfQ==");
    }          
}