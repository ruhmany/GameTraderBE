namespace GameTrader.API.ValidationModels
{
    public partial class ResponseResultModel
    {
        public class ValidationError
        {
            public ValidationError(string key, string message)
            {
                Key = key;
                Message = message;
            }

            public string Key { get; set; }
            public string Message { get; set; }
        }
    }
}
