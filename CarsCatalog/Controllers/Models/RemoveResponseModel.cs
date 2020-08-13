using System;

namespace CarsCatalog.Controllers.Models
{
    public class RemoveResponseModel : ControllerResponseModel
    {
        public bool NotExists { get; set; }
        public override bool IsSuccessful => !NotExists && !Exception;

        public static RemoveResponseModel NotExisting(ulong id, string itemName) => new RemoveResponseModel
        {
            NotExists = true,
            Message = $"{itemName} with id [{id}] is not exists"
        };

        public static RemoveResponseModel Successful() => new RemoveResponseModel();

        public static RemoveResponseModel Exceptional(Exception exception) =>
            new RemoveResponseModel
            {
                Exception = true,
                Message = exception.GetType().Name
            };
    }
}