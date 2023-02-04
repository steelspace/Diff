using diffApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace diffApiTest;

[TestClass]
public class DiffControllerTest
{
    [TestMethod]
    public void ReturnsDiffResult()
    {
        var diffServiceMock = new Mock<IDiffService>();
        diffServiceMock.Setup(r => r.GetDiff(It.IsAny<string>()))
            .Returns(new DiffResult("1", string.Empty, new List<Diff>() { new Diff(10, 'C', 'c') } ));

        var diffController = new DiffController(diffServiceMock.Object);
        var response = diffController.Diff("1");

        Assert.IsNotNull(response);
        Assert.IsInstanceOfType(response, typeof(OkObjectResult));

        var result = (OkObjectResult)response;
        Assert.IsNotNull(result.Value);
        Assert.IsInstanceOfType(result.Value, typeof(DiffResult));

        var diffResult = (DiffResult)result.Value;
        Assert.AreEqual("1", diffResult.id);
        Assert.AreEqual(string.Empty, diffResult.description);
        Assert.AreEqual(1, diffResult.differences.Count());
        Assert.AreEqual(new Diff(10, 'C', 'c'), diffResult.differences.First());
    }

    [TestMethod]
    public void ReturnsBadRequestIfBase64OrJsonAreNotInTheCorrectFormat()
    {
        var diffServiceMock = new Mock<IDiffService>();
        diffServiceMock.Setup(r => r.StoreInputData(It.IsAny<InputRecord>()))
            .Throws(new Base64JsonFormatException());

        var diffController = new DiffController(diffServiceMock.Object);

        var leftResponse = diffController.Left("1", new Input(""));
        Assert.IsNotNull(leftResponse);
        Assert.IsInstanceOfType(leftResponse, typeof(BadRequestResult));

        var rightResponse = diffController.Right("1", new Input(""));
        Assert.IsNotNull(rightResponse);
        Assert.IsInstanceOfType(rightResponse, typeof(BadRequestResult));
    }  

    [TestMethod]
    public void ReturnsConflictIfTheInputAlreadyExists()
    {
        var diffServiceMock = new Mock<IDiffService>();
        diffServiceMock.Setup(r => r.StoreInputData(It.IsAny<InputRecord>()))
            .Throws(new DuplicateInputException());

        var diffController = new DiffController(diffServiceMock.Object);

        var leftResponse = diffController.Left("1", new Input(""));
        Assert.IsNotNull(leftResponse);
        Assert.IsInstanceOfType(leftResponse, typeof(ConflictResult));

        var rightResponse = diffController.Right("1", new Input(""));
        Assert.IsNotNull(rightResponse);
        Assert.IsInstanceOfType(rightResponse, typeof(ConflictResult));
    }            
}