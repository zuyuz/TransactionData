using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace TransactionData.Service.ExtensionMethods
{
    public static class ResultExtensionMethods
    {
        /// <summary>
        ///     Selects result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static Result<TValue, string> Bind<TValue>(this Result<TValue> result, Func<TValue, Result<TValue, string>> func)
        {
            if (result.IsFailure)
                return Result.Failure<TValue, string>(result.Error);

            return func(result.Value);
        }

        /// <summary>
        ///     Selects result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static Task<Result<TResult, TError>> Bind<TValue, TResult, TError>(
            this Result<TValue, TError> result,
            Func<TValue, Task<Result<TResult, TError>>> func)
        {
            return result.IsFailure ? Task.FromResult(Result.Failure<TResult, TError>(result.Error)) : func(result.Value);
        }
        //public static TResult Match<T, TResult>(
        //    this Result<T> result,
        //    Func<T, TResult> onSuccess,
        //    Func<string, TResult> onFailure)
        //{
        //    if (result.IsSuccess)
        //        return onSuccess(result.Value);
        //    else
        //        return onFailure(result.Error);
        //}
    }
}
