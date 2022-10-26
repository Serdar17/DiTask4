using DiTask4.TestData.Interfaces;
using DiTask4.TestData.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// https://code-maze.com/using-httpclient-to-send-http-patch-requests-in-asp-net-core/ - можно так сделать
//
// 1) Создать контроллер который будет обрабатывать три пути
//
//     GetAll api/test/data возвращает TestDataModel[] - получение всех
//
//     Patch api/test/data/{id} возвращает int id,  принимает в body JsonPatchDocument<TestDataModel> - обновление одного
//
//     Post api/test/data возвращает int id,  принимает в body TestDataModel - добавление одного
//
//     Delete api/test/data/{id} - удаление записи
//
// 2) Создать класс и реализовать его от интерфейсов ITestDataGet ITestDataInsert ITestDataPatch ITestDataDelete - реализовать внутри класса singleton хранилище (например static concurrentdictionary или inmemory cache - что кстати одно и тоже)
//
// 3) Зарегистрировать его от каждого интерфейса в di
//
//     ITestDataGet как singleton
//
// ITestDataInsert как scope
//
//     ITestDataPatch как scope 
//
// ITestDataDelete как scope
//
// 4) Инжектировать в каждый метод контроллера данный класс через [FromService] по нужному интерфейсу

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITestDataGet, TestDataManager>();
builder.Services.AddScoped<ITestDataInsert, TestDataManager>()
    .AddScoped<ITestDataPatch, TestDataManager>()
    .AddScoped<ITestDataDelete, TestDataManager>();

builder.Services.AddScoped<TestDataManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }