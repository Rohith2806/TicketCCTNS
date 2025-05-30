using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace logindemo.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string TicketId { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Police Station is required.")]
        public string PoliceStation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Reporter Name is required.")]
        public string ReporterName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Issue is required.")]
        public string Issue { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Priority is required.")]
        public string Priority { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } = string.Empty;

        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
