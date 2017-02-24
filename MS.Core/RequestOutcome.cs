
namespace MS.Core
{
    public class RequestOutcome<T>
    {
        public RequestOutcome()
        {
            IsSuccess = true;        
        }
        public T Data { get; set; }
        public string RedirectUrl { get; set; }
        public bool IsSuccess { get; set; }
    }
}
