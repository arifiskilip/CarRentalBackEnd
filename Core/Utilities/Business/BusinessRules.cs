using Core.Utilities.Results;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public static class BusinessRules<T>
    {
        public static IResult RunResult(params IResult[] results)
        {
            foreach (var result in results)
            {
                if (!result.Success)
                {
                    return result;
                }
            }
            return null;
        }
        public static IDataResult<T> RunDataResult(params IDataResult<T>[] results)
        {
            foreach (var result in results)
            {
                if (!result.Success)
                {
                    return result;
                }
            }
            return null;
        }
    }
}
