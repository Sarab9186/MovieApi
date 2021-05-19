using System;

namespace Movie.Data.HelperClasses
{
    public class OperationResult<TResult>
    {
        public bool Success { get; protected set; }

        public bool Failed => !Success;

        public Exception Exception { get; protected set; }

        public bool IsException => Exception != null;

        public string NonSuccessMessage { get; protected set; }

        public TResult Result { get; private set; }

        // TODO: unify NonSuccessMessage and UserExceptionMessage into one property
        public string UserExceptionMessage { get; set; }

        public static OperationResult<TResult> CreateSuccessResult(TResult result)
        {
            return new OperationResult<TResult> { Success = true, Result = result };
        }

        public static OperationResult<TResult> CreateFailure(string nonSuccessMessage)
        {
            return new OperationResult<TResult> { Success = false, NonSuccessMessage = nonSuccessMessage };
        }

        public static OperationResult<TResult> CreateFailure(string userExceptionMessage, Exception ex)
        {
            return new OperationResult<TResult>
            {
                Success = false,
                Exception = ex,
                UserExceptionMessage = userExceptionMessage,
                NonSuccessMessage = userExceptionMessage
            };
        }
    }
}
