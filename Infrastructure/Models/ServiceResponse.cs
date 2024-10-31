namespace Infrastructure.Models
{
    public class ServiceResponse
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public OutputTokens OutputTokens { get; set; }
    }
}
