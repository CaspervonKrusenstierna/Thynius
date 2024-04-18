using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThyniusWeb.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("Organization")]
        public string OrganizationEmailExtension { get; set; }
        public Organization Organization { get; set; }
        public string FullName { get; set; }
        public ICollection<Group> Groups { get; set; } = [];
    }
}