namespace School.Model.Response
{
    public class CommonResponse
    {
        public CommonResponse(bool status,object payload,string err)
        {
            Status = status;
            Payload = payload;
            Error = err;
        }
    
        public bool Status { get; set; }
        public object Payload { get; set; }
        public string Error { get; set; }
    }
}
