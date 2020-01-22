namespace MODEL
{
    public class ResultModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public enum ResultCode
    {
        Success = 1,
        Fail = 2
    }
}