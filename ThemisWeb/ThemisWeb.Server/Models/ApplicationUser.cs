using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThemisWeb.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("Organization")]
        public string? OrganizationEmailExtension { get; set; }
        public Organization? Organization { get; set; }

        public ICollection<Group> Groups { get; set; } = [];

        public string? ProfilePictureUrl;
    }
}