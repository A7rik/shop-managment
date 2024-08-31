namespace Domain.Models.Utils
{
    public class ApiResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }

    public class ApiResponseModel<T> : ApiResponseModel
    {
        public T Data { get; set; }
    }
}
