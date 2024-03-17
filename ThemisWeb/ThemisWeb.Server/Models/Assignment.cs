using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThemisWeb.Server.Models
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

        public string DueDate { get; set; }
        public ICollection<Submittment> Submittments { get; set;}
    }
}
