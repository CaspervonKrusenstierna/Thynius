using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThemisWeb.Server.Models
{
    public class Submittment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [ForeignKey("ApplicationUser")]
        public string OwnerId { get; set; }

        ApplicationUser Owner;

        [ForeignKey("Task")]
        public int AssignmentId { get; set; }
        Assignment AssignmentSubmittedTo { get; set; }
        public String Title { get; set; }
        public String TimeSubmitted {  get; set; }
        public byte[] _RawData { get; set; }

    }
}
