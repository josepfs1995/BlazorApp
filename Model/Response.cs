namespace BlazorApp.Model
{
    public class Response<T> where T:class
    {
        public int errorCode { get; set; }
        public string errorMsg { get; set; }
        public T data { get; set; }
    }
}