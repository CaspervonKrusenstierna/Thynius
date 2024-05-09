using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThyniusWeb.Server.Models
{
    public class Organization
    {
        [Key]
        public string EmailExtension { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? OwnerId { get; set; }

        ApplicationUser? Owner { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

    }
}
