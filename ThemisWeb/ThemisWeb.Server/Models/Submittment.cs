using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThemisWeb.Server.Models
{
    public class Submittment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [ForeignKey("Assignment")]
        public int AssignmentId { get; set; }
        Assignment AssignmentSubmittedTo { get; set; }

        [ForeignKey("UserText")]
        public int UserTextId { get; set; }
        UserText Text { get; set; }

        public int? WarningLevel {  get; set; }
        public String TimeSubmitted {  get; set; }
    }
}
