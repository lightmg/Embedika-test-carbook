using System;

namespace CarsCatalog.Controllers.Models
{
    public class AddResponseModel : ControllerResponseModel
    {
        public ulong Id { get; set; }
        public bool IsAlreadyExists { get; set; }
        public override bool IsSuccessful => !IsAlreadyExists && !Exception;

        public static AddResponseModel AlreadyExists(string itemName) => new AddResponseModel
        {
            IsAlreadyExists = true,
            Message = $"{itemName} with specified parameters is already exists"
        };

        public static AddResponseModel Successful(ulong id) => new AddResponseModel
        {
            Id = id,
            Message = $"Created with id [{id}]"
        };

        public static AddResponseModel Failed(Exception exception) =>
            new AddResponseModel()
            {
                Exception = true,
                Message = exception.GetType().Name
            };
        
        public static AddResponseModel BadInput() => new AddResponseModel
        {
            Exception = true,
            Message = "Bad input"
        };
    }
}