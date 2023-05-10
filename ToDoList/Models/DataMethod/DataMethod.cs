using System.Linq;
namespace ToDoList.Models.DataMethod
{
    public static class DataMethod
    {
        public const int MaxDataIndex = 2; 

        public static int FromCookie(HttpContext context)
        {
            if (!context.Request.Cookies.TryGetValue("FileMethod", out string? value))
            {
                throw new ArgumentException("There is no header with name 'File Method' ");
            }

            if (!int.TryParse(value, out int dataType))
            {
                throw new ArgumentException("There is uncorrect file method id");
            }

            if (dataType <= 0 || dataType > MaxDataIndex)
            {
                throw new ArgumentException("There is uncorrect file method id");

            }
            return dataType;
        }

        public static int FromHead(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("FileMethod", out Microsoft.Extensions.Primitives.StringValues value))
            {
                throw new ArgumentException("There is no header with name  'File Method'  ");
            }

            if (!int.TryParse(value, out int dataType))
            {
                throw new ArgumentException("There is uncorrect file method id");
            }

            if (dataType <= 0 || dataType > MaxDataIndex)
            {
                throw new ArgumentException("There is uncorrect file method id");

            }
            return dataType;
        }
    }
}
