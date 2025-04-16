using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SigmaSoftware.Common.Models;

public class UpdateUserModel
{
   public string? FirstName { get; set; } = string.Empty;


    public string? LastName { get; set; } = string.Empty;

    public string? Email { get; set; } = string.Empty;


    public string? PhoneNumber { get; set; } = string.Empty;

    public string? CallTime { get; set; } = string.Empty;

    public string? LinkedInUrl { get; set; } = string.Empty;

    public string? GitHubUrl { get; set; } = string.Empty;
    public string Comments { get; set; } = string.Empty;

}
