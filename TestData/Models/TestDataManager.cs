using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DiTask4.TestData.Interfaces;
using Microsoft.AspNetCore.JsonPatch;

namespace DiTask4.TestData.Models;

public class TestDataManager : ITestDataGet, ITestDataInsert, ITestDataPatch, ITestDataDelete
{
    private static ConcurrentDictionary<int, TestDataModel>
        _dictionary = new ConcurrentDictionary<int, TestDataModel>();
    
    public TestDataModel[] GetAll()
    {
        var values = _dictionary.Select(pair => pair.Value);
        return values.ToArray();
    }

    public int Insert(TestDataModel dataModel)
    {
        var id = _dictionary.Count + 1;
        dataModel.Id = id;
        if (_dictionary.TryAdd(id, dataModel))
        {
            return id;
        }
        throw new Exception();
    }

    public int Patch(int id, JsonPatchDocument<TestDataModel> patchDocument)
    { 
        var modelToPatch = _dictionary[id];
        patchDocument.ApplyTo(modelToPatch);
        return id;

    }

    public void Delete(int id)
    {
        var current = _dictionary.Where(dict => dict.Key == id).FirstOrDefault();
        if (!_dictionary.TryRemove(current))
        {
            throw new Exception();
        }
    }

    public bool HasTestDataModelById(int id) => _dictionary.ContainsKey(id);
}