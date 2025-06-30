using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace GameTrader.API.ValidationModels
{
    public partial class ResponseResultModel
    {
        public IEnumerable<ValidationError> ValidationMessages { get; set; }

        public object Data { get; set; }

        public bool Success { get; set; }
        public ResponseResultModel(ModelStateDictionary modelState)
        {
            Success = false;
            Data = new();
            ValidationMessages = from ms in modelState
                                 where ms.Value.Errors.Any()
                                 let fieldKey = ms.Key
                                 let errors = ms.Value.Errors
                                 from error in errors
                                 select new ValidationError(fieldKey, error.ErrorMessage);
        }
    }
}
