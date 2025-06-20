namespace ChatAppAPI.Common.ErrorHandling
{
    public class OperationResult
    {
        public bool IsSuccessful { get; set; }
        public string? ErrorMessage { get; set; }

        public static OperationResult Failure(string errorMessage)
        {
            return new OperationResult
            {
                IsSuccessful = false,
                ErrorMessage = errorMessage
            };
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T? Data { get; set; }

        public static OperationResult<T> Success(T data)
        {
            return new OperationResult<T>
            {
                IsSuccessful = true,
                Data = data
            };
        }
    }
}
