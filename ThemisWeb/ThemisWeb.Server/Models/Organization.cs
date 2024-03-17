using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace ThemisWeb.Server.Models
{
    public class Organization
    {
        [Key]
        public string EmailExtension { get; set; }

        public ICollection<ApplicationUser> Users = [];
    }
}
