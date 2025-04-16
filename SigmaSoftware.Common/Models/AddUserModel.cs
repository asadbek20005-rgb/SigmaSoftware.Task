using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SigmaSoftware.Common.Models;

public class AddUserModel
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;


    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;


    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;


    [StringLength(15)]
    [Phone]
    public string? PhoneNumber { get; set; } = string.Empty;


    [Column("call_time")]
    [StringLength(50)]
    public string? CallTime { get; set; } = string.Empty;

    [StringLength(2048)]
    [Url]
    public string? LinkedInUrl { get; set; } = string.Empty;


    [StringLength(2048)]
    [Url]
    public string? GitHubUrl { get; set; } = string.Empty;
    [Required]
    public string Comments { get; set; } = string.Empty;

}

