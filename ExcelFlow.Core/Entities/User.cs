using System;
using System.ComponentModel.DataAnnotations;

namespace ExcelFlow.Core.Entities;

public class User : BaseEntity
{
    [Required]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    public string? Email { get; set; }

    [Required]
    [StringLength(500, ErrorMessage = "Password hash cannot exceed 500 characters.")]
    public string? PasswordHash { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    [Phone]
    [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
    public string? PhoneNumber { get; set; }
}
