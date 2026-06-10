using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Этап_процесса")]
public class ProcessStage
{
    [Key]
    [Column("ID_Этап_процесса")]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("Наименование")]
    public string Name { get; set; } = string.Empty;

    [Column("Номер_этапа")]
    public int StageNumber { get; set; }

    [MaxLength(1000)]
    [Column("Описание")]
    public string? Description { get; set; }

    [MaxLength(1000)]
    [Column("Входные_данные")]
    public string? InputData { get; set; }

    [MaxLength(1000)]
    [Column("Результат")]
    public string? Result { get; set; }

    [Column("FK_Процесс")]
    public int ProcessId { get; set; }

    [ForeignKey(nameof(ProcessId))]
    public ProductionProcess Process { get; set; } = null!;

    public ICollection<StagePosition> StagePositions { get; set; } = new List<StagePosition>();

    [NotMapped]
    public string DisplayName => $"{Process?.Name ?? "Процесс"}: {StageNumber}. {Name}";
}
