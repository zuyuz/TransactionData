using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;

namespace TransactionData.Domain.ExtensionMethods
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
    }
}
