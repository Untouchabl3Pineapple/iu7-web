using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace db_cp.Models
{
    public class Users
    {
        [Key]
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Permission { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string MiddleName { get; set; }
    }
}