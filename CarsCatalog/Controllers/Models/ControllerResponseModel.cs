namespace CarsCatalog.Controllers.Models
{
    public class ControllerResponseModel
    {
        public string Message { get; set; }
        public virtual bool IsSuccessful => !Exception;
        public bool Exception { get; set; }
    }
}