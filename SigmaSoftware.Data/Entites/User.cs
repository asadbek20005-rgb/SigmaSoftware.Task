using SigmaSoftware.Data.Entites.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SigmaSoftware.Data.Entites;
[Table("users")]
public class User : Date
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("first_name")]
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Column("last_name")]
    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Column("email")]
    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;


    [Column("phone_number")]
    [StringLength(15)]
    [Phone]
    public string? PhoneNumber { get; set; } = string.Empty;

    [Column("call_time")]
    [StringLength(50)]
    public string? CallTime { get; set; } = string.Empty;

    [Column("linkedin_url")]
    [StringLength(2048)]
    [Url]
    public string? LinkedInUrl { get; set; } = string.Empty;

    [Column("git_hub_url")]
    [StringLength(2048)]
    [Url]
    public string? GitHubUrl { get; set; } = string.Empty;

    [Column("comments")]
    [DataType(DataType.Text)]
    [Required]
    public string Comments { get; set; } = string.Empty;

}
