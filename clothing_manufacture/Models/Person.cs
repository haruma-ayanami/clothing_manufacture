using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Человек")]
public class Person
{
    [Key]
    [Column("ID_Человек")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Фамилия")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("Имя")]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column("Отчество")]
    public string? MiddleName { get; set; }

    [MaxLength(30)]
    [Column("Телефон")]
    public string? Phone { get; set; }

    [MaxLength(150)]
    [Column("Email")]
    public string? Email { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [NotMapped]
    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
}
