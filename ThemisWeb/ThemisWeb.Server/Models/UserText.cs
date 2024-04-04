using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThemisWeb.Server.Models
{
    public class UserText
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [ForeignKey("ApplicationUser")]
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        [ForeignKey("Assignment")]
        public int? AssignmentId { get; set; }
        public Assignment AssignmentSubmittedTo { get; set; }
        public string? TimeSubmitted {  get; set; }

        public int? WarningLevel { get; set; }

        public UInt64 guid {  get; set; }

    }
}
