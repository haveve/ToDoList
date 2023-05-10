using GraphQL;
using GraphQL.Execution;

namespace ToDoList.Graphql.Models.ErrorHandleProvider
{
    public class ErrorHandleProvider: IErrorInfoProvider
    {
        public ErrorInfo GetInfo(ExecutionError executionError)
        {
           var errorInfo = new ErrorInfo();

            if (executionError.InnerException != null)
                errorInfo.Message = executionError.InnerException.Message;
            else
                errorInfo.Message = executionError.Message;
            errorInfo.Extensions = null;
            return errorInfo;
        }
    }
}
