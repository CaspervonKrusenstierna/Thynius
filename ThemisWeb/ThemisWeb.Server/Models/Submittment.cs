using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThemisWeb.Server.Models
{
    public class Submittment
    {
        [Key]
        public int Id { get; set; } 

        [ForeignKey("ApplicationUser")]
        public int ApplicationUserId { get; set; }

        ApplicationUser Owner;

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        Group GroupSubmittedTo { get; set; }


        byte[] _RawData;

    }
}
