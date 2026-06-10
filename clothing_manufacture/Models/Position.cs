using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Должность")]
public class Position
{
    [Key]
    [Column("ID_Должность")]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    [Column("Наименование")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    [Column("Описание")]
    public string? Description { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public ICollection<StagePosition> StagePositions { get; set; } = new List<StagePosition>();
}
