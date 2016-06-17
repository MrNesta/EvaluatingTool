using System;

namespace EvaluatingTool.BLL.Interfaces
{
    public class ValidationException : Exception
    {
        public string Property { get; protected set; }
        public ValidationException(string message, string pop) : base(message)
        {
            Property = pop;
        }
    }
}
