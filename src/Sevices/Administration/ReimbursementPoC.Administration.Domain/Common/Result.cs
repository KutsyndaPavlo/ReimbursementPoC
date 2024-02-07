namespace ReimbursementPoC.Administration.Domain.Common
{
    public class Result<T>
    {
        private Result(bool isSuccess, T data, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
            Data = data;
        }

        public bool IsSuccess { get; }

        public T Data { get; }

        public Error Error { get; }

        public static Result<T> Success(T data) => new(true, data, Error.None);

        public static Result<T> Failure(Error error) => new(false, default, error);
    }
}
