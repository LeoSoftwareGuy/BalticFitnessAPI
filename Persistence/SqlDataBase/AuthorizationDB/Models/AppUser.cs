using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Persistence.SqlDataBase.AuthorizationDB.Models
{
    public class AppUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHashed { get; set; }
        public string Nationality { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}
