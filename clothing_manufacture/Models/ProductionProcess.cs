using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Процесс")]
public class ProductionProcess
{
    [Key]
    [Column("ID_Процесс")]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("Наименование")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    [Column("Описание")]
    public string? Description { get; set; }

    public ICollection<ProcessStage> Stages { get; set; } = new List<ProcessStage>();
}
