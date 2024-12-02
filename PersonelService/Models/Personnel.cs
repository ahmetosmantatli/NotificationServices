using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PersonelService.Models
{
    public class Personnel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Position { get; set; }

        [EmailAddress]
        public string Email { get; set; } // E-posta alanı eklendi
    }
}
