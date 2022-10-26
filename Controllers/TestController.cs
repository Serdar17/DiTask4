using DiTask4.TestData.Interfaces;
using DiTask4.TestData.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DiTask4.Controllers;

[ApiController]
[Route("api/test/data")]
public class TestController : Controller
{
    private TestDataManager _testDataManager;

    public TestController(TestDataManager testDataManager)
    {
        _testDataManager = testDataManager;
    }
    
    [HttpGet()]
    public IActionResult GetAll([FromServices] ITestDataGet testDataGet)
    {
        var models = testDataGet.GetAll();
        return Ok(models);
    }

    [HttpPost()]
    public IActionResult CreateTestData(
        [FromServices] ITestDataInsert testDataInsert,
        [FromBody] TestDataModel testDataModel)
    {
        var id = testDataInsert.Insert(testDataModel);
        return Ok(id);
    }

    [HttpPatch("{id:int}")]
    public IActionResult UpdateTestData(
        [FromServices] ITestDataPatch testDataPatch,
        [FromRoute] int id,
        [FromBody] JsonPatchDocument<TestDataModel> patchDocument)
    {
        if (patchDocument is null)
        {
            return BadRequest("patchDocument is null");
        }
        if (!_testDataManager.HasTestDataModelById(id))
        {
            return NotFound();
        }
        var result = testDataPatch.Patch(id, patchDocument);
        return Ok(result);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult DeleteTestData([FromServices] ITestDataDelete testDataDelete, [FromRoute] int id)
    {
        if (!_testDataManager.HasTestDataModelById(id))
        {
            return NotFound();
        }
        testDataDelete.Delete(id);
        return NoContent();
    }
}