using Microsoft.AspNetCore.Identity;
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
        public string? ManagerId { get; set; }
        ApplicationUser? Manager { get; set; }

        public string? DateCreated { get; set; }
        public ICollection<ApplicationUser> Users { get; set; } = [];

        public ICollection<Assignment> Tasks { get; set; } = [];


    }
}
