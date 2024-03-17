using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThemisWeb.Server.Models
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ManagerId;
        ApplicationUser Manager { get; set; }

        List<ApplicationUser> Users { get; set; }

    }
}
