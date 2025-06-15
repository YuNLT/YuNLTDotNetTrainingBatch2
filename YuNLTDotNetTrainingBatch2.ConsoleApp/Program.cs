using Microsoft.Data.SqlClient;
using System.Data;
using YuNLTDotNetTrainingBatch2.ConsoleApp;

//ADO .Net
//Dapper
//EF Core

//AdoDotNetExample adoDotNet = new AdoDotNetExample();
//adoDotNet.Read();
//adoDotNet.Create();

DapperExample dapperExample = new DapperExample();
//dapperExample.Edit();
dapperExample.Read();

Console.ReadKey();