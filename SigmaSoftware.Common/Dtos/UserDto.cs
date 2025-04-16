namespace SigmaSoftware.Common.Dtos;

public class UserDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;


    public string? PhoneNumber { get; set; } = string.Empty;

    public string? CallTime { get; set; } = string.Empty;

    public string? LinkedInUrl { get; set; } = string.Empty;

    public string? GitHubUrl { get; set; } = string.Empty;
}
