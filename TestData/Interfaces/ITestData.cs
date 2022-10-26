using DiTask4.TestData.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace DiTask4.TestData.Interfaces;

public interface ITestDataGet
{
    TestDataModel[] GetAll();
}

public interface ITestDataInsert
{
    int Insert(TestDataModel dataModel);
}

public interface ITestDataPatch
{
    int Patch(int id, JsonPatchDocument<TestDataModel> patchDocument);
}

public interface ITestDataDelete
{
    void Delete(int id);
}