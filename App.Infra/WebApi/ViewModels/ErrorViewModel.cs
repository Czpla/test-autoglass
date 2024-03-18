namespace App.Infra.WebApi.ViewModels.User
{
    using System.Text.Json.Serialization;

    public class ErrorViewModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;

        public static implicit operator ErrorViewModel(Exception error)
        {
            return new ErrorViewModel { Message = error.Message, };
        }
    }   
}