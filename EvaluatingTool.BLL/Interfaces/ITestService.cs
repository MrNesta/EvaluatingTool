using EvaluatingTool.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvaluatingTool.BLL.Interfaces
{
    public interface ITestService: IDisposable
    {
        Task MakeTestAsync(string host);
        IEnumerable<TestDTO> GetAllTests();
        TestDTO GetTest(int? id);
        IEnumerable<PageDTO> GetTestPages(int? id);
    }
}
