using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThemisWeb.Server.Models
{
    public class TextSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("UserText")]
        public int TextId { get; set; }
        UserText Text { get; set; }

        [Required]
        public string S3Link { get; set; }

        [Required]
        public String Time { get; set; }
    }
}
