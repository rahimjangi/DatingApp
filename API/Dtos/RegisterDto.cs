using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class RegisterDto
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; } = string.Empty;
    public DateOnly Dateofbirth { get; set; }
    [Required]
    public string Knownas { get; set; } = string.Empty;
    [Required]
    public string Gender { get; set; } = string.Empty;
    [Required]
    public string City { get; set; } = string.Empty;
    [Required]
    public string Country { get; set; } = string.Empty;

}
