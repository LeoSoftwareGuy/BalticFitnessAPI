namespace Persistence.SqlDataBase.AuthorizationDB.Models
{
    public class RefreshTokens
    {
        public int Id { get; set; }
        public Guid AppUserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }


        public AppUser User { get; set; }
    }
}
