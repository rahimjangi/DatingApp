using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class RegisterDto
{
    [MaxLength(20)]
    public required string Username { get; set; }
    [MaxLength(8)]
    public required string Password { get; set; }

}
