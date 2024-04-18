using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThyniusWeb.Server.Models
{
    public class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        Group Group { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public ICollection<UserText> Submittments { get; set;}
    }
}
