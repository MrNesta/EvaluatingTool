using EvaluatingTool.DAL.Entities;
using System;

namespace EvaluatingTool.DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Test> Tests { get; }
        IRepository<Host> Hosts { get; }
        IRepository<Page> Pages { get; }
        void Save();
    }
}
